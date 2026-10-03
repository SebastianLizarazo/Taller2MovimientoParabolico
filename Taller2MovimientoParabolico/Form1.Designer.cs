namespace Taller2MovimientoParabolico
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabParametros;
        private System.Windows.Forms.TabPage tabMetricas;
        private System.Windows.Forms.TabPage tabResultados;
        private System.Windows.Forms.TabPage tabGraficas;

        // ---- Tab Parámetros y Simulación ----
        private System.Windows.Forms.GroupBox gbCondiciones;
        private System.Windows.Forms.Label lblX0;
        private System.Windows.Forms.TextBox txtX0;
        private System.Windows.Forms.Label lblY0;
        private System.Windows.Forms.TextBox txtY0;
        private System.Windows.Forms.Label lblV0;
        private System.Windows.Forms.TextBox txtV0;
        private System.Windows.Forms.Label lblAngulo;
        private System.Windows.Forms.TextBox txtAngulo;
        private System.Windows.Forms.Label lblGravedad;
        private System.Windows.Forms.TextBox txtGravedad;

        private System.Windows.Forms.GroupBox gbSimulacion;
        private System.Windows.Forms.Label lblDt;
        private System.Windows.Forms.TextBox txtDt;
        private System.Windows.Forms.Label lblObjetivoTitulo;
        private System.Windows.Forms.Label lblObjetivo;
        private System.Windows.Forms.Label lblObjetivoHit;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.Button btnPausar;
        private System.Windows.Forms.Button btnReiniciar;
        private System.Windows.Forms.Button btnNuevaSim;
        private System.Windows.Forms.CheckBox chkGenerarObjetivo;

        private System.Windows.Forms.GroupBox gbAnimacion;
        private System.Windows.Forms.Panel panelAnimacion;
        private System.Windows.Forms.Label lblEstadoAnim;
        private System.Windows.Forms.Label lblLeyendaAnim;

        // ---- Tab Métricas ----
        private System.Windows.Forms.GroupBox gbMetricasTiempo;
        private System.Windows.Forms.Label lblTiempoVal;
        private System.Windows.Forms.Label lblXVal;
        private System.Windows.Forms.Label lblYVal;
        private System.Windows.Forms.Label lblVxVal;
        private System.Windows.Forms.Label lblVyVal;
        private System.Windows.Forms.Label lblVmagVal;
        private System.Windows.Forms.Label lblAngVal;

        // ---- Tab Resultados ----
        private System.Windows.Forms.GroupBox gbComparacion;
        private System.Windows.Forms.DataGridView dgvComparacion;

        private System.Windows.Forms.GroupBox gbColisiones;
        private System.Windows.Forms.DataGridView dgvColisiones;

        private System.Windows.Forms.GroupBox gbFinales;
        private System.Windows.Forms.Label lblFinalTiempoTotalVal;
        private System.Windows.Forms.Label lblFinalAlturaMaxVal;
        private System.Windows.Forms.Label lblFinalXAlturaMaxVal;
        private System.Windows.Forms.Label lblFinalAlcanceVal;
        private System.Windows.Forms.Label lblFinalVxMaxVal;
        private System.Windows.Forms.Label lblFinalVyMaxVal;
        private System.Windows.Forms.Label lblFinalVMagMaxVal;
        private System.Windows.Forms.Label lblFinalAngMaxVal;
        private System.Windows.Forms.Label lblFinalVxImpVal;
        private System.Windows.Forms.Label lblFinalVyImpVal;
        private System.Windows.Forms.Label lblFinalVMagImpVal;
        private System.Windows.Forms.Label lblFinalAngImpVal;

        // ---- Tab Gráficas ----
        private Taller2MovimientoParabolico.GraficaControl graficaYt;
        private Taller2MovimientoParabolico.GraficaControl graficaXt;
        private Taller2MovimientoParabolico.GraficaControl graficaYx;
        private Taller2MovimientoParabolico.GraficaControl graficaVx;
        private Taller2MovimientoParabolico.GraficaControl graficaVy;
        private Taller2MovimientoParabolico.GraficaControl graficaVmag;
        private Taller2MovimientoParabolico.GraficaControl graficaTheta;

        // ---- Status strip ----
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;

        private System.Windows.Forms.Timer timerSimulacion;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();

            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabParametros = new System.Windows.Forms.TabPage();
            this.tabMetricas = new System.Windows.Forms.TabPage();
            this.tabResultados = new System.Windows.Forms.TabPage();
            this.tabGraficas = new System.Windows.Forms.TabPage();

            this.gbCondiciones = new System.Windows.Forms.GroupBox();
            this.lblX0 = new System.Windows.Forms.Label();
            this.txtX0 = new System.Windows.Forms.TextBox();
            this.lblY0 = new System.Windows.Forms.Label();
            this.txtY0 = new System.Windows.Forms.TextBox();
            this.lblV0 = new System.Windows.Forms.Label();
            this.txtV0 = new System.Windows.Forms.TextBox();
            this.lblAngulo = new System.Windows.Forms.Label();
            this.txtAngulo = new System.Windows.Forms.TextBox();
            this.lblGravedad = new System.Windows.Forms.Label();
            this.txtGravedad = new System.Windows.Forms.TextBox();

            this.gbSimulacion = new System.Windows.Forms.GroupBox();
            this.lblDt = new System.Windows.Forms.Label();
            this.txtDt = new System.Windows.Forms.TextBox();
            this.lblObjetivoTitulo = new System.Windows.Forms.Label();
            this.lblObjetivo = new System.Windows.Forms.Label();
            this.lblObjetivoHit = new System.Windows.Forms.Label();
            this.chkGenerarObjetivo = new System.Windows.Forms.CheckBox();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnPausar = new System.Windows.Forms.Button();
            this.btnReiniciar = new System.Windows.Forms.Button();
            this.btnNuevaSim = new System.Windows.Forms.Button();

            this.gbAnimacion = new System.Windows.Forms.GroupBox();
            this.panelAnimacion = new System.Windows.Forms.Panel();
            this.lblEstadoAnim = new System.Windows.Forms.Label();
            this.lblLeyendaAnim = new System.Windows.Forms.Label();

            this.gbMetricasTiempo = new System.Windows.Forms.GroupBox();
            this.lblTiempoVal = new System.Windows.Forms.Label();
            this.lblXVal = new System.Windows.Forms.Label();
            this.lblYVal = new System.Windows.Forms.Label();
            this.lblVxVal = new System.Windows.Forms.Label();
            this.lblVyVal = new System.Windows.Forms.Label();
            this.lblVmagVal = new System.Windows.Forms.Label();
            this.lblAngVal = new System.Windows.Forms.Label();

            this.gbComparacion = new System.Windows.Forms.GroupBox();
            this.dgvComparacion = new System.Windows.Forms.DataGridView();

            this.gbColisiones = new System.Windows.Forms.GroupBox();
            this.dgvColisiones = new System.Windows.Forms.DataGridView();

            this.gbFinales = new System.Windows.Forms.GroupBox();
            this.lblFinalTiempoTotalVal = new System.Windows.Forms.Label();
            this.lblFinalAlturaMaxVal = new System.Windows.Forms.Label();
            this.lblFinalXAlturaMaxVal = new System.Windows.Forms.Label();
            this.lblFinalAlcanceVal = new System.Windows.Forms.Label();
            this.lblFinalVxMaxVal = new System.Windows.Forms.Label();
            this.lblFinalVyMaxVal = new System.Windows.Forms.Label();
            this.lblFinalVMagMaxVal = new System.Windows.Forms.Label();
            this.lblFinalAngMaxVal = new System.Windows.Forms.Label();
            this.lblFinalVxImpVal = new System.Windows.Forms.Label();
            this.lblFinalVyImpVal = new System.Windows.Forms.Label();
            this.lblFinalVMagImpVal = new System.Windows.Forms.Label();
            this.lblFinalAngImpVal = new System.Windows.Forms.Label();

            this.graficaYt = new Taller2MovimientoParabolico.GraficaControl();
            this.graficaXt = new Taller2MovimientoParabolico.GraficaControl();
            this.graficaYx = new Taller2MovimientoParabolico.GraficaControl();
            this.graficaVx = new Taller2MovimientoParabolico.GraficaControl();
            this.graficaVy = new Taller2MovimientoParabolico.GraficaControl();
            this.graficaVmag = new Taller2MovimientoParabolico.GraficaControl();
            this.graficaTheta = new Taller2MovimientoParabolico.GraficaControl();

            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();

            this.timerSimulacion = new System.Windows.Forms.Timer(this.components);

            // tabControl
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl.Controls.Add(this.tabParametros);
            this.tabControl.Controls.Add(this.tabMetricas);
            this.tabControl.Controls.Add(this.tabResultados);
            this.tabControl.Controls.Add(this.tabGraficas);

            // tabParametros
            this.tabParametros.Name = "tabParametros";
            this.tabParametros.Padding = new System.Windows.Forms.Padding(8);
            this.tabParametros.Text = "Parámetros y Simulación";
            this.tabParametros.Controls.Add(this.gbCondiciones);
            this.tabParametros.Controls.Add(this.gbSimulacion);
            this.tabParametros.Controls.Add(this.gbAnimacion);

            // tabMetricas
            this.tabMetricas.Name = "tabMetricas";
            this.tabMetricas.Padding = new System.Windows.Forms.Padding(8);
            this.tabMetricas.Text = "Métricas en Tiempo Real";
            this.tabMetricas.Controls.Add(this.gbMetricasTiempo);

            // tabResultados
            this.tabResultados.Name = "tabResultados";
            this.tabResultados.Padding = new System.Windows.Forms.Padding(8);
            this.tabResultados.Text = "Resultados y Comparación";
            this.tabResultados.Controls.Add(this.gbComparacion);
            this.tabResultados.Controls.Add(this.gbColisiones);
            this.tabResultados.Controls.Add(this.gbFinales);

            // tabGraficas
            this.tabGraficas.Name = "tabGraficas";
            this.tabGraficas.Padding = new System.Windows.Forms.Padding(8);
            this.tabGraficas.Text = "Gráficas";
            this.tabGraficas.AutoScroll = true;
            this.tabGraficas.Controls.Add(this.graficaYt);
            this.tabGraficas.Controls.Add(this.graficaXt);
            this.tabGraficas.Controls.Add(this.graficaYx);
            this.tabGraficas.Controls.Add(this.graficaVx);
            this.tabGraficas.Controls.Add(this.graficaVy);
            this.tabGraficas.Controls.Add(this.graficaVmag);
            this.tabGraficas.Controls.Add(this.graficaTheta);

            // gbCondiciones
            this.gbCondiciones.Location = new System.Drawing.Point(8, 8);
            this.gbCondiciones.Name = "gbCondiciones";
            this.gbCondiciones.Size = new System.Drawing.Size(320, 280);
            this.gbCondiciones.TabIndex = 0;
            this.gbCondiciones.Text = "Condiciones Iniciales";
            this.gbCondiciones.Controls.Add(this.lblX0);
            this.gbCondiciones.Controls.Add(this.txtX0);
            this.gbCondiciones.Controls.Add(this.lblY0);
            this.gbCondiciones.Controls.Add(this.txtY0);
            this.gbCondiciones.Controls.Add(this.lblV0);
            this.gbCondiciones.Controls.Add(this.txtV0);
            this.gbCondiciones.Controls.Add(this.lblAngulo);
            this.gbCondiciones.Controls.Add(this.txtAngulo);
            this.gbCondiciones.Controls.Add(this.lblGravedad);
            this.gbCondiciones.Controls.Add(this.txtGravedad);

            this.lblX0.AutoSize = true;
            this.lblX0.Location = new System.Drawing.Point(12, 28);
            this.lblX0.Text = "Posición horizontal inicial x₀ (m):";
            this.txtX0.Location = new System.Drawing.Point(12, 48);
            this.txtX0.Size = new System.Drawing.Size(290, 23);
            this.txtX0.Text = "0";
            this.txtX0.Name = "txtX0";

            this.lblY0.AutoSize = true;
            this.lblY0.Location = new System.Drawing.Point(12, 78);
            this.lblY0.Text = "Altura inicial y₀ (m):";
            this.txtY0.Location = new System.Drawing.Point(12, 98);
            this.txtY0.Size = new System.Drawing.Size(290, 23);
            this.txtY0.Text = "10";
            this.txtY0.Name = "txtY0";

            this.lblV0.AutoSize = true;
            this.lblV0.Location = new System.Drawing.Point(12, 128);
            this.lblV0.Text = "Magnitud velocidad inicial v₀ (m/s):";
            this.txtV0.Location = new System.Drawing.Point(12, 148);
            this.txtV0.Size = new System.Drawing.Size(290, 23);
            this.txtV0.Text = "20";
            this.txtV0.Name = "txtV0";

            this.lblAngulo.AutoSize = true;
            this.lblAngulo.Location = new System.Drawing.Point(12, 178);
            this.lblAngulo.Text = "Ángulo de lanzamiento θ (°):";
            this.txtAngulo.Location = new System.Drawing.Point(12, 198);
            this.txtAngulo.Size = new System.Drawing.Size(290, 23);
            this.txtAngulo.Text = "45";
            this.txtAngulo.Name = "txtAngulo";

            this.lblGravedad.AutoSize = true;
            this.lblGravedad.Location = new System.Drawing.Point(12, 228);
            this.lblGravedad.Text = "Aceleración gravitacional g (m/s²):";
            this.txtGravedad.Location = new System.Drawing.Point(12, 248);
            this.txtGravedad.Size = new System.Drawing.Size(290, 23);
            this.txtGravedad.Text = "9.81";
            this.txtGravedad.Name = "txtGravedad";

            // gbSimulacion
            this.gbSimulacion.Location = new System.Drawing.Point(8, 296);
            this.gbSimulacion.Name = "gbSimulacion";
            this.gbSimulacion.Size = new System.Drawing.Size(320, 270);
            this.gbSimulacion.TabIndex = 1;
            this.gbSimulacion.Text = "Simulación";
            this.gbSimulacion.Controls.Add(this.lblDt);
            this.gbSimulacion.Controls.Add(this.txtDt);
            this.gbSimulacion.Controls.Add(this.chkGenerarObjetivo);
            this.gbSimulacion.Controls.Add(this.lblObjetivoTitulo);
            this.gbSimulacion.Controls.Add(this.lblObjetivo);
            this.gbSimulacion.Controls.Add(this.lblObjetivoHit);
            this.gbSimulacion.Controls.Add(this.btnIniciar);
            this.gbSimulacion.Controls.Add(this.btnPausar);
            this.gbSimulacion.Controls.Add(this.btnReiniciar);
            this.gbSimulacion.Controls.Add(this.btnNuevaSim);

            this.lblDt.AutoSize = true;
            this.lblDt.Location = new System.Drawing.Point(12, 28);
            this.lblDt.Text = "Intervalo temporal Δt (s):";
            this.txtDt.Location = new System.Drawing.Point(12, 48);
            this.txtDt.Size = new System.Drawing.Size(120, 23);
            this.txtDt.Text = "0.02";
            this.txtDt.Name = "txtDt";

            this.chkGenerarObjetivo.AutoSize = true;
            this.chkGenerarObjetivo.Checked = true;
            this.chkGenerarObjetivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGenerarObjetivo.Location = new System.Drawing.Point(150, 50);
            this.chkGenerarObjetivo.Text = "Generar objetivo";
            this.chkGenerarObjetivo.Name = "chkGenerarObjetivo";

            this.lblObjetivoTitulo.AutoSize = true;
            this.lblObjetivoTitulo.Location = new System.Drawing.Point(12, 80);
            this.lblObjetivoTitulo.Text = "Objetivo (y = ?):";
            this.lblObjetivo.AutoSize = true;
            this.lblObjetivo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblObjetivo.ForeColor = System.Drawing.Color.FromArgb(180, 60, 0);
            this.lblObjetivo.Location = new System.Drawing.Point(110, 80);
            this.lblObjetivo.Text = "(no generado)";
            this.lblObjetivoHit.AutoSize = true;
            this.lblObjetivoHit.Location = new System.Drawing.Point(12, 100);
            this.lblObjetivoHit.Text = "";
            this.lblObjetivoHit.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblObjetivoHit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.btnIniciar.Location = new System.Drawing.Point(12, 130);
            this.btnIniciar.Size = new System.Drawing.Size(140, 35);
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);

            this.btnPausar.Location = new System.Drawing.Point(160, 130);
            this.btnPausar.Size = new System.Drawing.Size(140, 35);
            this.btnPausar.Text = "Pausar";
            this.btnPausar.UseVisualStyleBackColor = true;
            this.btnPausar.Enabled = false;
            this.btnPausar.Name = "btnPausar";
            this.btnPausar.Click += new System.EventHandler(this.btnPausar_Click);

            this.btnReiniciar.Location = new System.Drawing.Point(12, 175);
            this.btnReiniciar.Size = new System.Drawing.Size(140, 35);
            this.btnReiniciar.Text = "Reiniciar";
            this.btnReiniciar.UseVisualStyleBackColor = true;
            this.btnReiniciar.Enabled = false;
            this.btnReiniciar.Name = "btnReiniciar";
            this.btnReiniciar.Click += new System.EventHandler(this.btnReiniciar_Click);

            this.btnNuevaSim.Location = new System.Drawing.Point(160, 175);
            this.btnNuevaSim.Size = new System.Drawing.Size(140, 35);
            this.btnNuevaSim.Text = "Nueva simulación";
            this.btnNuevaSim.UseVisualStyleBackColor = true;
            this.btnNuevaSim.Name = "btnNuevaSim";
            this.btnNuevaSim.Click += new System.EventHandler(this.btnNuevaSim_Click);

            // gbAnimacion
            this.gbAnimacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))));
            this.gbAnimacion.Location = new System.Drawing.Point(336, 8);
            this.gbAnimacion.Name = "gbAnimacion";
            this.gbAnimacion.Size = new System.Drawing.Size(760, 500);
            this.gbAnimacion.TabIndex = 2;
            this.gbAnimacion.Text = "Animación del movimiento";
            this.gbAnimacion.Controls.Add(this.panelAnimacion);
            this.gbAnimacion.Controls.Add(this.lblEstadoAnim);
            this.gbAnimacion.Controls.Add(this.lblLeyendaAnim);

            this.panelAnimacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left))));
            this.panelAnimacion.BackColor = System.Drawing.Color.FromArgb(245, 248, 255);
            this.panelAnimacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAnimacion.Location = new System.Drawing.Point(12, 50);
            this.panelAnimacion.Name = "panelAnimacion";
            this.panelAnimacion.Size = new System.Drawing.Size(736, 428);
            this.panelAnimacion.Paint += new System.Windows.Forms.PaintEventHandler(this.panelAnimacion_Paint);

            this.lblEstadoAnim.AutoSize = true;
            this.lblEstadoAnim.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoAnim.Location = new System.Drawing.Point(12, 25);
            this.lblEstadoAnim.Text = "Estado: detenido";
            this.lblEstadoAnim.Name = "lblEstadoAnim";

            this.lblLeyendaAnim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLeyendaAnim.AutoSize = true;
            this.lblLeyendaAnim.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblLeyendaAnim.Location = new System.Drawing.Point(450, 30);
            this.lblLeyendaAnim.Text = "Eje X = metros  |  Eje Y = metros  |  Coordenadas físicas (no píxeles)";
            this.lblLeyendaAnim.Name = "lblLeyendaAnim";

            // gbMetricasTiempo (tab métricas)
            this.gbMetricasTiempo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMetricasTiempo.Location = new System.Drawing.Point(8, 8);
            this.gbMetricasTiempo.Name = "gbMetricasTiempo";
            this.gbMetricasTiempo.Size = new System.Drawing.Size(1168, 700);
            this.gbMetricasTiempo.TabIndex = 0;
            this.gbMetricasTiempo.Text = "Variables cinemáticas durante la simulación";
            this.gbMetricasTiempo.Controls.Add(this.lblTiempoVal);
            this.gbMetricasTiempo.Controls.Add(this.lblXVal);
            this.gbMetricasTiempo.Controls.Add(this.lblYVal);
            this.gbMetricasTiempo.Controls.Add(this.lblVxVal);
            this.gbMetricasTiempo.Controls.Add(this.lblVyVal);
            this.gbMetricasTiempo.Controls.Add(this.lblVmagVal);
            this.gbMetricasTiempo.Controls.Add(this.lblAngVal);

            this.lblTiempoVal.AutoSize = true;
            this.lblTiempoVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblTiempoVal.Location = new System.Drawing.Point(40, 60);
            this.lblTiempoVal.Text = "t = 0.00 s";
            this.lblTiempoVal.Name = "lblTiempoVal";

            this.lblXVal.AutoSize = true;
            this.lblXVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblXVal.Location = new System.Drawing.Point(40, 110);
            this.lblXVal.Text = "x = 0.00 m";
            this.lblXVal.Name = "lblXVal";

            this.lblYVal.AutoSize = true;
            this.lblYVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblYVal.Location = new System.Drawing.Point(40, 160);
            this.lblYVal.Text = "y = 0.00 m";
            this.lblYVal.Name = "lblYVal";

            this.lblVxVal.AutoSize = true;
            this.lblVxVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblVxVal.Location = new System.Drawing.Point(40, 220);
            this.lblVxVal.Text = "vx = 0.00 m/s";
            this.lblVxVal.Name = "lblVxVal";

            this.lblVyVal.AutoSize = true;
            this.lblVyVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblVyVal.Location = new System.Drawing.Point(40, 270);
            this.lblVyVal.Text = "vy = 0.00 m/s";
            this.lblVyVal.Name = "lblVyVal";

            this.lblVmagVal.AutoSize = true;
            this.lblVmagVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblVmagVal.Location = new System.Drawing.Point(40, 330);
            this.lblVmagVal.Text = "|v| = 0.00 m/s";
            this.lblVmagVal.Name = "lblVmagVal";

            this.lblAngVal.AutoSize = true;
            this.lblAngVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblAngVal.Location = new System.Drawing.Point(40, 380);
            this.lblAngVal.Text = "θ = 0.00 °";
            this.lblAngVal.Name = "lblAngVal";

            // gbComparacion
            this.gbComparacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.gbComparacion.Location = new System.Drawing.Point(8, 8);
            this.gbComparacion.Name = "gbComparacion";
            this.gbComparacion.Size = new System.Drawing.Size(1168, 220);
            this.gbComparacion.TabIndex = 0;
            this.gbComparacion.Text = "Comparación Modelo Teórico vs Simulación";
            this.gbComparacion.Controls.Add(this.dgvComparacion);

            this.dgvComparacion.AllowUserToAddRows = false;
            this.dgvComparacion.AllowUserToDeleteRows = false;
            this.dgvComparacion.AllowUserToResizeRows = false;
            this.dgvComparacion.BackgroundColor = System.Drawing.Color.White;
            this.dgvComparacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComparacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComparacion.Location = new System.Drawing.Point(3, 18);
            this.dgvComparacion.Name = "dgvComparacion";
            this.dgvComparacion.ReadOnly = true;
            this.dgvComparacion.RowHeadersVisible = false;
            this.dgvComparacion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComparacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // gbColisiones
            this.gbColisiones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.gbColisiones.Location = new System.Drawing.Point(8, 236);
            this.gbColisiones.Name = "gbColisiones";
            this.gbColisiones.Size = new System.Drawing.Size(1168, 220);
            this.gbColisiones.TabIndex = 1;
            this.gbColisiones.Text = "Registro de colisiones";
            this.gbColisiones.Controls.Add(this.dgvColisiones);

            this.dgvColisiones.AllowUserToAddRows = false;
            this.dgvColisiones.AllowUserToDeleteRows = false;
            this.dgvColisiones.AllowUserToResizeRows = false;
            this.dgvColisiones.BackgroundColor = System.Drawing.Color.White;
            this.dgvColisiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColisiones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColisiones.Location = new System.Drawing.Point(3, 18);
            this.dgvColisiones.Name = "dgvColisiones";
            this.dgvColisiones.ReadOnly = true;
            this.dgvColisiones.RowHeadersVisible = false;
            this.dgvColisiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvColisiones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // gbFinales
            this.gbFinales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFinales.Location = new System.Drawing.Point(8, 464);
            this.gbFinales.Name = "gbFinales";
            this.gbFinales.Size = new System.Drawing.Size(1168, 240);
            this.gbFinales.TabIndex = 2;
            this.gbFinales.Text = "Resultados finales";
            this.gbFinales.Controls.Add(this.lblFinalTiempoTotalVal);
            this.gbFinales.Controls.Add(this.lblFinalAlturaMaxVal);
            this.gbFinales.Controls.Add(this.lblFinalXAlturaMaxVal);
            this.gbFinales.Controls.Add(this.lblFinalAlcanceVal);
            this.gbFinales.Controls.Add(this.lblFinalVxMaxVal);
            this.gbFinales.Controls.Add(this.lblFinalVyMaxVal);
            this.gbFinales.Controls.Add(this.lblFinalVMagMaxVal);
            this.gbFinales.Controls.Add(this.lblFinalAngMaxVal);
            this.gbFinales.Controls.Add(this.lblFinalVxImpVal);
            this.gbFinales.Controls.Add(this.lblFinalVyImpVal);
            this.gbFinales.Controls.Add(this.lblFinalVMagImpVal);
            this.gbFinales.Controls.Add(this.lblFinalAngImpVal);

            // Fila 1: Tiempo total, Altura máx, X altura máx, Alcance
            this.lblFinalTiempoTotalVal.Location = new System.Drawing.Point(30, 30);
            this.lblFinalTiempoTotalVal.AutoSize = true;
            this.lblFinalTiempoTotalVal.Text = "Tiempo total de vuelo:";
            this.lblFinalTiempoTotalVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblFinalAlturaMaxVal.Location = new System.Drawing.Point(320, 30);
            this.lblFinalAlturaMaxVal.AutoSize = true;
            this.lblFinalAlturaMaxVal.Text = "Altura máxima:";
            this.lblFinalAlturaMaxVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblFinalXAlturaMaxVal.Location = new System.Drawing.Point(620, 30);
            this.lblFinalXAlturaMaxVal.AutoSize = true;
            this.lblFinalXAlturaMaxVal.Text = "x en altura máxima:";
            this.lblFinalXAlturaMaxVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblFinalAlcanceVal.Location = new System.Drawing.Point(920, 30);
            this.lblFinalAlcanceVal.AutoSize = true;
            this.lblFinalAlcanceVal.Text = "Alcance horizontal:";
            this.lblFinalAlcanceVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // Los labels de valor se posicionan justo debajo de los títulos con un offset
            // (los agregamos en código al inicializar los textos en blanco).

            // Velocidad en altura máxima (fila 2, y = 90)
            this.lblFinalVxMaxVal.Location = new System.Drawing.Point(30, 90);
            this.lblFinalVxMaxVal.AutoSize = true;
            this.lblFinalVxMaxVal.Text = "vx (h.max):";
            this.lblFinalVxMaxVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblFinalVyMaxVal.Location = new System.Drawing.Point(320, 90);
            this.lblFinalVyMaxVal.AutoSize = true;
            this.lblFinalVyMaxVal.Text = "vy (h.max):";
            this.lblFinalVyMaxVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblFinalVMagMaxVal.Location = new System.Drawing.Point(620, 90);
            this.lblFinalVMagMaxVal.AutoSize = true;
            this.lblFinalVMagMaxVal.Text = "|v| (h.max):";
            this.lblFinalVMagMaxVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblFinalAngMaxVal.Location = new System.Drawing.Point(920, 90);
            this.lblFinalAngMaxVal.AutoSize = true;
            this.lblFinalAngMaxVal.Text = "θ (h.max):";
            this.lblFinalAngMaxVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // Velocidad en impacto (fila 3, y = 150)
            this.lblFinalVxImpVal.Location = new System.Drawing.Point(30, 150);
            this.lblFinalVxImpVal.AutoSize = true;
            this.lblFinalVxImpVal.Text = "vx (impacto):";
            this.lblFinalVxImpVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblFinalVyImpVal.Location = new System.Drawing.Point(320, 150);
            this.lblFinalVyImpVal.AutoSize = true;
            this.lblFinalVyImpVal.Text = "vy (impacto):";
            this.lblFinalVyImpVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblFinalVMagImpVal.Location = new System.Drawing.Point(620, 150);
            this.lblFinalVMagImpVal.AutoSize = true;
            this.lblFinalVMagImpVal.Text = "|v| (impacto):";
            this.lblFinalVMagImpVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            this.lblFinalAngImpVal.Location = new System.Drawing.Point(920, 150);
            this.lblFinalAngImpVal.AutoSize = true;
            this.lblFinalAngImpVal.Text = "θ (impacto):";
            this.lblFinalAngImpVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // ---- Tab Gráficas ----
            // Las 7 gráficas se acomodan en una sola columna con tamaños uniformes.

            this.graficaYt.Location = new System.Drawing.Point(8, 8);
            this.graficaYt.Size = new System.Drawing.Size(1140, 280);
            this.graficaYt.Name = "graficaYt";
            this.graficaYt.Titulo = "Posición vertical y vs tiempo t";
            this.graficaYt.EtiquetaX = "t";
            this.graficaYt.UnidadX = "s";
            this.graficaYt.EtiquetaY = "y";
            this.graficaYt.UnidadY = "m";

            this.graficaXt.Location = new System.Drawing.Point(8, 300);
            this.graficaXt.Size = new System.Drawing.Size(1140, 280);
            this.graficaXt.Name = "graficaXt";
            this.graficaXt.Titulo = "Posición horizontal x vs tiempo t";
            this.graficaXt.EtiquetaX = "t";
            this.graficaXt.UnidadX = "s";
            this.graficaXt.EtiquetaY = "x";
            this.graficaXt.UnidadY = "m";

            this.graficaYx.Location = new System.Drawing.Point(8, 592);
            this.graficaYx.Size = new System.Drawing.Size(1140, 280);
            this.graficaYx.Name = "graficaYx";
            this.graficaYx.Titulo = "Trayectoria: posición vertical y vs posición horizontal x";
            this.graficaYx.EtiquetaX = "x";
            this.graficaYx.UnidadX = "m";
            this.graficaYx.EtiquetaY = "y";
            this.graficaYx.UnidadY = "m";

            this.graficaVx.Location = new System.Drawing.Point(8, 884);
            this.graficaVx.Size = new System.Drawing.Size(1140, 280);
            this.graficaVx.Name = "graficaVx";
            this.graficaVx.Titulo = "Velocidad horizontal vx vs tiempo t";
            this.graficaVx.EtiquetaX = "t";
            this.graficaVx.UnidadX = "s";
            this.graficaVx.EtiquetaY = "vx";
            this.graficaVx.UnidadY = "m/s";

            this.graficaVy.Location = new System.Drawing.Point(8, 1176);
            this.graficaVy.Size = new System.Drawing.Size(1140, 280);
            this.graficaVy.Name = "graficaVy";
            this.graficaVy.Titulo = "Velocidad vertical vy vs tiempo t";
            this.graficaVy.EtiquetaX = "t";
            this.graficaVy.UnidadX = "s";
            this.graficaVy.EtiquetaY = "vy";
            this.graficaVy.UnidadY = "m/s";

            this.graficaVmag.Location = new System.Drawing.Point(8, 1468);
            this.graficaVmag.Size = new System.Drawing.Size(1140, 280);
            this.graficaVmag.Name = "graficaVmag";
            this.graficaVmag.Titulo = "Magnitud de la velocidad |v| vs tiempo t";
            this.graficaVmag.EtiquetaX = "t";
            this.graficaVmag.UnidadX = "s";
            this.graficaVmag.EtiquetaY = "|v|";
            this.graficaVmag.UnidadY = "m/s";

            this.graficaTheta.Location = new System.Drawing.Point(8, 1760);
            this.graficaTheta.Size = new System.Drawing.Size(1140, 280);
            this.graficaTheta.Name = "graficaTheta";
            this.graficaTheta.Titulo = "Ángulo del vector velocidad θ vs tiempo t";
            this.graficaTheta.EtiquetaX = "t";
            this.graficaTheta.UnidadX = "s";
            this.graficaTheta.EtiquetaY = "θ";
            this.graficaTheta.UnidadY = "°";

            // statusStrip
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.statusLabel });
            this.statusStrip.Location = new System.Drawing.Point(0, 720);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1200, 22);

            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Text = "Listo. Configure las condiciones iniciales y presione Iniciar.";

            // timerSimulacion
            this.timerSimulacion.Interval = 20;
            this.timerSimulacion.Tick += new System.EventHandler(this.timerSimulacion_Tick);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.Name = "Form1";
            this.Text = "Simulador de Movimiento Parabólico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.MinimumSize = new System.Drawing.Size(1024, 700);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}