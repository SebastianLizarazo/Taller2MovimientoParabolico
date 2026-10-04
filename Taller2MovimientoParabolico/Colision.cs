using System;

namespace Taller2MovimientoParabolico
{
    /// <summary>
    /// Tipo de superficie contra la que colisionó el proyectil.
    /// </summary>
    public enum TipoColision
    {
        Suelo,
        Techo,
        Izquierda,
        Derecha,
        Objetivo
    }

    /// <summary>
    /// Registro de una colisión ocurrida durante la simulación.
    /// Almacena el instante, la posición, y las componentes de la velocidad
    /// antes y después del impacto.
    /// </summary>
    public class Colision
    {
        public TipoColision Tipo { get; set; }

        /// <summary>Número de impacto dentro de la simulación (1, 2, 3...).</summary>
        public int Numero { get; set; }

        public double Tiempo { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public double VxAntes { get; set; }
        public double VyAntes { get; set; }

        public double VxDespues { get; set; }
        public double VyDespues { get; set; }

        public double MagnitudAntes
        {
            get { return Math.Sqrt(VxAntes * VxAntes + VyAntes * VyAntes); }
        }

        public double MagnitudDespues
        {
            get { return Math.Sqrt(VxDespues * VxDespues + VyDespues * VyDespues); }
        }

        /// <summary>Ángulo en grados antes del impacto.</summary>
        public double AnguloAntes
        {
            get { return Math.Atan2(VyAntes, VxAntes) * 180.0 / Math.PI; }
        }

        /// <summary>Ángulo en grados después del impacto.</summary>
        public double AnguloDespues
        {
            get { return Math.Atan2(VyDespues, VxDespues) * 180.0 / Math.PI; }
        }
    }
}