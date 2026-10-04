using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace Taller2MovimientoParabolico
{
    public partial class Form1 : Form
    {
        private Simulador simulador;
        private bool simulacionActiva;
        private bool simulacionPausada;
        private int pasosPorTick; // cuántos pasos de simulación se ejecutan por tick del Timer

        // Para acumular la trayectoria ya dibujada (no solo el estado actual)
        private List<PointF> trayectoriaParaDibujar = new List<PointF>();

        // Para métricas: última muestra válida mostrada.
        private Muestra ultimaMuestra;

        // Cache de unidades de conversión de píxeles a metros (animación)
        private float pixPorMetroX;
        private float pixPorMetroY;
        private float origenXpx;
        private float origenYpx;
        private double mundoXMax;
        private double mundoYMax;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            simulacionActiva = false;
            simulacionPausada = false;
            trayectoriaParaDibujar = new List<PointF>();
            ultimaMuestra = null;
            ConfigurarDataGridViews();

            statusLabel.Text = "Configure las condiciones iniciales y presione Iniciar.";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DetenerTimer();
        }

        // Configuración DataGridView
        private void ConfigurarDataGridViews()
        {
            // Comparación teórico vs simulación
            dgvComparacion.Columns.Clear();
            dgvComparacion.Columns.Add("var", "Variable");
            dgvComparacion.Columns.Add("unidad", "Unidad");
            dgvComparacion.Columns.Add("teorico", "Valor teórico");
            dgvComparacion.Columns.Add("sim", "Valor simulación");
            dgvComparacion.Columns.Add("dif", "Diferencia");
            dgvComparacion.Columns.Add("pct", "Diferencia %");
            dgvComparacion.Columns[0].FillWeight = 30;
            dgvComparacion.Columns[1].FillWeight = 12;
            dgvComparacion.Columns[2].FillWeight = 22;
            dgvComparacion.Columns[3].FillWeight = 22;
            dgvComparacion.Columns[4].FillWeight = 14;
            dgvComparacion.Columns[5].FillWeight = 14;

            // Colisiones
            dgvColisiones.Columns.Clear();
            dgvColisiones.Columns.Add("n", "#");
            dgvColisiones.Columns.Add("tipo", "Tipo");
            dgvColisiones.Columns.Add("t", "t (s)");
            dgvColisiones.Columns.Add("x", "x (m)");
            dgvColisiones.Columns.Add("y", "y (m)");
            dgvColisiones.Columns.Add("vxA", "vx antes");
            dgvColisiones.Columns.Add("vyA", "vy antes");
            dgvColisiones.Columns.Add("magA", "|v| antes");
            dgvColisiones.Columns.Add("angA", "θ antes (°)");
            dgvColisiones.Columns.Add("vxD", "vx después");
            dgvColisiones.Columns.Add("vyD", "vy después");
            dgvColisiones.Columns.Add("magD", "|v| después");
            dgvColisiones.Columns.Add("angD", "θ después (°)");
        }

        // Lectura y validación de parámetros
        private bool LeerParametros(out double x0, out double y0, out double v0,
                                    out double angulo, out double gravedad, out double dt,
                                    out string mensajeError)
        {
            x0 = y0 = v0 = angulo = gravedad = dt = 0;
            mensajeError = null;

            if (!TryParse(txtX0.Text, out x0) || x0 < 0 || x0 > 100)
            {
                mensajeError = "La posición horizontal inicial debe estar entre 0 m y 100 m.";
                return false;
            }
            if (!TryParse(txtY0.Text, out y0) || y0 < 0 || y0 > 50)
            {
                mensajeError = "La altura inicial debe estar entre 0 m y 50 m.";
                return false;
            }
            if (!TryParse(txtV0.Text, out v0) || v0 <= 0)
            {
                mensajeError = "La magnitud de la velocidad inicial debe ser mayor a cero.";
                return false;
            }
            if (!TryParse(txtAngulo.Text, out angulo) || angulo < -90 || angulo > 90)
            {
                mensajeError = "El ángulo debe estar entre -90° y 90°.";
                return false;
            }
            if (!TryParse(txtGravedad.Text, out gravedad) || gravedad <= 0)
            {
                mensajeError = "La gravedad debe ser un número positivo.";
                return false;
            }
            if (!TryParse(txtDt.Text, out dt) || dt <= 0)
            {
                mensajeError = "El intervalo Δt debe ser un número positivo.";
                return false;
            }
            return true;
        }

        private static bool TryParse(string s, out double v)
        {
            return double.TryParse(s.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out v);
        }

        // Botones de control de simulación
        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (simulacionActiva && !simulacionPausada) return;

            if (!LeerParametros(out double x0, out double y0, out double v0,
                                out double angulo, out double gravedad, out double dt,
                                out string mensaje))
            {
                MessageBox.Show(this, mensaje, "Parámetros inválidos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (simulador == null || !MismosParametros(x0, y0, v0, angulo, gravedad, dt))
            {
                simulador = new Simulador(x0, y0, v0, angulo, gravedad, dt);
                simulador.Iniciar();
                trayectoriaParaDibujar = new List<PointF>();
                PrepararAnimacion();
            }
            else
            {
                // Mismos parámetros: simplemente reiniciar desde el principio.
                simulador.Iniciar();
                trayectoriaParaDibujar = new List<PointF>();
            }

            // Pasos por tick: apuntamos a ~3 segundos totales de animación.
            int pasosTotalesEstimados = Math.Max(50, (int)(simulador.Muestras.Count > 0
                ? (ModeloTeorico.TiempoTotalVuelo(y0, v0 * Math.Sin(angulo * Math.PI / 180), gravedad) / dt)
                : 100));
            pasosPorTick = Math.Max(1, pasosTotalesEstimados / 150);

            simulacionActiva = true;
            simulacionPausada = false;
            ActualizarEstadoBotones();
            ActualizarLabelEstadoAnim("Estado: en ejecución");
            statusLabel.Text = "Simulación en curso...";
            timerSimulacion.Start();
        }

        private void btnPausar_Click(object sender, EventArgs e)
        {
            if (!simulacionActiva) return;
            if (simulacionPausada)
            {
                simulacionPausada = false;
                timerSimulacion.Start();
                btnPausar.Text = "Pausar";
                ActualizarLabelEstadoAnim("Estado: en ejecución");
                statusLabel.Text = "Simulación reanudada.";
            }
            else
            {
                simulacionPausada = true;
                timerSimulacion.Stop();
                btnPausar.Text = "Reanudar";
                ActualizarLabelEstadoAnim("Estado: pausada");
                statusLabel.Text = "Simulación pausada.";
            }
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            if (simulador == null) return;
            DetenerTimer();
            simulador.Iniciar();
            trayectoriaParaDibujar = new List<PointF>();
            simulacionActiva = false;
            simulacionPausada = false;
            ActualizarEstadoBotones();
            ActualizarLabelEstadoAnim("Estado: detenido");
            statusLabel.Text = "Simulación reiniciada. Presione Iniciar.";
            LimpiarMetricasYResultados();
            panelAnimacion.Invalidate();
        }

        private void btnNuevaSim_Click(object sender, EventArgs e)
        {
            DetenerTimer();
            simulador = null;
            simulacionActiva = false;
            simulacionPausada = false;
            trayectoriaParaDibujar = new List<PointF>();
            ultimaMuestra = null;
            ActualizarEstadoBotones();
            ActualizarLabelEstadoAnim("Estado: detenido");
            LimpiarMetricasYResultados();
            statusLabel.Text = "Nueva simulación. Configure los parámetros e Iniciar.";
            btnIniciar.Enabled = true;
            btnPausar.Enabled = false;
            btnReiniciar.Enabled = false;
            panelAnimacion.Invalidate();
        }

        private bool MismosParametros(double x0, double y0, double v0, double angulo,
                                      double gravedad, double dt)
        {
            return simulador != null
                && Math.Abs(simulador.X0 - x0) < 1e-9
                && Math.Abs(simulador.Y0 - y0) < 1e-9
                && Math.Abs(simulador.V0 - v0) < 1e-9
                && Math.Abs(simulador.AnguloGrados - angulo) < 1e-9
                && Math.Abs(simulador.Gravedad - gravedad) < 1e-9
                && Math.Abs(simulador.DT - dt) < 1e-9;
        }

        private void DetenerTimer()
        {
            timerSimulacion.Stop();
        }

        private void ActualizarEstadoBotones()
        {
            btnIniciar.Enabled = !simulacionActiva || simulacionPausada;
            btnPausar.Enabled = simulacionActiva;
            btnPausar.Text = simulacionPausada ? "Reanudar" : "Pausar";
            btnReiniciar.Enabled = simulador != null;
            btnNuevaSim.Enabled = true;
            txtX0.Enabled = !simulacionActiva;
            txtY0.Enabled = !simulacionActiva;
            txtV0.Enabled = !simulacionActiva;
            txtAngulo.Enabled = !simulacionActiva;
            txtGravedad.Enabled = !simulacionActiva;
            txtDt.Enabled = !simulacionActiva;
        }

        private void ActualizarLabelEstadoAnim(string texto)
        {
            lblEstadoAnim.Text = texto;
        }
        // timer
        private void timerSimulacion_Tick(object sender, EventArgs e)
        {
            if (simulador == null) return;

            for (int i = 0; i < pasosPorTick && !simulador.Terminado; i++)
            {
                if (!simulador.Paso()) break;
            }

            if (simulador.Muestras.Count > 0)
            {
                ultimaMuestra = simulador.Muestras[simulador.Muestras.Count - 1];
                trayectoriaParaDibujar.Add(new PointF((float)ultimaMuestra.X, (float)ultimaMuestra.Y));
            }

            ActualizarMetricas(ultimaMuestra);
            panelAnimacion.Invalidate();

            if (simulador.Terminado)
            {
                DetenerTimer();
                simulacionActiva = false;
                simulacionPausada = false;
                ActualizarEstadoBotones();
                ActualizarLabelEstadoAnim("Estado: finalizada");
                if (simulador.TerminadoPorTopeMuestras)
                {
                    statusLabel.Text = string.Format(CultureInfo.InvariantCulture,
                        "Simulación finalizada por tope de seguridad ({0} muestras). Tiempo total: {1:0.###} s. Colisiones: {2}.",
                        Simulador.TopeMuestras, simulador.TiempoTotalVueloSim, simulador.Colisiones.Count);
                }
                else
                {
                    statusLabel.Text = string.Format(CultureInfo.InvariantCulture,
                        "Simulación finalizada. Tiempo total: {0:0.###} s. Colisiones: {1}.",
                        simulador.TiempoTotalVueloSim, simulador.Colisiones.Count);
                }
                MostrarResultadosFinales();
            }
        }
        // Métricas en tiempo real
        private void ActualizarMetricas(Muestra m)
        {
            if (m == null) return;
            lblTiempoVal.Text = FormatearMetrica("t", m.Tiempo, "s");
            lblXVal.Text = FormatearMetrica("x", m.X, "m");
            lblYVal.Text = FormatearMetrica("y", m.Y, "m");
            lblVxVal.Text = FormatearMetrica("vx", m.Vx, "m/s");
            lblVyVal.Text = FormatearMetrica("vy", m.Vy, "m/s");
            lblVmagVal.Text = FormatearMetrica("|v|", m.MagnitudVelocidad, "m/s");
            lblAngVal.Text = FormatearMetrica("θ", m.AnguloVelocidad, "°");
        }

        private static string FormatearMetrica(string nombre, double valor, string unidad)
        {
            return string.Format(CultureInfo.InvariantCulture,
                "{0} = {1:0.000} {2}", nombre, valor, unidad);
        }

        private void LimpiarMetricasYResultados()
        {
            ultimaMuestra = null;
            lblTiempoVal.Text = "t = 0.000 s";
            lblXVal.Text = "x = 0.000 m";
            lblYVal.Text = "y = 0.000 m";
            lblVxVal.Text = "vx = 0.000 m/s";
            lblVyVal.Text = "vy = 0.000 m/s";
            lblVmagVal.Text = "|v| = 0.000 m/s";
            lblAngVal.Text = "θ = 0.000 °";

            dgvComparacion.Rows.Clear();
            dgvColisiones.Rows.Clear();
            lblFinalTiempoTotalVal.Text = "Tiempo total de vuelo:";
            lblFinalAlturaMaxVal.Text = "Altura máxima:";
            lblFinalXAlturaMaxVal.Text = "x en altura máxima:";
            lblFinalAlcanceVal.Text = "Alcance horizontal:";
            lblFinalVxMaxVal.Text = "vx (altura máx):";
            lblFinalVyMaxVal.Text = "vy (altura máx):";
            lblFinalVMagMaxVal.Text = "|v| (altura máx):";
            lblFinalAngMaxVal.Text = "θ (altura máx):";
            lblFinalVxImpVal.Text = "vx (impacto):";
            lblFinalVyImpVal.Text = "vy (impacto):";
            lblFinalVMagImpVal.Text = "|v| (impacto):";
            lblFinalAngImpVal.Text = "θ (impacto):";

            graficaYt.Puntos = new List<PointF>();
            graficaXt.Puntos = new List<PointF>();
            graficaYx.Puntos = new List<PointF>();
            graficaVx.Puntos = new List<PointF>();
            graficaVy.Puntos = new List<PointF>();
            graficaVmag.Puntos = new List<PointF>();
            graficaTheta.Puntos = new List<PointF>();
            graficaYt.Marcadores = new List<MarcadorGrafica>();
            graficaXt.Marcadores = new List<MarcadorGrafica>();
            graficaYx.Marcadores = new List<MarcadorGrafica>();
            graficaVx.Marcadores = new List<MarcadorGrafica>();
            graficaVy.Marcadores = new List<MarcadorGrafica>();
            graficaVmag.Marcadores = new List<MarcadorGrafica>();
            graficaTheta.Marcadores = new List<MarcadorGrafica>();
            graficaYt.Invalidate();
            graficaXt.Invalidate();
            graficaYx.Invalidate();
            graficaVx.Invalidate();
            graficaVy.Invalidate();
            graficaVmag.Invalidate();
            graficaTheta.Invalidate();
        }

        // Resultados finales: tablas, valores y gráficas
        private void MostrarResultadosFinales()
        {
            if (simulador == null) return;

            // Resultados finales (labels)
            lblFinalTiempoTotalVal.Text = string.Format(CultureInfo.InvariantCulture,
                "Tiempo total (1er impacto): {0:0.###} s — Total simulación: {1:0.###} s",
                simulador.TiempoTotalVueloSim, simulador.TiempoFinalSim);
            lblFinalAlturaMaxVal.Text = string.Format(CultureInfo.InvariantCulture,
                "Altura máxima: {0:0.###} m", simulador.AlturaMaximaSim);
            lblFinalXAlturaMaxVal.Text = string.Format(CultureInfo.InvariantCulture,
                "x en altura máxima: {0:0.###} m", simulador.XAlturaMaximaSim);
            lblFinalAlcanceVal.Text = string.Format(CultureInfo.InvariantCulture,
                "Alcance (1er impacto): {0:0.###} m — Alcance total: {1:0.###} m",
                simulador.AlcanceSim, simulador.AlcanceFinalSim);
            if (simulador.VelocidadEnAlturaMaxima != null)
            {
                lblFinalVxMaxVal.Text = string.Format(CultureInfo.InvariantCulture,
                    "vx (altura máx): {0:0.###} m/s", simulador.VelocidadEnAlturaMaxima.Vx);
                lblFinalVyMaxVal.Text = string.Format(CultureInfo.InvariantCulture,
                    "vy (altura máx): {0:0.###} m/s", simulador.VelocidadEnAlturaMaxima.Vy);
                lblFinalVMagMaxVal.Text = string.Format(CultureInfo.InvariantCulture,
                    "|v| (altura máx): {0:0.###} m/s", simulador.VelocidadEnAlturaMaxima.MagnitudVelocidad);
                lblFinalAngMaxVal.Text = string.Format(CultureInfo.InvariantCulture,
                    "θ (altura máx): {0:0.###} °", simulador.VelocidadEnAlturaMaxima.AnguloVelocidad);
            }
            if (simulador.VelocidadEnImpacto != null)
            {
                lblFinalVxImpVal.Text = string.Format(CultureInfo.InvariantCulture,
                    "vx (impacto): {0:0.###} m/s", simulador.VelocidadEnImpacto.Vx);
                lblFinalVyImpVal.Text = string.Format(CultureInfo.InvariantCulture,
                    "vy (impacto): {0:0.###} m/s", simulador.VelocidadEnImpacto.Vy);
                lblFinalVMagImpVal.Text = string.Format(CultureInfo.InvariantCulture,
                    "|v| (impacto): {0:0.###} m/s", simulador.VelocidadEnImpacto.MagnitudVelocidad);
                lblFinalAngImpVal.Text = string.Format(CultureInfo.InvariantCulture,
                    "θ (impacto): {0:0.###} °", simulador.VelocidadEnImpacto.AnguloVelocidad);
            }

            // Modelo teórico para comparación
            double angRad = simulador.AnguloGrados * Math.PI / 180.0;
            double v0x = simulador.V0 * Math.Cos(angRad);
            double v0y = simulador.V0 * Math.Sin(angRad);
            double tTeor = ModeloTeorico.TiempoTotalVuelo(simulador.Y0, v0y, simulador.Gravedad);
            double tHMaxTeor = ModeloTeorico.TiempoAlturaMaxima(v0y, simulador.Gravedad);
            double yMaxTeor = ModeloTeorico.AlturaMaxima(simulador.Y0, v0y, simulador.Gravedad);
            double xAlcanceTeor = ModeloTeorico.Alcance(simulador.X0, v0x, tTeor);

            // Cargar tabla de comparación
            dgvComparacion.Rows.Clear();
            dgvComparacion.Rows.Add(FilaComparacion("Tiempo total de vuelo", "s", tTeor, simulador.TiempoTotalVueloSim));
            dgvComparacion.Rows.Add(FilaComparacion("Altura máxima", "m", yMaxTeor, simulador.AlturaMaximaSim));
            dgvComparacion.Rows.Add(FilaComparacion("Tiempo hasta altura máx", "s", tHMaxTeor, simulador.TiempoAlturaMaximaSim));
            dgvComparacion.Rows.Add(FilaComparacion("Alcance horizontal", "m", xAlcanceTeor, simulador.AlcanceSim));

            // Cargar tabla de colisiones
            dgvColisiones.Rows.Clear();
            foreach (var c in simulador.Colisiones)
            {
                dgvColisiones.Rows.Add(
                    c.Numero,
                    c.Tipo.ToString(),
                    Formatear(c.Tiempo),
                    Formatear(c.X),
                    Formatear(c.Y),
                    Formatear(c.VxAntes),
                    Formatear(c.VyAntes),
                    Formatear(c.MagnitudAntes),
                    Formatear(c.AnguloAntes),
                    Formatear(c.VxDespues),
                    Formatear(c.VyDespues),
                    Formatear(c.MagnitudDespues),
                    Formatear(c.AnguloDespues));
            }

            // Gráficas
            DibujarGraficas();
        }

        private string[] FilaComparacion(string var, string unidad, double teorico, double simulacion)
        {
            double dif = simulacion - teorico;
            double pct = teorico == 0 ? 0 : (simulacion - teorico) / teorico * 100.0;
            return new[]
            {
                var,
                unidad,
                Formatear(teorico),
                Formatear(simulacion),
                Formatear(dif),
                string.Format(CultureInfo.InvariantCulture, "{0:0.000}%", pct)
            };
        }

        private static string Formatear(double v)
        {
            if (double.IsNaN(v) || double.IsInfinity(v)) return "-";
            return v.ToString("0.000", CultureInfo.InvariantCulture);
        }

        // Construcción de las gráficas
        private void DibujarGraficas()
        {
            if (simulador == null) return;

            // Tomamos muestras del simulador (que contiene la trayectoria completa).
            List<Muestra> s = simulador.Muestras;
            if (s == null || s.Count < 1) return;

            // Construir listas de puntos y marcadores
            List<PointF> ptsYt = new List<PointF>(s.Count);
            List<PointF> ptsXt = new List<PointF>(s.Count);
            List<PointF> ptsYx = new List<PointF>(s.Count);
            List<PointF> ptsVx = new List<PointF>(s.Count);
            List<PointF> ptsVy = new List<PointF>(s.Count);
            List<PointF> ptsVmag = new List<PointF>(s.Count);
            List<PointF> ptsTheta = new List<PointF>(s.Count);

            foreach (var m in s)
            {
                ptsYt.Add(new PointF((float)m.Tiempo, (float)m.Y));
                ptsXt.Add(new PointF((float)m.Tiempo, (float)m.X));
                ptsYx.Add(new PointF((float)m.X, (float)m.Y));
                ptsVx.Add(new PointF((float)m.Tiempo, (float)m.Vx));
                ptsVy.Add(new PointF((float)m.Tiempo, (float)m.Vy));
                ptsVmag.Add(new PointF((float)m.Tiempo, (float)m.MagnitudVelocidad));
                ptsTheta.Add(new PointF((float)m.Tiempo, (float)m.AnguloVelocidad));
            }

            // Marcadores: altura máxima (verde), colisiones (rojo / naranja), impactos finales (rojo oscuro)
            List<MarcadorGrafica> makersYt = new List<MarcadorGrafica>();
            List<MarcadorGrafica> makersXt = new List<MarcadorGrafica>();
            List<MarcadorGrafica> makersYx = new List<MarcadorGrafica>();
            List<MarcadorGrafica> makersVx = new List<MarcadorGrafica>();
            List<MarcadorGrafica> makersVy = new List<MarcadorGrafica>();
            List<MarcadorGrafica> makersVmag = new List<MarcadorGrafica>();
            List<MarcadorGrafica> makersTheta = new List<MarcadorGrafica>();

            // Altura máxima
            makersYt.Add(new MarcadorGrafica(simulador.TiempoAlturaMaximaSim, simulador.AlturaMaximaSim,
                "H.max", Color.LimeGreen));
            makersXt.Add(new MarcadorGrafica(simulador.TiempoAlturaMaximaSim, simulador.XAlturaMaximaSim,
                "H.max", Color.LimeGreen));
            makersYx.Add(new MarcadorGrafica(simulador.XAlturaMaximaSim, simulador.AlturaMaximaSim,
                "H.max", Color.LimeGreen));
            makersVx.Add(new MarcadorGrafica(simulador.TiempoAlturaMaximaSim,
                simulador.VelocidadEnAlturaMaxima != null ? simulador.VelocidadEnAlturaMaxima.Vx : 0,
                "H.max", Color.LimeGreen));
            makersVy.Add(new MarcadorGrafica(simulador.TiempoAlturaMaximaSim,
                simulador.VelocidadEnAlturaMaxima != null ? simulador.VelocidadEnAlturaMaxima.Vy : 0,
                "H.max", Color.LimeGreen));
            makersVmag.Add(new MarcadorGrafica(simulador.TiempoAlturaMaximaSim,
                simulador.VelocidadEnAlturaMaxima != null ? simulador.VelocidadEnAlturaMaxima.MagnitudVelocidad : 0,
                "H.max", Color.LimeGreen));
            makersTheta.Add(new MarcadorGrafica(simulador.TiempoAlturaMaximaSim,
                simulador.VelocidadEnAlturaMaxima != null ? simulador.VelocidadEnAlturaMaxima.AnguloVelocidad : 0,
                "H.max", Color.LimeGreen));

            // Colisiones
            foreach (var c in simulador.Colisiones)
            {
                Color color = Color.DarkRed;
                string etiqueta = string.Format("{0} #{1}", c.Tipo, c.Numero);
                makersYt.Add(new MarcadorGrafica(c.Tiempo, c.Y, etiqueta, color));
                makersXt.Add(new MarcadorGrafica(c.Tiempo, c.X, etiqueta, color));
                makersYx.Add(new MarcadorGrafica(c.X, c.Y, etiqueta, color));
                makersVx.Add(new MarcadorGrafica(c.Tiempo, c.VxAntes, etiqueta, color));
                makersVy.Add(new MarcadorGrafica(c.Tiempo, c.VyAntes, etiqueta, color));
                makersVmag.Add(new MarcadorGrafica(c.Tiempo, c.MagnitudAntes, etiqueta, color));
                makersTheta.Add(new MarcadorGrafica(c.Tiempo, c.AnguloAntes, etiqueta, color));
            }

            graficaYt.Puntos = ptsYt;
            graficaYt.Marcadores = makersYt;
            graficaXt.Puntos = ptsXt;
            graficaXt.Marcadores = makersXt;
            graficaYx.Puntos = ptsYx;
            graficaYx.Marcadores = makersYx;
            graficaVx.Puntos = ptsVx;
            graficaVx.Marcadores = makersVx;
            graficaVy.Puntos = ptsVy;
            graficaVy.Marcadores = makersVy;
            graficaVmag.Puntos = ptsVmag;
            graficaVmag.Marcadores = makersVmag;
            graficaTheta.Puntos = ptsTheta;
            graficaTheta.Marcadores = makersTheta;

            graficaYt.Invalidate();
            graficaXt.Invalidate();
            graficaYx.Invalidate();
            graficaVx.Invalidate();
            graficaVy.Invalidate();
            graficaVmag.Invalidate();
            graficaTheta.Invalidate();

            // Cambia automáticamente a la pestaña de gráficas para mostrar el resultado
            tabControl.SelectedIndex = 3;
        }

        private void PrepararAnimacion()
        {
            if (simulador == null) return;

            // El simulador ya calcula los límites del mundo físico en Iniciar();
            // los usamos para el escalado del panel de animación.
            mundoXMax = simulador.MundoXMax;
            mundoYMax = simulador.MundoYMax;
        }

        private void panelAnimacion_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(panelAnimacion.BackColor);

            if (simulador == null)
            {
                string msg = "Sin simulación en curso. Configure los parámetros y presione Iniciar.";
                using (Font f = new Font("Segoe UI", 11F, FontStyle.Italic))
                using (Brush b = new SolidBrush(Color.Gray))
                {
                    SizeF tam = g.MeasureString(msg, f);
                    g.DrawString(msg, f, b,
                        (panelAnimacion.Width - tam.Width) / 2,
                        (panelAnimacion.Height - tam.Height) / 2);
                }
                return;
            }

            float padding = 40f;
            float origenX = padding;
            float origenY = panelAnimacion.Height - padding;
            float anchoDisponible = panelAnimacion.Width - 2 * padding;
            float altoDisponible = panelAnimacion.Height - 2 * padding;

            double mundoAncho = mundoXMax;
            double mundoAlto = mundoYMax;
            if (mundoAncho <= 0) mundoAncho = 1;
            if (mundoAlto <= 0) mundoAlto = 1;

            float escalaX = anchoDisponible / (float)mundoAncho;
            float escalaY = altoDisponible / (float)mundoAlto;
            float escala = Math.Min(escalaX, escalaY); // preservamos coherencia

            float offsetXExtra = (anchoDisponible - escala * (float)mundoAncho) / 2f;
            float offsetYExtra = (altoDisponible - escala * (float)mundoAlto) / 2f;
            pixPorMetroX = escala;
            pixPorMetroY = escala;
            origenXpx = origenX + offsetXExtra;
            origenYpx = origenY - offsetYExtra;

            using (Pen penEje = new Pen(Color.Black, 1.2f))
            using (Pen penGrilla = new Pen(Color.LightGray, 0.5f))
            using (Brush brushTxt = new SolidBrush(Color.Black))
            using (Font fTxt = new Font("Segoe UI", 8F))
            {
                // Suelo
                g.DrawLine(penEje, origenXpx + offsetXExtra - 5, origenYpx,
                    origenXpx + offsetXExtra + anchoDisponible + 5, origenYpx);

                // Eje Y
                g.DrawLine(penEje, origenXpx, origenYpx - altoDisponible - 5 + offsetYExtra,
                    origenXpx, origenYpx + 5);

                // Tics y grilla cada cierta cantidad de metros
                int stepMetros = ElegirPasoSeparacion(mundoAncho, mundoAlto);

                for (int xm = 0; xm <= mundoAncho; xm += stepMetros)
                {
                    float px = origenXpx + offsetXExtra + xm * escala;
                    g.DrawLine(penGrilla, px, origenYpx - altoDisponible + offsetYExtra, px, origenYpx);
                    g.DrawLine(penEje, px, origenYpx - 4, px, origenYpx + 4);
                    g.DrawString(xm + " m", fTxt, brushTxt, px - 10, origenYpx + 6);
                }
                for (int ym = 0; ym <= mundoAlto; ym += stepMetros)
                {
                    float py = origenYpx - ym * escala;
                    g.DrawLine(penGrilla, origenXpx + offsetXExtra, py,
                        origenXpx + offsetXExtra + anchoDisponible, py);
                    g.DrawLine(penEje, origenXpx - 4, py, origenXpx + 4, py);
                    g.DrawString(ym + " m", fTxt, brushTxt, origenXpx - 30, py - 7);
                }

                // Etiquetas de ejes
                using (Font fEje = new Font("Segoe UI", 9F, FontStyle.Bold))
                {
                    g.DrawString("x (m)", fEje, brushTxt,
                        origenXpx + offsetXExtra + anchoDisponible - 30,
                        origenYpx + 18);
                    g.DrawString("y (m)", fEje, brushTxt,
                        origenXpx - 40,
                        origenYpx - altoDisponible + offsetYExtra - 5);
                }
            }

            // 4 paredes del mundo físico (marco de referencia sutil sobre la grilla).
            float worldXMinPx = origenXpx + offsetXExtra;
            float worldXMaxPx = origenXpx + offsetXExtra + (float)mundoXMax * escala;
            float worldYMinPx = origenYpx;
            float worldYMaxPx = origenYpx - (float)mundoYMax * escala;
            using (Pen penBounds = new Pen(Color.LightSlateGray, 1f))
            {
                penBounds.DashStyle = DashStyle.Dash;
                // Techo
                g.DrawLine(penBounds, worldXMinPx, worldYMaxPx, worldXMaxPx, worldYMaxPx);
                // Suelo
                g.DrawLine(penBounds, worldXMinPx, worldYMinPx, worldXMaxPx, worldYMinPx);
                // Izquierda
                g.DrawLine(penBounds, worldXMinPx, worldYMinPx, worldXMinPx, worldYMaxPx);
                // Derecha
                g.DrawLine(penBounds, worldXMaxPx, worldYMinPx, worldXMaxPx, worldYMaxPx);
            }

            // Trayectoria (curva)
            if (trayectoriaParaDibujar.Count > 1)
            {
                using (Pen penTray = new Pen(Color.FromArgb(120, 0, 90, 200), 1.5f))
                {
                    PointF[] pixTray = new PointF[trayectoriaParaDibujar.Count];
                    for (int i = 0; i < trayectoriaParaDibujar.Count; i++)
                    {
                        var p = trayectoriaParaDibujar[i];
                        pixTray[i] = new PointF(
                            origenXpx + p.X * escala,
                            origenYpx - p.Y * escala);
                    }
                    g.DrawLines(penTray, pixTray);
                }
            }

            // Proyectil (estado actual)
            if (ultimaMuestra != null)
            {
                float px = origenXpx + (float)ultimaMuestra.X * escala;
                float py = origenYpx - (float)ultimaMuestra.Y * escala;
                float r = 10f;
                using (Brush brY = new SolidBrush(Color.FromArgb(220, 30, 30)))
                using (Pen penY = new Pen(Color.Black, 1f))
                {
                    g.FillEllipse(brY, px - r, py - r, 2 * r, 2 * r);
                    g.DrawEllipse(penY, px - r, py - r, 2 * r, 2 * r);
                }
                // Vector velocidad
                double mag = ultimaMuestra.MagnitudVelocidad;
                if (mag > 1e-6)
                {
                    double escalaV = (Math.Min(50, mag) / mag) * escala * 0.5;
                    float fx = px + (float)(ultimaMuestra.Vx * escalaV);
                    float fy = py - (float)(ultimaMuestra.Vy * escalaV);
                    using (Pen penV = new Pen(Color.DarkGreen, 2f))
                    {
                        penV.EndCap = LineCap.ArrowAnchor;
                        g.DrawLine(penV, px, py, fx, fy);
                    }
                }
            }
        }

        private static int ElegirPasoSeparacion(double mundoAncho, double mundoAlto)
        {
            double maxDim = Math.Max(mundoAncho, mundoAlto);
            if (maxDim <= 10) return 1;
            if (maxDim <= 25) return 2;
            if (maxDim <= 50) return 5;
            if (maxDim <= 100) return 10;
            if (maxDim <= 250) return 25;
            if (maxDim <= 500) return 50;
            return 100;
        }
    }
}