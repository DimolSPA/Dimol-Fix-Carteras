using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class DeudorQuiebra
    {
        public int TribunalId { get; set;}
        public int TipoCausaId {get; set;}
        public int MateriaJodicialId {get; set;}
        public string Rut {get; set;}
        public string Deudor { get; set;}
        public string RolNumero { get; set;}
        public string Tribunal { get; set;}
        public string Causa { get; set;}
        public string Materia {get; set;}
	}
    
}
