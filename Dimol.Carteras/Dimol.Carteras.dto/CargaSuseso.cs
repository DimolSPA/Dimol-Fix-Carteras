using System;
using System.Collections.Generic;

namespace Dimol.Carteras.dto
{
    public class CargaSuseso
    {
        public string RutaDirectorio { get; set; }
        public List<string> ListaArchivos { get; set; }
        public bool Error { get; set; }
        public string ErrorMensaje { get; set; }

        public List<SusesoArchivoInteresModel> ListaSusesoArchivoInteres { get; set; }
        public List<SusesoArchivoReajusteModel> ListaSusesoArchivoReajuste { get; set; }

        public CargaSuseso()
        {
            Error = false;
            ListaArchivos = new List<string>();
            ListaSusesoArchivoInteres = new List<SusesoArchivoInteresModel>();
            ListaSusesoArchivoReajuste = new List<SusesoArchivoReajusteModel>();
        }
    }

    public class SusesoArchivoInteresModel
    {
        public DateTime FechaPago { get; set; }
        public DateTime FechaDocumento { get; set; }
        public decimal TasaInteres { get; set; }
        public string Error { get; set; }
    }

    public class SusesoArchivoReajusteModel
    {
        public DateTime FechaPago { get; set; }
        public string Periodo { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal Reajuste { get; set; }
        public string Error { get; set; }
    }
}