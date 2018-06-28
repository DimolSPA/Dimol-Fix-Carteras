﻿using System;
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

namespace DescargarRolExternoService
{
    public partial class DescargarRolExternoService : ServiceBase
    {
        public DescargarRolExternoService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            string inicio = ConfigurationManager.AppSettings["ServiceStart"];
            string termino = ConfigurationManager.AppSettings["ServiceStop"];
            string anio = ConfigurationManager.AppSettings["ServiceAnio"];

            Demonio demonio = new Demonio(inicio, termino, anio);
            Thread t = new Thread(new ThreadStart(demonio.Start));
            t.Start();
        }

        protected override void OnStop()
        {
            string inicio = ConfigurationManager.AppSettings["ServiceStart"];
            string termino = ConfigurationManager.AppSettings["ServiceStop"];
            PJSpider.bcp.Causa.DetenerCargaRolHTMLDemonio(inicio, termino);
        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}