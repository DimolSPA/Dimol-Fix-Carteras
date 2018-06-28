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

namespace Dimol.ArchivoCopec
{
    public partial class CrearArchivoCopec : ServiceBase
    {
        public CrearArchivoCopec()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {            
            string inicio = ConfigurationManager.AppSettings["ServiceStart"];
            string termino = ConfigurationManager.AppSettings["ServiceStop"];

            Demonio demonio = new Demonio(inicio, termino);
            Thread t = new Thread(new ThreadStart(demonio.Start));
            t.Start();
        }

        protected override void OnStop()
        {

        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
    }
}