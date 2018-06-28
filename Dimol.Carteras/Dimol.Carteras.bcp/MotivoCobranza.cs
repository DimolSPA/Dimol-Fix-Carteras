using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dimol.dto;

namespace Dimol.Carteras.bcp
{
    public class MotivoCobranza: dto.MotivoCobranza
    {
        public void OperMotivoCobranza(string oper, int? id, UserSession objsession)
        {
            switch (oper)
            {
                case "add":
                    string sdfsdf = this.Nombre;
                    break;
                case "edit":
                    break;
                case "del":
                    break;
            }
        }
    }
}
