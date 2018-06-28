using PJSpider.bcp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PJSpider
{
    public partial class PJSpiderService : ServiceBase
    {
        public PJSpiderService()
        {
            InitializeComponent();
        }
        private Timer _timer;
        private DateTime _lastRun = DateTime.Now.AddDays(-1).Date + new TimeSpan(19, 30, 0);
        private int particion = 0;
        private int cantidad = 0;

        protected override void OnStart(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[] {"1","10000"};
            }
            Int32.TryParse( args[0], out particion);
            Int32.TryParse(args[1], out cantidad);
            EventLog.WriteEntry("Iniciando servicio de actualizacion desde el Poder Judicial. Particion: "+ particion);
            Console.WriteLine("Iniciando servicio de actualizacion desde el Poder Judicial. Particion: " + particion);
            
            _timer = new Timer(10*1000);// every day
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            //_timer = new Timer(24 * 60 * 60 * 1000);// every day
            _timer.Start();
            //...
        }


        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            // ignore the time, just compare the date
            if (_lastRun < DateTime.Now)
            {
                EventLog.WriteEntry("Actualizando desde el Poder Judicial. Particion: " + particion);
                Console.WriteLine("Actualizando desde el Poder Judicial. Particion: " + particion);
                // stop the timer while we are running the cleanup task
                _timer.Stop();
                //
                try
                {
                    Causa.ActualizarPoderJudicialParticion(1, 1, "-1", particion, cantidad);
                } 
                catch(Exception ex)
                {
                    Console.WriteLine("Error en el proceso. Particion: " + particion + "Mensaje: " + ex.Message);
                }
                //
                TimeSpan ts = new TimeSpan(18, 30, 0);
                _lastRun = DateTime.Now.AddDays(1).Date + ts;
                _timer = new Timer(60 * 60 * 1000);// every hour
                _timer.Start();
                EventLog.WriteEntry("Actualizacion terminada. Particion: " + particion);
                Console.WriteLine("Actualizacion terminada. Particion:" + particion);
            }
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            //Console.ReadLine();
            this.OnStop();
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("Deteniendo servicio de actualizacion desde el Poder Judicial. Particion: " + particion);
            Console.WriteLine("Deteniendo servicio de actualizacion desde el Poder Judicial. Particion: " + particion);
        }
    }
}
