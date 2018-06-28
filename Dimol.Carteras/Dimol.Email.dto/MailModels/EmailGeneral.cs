using System;

namespace Dimol.Email.dto.MailModels
{
    public class EmailGeneral : Email
    {
        public DeudorData Deudor { get; set; }
        public ClienteData Cliente { get; set; }
        public GestorData Gestor { get; set; }
        public DateTime? FechaVencimiento { get; set; }

    }

    public class DeudorData
    {
        public string Nombre { get; set; }
    }

    public class ClienteData
    {
        public string Nombre { get; set; }
    }

    public class GestorData
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
