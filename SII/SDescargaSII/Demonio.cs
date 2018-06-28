using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDescargaSII
{
    public class Demonio
    {
        //private string Anio { get; set; }
        //private string Estado { get; set; }
        private SII.dto.Captcha objCaptcha = new SII.dto.Captcha();
        private List<SII.dto.Status> Lst = new List<SII.dto.Status>();
        private int Indice = 0;
        public Demonio()
        {
            //this.Estado = estado;
            //this.Anio = anio;
        }

        public void Start()
        {
            List<SII.dto.Combobox> lstTipoActividad = SII.bcp.Status.ListarActividadEconomica();
            List<SII.dto.Combobox> lstTipoDocumento = SII.bcp.Status.ListarTipoDocumento();
            do
            {
                Indice = 0;
                Lst = SII.bcp.Status.ListarRutporDemonioNew(ConfigurationManager.AppSettings["ServiceNumber"]);
                RefrescarCaptcha(lstTipoActividad, lstTipoDocumento);
            } while (Lst.Count != 0);
        }

        private void RefrescarCaptcha(List<SII.dto.Combobox> lstTipoActividad, List<SII.dto.Combobox> lstTipoDocumento)
        {
            objCaptcha = SII.bcp.Status.ObtenerCaptcha();
            objCaptcha.Codigo = SII.bcp.Status.BuscarCaptcha(objCaptcha.txtCaptcha);
            if (!string.IsNullOrEmpty(objCaptcha.Codigo))
            {
                ConsultarCaptcha(lstTipoActividad, lstTipoDocumento);
                if (Indice < Lst.Count)
                {
                    RefrescarCaptcha(lstTipoActividad, lstTipoDocumento);
                }

            }
        }

        private void ConsultarCaptcha(List<SII.dto.Combobox> lstTipoActividad, List<SII.dto.Combobox> lstTipoDocumento)
        {

            int resultado = SII.bcp.Status.ConsultarRutDemonioNew(objCaptcha, Lst[Indice], lstTipoActividad, lstTipoDocumento);
            if (resultado > 0)
            {
                Indice++;
            }
        }
    }
}
