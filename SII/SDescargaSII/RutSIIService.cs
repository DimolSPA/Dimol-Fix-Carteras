using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SDescargaSII
{
    public partial class RutSIIService : ServiceBase
    {
        private SII.dto.Captcha objCaptcha = new SII.dto.Captcha();
        private List<SII.dto.Status> Lst = new List<SII.dto.Status>();
        private int Indice = 0;

        public RutSIIService(string[] args)
        {
            InitializeComponent();
        }

        public RutSIIService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            //do
            //{
            //    Indice = 0;
            //    Lst = SII.bcp.Status.ListarRutporDemonio(ConfigurationManager.AppSettings["ServiceNumber"]);
            //    RefrescarCaptcha();
            //} while ( Lst.Count != 0);
            Demonio demonio = new Demonio();
            Thread t = new Thread(new ThreadStart(demonio.Start));
            t.Start();

        }

        protected override void OnStop()
        {
            int error = SII.bcp.Status.DetenerRutporDemonio(ConfigurationManager.AppSettings["ServiceNumber"]);
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }

        private void RefrescarCaptcha()
        {
            objCaptcha = SII.bcp.Status.ObtenerCaptcha();
            objCaptcha.Codigo = SII.bcp.Status.BuscarCaptcha(objCaptcha.txtCaptcha);
            if (!string.IsNullOrEmpty(objCaptcha.Codigo))
            {
                ConsultarCaptcha();
                if (Indice < Lst.Count) 
                { 
                    RefrescarCaptcha();
                }
                
            }
        }

        private void ConsultarCaptcha()
        {
            int resultado = SII.bcp.Status.ConsultarRut(objCaptcha, Lst[Indice]);
            if (resultado > 0)
            {
                Indice++;
            }
        }
    }
}
