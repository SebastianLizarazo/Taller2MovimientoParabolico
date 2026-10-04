using System;

namespace Taller2MovimientoParabolico
{
    /// <summary>
    /// Representa un objetivo horizontal fijo contra el que puede colisionar el
    /// proyectil durante la simulación. Se modela como una "barra" centrada en
    /// una altura Y, con extensión horizontal [XMin, XMax] y un grosor Alto
    /// usado únicamente para la visualización (la detección de colisión se hace
    /// contra el plano horizontal a la altura Y, como una superficie sin
    /// grosor físico).
    /// </summary>
    public class Objetivo
    {
        /// <summary>Extensión horizontal izquierda del objetivo, en metros.</summary>
        public double XMin { get; private set; }

        /// <summary>Extensión horizontal derecha del objetivo, en metros.</summary>
        public double XMax { get; private set; }

        /// <summary>Coordenada vertical del centro del objetivo, en metros.</summary>
        public double Y { get; private set; }

        /// <summary>Grosor / altura del objetivo, en metros (default razonable 0.2 m).</summary>
        public double Alto { get; private set; }

        /// <summary>Borde vertical inferior del objetivo (Y - Alto/2), en metros.</summary>
        public double YMin { get { return Y - Alto / 2.0; } }

        /// <summary>Borde vertical superior del objetivo (Y + Alto/2), en metros.</summary>
        public double YMax { get { return Y + Alto / 2.0; } }

        /// <summary>
        /// Construye un objetivo horizontal.
        /// </summary>
        /// <param name="xMin">Extensión horizontal izquierda (m).</param>
        /// <param name="xMax">Extensión horizontal derecha (m).</param>
        /// <param name="y">Altura del centro del objetivo (m).</param>
        /// <param name="alto">Grosor del objetivo (m). Default: 0.2 m.</param>
        public Objetivo(double xMin, double xMax, double y, double alto = 0.2)
        {
            if (xMin >= xMax) throw new ArgumentException("xMin debe ser menor que xMax.");
            if (alto <= 0) throw new ArgumentException("alto debe ser positivo.");
            XMin = xMin;
            XMax = xMax;
            Y = y;
            Alto = alto;
        }
    }
}