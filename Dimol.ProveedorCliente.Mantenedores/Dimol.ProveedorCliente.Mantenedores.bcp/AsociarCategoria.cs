using Dimol.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Dimol.ProveedorCliente.Mantenedores.bcp
{
    public class AsociarCategoria 
    {
        dao.AsociarCategoria dao = new dao.AsociarCategoria();


        public static string ListarSuperCategorias(int codemp)
        {
            string salida = "";
            dao.AsociarCategoria dao = new dao.AsociarCategoria();
            salida = dao.ListarSuperCategorias(codemp);
            return salida;
        }

        public List<dto.Categoria> Trae_Categorias_Asociada(dto.SuperCategoria objAccion, int codemp, int idioma) 
        {
            List<dto.Categoria> salida = new List<dto.Categoria>();
            dao.AsociarCategoria dao = new dao.AsociarCategoria();
            salida = dao.Trae_Categorias_Asociada(objAccion, codemp, idioma);
            return salida;
        }

        public List<dto.Categoria> Trae_Categorias_NotAsociada(dto.SuperCategoria objAccion, int codemp, int idioma)
        {
            List<dto.Categoria> salida = new List<dto.Categoria>();
            dao.AsociarCategoria dao = new dao.AsociarCategoria();
            salida = dao.Trae_Categorias_NotAsociada(objAccion, codemp, idioma);
            return salida;
        }

        public static List<dto.SuperCategoria> ListarSuperCategorias2(int codemp)
        {
            List<dto.SuperCategoria> salida = new List<dto.SuperCategoria>();
            dao.AsociarCategoria dao = new dao.AsociarCategoria();
            salida = dao.ListarSuperCategorias2(codemp);
            return salida;
        }

    }
}
