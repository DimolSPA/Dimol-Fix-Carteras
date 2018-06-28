using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJSpider.dto
{
    public class TablaHistorial
    {
        public string Folio { get; set; }
        public string RutaDocumento { get; set; }
        public string Etapa { get; set; }
        public string Tramite { get; set; }
        public string DescTramite { get; set; }
        public DateTime FechaTramite { get; set; }
        public string Foja { get; set; }
        //Folio 	Doc. 	Etapa 	Trámite 	Desc. Trámite 	Fec.Tram 	Foja
    }
}
