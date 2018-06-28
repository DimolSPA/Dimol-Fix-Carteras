using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using CYPH;


namespace Dimol.dto
{
    public class Usuarios
    {

        #region "Variables"
        public int Empresa { get; set; }
        public int Sucursal { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int Idioma { get; set; }
        #endregion

    }
}


