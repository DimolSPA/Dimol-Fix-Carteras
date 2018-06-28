using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Dimol.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ConsultaRut" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ConsultaRut.svc o ConsultaRut.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ConsultaRut : IConsultaRut
    {
        public List<dto.ConsultaPJ> CausasPorRut(string rut)
        {
            return bcp.ConsultaPJ.ConsultarPorRut(rut);
        }

    }
}
