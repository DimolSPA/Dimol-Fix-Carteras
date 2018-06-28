using System;
using System.Collections.Generic;

namespace Dimol.Carteras.dto
{
    public class CargaSuceso
    {
        public string RutaDirectorio { get; set; }
        public List<string> ListaArchivos { get; set; }

        public List<SucesoArchivoInteresModel> ListaSucesoArchivoInteres { get; set; }
        public List<SucesoArchivoReajusteModel> ListaSucesoArchivoReajuste { get; set; }

        public CargaSuceso()
        {
            ListaArchivos = new List<string>();
            ListaSucesoArchivoInteres = new List<SucesoArchivoInteresModel>();
            ListaSucesoArchivoReajuste = new List<SucesoArchivoReajusteModel>();
        }
    }

    public class SucesoArchivoInteresModel
    {
        public DateTime FechaPago { get; set; }
        public DateTime FechaDocumento { get; set; }
        public decimal TasaInteres { get; set; }
    }

    public class SucesoArchivoReajusteModel
    {
        public DateTime FechaPago { get; set; }
        public string Periodo { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        public decimal Reajuste { get; set; }
    }
}