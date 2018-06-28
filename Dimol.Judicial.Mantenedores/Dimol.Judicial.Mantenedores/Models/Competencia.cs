namespace Dimol.Judicial.Mantenedores.Models
{
    public static class Competencia
    {
        public static int obtenerCompetenciaIdPorTipoCompetencia(string tipoCompetencia) {
            int idCompetencia = 0;

            switch (tipoCompetencia)
            {
                case "P":
                    idCompetencia = (int)Enums.Competencia.Cobranza;
                    break;
                default:
                    idCompetencia = (int)Enums.Competencia.Civíl;
                    break;
            }

            return idCompetencia;
        }

        public static string ParametroCompetenciaPorIdCompetencia(int IdCompetencia)
        {
            switch (IdCompetencia)
            {
                case 6:
                    return "P";
                default:
                    return "C";
            }
        }
    }
}