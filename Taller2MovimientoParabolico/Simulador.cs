using System;
using System.Collections.Generic;

namespace Taller2MovimientoParabolico
{
    /// <summary>
    /// Motor de simulación del movimiento parabólico. Integra con método de Euler,
    /// detecta colisiones contra el suelo, el techo y las paredes laterales del
    /// mundo físico, aplica rebotes con pérdida del 40 % de la magnitud de la
    /// velocidad e invierte la componente perpendicular. La simulación termina
    /// cuando el proyectil toca el suelo por segunda vez (después del rebote
    /// previo) o cuando se alcanza el tope de seguridad de muestras.
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

        /// <summary>Límite derecho del mundo físico (usado para colisión con la pared Derecha).</summary>
        public double MundoXMax { get; private set; }
        /// <summary>Límite superior del mundo físico (usado para colisión con el Techo).</summary>
        public double MundoYMax { get; private set; }

        // Estado interno
        private double t;
        private double x;
        private double y;
        private double vx;
        private double vy;
        private int rebotesSuelo;
        private bool terminado;

        /// <summary>Tope de seguridad: número máximo de muestras para evitar loops infinitos.</summary>
        public const int TopeMuestras = 5000;
        /// <summary>True si la simulación terminó porque se alcanzó <see cref="TopeMuestras"/>.</summary>
        public bool TerminadoPorTopeMuestras { get; private set; }

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
        /// Construye e inicializa una simulación.
        /// </summary>
        public Simulador(double x0, double y0, double v0, double anguloGrados,
                         double gravedad, double dt)
        {
            if (dt <= 0) throw new ArgumentException("dt debe ser positivo.");
            X0 = x0; Y0 = y0; V0 = v0;
            AnguloGrados = anguloGrados;
            Gravedad = gravedad;
            DT = dt;
        }

        /// <summary>
        /// Prepara el estado para iniciar (o reiniciar) la simulación.
        /// Calcula el tamaño del mundo físico (mundoXMax, mundoYMax) a partir
        /// del alcance y la altura máxima analíticos, con un margen de 5 m en X
        /// y 2 m en Y. Estos límites se usan para detectar colisiones contra
        /// las 4 paredes (Suelo, Techo, Izquierda, Derecha).
        /// </summary>
        public void Iniciar()
        {
            double angRad = anguloARadianes();
            double v0xT = V0 * Math.Cos(angRad);
            double v0yT = V0 * Math.Sin(angRad);
            double tTeor = ModeloTeorico.TiempoTotalVuelo(Y0, v0yT, Gravedad);
            double xAlcanceTeor = ModeloTeorico.Alcance(X0, v0xT, tTeor);
            double yMaxTeor = ModeloTeorico.AlturaMaxima(Y0, v0yT, Gravedad);
            MundoXMax = Math.Max(xAlcanceTeor, X0) + 5;
            MundoYMax = Math.Max(yMaxTeor, Y0) + 2;

            t = 0;
            x = X0;
            y = Y0;
            vx = v0xT;
            vy = v0yT;

            rebotesSuelo = 0;
            terminado = false;
            TerminadoPorTopeMuestras = false;

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

            // --- Colisión con techo (y >= mundoYMax) ---
            // El proyectil sube y choca el borde superior del mundo; rebota hacia abajo
            // con vy -> -0.6 * |vy|. vx no se modifica.
            if (MundoYMax > 0 && yPrev < MundoYMax && yNext >= MundoYMax)
            {
                double ratio = (MundoYMax - yPrev) / (yNext - yPrev);
                double tCross = tPrev + DT * ratio;
                double xCross = xPrev + vxPrev * DT * ratio;
                double vyCross = vyPrev - Gravedad * DT * ratio;

                double vxDespues = vxPrev;
                double vyDespues = -0.6 * Math.Abs(vyCross);

                RegistrarColisionPared(TipoColision.Techo, tCross, xCross, MundoYMax,
                    vxPrev, vyCross, vxDespues, vyDespues);

                t = tCross;
                x = xCross;
                y = MundoYMax;
                vx = vxDespues;
                vy = vyDespues;

                Muestras.Add(new Muestra(t, x, y, vx, vy));
                ActualizarMaximos();

                if (Muestras.Count >= TopeMuestras)
                {
                    TerminadoPorTopeMuestras = true;
                    terminado = true;
                    TiempoFinalSim = t;
                    AlcanceFinalSim = x;
                    return false;
                }
                return true;
            }

            // --- Colisión con pared izquierda (x <= 0) ---
            // El proyectil retrocede y choca el borde izquierdo; rebota hacia la derecha
            // con vx -> 0.6 * |vx|. vy no se modifica.
            if (xPrev > 0 && xNext <= 0)
            {
                double ratio = (0 - xPrev) / (xNext - xPrev);
                double tCross = tPrev + DT * ratio;
                double yCross = yPrev + vyPrev * DT * ratio;
                double vyCross = vyPrev - Gravedad * DT * ratio;

                double vxDespues = 0.6 * Math.Abs(vxPrev);
                double vyDespues = vyCross;

                RegistrarColisionPared(TipoColision.Izquierda, tCross, 0, yCross,
                    vxPrev, vyCross, vxDespues, vyDespues);

                t = tCross;
                x = 0;
                y = yCross;
                vx = vxDespues;
                vy = vyDespues;

                Muestras.Add(new Muestra(t, x, y, vx, vy));
                ActualizarMaximos();

                if (Muestras.Count >= TopeMuestras)
                {
                    TerminadoPorTopeMuestras = true;
                    terminado = true;
                    TiempoFinalSim = t;
                    AlcanceFinalSim = x;
                    return false;
                }
                return true;
            }

            // --- Colisión con pared derecha (x >= mundoXMax) ---
            // El proyectil avanza y choca el borde derecho; rebota hacia la izquierda
            // con vx -> -0.6 * |vx|. vy no se modifica.
            if (MundoXMax > 0 && xPrev < MundoXMax && xNext >= MundoXMax)
            {
                double ratio = (MundoXMax - xPrev) / (xNext - xPrev);
                double tCross = tPrev + DT * ratio;
                double yCross = yPrev + vyPrev * DT * ratio;
                double vyCross = vyPrev - Gravedad * DT * ratio;

                double vxDespues = -0.6 * Math.Abs(vxPrev);
                double vyDespues = vyCross;

                RegistrarColisionPared(TipoColision.Derecha, tCross, MundoXMax, yCross,
                    vxPrev, vyCross, vxDespues, vyDespues);

                t = tCross;
                x = MundoXMax;
                y = yCross;
                vx = vxDespues;
                vy = vyDespues;

                Muestras.Add(new Muestra(t, x, y, vx, vy));
                ActualizarMaximos();

                if (Muestras.Count >= TopeMuestras)
                {
                    TerminadoPorTopeMuestras = true;
                    terminado = true;
                    TiempoFinalSim = t;
                    AlcanceFinalSim = x;
                    return false;
                }
                return true;
            }

            // --- Avance normal ---
            t = tNext;
            x = xNext;
            y = yNext;
            vy = vyNext;
            // vx constante

            Muestras.Add(new Muestra(t, x, y, vx, vy));
            ActualizarMaximos();

            if (Muestras.Count >= TopeMuestras)
            {
                TerminadoPorTopeMuestras = true;
                terminado = true;
                TiempoFinalSim = t;
                AlcanceFinalSim = x;
                return false;
            }
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

        /// <summary>
        /// Registra una colisión contra una pared del mundo (Techo, Izquierda, Derecha).
        /// A diferencia del Suelo, el número de impacto es simplemente el orden global
        /// en la que ocurren (no hay conteo separado por pared).
        /// </summary>
        private void RegistrarColisionPared(TipoColision tipo, double tCross, double xCross, double yCross,
                                            double vxAntes, double vyAntes,
                                            double vxDespues, double vyDespues)
        {
            var c = new Colision
            {
                Tipo = tipo,
                Numero = Colisiones.Count + 1,
                Tiempo = tCross,
                X = xCross,
                Y = yCross,
                VxAntes = vxAntes,
                VyAntes = vyAntes,
                VxDespues = vxDespues,
                VyDespues = vyDespues
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