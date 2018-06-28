using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Management;

namespace Dimol.WindowsService.bcp
{
    public class Service
    {
        public string Name;
        public bool Running;
        private ServiceController serviceController;

        public Service(string name, string host)
        {
            Name = name;

            serviceController = new ServiceController(Name, host);
            Running = serviceController.Status == ServiceControllerStatus.Running;
        }

        public bool StartService()
        {
            ServiceControllerPermission scp = new ServiceControllerPermission(ServiceControllerPermissionAccess.Control, serviceController.MachineName, Name);
            scp.Assert();

            serviceController.Start();
            serviceController.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 5));
            serviceController.Refresh();

            Running = serviceController.Status == ServiceControllerStatus.Running;

            return Running;
        }

        public bool StopService()
        {
            ServiceControllerPermission scp = new ServiceControllerPermission(ServiceControllerPermissionAccess.Control, serviceController.MachineName, Name);
            scp.Assert();

            serviceController.Stop();
            serviceController.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 5));
            serviceController.Refresh();

            Running = serviceController.Status == ServiceControllerStatus.Running;

            return Running;
        }

        public void ServiceList(string host)
        {
            ServiceController[] services = ServiceController.GetServices( host);



        }
    }
}
