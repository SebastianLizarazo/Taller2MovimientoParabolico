using System;
using System.Collections.Generic;

namespace Taller2MovimientoParabolico
{
    /// <summary>
    /// Motor de simulación del movimiento parabólico. Integra con método de Euler,
    /// detecta colisiones con el objetivo y con el suelo, aplica rebotes con
    /// pérdida del 40 % de la magnitud de la velocidad e invierte la componente
    /// vertical. La simulación termina cuando el proyectil toca el suelo por
    /// segunda vez (después del rebote previo).
    /// </summary>
    public class Simulador
    {
        // Condiciones iniciales
        public double X0 { get; private set; }
        public double Y0 { get; private set; }
        public double V0 { get; private set; }
        public double AnguloGrados { get; private set; }
        public double Gravedad { get; private set; }
        public double DT { get; private set; }

        // Objetivo (segmento horizontal)
        public double ObjetivoY { get; private set; }
        public double ObjetivoXMin { get; private set; }
        public double ObjetivoXMax { get; private set; }
        public bool ObjetivoDefinido { get; private set; }
        public bool ObjetivoImpactado { get; private set; }

        // Estado interno
        private double t;
        private double x;
        private double y;
        private double vx;
        private double vy;
        private int rebotesSuelo;
        private bool terminado;

        // Resultados
        public List<Muestra> Muestras { get; private set; }
        public List<Colision> Colisiones { get; private set; }

        public double TiempoTotalVueloSim { get; private set; }
        public double AlturaMaximaSim { get; private set; }
        public double TiempoAlturaMaximaSim { get; private set; }
        public double XAlturaMaximaSim { get; private set; }
        public double AlcanceSim { get; private set; }
        public Muestra VelocidadEnAlturaMaxima { get; private set; }
        public Muestra VelocidadEnImpacto { get; private set; }

        /// <summary>Tiempo total de la simulación incluyendo rebotes (momento de terminación).</summary>
        public double TiempoFinalSim { get; private set; }
        /// <summary>Posición horizontal al final de la simulación.</summary>
        public double AlcanceFinalSim { get; private set; }

        /// <summary>True cuando la simulación ya no admitirá más pasos.</summary>
        public bool Terminado { get { return terminado; } }

        /// <summary>
        /// Construye e inicializa una simulación. Genera un objetivo horizontal
        /// aleatorio si rnd != null. Si rnd es null no se genera objetivo.
        /// </summary>
        public Simulador(double x0, double y0, double v0, double anguloGrados,
                         double gravedad, double dt, Random rnd)
        {
            if (dt <= 0) throw new ArgumentException("dt debe ser positivo.");
            X0 = x0; Y0 = y0; V0 = v0;
            AnguloGrados = anguloGrados;
            Gravedad = gravedad;
            DT = dt;

            if (rnd != null)
            {
                GenerarObjetivoAleatorio(rnd);
            }
            else
            {
                ObjetivoDefinido = false;
            }
        }

        private void GenerarObjetivoAleatorio(Random rnd)
        {
            // Calculamos el alcance y la altura máxima teóricos para colocar el objetivo
            // en una zona razonable de la trayectoria.
            double angRad = anguloARadianes();
            double v0x = V0 * Math.Cos(angRad);
            double v0y = V0 * Math.Sin(angRad);
            double tTotal = ModeloTeorico.TiempoTotalVuelo(Y0, v0y, Gravedad);
            double yMax = ModeloTeorico.AlturaMaxima(Y0, v0y, Gravedad);

            // Altura del objetivo: aleatoria entre una altura mínima razonable y el 65 % de la altura máxima.
            double yMin = Math.Max(0.5, Math.Min(Y0 * 0.3, 3.0));
            double yMaxPosible = Math.Max(yMin + 1.0, yMax * 0.65);
            ObjetivoY = yMin + rnd.NextDouble() * (yMaxPosible - yMin);

            // Ancho del objetivo: entre 2.5 m y 4.5 m (lo bastante ancho para tener buena
            // probabilidad de intersección con la trayectoria a pesar del error de Euler).
            double ancho = 2.5 + rnd.NextDouble() * 2.0;

            // Posición horizontal: hallamos los dos puntos donde la trayectoria cruza y=ObjetivoY
            // y(t) = Y0 + v0y t - 0.5 g t²  ->  t = (v0y ± sqrt(v0y² - 2 g (Y0 - ObjetivoY))) / g
            double disc = v0y * v0y - 2.0 * Gravedad * (Y0 - ObjetivoY);
            if (disc < 0 || v0x <= 0)
            {
                // La trayectoria no alcanza esta altura (caso raro). Colocamos el objetivo
                // en una zona neutra: el simulador no lo impactará.
                double xAlcance = ModeloTeorico.Alcance(X0, v0x, tTotal);
                double xCentroAux = X0 + (xAlcance - X0) * 0.5 + (rnd.NextDouble() - 0.5) * 2;
                ObjetivoXMin = xCentroAux;
                ObjetivoXMax = xCentroAux + ancho;
                ObjetivoDefinido = true;
                ObjetivoImpactado = false;
                return;
            }

            double t1 = (v0y - Math.Sqrt(disc)) / Gravedad;
            double t2 = (v0y + Math.Sqrt(disc)) / Gravedad;
            double xUp = X0 + v0x * t1;   // cruce subiendo
            double xDown = X0 + v0x * t2; // cruce bajando

            // Usamos el cruce descendente (más estable lejos del origen). El ancho es
            // generoso para absorber el error numérico del integrador de Euler
            // (que tiende a cruzar el nivel y un poco más tarde/después que la
            // solución analítica). El jitter se mantiene pequeño.
            double jitter = (rnd.NextDouble() - 0.5) * Math.Min(1.0, ancho * 0.2);

            double xIzquierda = xDown - ancho / 2.0 + jitter;
            double xDerecha = xIzquierda + ancho;

            if (xDerecha <= X0 + 0.1)
            {
                ObjetivoDefinido = false;
                ObjetivoImpactado = false;
                return;
            }

            ObjetivoXMin = xIzquierda;
            ObjetivoXMax = xDerecha;
            ObjetivoDefinido = true;
            ObjetivoImpactado = false;
        }

        /// <summary>
        /// Prepara el estado para iniciar (o reiniciar) la simulación.
        /// </summary>
        public void Iniciar()
        {
            double angRad = anguloARadianes();
            t = 0;
            x = X0;
            y = Y0;
            vx = V0 * Math.Cos(angRad);
            vy = V0 * Math.Sin(angRad);

            rebotesSuelo = 0;
            ObjetivoImpactado = false;
            terminado = false;

            Muestras = new List<Muestra>();
            Colisiones = new ColisionesList();

            TiempoTotalVueloSim = 0;
            AlturaMaximaSim = Y0;
            TiempoAlturaMaximaSim = 0;
            XAlturaMaximaSim = X0;
            AlcanceSim = X0;
            VelocidadEnAlturaMaxima = new Muestra(0, X0, Y0, vx, vy);
            VelocidadEnImpacto = null;

            Muestras.Add(new Muestra(t, x, y, vx, vy));
        }

        private double anguloARadianes()
        {
            return AnguloGrados * Math.PI / 180.0;
        }

        /// <summary>
        /// Avanza la simulación un paso de tiempo dt.
        /// Devuelve true si la simulación continúa; false si ya terminó.
        /// </summary>
        public bool Paso()
        {
            if (terminado) return false;

            double tPrev = t;
            double xPrev = x;
            double yPrev = y;
            double vxPrev = vx;
            double vyPrev = vy;

            double tNext = tPrev + DT;
            double xNext = xPrev + vxPrev * DT;
            double yNext = yPrev + vyPrev * DT;
            double vyNext = vyPrev - Gravedad * DT;

            // --- Colisión con objetivo (segmento horizontal) ---
            // Detección bidireccional: el proyectil puede cruzar y=ObjetivoY subiendo o bajando.
            if (ObjetivoDefinido && !ObjetivoImpactado &&
                yPrev != yNext &&
                (yPrev - ObjetivoY) * (yNext - ObjetivoY) <= 0)
            {
                double ratio = (ObjetivoY - yPrev) / (yNext - yPrev);
                double tCross = tPrev + DT * ratio;
                double xCross = xPrev + vxPrev * DT * ratio;
                double vyCross = vyPrev - Gravedad * DT * ratio;

                if (xCross >= ObjetivoXMin && xCross <= ObjetivoXMax)
                {
                    RegistrarColisionObjetivo(tCross, xCross, ObjetivoY, vxPrev, vyCross);

                    // Aplicar rebote: invertir vy y reducir magnitud al 60 % (escala 0.6 en ambas componentes).
                    t = tCross;
                    x = xCross;
                    y = ObjetivoY;
                    vx = 0.6 * vxPrev;
                    vy = 0.6 * (-vyCross);

                    Muestras.Add(new Muestra(t, x, y, vx, vy));
                    ObjetivoImpactado = true;
                    ActualizarMaximos();
                    return true;
                }
            }

            // --- Colisión con suelo ---
            if (yNext <= 0 && yPrev > 0)
            {
                double ratio = (yPrev - 0) / (yPrev - yNext);
                double tCross = tPrev + DT * ratio;
                double xCross = xPrev + vxPrev * DT * ratio;
                double vyCross = vyPrev - Gravedad * DT * ratio;

                if (rebotesSuelo == 0)
                {
                    // Primer impacto con el suelo: registrar y rebotar.
                    RegistrarColisionSuelo(tCross, xCross, 0, vxPrev, vyCross, rebotesSuelo + 1);
                    t = tCross;
                    x = xCross;
                    y = 0;
                    vx = 0.6 * vxPrev;
                    vy = 0.6 * (-vyCross);
                    rebotesSuelo = 1;

                    TiempoTotalVueloSim = tCross;
                    AlcanceSim = xCross;
                    VelocidadEnImpacto = new Muestra(tCross, xCross, 0, vx, vy);

                    Muestras.Add(new Muestra(t, x, y, vx, vy));
                    ActualizarMaximos();
                    return true;
                }
                else
                {
                    // Segundo impacto con el suelo: registrar y terminar.
                    double vxFinal = vxPrev;
                    double vyFinal = vyCross;
                    RegistrarColisionSuelo(tCross, xCross, 0, vxPrev, vyCross, rebotesSuelo + 1);

                    t = tCross;
                    x = xCross;
                    y = 0;
                    vx = 0;
                    vy = 0;

                    // TiempoTotalVueloSim / AlcanceSim se mantienen con los valores del
                    // PRIMER impacto (los que se comparan con el modelo teórico).
                    // Guardamos los valores finales por separado.
                    TiempoFinalSim = tCross;
                    AlcanceFinalSim = xCross;
                    VelocidadEnImpacto = new Muestra(tCross, xCross, 0, vxFinal, vyFinal);

                    Muestras.Add(new Muestra(t, x, y, vxFinal, vyFinal));
                    ActualizarMaximos();
                    terminado = true;
                    return false;
                }
            }

            // --- Caso especial: inicio exactamente en el suelo (y0 = 0) ---
            if (yPrev == 0 && vyPrev <= 0 && yNext <= 0)
            {
                if (rebotesSuelo == 0)
                {
                    RegistrarColisionSuelo(tPrev, 0, 0, vxPrev, vyPrev, 1);
                    t = tPrev; x = 0; y = 0;
                    vx = 0.6 * vxPrev;
                    vy = 0.6 * (-vyPrev);
                    rebotesSuelo = 1;
                    Muestras.Add(new Muestra(t, x, y, vx, vy));
                    return true;
                }
                else
                {
                    RegistrarColisionSuelo(tPrev, 0, 0, vxPrev, vyPrev, 2);
                    VelocidadEnImpacto = new Muestra(tPrev, 0, 0, vxPrev, vyPrev);
                    TiempoTotalVueloSim = tPrev;
                    AlcanceSim = 0;
                    terminado = true;
                    return false;
                }
            }

            // --- Avance normal ---
            t = tNext;
            x = xNext;
            y = yNext;
            vy = vyNext;
            // vx constante

            Muestras.Add(new Muestra(t, x, y, vx, vy));
            ActualizarMaximos();
            return true;
        }

        /// <summary>
        /// Ejecuta la simulación completa hasta terminar. Útil para generar las gráficas
        /// sin necesidad de animar paso a paso.
        /// </summary>
        public void EjecutarCompleto()
        {
            if (!Iniciado()) Iniciar();
            int safety = 10000000; // evita loops infinitos en configuraciones degeneradas
            while (!terminado && safety-- > 0)
            {
                if (!Paso()) break;
            }
        }

        private bool Iniciado()
        {
            return Muestras != null;
        }

        private void ActualizarMaximos()
        {
            if (y > AlturaMaximaSim)
            {
                AlturaMaximaSim = y;
                TiempoAlturaMaximaSim = t;
                XAlturaMaximaSim = x;
                VelocidadEnAlturaMaxima = new Muestra(t, x, y, vx, vy);
            }
        }

        private void RegistrarColisionObjetivo(double tCross, double xCross, double yCross,
                                              double vxAntes, double vyAntes)
        {
            var c = new Colision
            {
                Tipo = TipoColision.Objetivo,
                Numero = Colisiones.Count + 1,
                Tiempo = tCross,
                X = xCross,
                Y = yCross,
                VxAntes = vxAntes,
                VyAntes = vyAntes,
                VxDespues = 0.6 * vxAntes,
                VyDespues = 0.6 * (-vyAntes)
            };
            Colisiones.Add(c);
        }

        private void RegistrarColisionSuelo(double tCross, double xCross, double yCross,
                                            double vxAntes, double vyAntes, int numero)
        {
            // En el último impacto, vx/vy después son 0.
            double vxD, vyD;
            if (rebotesSuelo == 0)
            {
                vxD = 0.6 * vxAntes;
                vyD = 0.6 * (-vyAntes);
            }
            else
            {
                vxD = 0;
                vyD = 0;
            }

            var c = new Colision
            {
                Tipo = TipoColision.Suelo,
                Numero = numero,
                Tiempo = tCross,
                X = xCross,
                Y = yCross,
                VxAntes = vxAntes,
                VyAntes = vyAntes,
                VxDespues = vxD,
                VyDespues = vyD
            };
            Colisiones.Add(c);
        }
    }

    /// <summary>
    /// Lista de colisiones que respeta el orden de inserción.
    /// (List&lt;T&gt; ya lo hace, esto queda como tipo nominal para claridad.)
    /// </summary>
    public class ColisionesList : List<Colision> { }
}