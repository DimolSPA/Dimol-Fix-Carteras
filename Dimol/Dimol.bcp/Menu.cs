using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.bcp
{
    public class Menu
    {
        public dao.Menu operador = new dao.Menu();
        List<dto.Menu> LstMenu {set;get;}

         public string[] TraeMenuEncriptado(int codemp)
        {
            return operador.TraeMenuEncriptado(codemp);
            
        }

         public List<dto.Menu> TraeMenuUsuario(int usuario, int idioma, int codemp, string menu, string dominio)
        {
            return operador.TraeMenuUsuario(usuario, idioma, codemp, menu,dominio);
        }

        public string Cargar(int usuario, int idioma, int codemp, string dominio)
        {
            string[] menu = TraeMenuEncriptado(codemp);
            List<dto.Menu> lstMenu = operador.TraeMenuUsuario(usuario, idioma, codemp, menu[0], dominio);

            LstMenu = OrdenarMenu(lstMenu);
            
            //LstMenu.RemoveRange(0,13);
            string menuDibujado = DibujaMenu(LstMenu);
            return menuDibujado;
        }

        public List<dto.Menu> OrdenarMenu(List<dto.Menu> lstMenu)
        {
            List<dto.Menu> lstMenuOrdenado = new List<dto.Menu>();
            dto.Menu nuevoMenu;
            //bool tieneMenu = false;
            lstMenu = (from myObject in lstMenu
                       //where myObject.ParentId != ""
                        orderby myObject.ParentId descending
                        select myObject).ToList();

            foreach (dto.Menu menu in lstMenu)
            {
                nuevoMenu = lstMenu.Find(x => x.Id == menu.ParentId);
                if (menu.ParentId != "")
                {
                    nuevoMenu.Hijo = (from myObject in lstMenu
                                      where myObject.ParentId == menu.ParentId
                                      orderby myObject.Text
                                      select myObject).ToList();
                }
            }

            lstMenuOrdenado = (from myObject in lstMenu
                        where myObject.ParentId == ""
                        orderby myObject.Text
                               select myObject).ToList();

            //--------- Menu de Sistema

            List<string> lista = new List<string>() { "Empresa", "Parametros", "Personal", "Seguridad", "Utilitarios", "Web" };
            //tieneMenu = false;
            //foreach(string s in lista){
            //    tieneMenu = lstMenu.FindAll(x => x.Text.Contains(s)).Count > 0;
            //}
            //if (tieneMenu)
            //{
                lstMenuOrdenado.Add(new dto.Menu
                {
                    Id = "1000",
                    Text = "Sistema",
                    ParentId = "",
                    Tooltip = "Herramientas de Configuración del Sistema",
                    Url = "",
                    Hijo = (from myObject in lstMenuOrdenado
                            where lista.Contains(myObject.Text)
                            orderby myObject.Text
                            select myObject).ToList()
                });

                lstMenuOrdenado = (from myObject in lstMenuOrdenado
                                   where !(lista.Contains(myObject.Text))
                                   orderby myObject.Text
                                   select myObject).ToList();
            //}
            //--------- Menu de Sistema

            //--------- Menu de Compras

            lista = new List<string>() { "Compras", "Insumos", "Productos"};
            //tieneMenu = false;
            //foreach(string s in lista){
            //    tieneMenu = lstMenu.FindAll(x => x.Text.Contains(s)).Count > 0;
            //}
            //if (tieneMenu)
            //{
                lstMenuOrdenado.Add(new dto.Menu
                {
                    Id = "1001",
                    Text = "Compra",
                    ParentId = "",
                    Tooltip = "Compras, Insumos y Productos",
                    Url = "",
                    Hijo = (from myObject in lstMenuOrdenado
                            where lista.Contains(myObject.Text)
                            orderby myObject.Text
                            select myObject).ToList()
                });

                lstMenuOrdenado = (from myObject in lstMenuOrdenado
                                   where !(lista.Contains(myObject.Text))
                                   orderby myObject.Text
                                   select myObject).ToList();
            //}
            //--------- Menu de Compras

            //--------- Menu de Control Gestion dentro de CARTERAS

            lista = new List<string>() { "Control Gestion"};
            //tieneMenu = false;
            //foreach(string s in lista){
            //    tieneMenu = lstMenu.FindAll(x => x.Text.Contains(s)).Count > 0;
            //}
            //if (tieneMenu)
            //{
                lstMenuOrdenado.Find(x => x.Text == "Carteras").Hijo.AddRange((from myObject in lstMenuOrdenado
                                                                               where lista.Contains(myObject.Text)
                                                                               orderby myObject.Text
                                                                               select myObject).ToList());

                lstMenuOrdenado = (from myObject in lstMenuOrdenado
                                   where !(lista.Contains(myObject.Text))
                                   orderby myObject.Text
                                   select myObject).ToList();

                //--------- Menu de Control Gestion dentro de CARTERAS
            //}
            return lstMenuOrdenado;

        }

        public void MenuSistema()
        {
            


        }

        public string DibujaMenu(List<dto.Menu> lstMenu)
        {
            StringBuilder strMenu = new StringBuilder("<div id='cssmenu' style='position: absolute;z-index: 9;'><ul>");
            strMenu.Append( DibujaLink(lstMenu));
            strMenu.Append("</ul></div>");
            return strMenu.ToString();
        }

        public string DibujaLink(List<dto.Menu> lstSubMenu)
        {
            StringBuilder strLink = new StringBuilder("");
            dto.Menu menu;

            for (int i = 0 ; i < lstSubMenu.Count; i++)
            {
                menu = lstSubMenu[i];
                if (menu.Hijo == null)
                {
                    if (i == lstSubMenu.Count - 1)
                    {
                        strLink.Append("<li class='last'><a href='" + menu.Url + "' title='" + menu.Tooltip + "'><span>" + menu.Text + "</span></a></li>");
                    }
                    else
                    {
                        strLink.Append("<li><a href='" + menu.Url + "' title='" + menu.Tooltip + "'><span>" + menu.Text + "</span></a></li>");
                    }
                }
                else
                {
                    strLink.Append("<li class='has-sub'><a href='#' title='" + menu.Tooltip + "'><span>" + menu.Text + "</span></a><ul>");
                    strLink.Append(DibujaLink(menu.Hijo));
                    strLink.Append("</ul></li>");
                }
            }

            return strLink.ToString();
        }
    }
}
