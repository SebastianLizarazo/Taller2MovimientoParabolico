using System;

namespace Taller2MovimientoParabolico
{
    /// <summary>
    /// Cálculos analíticos para movimiento parabólico bajo gravedad constante,
    /// sin resistencia del aire y sin colisiones. Sirve como referencia para
    /// comparar contra los valores obtenidos por la simulación numérica.
    /// </summary>
    public static class ModeloTeorico
    {
        /// <summary>Componente horizontal de la velocidad inicial.</summary>
        public static double V0x(double v0, double anguloRad)
        {
            return v0 * Math.Cos(anguloRad);
        }

        /// <summary>Componente vertical de la velocidad inicial (positiva hacia arriba).</summary>
        public static double V0y(double v0, double anguloRad)
        {
            return v0 * Math.Sin(anguloRad);
        }

        /// <summary>
        /// Tiempo total de vuelo: tiempo en que y(t) vuelve a ser 0 partiendo de y0.
        /// Fórmula: (v0y + sqrt(v0y² + 2 g y0)) / g.
        /// </summary>
        public static double TiempoTotalVuelo(double y0, double v0y, double g)
        {
            if (g <= 0) return double.PositiveInfinity;
            double disc = v0y * v0y + 2.0 * g * y0;
            if (disc < 0) disc = 0;
            return (v0y + Math.Sqrt(disc)) / g;
        }

        /// <summary>Instante en el que se alcanza la altura máxima (vy = 0).</summary>
        public static double TiempoAlturaMaxima(double v0y, double g)
        {
            if (g <= 0) return 0;
            return v0y / g;
        }

        /// <summary>Altura máxima alcanzada sobre el suelo.</summary>
        public static double AlturaMaxima(double y0, double v0y, double g)
        {
            return y0 + (v0y * v0y) / (2.0 * g);
        }

        /// <summary>Posición horizontal en el instante de altura máxima.</summary>
        public static double XAlturaMaxima(double x0, double v0x, double tHMax)
        {
            return x0 + v0x * tHMax;
        }

        /// <summary>Alcance horizontal total (posición en el momento del impacto).</summary>
        public static double Alcance(double x0, double v0x, double tTotal)
        {
            return x0 + v0x * tTotal;
        }

        /// <summary>Componente horizontal de la velocidad en altura máxima. (Es v0x).</summary>
        public static double VxAlturaMaxima(double v0x)
        {
            return v0x;
        }

        /// <summary>Componente vertical de la velocidad en altura máxima (es 0).</summary>
        public static double VyAlturaMaxima()
        {
            return 0.0;
        }

        /// <summary>Magnitud de la velocidad en altura máxima (igual a |v0x|).</summary>
        public static double MagnitudAlturaMaxima(double v0x)
        {
            return Math.Abs(v0x);
        }

        /// <summary>Ángulo en grados en altura máxima (0° si v0x > 0).</summary>
        public static double AnguloAlturaMaxima(double v0x)
        {
            return Math.Atan2(0.0, v0x) * 180.0 / Math.PI;
        }

        /// <summary>Componente horizontal de la velocidad justo antes del impacto.</summary>
        public static double VxImpacto(double v0x)
        {
            return v0x;
        }

        /// <summary>Componente vertical de la velocidad justo antes del impacto (negativa).</summary>
        public static double VyImpacto(double v0y, double g, double tTotal)
        {
            return v0y - g * tTotal;
        }

        /// <summary>Magnitud de la velocidad justo antes del impacto.</summary>
        public static double MagnitudImpacto(double vx, double vy)
        {
            return Math.Sqrt(vx * vx + vy * vy);
        }

        /// <summary>Ángulo en grados del vector velocidad justo antes del impacto.</summary>
        public static double AnguloImpacto(double vx, double vy)
        {
            return Math.Atan2(vy, vx) * 180.0 / Math.PI;
        }
    }
}