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

        /// <summary>
        /// Objetivo horizontal fijo generado al iniciar la simulación.
        /// Es null hasta que se llama a <see cref="Iniciar"/>, y queda en null
        /// si <see cref="ObjetivoHabilitado"/> es false.
        /// </summary>
        public Objetivo Objetivo { get; private set; }

        /// <summary>
        /// Si es false, <see cref="Iniciar"/> no genera un objetivo y
        /// <see cref="Objetivo"/> queda en null (la simulación se comporta
        /// como si no hubiera objetivo: cae, rebota en el suelo 2 veces y termina).
        /// Default: true.
        /// </summary>
        public bool ObjetivoHabilitado { get; set; } = true;

        // Generador de números aleatorios para el objetivo.
        private Random rng;
        // Marca interna: ya se registró el impacto contra el objetivo en esta corrida.
        private bool objetivoGolpeado;

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
            rng = new Random();
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

            if (ObjetivoHabilitado)
            {
                GenerarObjetivo();
                objetivoGolpeado = false;
            }
            else
            {
                Objetivo = null;
            }

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
        /// Genera un objetivo horizontal aleatorio para esta corrida. Para
        /// garantizar que el proyectil SIEMPRE intersecte el objetivo (y se
        /// pueda observar la cadena de colisiones descrita en el enunciado),
        /// se elige una X aleatoria dentro del span horizontal de la trayectoria
        /// y se coloca el objetivo EXACTAMENTE sobre la curva analítica en esa X.
        /// El ancho del objetivo es 10 % - 25 % del rango horizontal.
        /// </summary>
        private void GenerarObjetivo()
        {
            double angRad = anguloARadianes();
            double v0xT = V0 * Math.Cos(angRad);
            double v0yT = V0 * Math.Sin(angRad);
            double tTeor = ModeloTeorico.TiempoTotalVuelo(Y0, v0yT, Gravedad);
            double alcanceTeor = ModeloTeorico.Alcance(X0, v0xT, tTeor);
            double alturaMaxTeor = ModeloTeorico.AlturaMaxima(Y0, v0yT, Gravedad);

            // Rango horizontal de la trayectoria. Si v0x <= 0 (alcance <= X0) usamos
            // un valor positivo mínimo para que las cuentas no exploten; en ese caso
            // el objetivo queda sobre x0 y la colisión sigue siendo detectable.
            double rangeX = alcanceTeor - X0;
            if (rangeX <= 0) rangeX = Math.Max(Math.Abs(alcanceTeor), Math.Abs(X0)) + 1.0;

            // 1. Elegir X aleatorio entre el 15 % y el 75 % del rango (evita los
            //    extremos donde la trayectoria se aplana y haría falta dt muy fino
            //    para detectar el cruce).
            double xMinBase = X0 + 0.15 * rangeX;
            double xMaxBase = X0 + 0.75 * rangeX;
            if (xMaxBase <= xMinBase) xMaxBase = xMinBase + 0.5 * rangeX;

            double xCentroObj = xMinBase + rng.NextDouble() * (xMaxBase - xMinBase);

            // 2. Calcular Y en la trayectoria analítica en esa X. Como la trayectoria
            //    pasa DOS veces por cada X (subiendo y bajando), tomamos el primer
            //    cruce: t = (x - x0) / v0x. Si v0x ≈ 0 (caso vertical), el objetivo
            //    va a media altura.
            double yEnTrayectoria;
            if (Math.Abs(v0xT) < 1e-6)
            {
                yEnTrayectoria = (Y0 + alturaMaxTeor) * 0.5;
            }
            else
            {
                double t = (xCentroObj - X0) / v0xT;
                if (t < 0) t = 0;
                if (t > tTeor) t = tTeor;
                yEnTrayectoria = Y0 + v0yT * t - 0.5 * Gravedad * t * t;
            }

            // 3. Ancho del objetivo: 10 % - 25 % del rango horizontal.
            double minWidth = 0.10 * rangeX;
            double maxWidth = 0.25 * rangeX;
            if (maxWidth < minWidth) maxWidth = minWidth;
            double width = minWidth + rng.NextDouble() * (maxWidth - minWidth);

            double xMinObj = xCentroObj - width / 2.0;
            double xMaxObj = xCentroObj + width / 2.0;

            // 4. El objetivo va sobre la trayectoria. Y no negativa para evitar
            //    problemas visuales con el suelo.
            double yObj = Math.Max(0.05, yEnTrayectoria);

            Objetivo = new Objetivo(xMinObj, xMaxObj, yObj, 0.2);
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

            // --- Colisión con objetivo (barra horizontal) ---
            // Se detecta el cruce del plano Y del objetivo (subiendo O bajando) y se
            // comprueba el x INTERPOLADO en el cruce contra el rango [XMin, XMax] del
            // objetivo. Antes el chequeo usaba xNext y solo miraba cruces bajando,
            // lo que dejaba pasar el cruce ascendente (el proyectil atraviesa el
            // target subiendo) y los casos en los que el xNext quedaba fuera del
            // rango aunque el xCross estuviera dentro.
            if (!objetivoGolpeado && Objetivo != null
                && (yPrev - Objetivo.Y) * (yNext - Objetivo.Y) <= 0
                && yPrev != Objetivo.Y)
            {
                double ratio = (yPrev - Objetivo.Y) / (yPrev - yNext);
                double xCross = xPrev + vxPrev * DT * ratio;

                if (xCross >= Objetivo.XMin && xCross <= Objetivo.XMax)
                {
                    double tCross = tPrev + DT * ratio;
                    double vyCross = vyPrev - Gravedad * DT * ratio;

                    // Rebote: la magnitud de la velocidad se reduce al 60 % y la componente
                    // vertical cambia de sentido (equivalente a multiplicar ambas componentes
                    // por 0.6 e invertir vy).
                    double vxNew = 0.6 * vxPrev;
                    double vyNew = -0.6 * vyCross;

                    RegistrarColisionObjetivo(tCross, xCross, Objetivo.Y,
                        vxPrev, vyCross, vxNew, vyNew);

                    t = tCross;
                    x = xCross;
                    y = Objetivo.Y;
                    vx = vxNew;
                    vy = vyNew;
                    objetivoGolpeado = true;

                    Muestras.Add(new Muestra(t, x, y, vx, vy));
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

        /// <summary>
        /// Registra la colisión contra el objetivo horizontal. Sigue el mismo
        /// patrón que <see cref="RegistrarColisionPared"/> pero tipifica la
        /// colisión como <see cref="TipoColision.Objetivo"/>.
        /// </summary>
        private void RegistrarColisionObjetivo(double tCross, double xCross, double yCross,
                                               double vxAntes, double vyAntes,
                                               double vxDespues, double vyDespues)
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