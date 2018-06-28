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

namespace ProcesaRolService
{
    public partial class ProcesaRolService : ServiceBase
    {
        public ProcesaRolService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string anio =  ConfigurationManager.AppSettings["ServiceYear"];
            string estado = ConfigurationManager.AppSettings["ServiceNumber"];
            
            Demonio demonio = new Demonio(anio, estado);
            Thread t = new Thread(new ThreadStart(demonio.Start));
            t.Start();
        }

        protected override void OnStop()
        {
            string anio =  ConfigurationManager.AppSettings["ServiceYear"];
            string estado = ConfigurationManager.AppSettings["ServiceNumber"];
            PJSpider.bcp.Causa.DetenerCargaRolHTMLDemonio(anio, estado);
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
