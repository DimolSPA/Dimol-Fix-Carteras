using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Judicial.Mantenedores.dto
{
    public class Rol
    {
        public int rol_rolid { get; set; }
        public int rol_ctcid { get; set; }
        public int rol_estid { get; set; }
        public string rol_numero { get; set; }
        public string trb_nombre { get; set; }
        public string pcl_nomfant { get; set; }
        public string ctc_rut { get; set; }
        public string rol_comentario { get; set; }
        public string tca_nombre { get; set; }
        public string rle_usrid { get; set; }
        public int rol_trbid { get; set; }
        public int rol_tcaid { get; set; }
        public int rol_pclid { get; set; }
        public int rol_esjid { get; set; }
        public string rol_fecjud { get; set; }
        public string rol_fecrol { get; set; }
        public string rol_fecdem { get; set; }
        public string ctc_nomfant { get; set; }
        public int prov_cli { get; set; }
        public string rol_bloqueo{ get; set; }
        public string rol_prequiebra { get; set; }
        public string rol_tipo_rol { get; set; }
    }
}
