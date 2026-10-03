using System;

namespace Taller2MovimientoParabolico
{
    /// <summary>
    /// Una muestra del estado del proyectil en un instante de la simulación.
    /// Se usa para alimentar las gráficas y para comparar con el modelo teórico.
    /// </summary>
    public class Muestra
    {
        public double Tiempo { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Vx { get; set; }
        public double Vy { get; set; }

        public double MagnitudVelocidad
        {
            get { return Math.Sqrt(Vx * Vx + Vy * Vy); }
        }

        /// <summary>
        /// Ángulo del vector velocidad respecto al eje horizontal positivo, en grados.
        /// Rango: [-180, 180].
        /// </summary>
        public double AnguloVelocidad
        {
            get { return Math.Atan2(Vy, Vx) * 180.0 / Math.PI; }
        }

        public Muestra() { }

        public Muestra(double t, double x, double y, double vx, double vy)
        {
            Tiempo = t;
            X = x;
            Y = y;
            Vx = vx;
            Vy = vy;
        }

        public Muestra Clonar()
        {
            return new Muestra(Tiempo, X, Y, Vx, Vy);
        }
    }
}