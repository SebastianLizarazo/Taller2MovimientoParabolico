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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabParametros = new System.Windows.Forms.TabPage();
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
            this.chkGenerarObjetivo = new System.Windows.Forms.CheckBox();
            this.lblObjetivoTitulo = new System.Windows.Forms.Label();
            this.lblObjetivo = new System.Windows.Forms.Label();
            this.lblObjetivoHit = new System.Windows.Forms.Label();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.btnPausar = new System.Windows.Forms.Button();
            this.btnReiniciar = new System.Windows.Forms.Button();
            this.btnNuevaSim = new System.Windows.Forms.Button();
            this.gbAnimacion = new System.Windows.Forms.GroupBox();
            this.panelAnimacion = new System.Windows.Forms.Panel();
            this.lblEstadoAnim = new System.Windows.Forms.Label();
            this.lblLeyendaAnim = new System.Windows.Forms.Label();
            this.tabMetricas = new System.Windows.Forms.TabPage();
            this.gbMetricasTiempo = new System.Windows.Forms.GroupBox();
            this.lblTiempoVal = new System.Windows.Forms.Label();
            this.lblXVal = new System.Windows.Forms.Label();
            this.lblYVal = new System.Windows.Forms.Label();
            this.lblVxVal = new System.Windows.Forms.Label();
            this.lblVyVal = new System.Windows.Forms.Label();
            this.lblVmagVal = new System.Windows.Forms.Label();
            this.lblAngVal = new System.Windows.Forms.Label();
            this.tabResultados = new System.Windows.Forms.TabPage();
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
            this.tabGraficas = new System.Windows.Forms.TabPage();
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
            this.tabControl.SuspendLayout();
            this.tabParametros.SuspendLayout();
            this.gbCondiciones.SuspendLayout();
            this.gbSimulacion.SuspendLayout();
            this.gbAnimacion.SuspendLayout();
            this.tabMetricas.SuspendLayout();
            this.gbMetricasTiempo.SuspendLayout();
            this.tabResultados.SuspendLayout();
            this.gbComparacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComparacion)).BeginInit();
            this.gbColisiones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColisiones)).BeginInit();
            this.gbFinales.SuspendLayout();
            this.tabGraficas.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabParametros);
            this.tabControl.Controls.Add(this.tabMetricas);
            this.tabControl.Controls.Add(this.tabResultados);
            this.tabControl.Controls.Add(this.tabGraficas);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1600, 827);
            this.tabControl.TabIndex = 0;
            // 
            // tabParametros
            // 
            this.tabParametros.Controls.Add(this.gbCondiciones);
            this.tabParametros.Controls.Add(this.gbSimulacion);
            this.tabParametros.Controls.Add(this.gbAnimacion);
            this.tabParametros.Location = new System.Drawing.Point(4, 29);
            this.tabParametros.Name = "tabParametros";
            this.tabParametros.Padding = new System.Windows.Forms.Padding(9);
            this.tabParametros.Size = new System.Drawing.Size(1592, 794);
            this.tabParametros.TabIndex = 0;
            this.tabParametros.Text = "Parámetros y Simulación";
            // 
            // gbCondiciones
            // 
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
            this.gbCondiciones.Location = new System.Drawing.Point(9, 9);
            this.gbCondiciones.Name = "gbCondiciones";
            this.gbCondiciones.Size = new System.Drawing.Size(366, 299);
            this.gbCondiciones.TabIndex = 0;
            this.gbCondiciones.TabStop = false;
            this.gbCondiciones.Text = "Condiciones Iniciales";
            // 
            // lblX0
            // 
            this.lblX0.AutoSize = true;
            this.lblX0.Location = new System.Drawing.Point(14, 30);
            this.lblX0.Name = "lblX0";
            this.lblX0.Size = new System.Drawing.Size(300, 20);
            this.lblX0.TabIndex = 0;
            this.lblX0.Text = "Posición horizontal inicial x₀ (m, máx. 100):";
            // 
            // txtX0
            // 
            this.txtX0.Location = new System.Drawing.Point(14, 51);
            this.txtX0.Name = "txtX0";
            this.txtX0.Size = new System.Drawing.Size(331, 27);
            this.txtX0.TabIndex = 1;
            this.txtX0.Text = "0";
            // 
            // lblY0
            // 
            this.lblY0.AutoSize = true;
            this.lblY0.Location = new System.Drawing.Point(14, 83);
            this.lblY0.Name = "lblY0";
            this.lblY0.Size = new System.Drawing.Size(195, 20);
            this.lblY0.TabIndex = 2;
            this.lblY0.Text = "Altura inicial y₀ (m, máx. 50):";
            // 
            // txtY0
            // 
            this.txtY0.Location = new System.Drawing.Point(14, 105);
            this.txtY0.Name = "txtY0";
            this.txtY0.Size = new System.Drawing.Size(331, 27);
            this.txtY0.TabIndex = 3;
            this.txtY0.Text = "10";
            // 
            // lblV0
            // 
            this.lblV0.AutoSize = true;
            this.lblV0.Location = new System.Drawing.Point(14, 137);
            this.lblV0.Name = "lblV0";
            this.lblV0.Size = new System.Drawing.Size(244, 20);
            this.lblV0.TabIndex = 4;
            this.lblV0.Text = "Magnitud velocidad inicial v₀ (m/s):";
            // 
            // txtV0
            // 
            this.txtV0.Location = new System.Drawing.Point(14, 158);
            this.txtV0.Name = "txtV0";
            this.txtV0.Size = new System.Drawing.Size(331, 27);
            this.txtV0.TabIndex = 5;
            this.txtV0.Text = "20";
            // 
            // lblAngulo
            // 
            this.lblAngulo.AutoSize = true;
            this.lblAngulo.Location = new System.Drawing.Point(14, 190);
            this.lblAngulo.Name = "lblAngulo";
            this.lblAngulo.Size = new System.Drawing.Size(200, 20);
            this.lblAngulo.TabIndex = 6;
            this.lblAngulo.Text = "Ángulo de lanzamiento θ (°):";
            // 
            // txtAngulo
            // 
            this.txtAngulo.Location = new System.Drawing.Point(14, 211);
            this.txtAngulo.Name = "txtAngulo";
            this.txtAngulo.Size = new System.Drawing.Size(331, 27);
            this.txtAngulo.TabIndex = 7;
            this.txtAngulo.Text = "45";
            // 
            // lblGravedad
            // 
            this.lblGravedad.AutoSize = true;
            this.lblGravedad.Location = new System.Drawing.Point(14, 243);
            this.lblGravedad.Name = "lblGravedad";
            this.lblGravedad.Size = new System.Drawing.Size(238, 20);
            this.lblGravedad.TabIndex = 8;
            this.lblGravedad.Text = "Aceleración gravitacional g (m/s²):";
            // 
            // txtGravedad
            // 
            this.txtGravedad.Location = new System.Drawing.Point(14, 265);
            this.txtGravedad.Name = "txtGravedad";
            this.txtGravedad.Size = new System.Drawing.Size(331, 27);
            this.txtGravedad.TabIndex = 9;
            this.txtGravedad.Text = "9.81";
            // 
            // gbSimulacion
            // 
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
            this.gbSimulacion.Location = new System.Drawing.Point(9, 316);
            this.gbSimulacion.Name = "gbSimulacion";
            this.gbSimulacion.Size = new System.Drawing.Size(366, 288);
            this.gbSimulacion.TabIndex = 1;
            this.gbSimulacion.TabStop = false;
            this.gbSimulacion.Text = "Simulación";
            // 
            // lblDt
            // 
            this.lblDt.AutoSize = true;
            this.lblDt.Location = new System.Drawing.Point(14, 30);
            this.lblDt.Name = "lblDt";
            this.lblDt.Size = new System.Drawing.Size(174, 20);
            this.lblDt.TabIndex = 0;
            this.lblDt.Text = "Intervalo temporal Δt (s):";
            // 
            // txtDt
            // 
            this.txtDt.Location = new System.Drawing.Point(14, 51);
            this.txtDt.Name = "txtDt";
            this.txtDt.Size = new System.Drawing.Size(137, 27);
            this.txtDt.TabIndex = 1;
            this.txtDt.Text = "0.02";
            // 
            // chkGenerarObjetivo
            // 
            this.chkGenerarObjetivo.AutoSize = true;
            this.chkGenerarObjetivo.Checked = true;
            this.chkGenerarObjetivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGenerarObjetivo.Location = new System.Drawing.Point(171, 53);
            this.chkGenerarObjetivo.Name = "chkGenerarObjetivo";
            this.chkGenerarObjetivo.Size = new System.Drawing.Size(142, 24);
            this.chkGenerarObjetivo.TabIndex = 2;
            this.chkGenerarObjetivo.Text = "Generar objetivo";
            // 
            // lblObjetivoTitulo
            // 
            this.lblObjetivoTitulo.AutoSize = true;
            this.lblObjetivoTitulo.Location = new System.Drawing.Point(14, 85);
            this.lblObjetivoTitulo.Name = "lblObjetivoTitulo";
            this.lblObjetivoTitulo.Size = new System.Drawing.Size(115, 20);
            this.lblObjetivoTitulo.TabIndex = 3;
            this.lblObjetivoTitulo.Text = "Objetivo (y = ?):";
            // 
            // lblObjetivo
            // 
            this.lblObjetivo.AutoSize = true;
            this.lblObjetivo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblObjetivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(60)))), ((int)(((byte)(0)))));
            this.lblObjetivo.Location = new System.Drawing.Point(126, 85);
            this.lblObjetivo.Name = "lblObjetivo";
            this.lblObjetivo.Size = new System.Drawing.Size(109, 20);
            this.lblObjetivo.TabIndex = 4;
            this.lblObjetivo.Text = "(no generado)";
            // 
            // lblObjetivoHit
            // 
            this.lblObjetivoHit.AutoSize = true;
            this.lblObjetivoHit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblObjetivoHit.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblObjetivoHit.Location = new System.Drawing.Point(14, 107);
            this.lblObjetivoHit.Name = "lblObjetivoHit";
            this.lblObjetivoHit.Size = new System.Drawing.Size(0, 20);
            this.lblObjetivoHit.TabIndex = 5;
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(14, 139);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(160, 37);
            this.btnIniciar.TabIndex = 6;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // btnPausar
            // 
            this.btnPausar.Enabled = false;
            this.btnPausar.Location = new System.Drawing.Point(183, 139);
            this.btnPausar.Name = "btnPausar";
            this.btnPausar.Size = new System.Drawing.Size(160, 37);
            this.btnPausar.TabIndex = 7;
            this.btnPausar.Text = "Pausar";
            this.btnPausar.UseVisualStyleBackColor = true;
            this.btnPausar.Click += new System.EventHandler(this.btnPausar_Click);
            // 
            // btnReiniciar
            // 
            this.btnReiniciar.Enabled = false;
            this.btnReiniciar.Location = new System.Drawing.Point(14, 187);
            this.btnReiniciar.Name = "btnReiniciar";
            this.btnReiniciar.Size = new System.Drawing.Size(160, 37);
            this.btnReiniciar.TabIndex = 8;
            this.btnReiniciar.Text = "Reiniciar";
            this.btnReiniciar.UseVisualStyleBackColor = true;
            this.btnReiniciar.Click += new System.EventHandler(this.btnReiniciar_Click);
            // 
            // btnNuevaSim
            // 
            this.btnNuevaSim.Location = new System.Drawing.Point(183, 187);
            this.btnNuevaSim.Name = "btnNuevaSim";
            this.btnNuevaSim.Size = new System.Drawing.Size(160, 37);
            this.btnNuevaSim.TabIndex = 9;
            this.btnNuevaSim.Text = "Nueva simulación";
            this.btnNuevaSim.UseVisualStyleBackColor = true;
            this.btnNuevaSim.Click += new System.EventHandler(this.btnNuevaSim_Click);
            // 
            // gbAnimacion
            // 
            this.gbAnimacion.Controls.Add(this.panelAnimacion);
            this.gbAnimacion.Controls.Add(this.lblEstadoAnim);
            this.gbAnimacion.Controls.Add(this.lblLeyendaAnim);
            this.gbAnimacion.Location = new System.Drawing.Point(384, 9);
            this.gbAnimacion.Name = "gbAnimacion";
            this.gbAnimacion.Size = new System.Drawing.Size(869, 533);
            this.gbAnimacion.TabIndex = 2;
            this.gbAnimacion.TabStop = false;
            this.gbAnimacion.Text = "Animación del movimiento";
            // 
            // panelAnimacion
            // 
            this.panelAnimacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelAnimacion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAnimacion.Location = new System.Drawing.Point(14, 53);
            this.panelAnimacion.Name = "panelAnimacion";
            this.panelAnimacion.Size = new System.Drawing.Size(841, 456);
            this.panelAnimacion.TabIndex = 0;
            this.panelAnimacion.Paint += new System.Windows.Forms.PaintEventHandler(this.panelAnimacion_Paint);
            // 
            // lblEstadoAnim
            // 
            this.lblEstadoAnim.AutoSize = true;
            this.lblEstadoAnim.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEstadoAnim.Location = new System.Drawing.Point(14, 27);
            this.lblEstadoAnim.Name = "lblEstadoAnim";
            this.lblEstadoAnim.Size = new System.Drawing.Size(126, 20);
            this.lblEstadoAnim.TabIndex = 1;
            this.lblEstadoAnim.Text = "Estado: detenido";
            // 
            // lblLeyendaAnim
            // 
            this.lblLeyendaAnim.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLeyendaAnim.AutoSize = true;
            this.lblLeyendaAnim.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblLeyendaAnim.Location = new System.Drawing.Point(514, 32);
            this.lblLeyendaAnim.Name = "lblLeyendaAnim";
            this.lblLeyendaAnim.Size = new System.Drawing.Size(419, 19);
            this.lblLeyendaAnim.TabIndex = 2;
            this.lblLeyendaAnim.Text = "Eje X = metros  |  Eje Y = metros  |  Coordenadas físicas (no píxeles)";
            // 
            // tabMetricas
            // 
            this.tabMetricas.Controls.Add(this.gbMetricasTiempo);
            this.tabMetricas.Location = new System.Drawing.Point(4, 29);
            this.tabMetricas.Name = "tabMetricas";
            this.tabMetricas.Padding = new System.Windows.Forms.Padding(9);
            this.tabMetricas.Size = new System.Drawing.Size(1592, 794);
            this.tabMetricas.TabIndex = 1;
            this.tabMetricas.Text = "Métricas en Tiempo Real";
            // 
            // gbMetricasTiempo
            // 
            this.gbMetricasTiempo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMetricasTiempo.Controls.Add(this.lblTiempoVal);
            this.gbMetricasTiempo.Controls.Add(this.lblXVal);
            this.gbMetricasTiempo.Controls.Add(this.lblYVal);
            this.gbMetricasTiempo.Controls.Add(this.lblVxVal);
            this.gbMetricasTiempo.Controls.Add(this.lblVyVal);
            this.gbMetricasTiempo.Controls.Add(this.lblVmagVal);
            this.gbMetricasTiempo.Controls.Add(this.lblAngVal);
            this.gbMetricasTiempo.Location = new System.Drawing.Point(9, 9);
            this.gbMetricasTiempo.Name = "gbMetricasTiempo";
            this.gbMetricasTiempo.Size = new System.Drawing.Size(1335, 747);
            this.gbMetricasTiempo.TabIndex = 0;
            this.gbMetricasTiempo.TabStop = false;
            this.gbMetricasTiempo.Text = "Variables cinemáticas durante la simulación";
            // 
            // lblTiempoVal
            // 
            this.lblTiempoVal.AutoSize = true;
            this.lblTiempoVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblTiempoVal.Location = new System.Drawing.Point(46, 64);
            this.lblTiempoVal.Name = "lblTiempoVal";
            this.lblTiempoVal.Size = new System.Drawing.Size(113, 32);
            this.lblTiempoVal.TabIndex = 0;
            this.lblTiempoVal.Text = "t = 0.00 s";
            // 
            // lblXVal
            // 
            this.lblXVal.AutoSize = true;
            this.lblXVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblXVal.Location = new System.Drawing.Point(46, 117);
            this.lblXVal.Name = "lblXVal";
            this.lblXVal.Size = new System.Drawing.Size(127, 32);
            this.lblXVal.TabIndex = 1;
            this.lblXVal.Text = "x = 0.00 m";
            // 
            // lblYVal
            // 
            this.lblYVal.AutoSize = true;
            this.lblYVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblYVal.Location = new System.Drawing.Point(46, 171);
            this.lblYVal.Name = "lblYVal";
            this.lblYVal.Size = new System.Drawing.Size(128, 32);
            this.lblYVal.TabIndex = 2;
            this.lblYVal.Text = "y = 0.00 m";
            // 
            // lblVxVal
            // 
            this.lblVxVal.AutoSize = true;
            this.lblVxVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblVxVal.Location = new System.Drawing.Point(46, 235);
            this.lblVxVal.Name = "lblVxVal";
            this.lblVxVal.Size = new System.Drawing.Size(158, 32);
            this.lblVxVal.TabIndex = 3;
            this.lblVxVal.Text = "vx = 0.00 m/s";
            // 
            // lblVyVal
            // 
            this.lblVyVal.AutoSize = true;
            this.lblVyVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblVyVal.Location = new System.Drawing.Point(46, 288);
            this.lblVyVal.Name = "lblVyVal";
            this.lblVyVal.Size = new System.Drawing.Size(159, 32);
            this.lblVyVal.TabIndex = 4;
            this.lblVyVal.Text = "vy = 0.00 m/s";
            // 
            // lblVmagVal
            // 
            this.lblVmagVal.AutoSize = true;
            this.lblVmagVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblVmagVal.Location = new System.Drawing.Point(46, 352);
            this.lblVmagVal.Name = "lblVmagVal";
            this.lblVmagVal.Size = new System.Drawing.Size(159, 32);
            this.lblVmagVal.TabIndex = 5;
            this.lblVmagVal.Text = "|v| = 0.00 m/s";
            // 
            // lblAngVal
            // 
            this.lblAngVal.AutoSize = true;
            this.lblAngVal.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblAngVal.Location = new System.Drawing.Point(46, 405);
            this.lblAngVal.Name = "lblAngVal";
            this.lblAngVal.Size = new System.Drawing.Size(118, 32);
            this.lblAngVal.TabIndex = 6;
            this.lblAngVal.Text = "θ = 0.00 °";
            // 
            // tabResultados
            // 
            this.tabResultados.Controls.Add(this.gbComparacion);
            this.tabResultados.Controls.Add(this.gbColisiones);
            this.tabResultados.Controls.Add(this.gbFinales);
            this.tabResultados.Location = new System.Drawing.Point(4, 29);
            this.tabResultados.Name = "tabResultados";
            this.tabResultados.Padding = new System.Windows.Forms.Padding(9);
            this.tabResultados.Size = new System.Drawing.Size(1592, 794);
            this.tabResultados.TabIndex = 2;
            this.tabResultados.Text = "Resultados y Comparación";
            // 
            // gbComparacion
            // 
            this.gbComparacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbComparacion.Controls.Add(this.dgvComparacion);
            this.gbComparacion.Location = new System.Drawing.Point(9, 9);
            this.gbComparacion.Name = "gbComparacion";
            this.gbComparacion.Size = new System.Drawing.Size(1335, 235);
            this.gbComparacion.TabIndex = 0;
            this.gbComparacion.TabStop = false;
            this.gbComparacion.Text = "Comparación Modelo Teórico vs Simulación";
            // 
            // dgvComparacion
            // 
            this.dgvComparacion.AllowUserToAddRows = false;
            this.dgvComparacion.AllowUserToDeleteRows = false;
            this.dgvComparacion.AllowUserToResizeRows = false;
            this.dgvComparacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvComparacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvComparacion.BackgroundColor = System.Drawing.Color.White;
            this.dgvComparacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComparacion.Location = new System.Drawing.Point(3, 23);
            this.dgvComparacion.Name = "dgvComparacion";
            this.dgvComparacion.ReadOnly = true;
            this.dgvComparacion.RowHeadersVisible = false;
            this.dgvComparacion.RowHeadersWidth = 51;
            this.dgvComparacion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComparacion.Size = new System.Drawing.Size(1329, 209);
            this.dgvComparacion.TabIndex = 0;
            // 
            // gbColisiones
            // 
            this.gbColisiones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbColisiones.Controls.Add(this.dgvColisiones);
            this.gbColisiones.Location = new System.Drawing.Point(9, 252);
            this.gbColisiones.Name = "gbColisiones";
            this.gbColisiones.Size = new System.Drawing.Size(1335, 235);
            this.gbColisiones.TabIndex = 1;
            this.gbColisiones.TabStop = false;
            this.gbColisiones.Text = "Registro de colisiones";
            // 
            // dgvColisiones
            // 
            this.dgvColisiones.AllowUserToAddRows = false;
            this.dgvColisiones.AllowUserToDeleteRows = false;
            this.dgvColisiones.AllowUserToResizeRows = false;
            this.dgvColisiones.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvColisiones.BackgroundColor = System.Drawing.Color.White;
            this.dgvColisiones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColisiones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvColisiones.Location = new System.Drawing.Point(3, 23);
            this.dgvColisiones.Name = "dgvColisiones";
            this.dgvColisiones.ReadOnly = true;
            this.dgvColisiones.RowHeadersVisible = false;
            this.dgvColisiones.RowHeadersWidth = 51;
            this.dgvColisiones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvColisiones.Size = new System.Drawing.Size(1329, 209);
            this.dgvColisiones.TabIndex = 0;
            // 
            // gbFinales
            // 
            this.gbFinales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.gbFinales.Location = new System.Drawing.Point(9, 495);
            this.gbFinales.Name = "gbFinales";
            this.gbFinales.Size = new System.Drawing.Size(1335, 256);
            this.gbFinales.TabIndex = 2;
            this.gbFinales.TabStop = false;
            this.gbFinales.Text = "Resultados finales";
            // 
            // lblFinalTiempoTotalVal
            // 
            this.lblFinalTiempoTotalVal.AutoSize = true;
            this.lblFinalTiempoTotalVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinalTiempoTotalVal.Location = new System.Drawing.Point(34, 32);
            this.lblFinalTiempoTotalVal.Name = "lblFinalTiempoTotalVal";
            this.lblFinalTiempoTotalVal.Size = new System.Drawing.Size(166, 20);
            this.lblFinalTiempoTotalVal.TabIndex = 0;
            this.lblFinalTiempoTotalVal.Text = "Tiempo total de vuelo:";
            // 
            // lblFinalAlturaMaxVal
            // 
            this.lblFinalAlturaMaxVal.AutoSize = true;
            this.lblFinalAlturaMaxVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinalAlturaMaxVal.Location = new System.Drawing.Point(366, 32);
            this.lblFinalAlturaMaxVal.Name = "lblFinalAlturaMaxVal";
            this.lblFinalAlturaMaxVal.Size = new System.Drawing.Size(117, 20);
            this.lblFinalAlturaMaxVal.TabIndex = 1;
            this.lblFinalAlturaMaxVal.Text = "Altura máxima:";
            // 
            // lblFinalXAlturaMaxVal
            // 
            this.lblFinalXAlturaMaxVal.AutoSize = true;
            this.lblFinalXAlturaMaxVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinalXAlturaMaxVal.Location = new System.Drawing.Point(709, 32);
            this.lblFinalXAlturaMaxVal.Name = "lblFinalXAlturaMaxVal";
            this.lblFinalXAlturaMaxVal.Size = new System.Drawing.Size(147, 20);
            this.lblFinalXAlturaMaxVal.TabIndex = 2;
            this.lblFinalXAlturaMaxVal.Text = "x en altura máxima:";
            // 
            // lblFinalAlcanceVal
            // 
            this.lblFinalAlcanceVal.AutoSize = true;
            this.lblFinalAlcanceVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinalAlcanceVal.Location = new System.Drawing.Point(1051, 32);
            this.lblFinalAlcanceVal.Name = "lblFinalAlcanceVal";
            this.lblFinalAlcanceVal.Size = new System.Drawing.Size(142, 20);
            this.lblFinalAlcanceVal.TabIndex = 3;
            this.lblFinalAlcanceVal.Text = "Alcance horizontal:";
            // 
            // lblFinalVxMaxVal
            // 
            this.lblFinalVxMaxVal.AutoSize = true;
            this.lblFinalVxMaxVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinalVxMaxVal.Location = new System.Drawing.Point(34, 96);
            this.lblFinalVxMaxVal.Name = "lblFinalVxMaxVal";
            this.lblFinalVxMaxVal.Size = new System.Drawing.Size(88, 20);
            this.lblFinalVxMaxVal.TabIndex = 4;
            this.lblFinalVxMaxVal.Text = "vx (h.max):";
            // 
            // lblFinalVyMaxVal
            // 
            this.lblFinalVyMaxVal.AutoSize = true;
            this.lblFinalVyMaxVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinalVyMaxVal.Location = new System.Drawing.Point(366, 96);
            this.lblFinalVyMaxVal.Name = "lblFinalVyMaxVal";
            this.lblFinalVyMaxVal.Size = new System.Drawing.Size(88, 20);
            this.lblFinalVyMaxVal.TabIndex = 5;
            this.lblFinalVyMaxVal.Text = "vy (h.max):";
            // 
            // lblFinalVMagMaxVal
            // 
            this.lblFinalVMagMaxVal.AutoSize = true;
            this.lblFinalVMagMaxVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinalVMagMaxVal.Location = new System.Drawing.Point(709, 96);
            this.lblFinalVMagMaxVal.Name = "lblFinalVMagMaxVal";
            this.lblFinalVMagMaxVal.Size = new System.Drawing.Size(90, 20);
            this.lblFinalVMagMaxVal.TabIndex = 6;
            this.lblFinalVMagMaxVal.Text = "|v| (h.max):";
            // 
            // lblFinalAngMaxVal
            // 
            this.lblFinalAngMaxVal.AutoSize = true;
            this.lblFinalAngMaxVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinalAngMaxVal.Location = new System.Drawing.Point(1051, 96);
            this.lblFinalAngMaxVal.Name = "lblFinalAngMaxVal";
            this.lblFinalAngMaxVal.Size = new System.Drawing.Size(81, 20);
            this.lblFinalAngMaxVal.TabIndex = 7;
            this.lblFinalAngMaxVal.Text = "θ (h.max):";
            // 
            // lblFinalVxImpVal
            // 
            this.lblFinalVxImpVal.AutoSize = true;
            this.lblFinalVxImpVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinalVxImpVal.Location = new System.Drawing.Point(34, 160);
            this.lblFinalVxImpVal.Name = "lblFinalVxImpVal";
            this.lblFinalVxImpVal.Size = new System.Drawing.Size(102, 20);
            this.lblFinalVxImpVal.TabIndex = 8;
            this.lblFinalVxImpVal.Text = "vx (impacto):";
            // 
            // lblFinalVyImpVal
            // 
            this.lblFinalVyImpVal.AutoSize = true;
            this.lblFinalVyImpVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinalVyImpVal.Location = new System.Drawing.Point(366, 160);
            this.lblFinalVyImpVal.Name = "lblFinalVyImpVal";
            this.lblFinalVyImpVal.Size = new System.Drawing.Size(102, 20);
            this.lblFinalVyImpVal.TabIndex = 9;
            this.lblFinalVyImpVal.Text = "vy (impacto):";
            // 
            // lblFinalVMagImpVal
            // 
            this.lblFinalVMagImpVal.AutoSize = true;
            this.lblFinalVMagImpVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinalVMagImpVal.Location = new System.Drawing.Point(709, 160);
            this.lblFinalVMagImpVal.Name = "lblFinalVMagImpVal";
            this.lblFinalVMagImpVal.Size = new System.Drawing.Size(104, 20);
            this.lblFinalVMagImpVal.TabIndex = 10;
            this.lblFinalVMagImpVal.Text = "|v| (impacto):";
            // 
            // lblFinalAngImpVal
            // 
            this.lblFinalAngImpVal.AutoSize = true;
            this.lblFinalAngImpVal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFinalAngImpVal.Location = new System.Drawing.Point(1051, 160);
            this.lblFinalAngImpVal.Name = "lblFinalAngImpVal";
            this.lblFinalAngImpVal.Size = new System.Drawing.Size(95, 20);
            this.lblFinalAngImpVal.TabIndex = 11;
            this.lblFinalAngImpVal.Text = "θ (impacto):";
            // 
            // tabGraficas
            // 
            this.tabGraficas.AutoScroll = true;
            this.tabGraficas.Controls.Add(this.graficaYt);
            this.tabGraficas.Controls.Add(this.graficaXt);
            this.tabGraficas.Controls.Add(this.graficaYx);
            this.tabGraficas.Controls.Add(this.graficaVx);
            this.tabGraficas.Controls.Add(this.graficaVy);
            this.tabGraficas.Controls.Add(this.graficaVmag);
            this.tabGraficas.Controls.Add(this.graficaTheta);
            this.tabGraficas.Location = new System.Drawing.Point(4, 29);
            this.tabGraficas.Name = "tabGraficas";
            this.tabGraficas.Padding = new System.Windows.Forms.Padding(9);
            this.tabGraficas.Size = new System.Drawing.Size(1592, 794);
            this.tabGraficas.TabIndex = 3;
            this.tabGraficas.Text = "Gráficas";
            // 
            // graficaYt
            // 
            this.graficaYt.BackColor = System.Drawing.Color.White;
            this.graficaYt.EtiquetaX = "t";
            this.graficaYt.EtiquetaY = "y";
            this.graficaYt.Location = new System.Drawing.Point(9, 9);
            this.graficaYt.Name = "graficaYt";
            this.graficaYt.Size = new System.Drawing.Size(1303, 299);
            this.graficaYt.TabIndex = 0;
            this.graficaYt.Titulo = "Posición vertical y vs tiempo t";
            this.graficaYt.UnidadX = "s";
            this.graficaYt.UnidadY = "m";
            // 
            // graficaXt
            // 
            this.graficaXt.BackColor = System.Drawing.Color.White;
            this.graficaXt.EtiquetaX = "t";
            this.graficaXt.EtiquetaY = "x";
            this.graficaXt.Location = new System.Drawing.Point(9, 320);
            this.graficaXt.Name = "graficaXt";
            this.graficaXt.Size = new System.Drawing.Size(1303, 299);
            this.graficaXt.TabIndex = 1;
            this.graficaXt.Titulo = "Posición horizontal x vs tiempo t";
            this.graficaXt.UnidadX = "s";
            this.graficaXt.UnidadY = "m";
            // 
            // graficaYx
            // 
            this.graficaYx.BackColor = System.Drawing.Color.White;
            this.graficaYx.EtiquetaX = "x";
            this.graficaYx.EtiquetaY = "y";
            this.graficaYx.Location = new System.Drawing.Point(9, 631);
            this.graficaYx.Name = "graficaYx";
            this.graficaYx.Size = new System.Drawing.Size(1303, 299);
            this.graficaYx.TabIndex = 2;
            this.graficaYx.Titulo = "Trayectoria: posición vertical y vs posición horizontal x";
            this.graficaYx.UnidadX = "m";
            this.graficaYx.UnidadY = "m";
            // 
            // graficaVx
            // 
            this.graficaVx.BackColor = System.Drawing.Color.White;
            this.graficaVx.EtiquetaX = "t";
            this.graficaVx.EtiquetaY = "vx";
            this.graficaVx.Location = new System.Drawing.Point(9, 943);
            this.graficaVx.Name = "graficaVx";
            this.graficaVx.Size = new System.Drawing.Size(1303, 299);
            this.graficaVx.TabIndex = 3;
            this.graficaVx.Titulo = "Velocidad horizontal vx vs tiempo t";
            this.graficaVx.UnidadX = "s";
            this.graficaVx.UnidadY = "m/s";
            // 
            // graficaVy
            // 
            this.graficaVy.BackColor = System.Drawing.Color.White;
            this.graficaVy.EtiquetaX = "t";
            this.graficaVy.EtiquetaY = "vy";
            this.graficaVy.Location = new System.Drawing.Point(9, 1254);
            this.graficaVy.Name = "graficaVy";
            this.graficaVy.Size = new System.Drawing.Size(1303, 299);
            this.graficaVy.TabIndex = 4;
            this.graficaVy.Titulo = "Velocidad vertical vy vs tiempo t";
            this.graficaVy.UnidadX = "s";
            this.graficaVy.UnidadY = "m/s";
            // 
            // graficaVmag
            // 
            this.graficaVmag.BackColor = System.Drawing.Color.White;
            this.graficaVmag.EtiquetaX = "t";
            this.graficaVmag.EtiquetaY = "|v|";
            this.graficaVmag.Location = new System.Drawing.Point(9, 1566);
            this.graficaVmag.Name = "graficaVmag";
            this.graficaVmag.Size = new System.Drawing.Size(1303, 299);
            this.graficaVmag.TabIndex = 5;
            this.graficaVmag.Titulo = "Magnitud de la velocidad |v| vs tiempo t";
            this.graficaVmag.UnidadX = "s";
            this.graficaVmag.UnidadY = "m/s";
            // 
            // graficaTheta
            // 
            this.graficaTheta.BackColor = System.Drawing.Color.White;
            this.graficaTheta.EtiquetaX = "t";
            this.graficaTheta.EtiquetaY = "θ";
            this.graficaTheta.Location = new System.Drawing.Point(9, 1877);
            this.graficaTheta.Name = "graficaTheta";
            this.graficaTheta.Size = new System.Drawing.Size(1303, 299);
            this.graficaTheta.TabIndex = 6;
            this.graficaTheta.Titulo = "Ángulo del vector velocidad θ vs tiempo t";
            this.graficaTheta.UnidadX = "s";
            this.graficaTheta.UnidadY = "°";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 827);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip.Size = new System.Drawing.Size(1600, 26);
            this.statusStrip.TabIndex = 1;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(393, 20);
            this.statusLabel.Text = "Listo. Configure las condiciones iniciales y presione Iniciar.";
            // 
            // timerSimulacion
            // 
            this.timerSimulacion.Interval = 20;
            this.timerSimulacion.Tick += new System.EventHandler(this.timerSimulacion_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 853);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.statusStrip);
            this.MinimumSize = new System.Drawing.Size(1168, 744);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulador de Movimiento Parabólico";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl.ResumeLayout(false);
            this.tabParametros.ResumeLayout(false);
            this.gbCondiciones.ResumeLayout(false);
            this.gbCondiciones.PerformLayout();
            this.gbSimulacion.ResumeLayout(false);
            this.gbSimulacion.PerformLayout();
            this.gbAnimacion.ResumeLayout(false);
            this.gbAnimacion.PerformLayout();
            this.tabMetricas.ResumeLayout(false);
            this.gbMetricasTiempo.ResumeLayout(false);
            this.gbMetricasTiempo.PerformLayout();
            this.tabResultados.ResumeLayout(false);
            this.gbComparacion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvComparacion)).EndInit();
            this.gbColisiones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColisiones)).EndInit();
            this.gbFinales.ResumeLayout(false);
            this.gbFinales.PerformLayout();
            this.tabGraficas.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}