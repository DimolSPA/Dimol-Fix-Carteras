using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Carteras.dto
{
    public class ClasificacionComprobante
    {
        public string Tipcpbtdoc { get; set; }
        public int Tipprod { get; set; }
        public string Costos { get; set; }
        public string Selcpbt { get; set; }
        public string Cartcli { get; set; }
        public string Contable { get; set; }
        public string Selapl { get; set; }
        public string Aplica { get; set; }
        public string Cptoctbl { get; set; }
        public string Findeuda { get; set; }
        public string Cancela { get; set; }
        public int Libcompra { get; set; }
        public string Cambiodoc { get; set; }
        public string Remesa { get; set; }
        public string Forpag { get; set; }
        public int Tipdig { get; set; }
        public string Ordcomp { get; set; }
        public int Clbid { get; set; }
        public string Sinimp { get; set; }

        public string TipoImpuesto
        {
            get
            {
                if (Libcompra == 2)
                {
                    return "R";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
