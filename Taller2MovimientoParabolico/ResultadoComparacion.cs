namespace Taller2MovimientoParabolico
{
    /// <summary>
    /// Comparación entre un valor teórico (analítico) y un valor obtenido por la simulación.
    /// </summary>
    public class ResultadoComparacion
    {
        public string Variable { get; set; }
        public string Unidad { get; set; }
        public double ValorTeorico { get; set; }
        public double ValorSimulacion { get; set; }

        public double Diferencia
        {
            get { return ValorSimulacion - ValorTeorico; }
        }

        public double DiferenciaPorcentual
        {
            get
            {
                if (ValorTeorico == 0) return 0;
                return (ValorSimulacion - ValorTeorico) / ValorTeorico * 100.0;
            }
        }

        public ResultadoComparacion() { }

        public ResultadoComparacion(string variable, string unidad, double teorico, double simulacion)
        {
            Variable = variable;
            Unidad = unidad;
            ValorTeorico = teorico;
            ValorSimulacion = simulacion;
        }
    }
}