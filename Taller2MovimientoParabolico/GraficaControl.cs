using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;

namespace Taller2MovimientoParabolico
{
    /// <summary>
    /// Marcador que se dibuja encima de la curva (puntos de máximo, impacto, rebote...).
    /// </summary>
    public class MarcadorGrafica
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Etiqueta { get; set; }
        public Color Color { get; set; }
        public bool EscribirEtiqueta { get; set; }

        public MarcadorGrafica() { }

        public MarcadorGrafica(double x, double y, string etiqueta, Color color)
        {
            X = x; Y = y; Etiqueta = etiqueta; Color = color; EscribirEtiqueta = true;
        }
    }

    /// <summary>
    /// Control personalizado que dibuja una gráfica 2D a partir de una lista de
    /// puntos (X,Y) usando GDI+. Dibuja ejes con marcas, etiquetas con unidades,
    /// título, la curva y marcadores opcionales para eventos (altura máxima,
    /// impactos, rebotes...).
    /// </summary>
    public class GraficaControl : Control
    {
        private const int MargenIzq = 70;
        private const int MargenDer = 20;
        private const int MargenSup = 50;
        private const int MargenInf = 55;

        private static readonly Color ColorEjes = Color.Black;
        private static readonly Color ColorGrilla = Color.LightGray;
        private static readonly Color ColorCurva = Color.FromArgb(0, 90, 200);
        private static readonly Color ColorTexto = Color.Black;
        private static readonly Font FuenteTitulo = new Font("Segoe UI", 10F, FontStyle.Bold);
        private static readonly Font FuenteEjes = new Font("Segoe UI", 8F);
        private static readonly Font FuenteMarcador = new Font("Segoe UI", 7F, FontStyle.Bold);

        public string Titulo { get; set; }
        public string EtiquetaX { get; set; }
        public string UnidadX { get; set; }
        public string EtiquetaY { get; set; }
        public string UnidadY { get; set; }
        public List<PointF> Puntos { get; set; }
        public List<MarcadorGrafica> Marcadores { get; set; }
        public Color ColorCurvaPersonalizado { get; set; }

        public GraficaControl()
        {
            // No se llama a SetStyle aquí para no interferir con la instanciación
            // del control durante la carga del Diseñador de Windows Forms.
            // Los estilos se aplican cuando se crea el handle (ver OnHandleCreated).

            BackColor = Color.White;
            Titulo = "Gráfica";
            EtiquetaX = "x";
            UnidadX = "";
            EtiquetaY = "y";
            UnidadY = "";
            Puntos = new List<PointF>();
            Marcadores = new List<MarcadorGrafica>();
            ColorCurvaPersonalizado = ColorCurva;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(BackColor);

            Rectangle areaCliente = ClientRectangle;
            Rectangle areaPlot = new Rectangle(
                areaCliente.Left + MargenIzq,
                areaCliente.Top + MargenSup,
                Math.Max(10, areaCliente.Width - MargenIzq - MargenDer),
                Math.Max(10, areaCliente.Height - MargenSup - MargenInf));

            if (areaPlot.Width < 20 || areaPlot.Height < 20) return;

            using (Pen penEje = new Pen(ColorEjes, 1.2f))
            using (Pen penGrilla = new Pen(ColorGrilla, 0.5f))
            using (Brush brushTexto = new SolidBrush(ColorTexto))
            using (Brush brushCurva = new SolidBrush(ColorCurvaPersonalizado))
            using (Pen penCurva = new Pen(ColorCurvaPersonalizado, 1.6f))
            {
                // Título
                if (!string.IsNullOrEmpty(Titulo))
                {
                    SizeF tamTitulo = g.MeasureString(Titulo, FuenteTitulo);
                    g.DrawString(Titulo, FuenteTitulo, brushTexto,
                        areaCliente.Left + (areaCliente.Width - tamTitulo.Width) / 2,
                        areaCliente.Top + 8);
                }

                if (Puntos.Count == 0)
                {
                    string msg = "Sin datos";
                    SizeF tamMsg = g.MeasureString(msg, FuenteEjes);
                    g.DrawString(msg, FuenteEjes, brushTexto,
                        areaPlot.Left + (areaPlot.Width - tamMsg.Width) / 2,
                        areaPlot.Top + (areaPlot.Height - tamMsg.Height) / 2);
                    DibujarEtiquetasEjes(g, areaPlot, brushTexto);
                    return;
                }

                // Calcular rangos con un pequeño padding.
                float minX = float.PositiveInfinity, maxX = float.NegativeInfinity;
                float minY = float.PositiveInfinity, maxY = float.NegativeInfinity;
                foreach (var p in Puntos)
                {
                    if (p.X < minX) minX = p.X;
                    if (p.X > maxX) maxX = p.X;
                    if (p.Y < minY) minY = p.Y;
                    if (p.Y > maxY) maxY = p.Y;
                }
                foreach (var m in Marcadores)
                {
                    if (m.X < minX) minX = (float)m.X;
                    if (m.X > maxX) maxX = (float)m.X;
                    if (m.Y < minY) minY = (float)m.Y;
                    if (m.Y > maxY) maxY = (float)m.Y;
                }

                if (Math.Abs(maxX - minX) < 1e-6f) { maxX += 1; minX -= 0; }
                if (Math.Abs(maxY - minY) < 1e-6f) { maxY += 1; minY -= 0; }

                double padX = (maxX - minX) * 0.05;
                double padY = (maxY - minY) * 0.08;
                minX -= (float)padX;
                maxX += (float)padX;
                minY -= (float)padY;
                maxY += (float)padY;

                // Dibujar grilla y marcas.
                int numTicksX = 6;
                int numTicksY = 6;
                DibujarGrilla(g, areaPlot, penGrilla, brushTexto,
                    minX, maxX, minY, maxY, numTicksX, numTicksY);

                // Bordes del área de plot.
                g.DrawRectangle(penEje, areaPlot);

                // Transformaciones: X horizontal, Y invertido (hacia arriba).
                float escalaX = areaPlot.Width / (maxX - minX);
                float escalaY = areaPlot.Height / (maxY - minY);

                PointF Transformar(PointF p)
                {
                    float px = areaPlot.Left + (p.X - minX) * escalaX;
                    float py = areaPlot.Top + (maxY - p.Y) * escalaY;
                    return new PointF(px, py);
                }

                // Curva
                if (Puntos.Count == 1)
                {
                    PointF p0 = Transformar(Puntos[0]);
                    g.FillEllipse(brushCurva, p0.X - 2, p0.Y - 2, 4, 4);
                }
                else if (Puntos.Count > 1)
                {
                    // Subsample si hay demasiados puntos para no degradar la performance.
                    int step = 1;
                    if (Puntos.Count > 4000)
                        step = (Puntos.Count + 3999) / 4000;

                    List<PointF> render = new List<PointF>();
                    for (int i = 0; i < Puntos.Count; i += step)
                        render.Add(Transformar(Puntos[i]));
                    if ((Puntos.Count - 1) % step != 0)
                        render.Add(Transformar(Puntos[Puntos.Count - 1]));

                    g.DrawLines(penCurva, render.ToArray());
                }

                // Marcadores
                foreach (var m in Marcadores)
                {
                    PointF p = Transformar(new PointF((float)m.X, (float)m.Y));
                    using (Brush b = new SolidBrush(m.Color))
                    using (Pen penM = new Pen(Color.Black, 0.8f))
                    {
                        g.FillEllipse(b, p.X - 4, p.Y - 4, 8, 8);
                        g.DrawEllipse(penM, p.X - 4, p.Y - 4, 8, 8);

                        if (m.EscribirEtiqueta && !string.IsNullOrEmpty(m.Etiqueta))
                        {
                            SizeF tam = g.MeasureString(m.Etiqueta, FuenteMarcador);
                            float tx = p.X + 6;
                            float ty = p.Y - tam.Height - 2;
                            if (tx + tam.Width > areaPlot.Right) tx = p.X - tam.Width - 6;
                            if (ty < areaPlot.Top) ty = p.Y + 6;
                            g.FillRectangle(new SolidBrush(Color.FromArgb(220, Color.White)), tx - 2, ty - 1, tam.Width + 4, tam.Height + 2);
                            g.DrawString(m.Etiqueta, FuenteMarcador, b, tx, ty);
                        }
                    }
                }

                DibujarEtiquetasEjes(g, areaPlot, brushTexto);
            }
        }

        private void DibujarGrilla(Graphics g, Rectangle area,
                                   Pen penGrilla, Brush brushTexto,
                                   float minX, float maxX, float minY, float maxY,
                                   int numTicksX, int numTicksY)
        {
            // Tics X
            for (int i = 0; i <= numTicksX; i++)
            {
                float t = i / (float)numTicksX;
                float x = area.Left + t * area.Width;
                float valor = minX + t * (maxX - minX);
                g.DrawLine(penGrilla, x, area.Top, x, area.Bottom);
                string s = FormatearNumero(valor);
                SizeF tamStr = g.MeasureString(s, FuenteEjes);
                g.DrawString(s, FuenteEjes, brushTexto,
                    x - tamStr.Width / 2, area.Bottom + 3);
            }
            // Tics Y
            for (int i = 0; i <= numTicksY; i++)
            {
                float t = i / (float)numTicksY;
                float y = area.Top + t * area.Height;
                float valor = maxY - t * (maxY - minY);
                g.DrawLine(penGrilla, area.Left, y, area.Right, y);
                string s = FormatearNumero(valor);
                SizeF tamStr = g.MeasureString(s, FuenteEjes);
                g.DrawString(s, FuenteEjes, brushTexto,
                    area.Left - tamStr.Width - 4, y - tamStr.Height / 2);
            }
        }

        private void DibujarEtiquetasEjes(Graphics g, Rectangle area, Brush brushTexto)
        {
            // Etiqueta X
            if (!string.IsNullOrEmpty(EtiquetaX))
            {
                string sx = string.IsNullOrEmpty(UnidadX)
                    ? EtiquetaX
                    : string.Format("{0} [{1}]", EtiquetaX, UnidadX);
                SizeF tamX = g.MeasureString(sx, FuenteEjes);
                g.DrawString(sx, FuenteEjes, brushTexto,
                    area.Left + (area.Width - tamX.Width) / 2,
                    area.Bottom + 22);
            }
            // Etiqueta Y (rotada)
            if (!string.IsNullOrEmpty(EtiquetaY))
            {
                string sy = string.IsNullOrEmpty(UnidadY)
                    ? EtiquetaY
                    : string.Format("{0} [{1}]", EtiquetaY, UnidadY);
                GraphicsState estado = g.Save();
                g.TranslateTransform(15, area.Top + area.Height / 2);
                g.RotateTransform(-90);
                SizeF tamY = g.MeasureString(sy, FuenteEjes);
                g.DrawString(sy, FuenteEjes, brushTexto, -tamY.Width / 2, 0);
                g.Restore(estado);
            }
        }

        private static string FormatearNumero(float v)
        {
            if (float.IsNaN(v) || float.IsInfinity(v)) return "-";
            float abs = Math.Abs(v);
            if (abs >= 1000f || (abs > 0 && abs < 0.01f))
                return v.ToString("E2", CultureInfo.InvariantCulture);
            return v.ToString("0.##", CultureInfo.InvariantCulture);
        }
    }
}