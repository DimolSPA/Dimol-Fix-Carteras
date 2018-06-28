using System;
using System.Globalization;

namespace Dimol.Empleado.dto
{
    public class Empleado
    {
        public int CodigoEmpleado { get; set; }
        public int EmpleadoId { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int EstadoEmpleadoId { get; set; }
        public int ComId { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Mail { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaFin { get; set; }
        public int SucursalId { get; set; }
        public int Digito { get; set; }
        public string Huella { get; set; }

        public Empleado(object[] ArrayDatos) {
            CodigoEmpleado  = int.Parse(ArrayDatos[0].ToString());
            EmpleadoId      = int.Parse(ArrayDatos[1].ToString());
            Rut             = ArrayDatos[2].ToString();
            Nombre          = ArrayDatos[3].ToString();
            ApellidoPaterno = ArrayDatos[4].ToString();
            ApellidoMaterno = ArrayDatos[5].ToString();
            EstadoEmpleadoId = int.Parse(ArrayDatos[6].ToString());
            ComId           = int.Parse(ArrayDatos[7].ToString());
            Direccion       = ArrayDatos[8].ToString();
            Telefono        = ArrayDatos[9].ToString();
            Celular         = ArrayDatos[10].ToString();
            Mail            = ArrayDatos[11].ToString();
            FechaIngreso    = DateTime.ParseExact(ArrayDatos[12].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            FechaFin        = ArrayDatos[13].ToString() != "" ? (DateTime?)DateTime.ParseExact(ArrayDatos[13].ToString(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture) : null;
            SucursalId      = int.Parse(ArrayDatos[14].ToString());
        }
    }
}