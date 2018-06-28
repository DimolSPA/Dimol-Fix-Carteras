using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
namespace Dimol.ArchivoCoopeuch
{
    partial class CrearArchivoCoopeuch : ServiceBase
    {
        public CrearArchivoCoopeuch()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            string inicio = ConfigurationManager.AppSettings["ServiceStart"];
            string termino = ConfigurationManager.AppSettings["ServiceStop"];

            Demonio demonio = new Demonio(inicio, termino);
            Thread t = new Thread(new ThreadStart(demonio.Start));
            t.Start();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
           
        }
        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}
