﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Configuration.Install;
using System.Reflection;
namespace Dimol.ArchivoCoopeuch
{
    [RunInstaller(true)]
    public partial class CrearArchivoCoopeuchInstaller : Installer
    {
        public CrearArchivoCoopeuchInstaller()
        {
            InitializeComponent();

            this.Installers.Add(GetServiceInstaller());
            this.Installers.Add(GetServiceProcessInstaller());
        }
        private ServiceInstaller GetServiceInstaller()
        {
            ServiceInstaller installer = new ServiceInstaller();
            installer.ServiceName = GetConfigurationValue("ServiceName");
            return installer;
        }

        private ServiceProcessInstaller GetServiceProcessInstaller()
        {
            ServiceProcessInstaller installer = new ServiceProcessInstaller();
            installer.Account = ServiceAccount.LocalSystem;
            return installer;
        }

        private string GetConfigurationValue(string key)
        {
            Assembly service = Assembly.GetAssembly(typeof(CrearArchivoCoopeuchInstaller));
            Configuration config = ConfigurationManager.OpenExeConfiguration(service.Location);
            if (config.AppSettings.Settings[key] != null)
            {
                return config.AppSettings.Settings[key].Value;
            }
            else
            {
                throw new IndexOutOfRangeException("Settings collection does not contain the requested key:" + key);
            }
        }
    }
}