using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data;
using Dimol.dao;
using System.Drawing.Imaging;
using System.Data.SqlClient;

namespace Dimol.ConvertidorImagenes
{
    public class Convertidor
    {
        public List<ConverterEntity> ListaImagenes = new List<ConverterEntity>();
        public List<EmpladoConverterEntity> ListaImagenesEmpleados = new List<EmpladoConverterEntity>();

        public byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public void Convertir(string ruta, int inicio,int cantidad)
        {
            try
            {
                GenerarListaImagenes(inicio, cantidad);
                GenerarArchivos(ruta);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ConvertirEmpleados(string ruta, int inicio, int cantidad)
        {
            try
            {
                GenerarListaImagenesEmpleados();
                GenerarArchivosEmpleados(ruta);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void GenerarListaImagenes(int inicio, int cantidad)
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Imagenes_Convertir");
                sp.AgregarParametro("inicio", inicio);
                sp.AgregarParametro("limite", cantidad);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        this.ListaImagenes.Add(new ConverterEntity()
                        {
                            Codemp = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Pclid = Int32.Parse(ds.Tables[0].Rows[i]["Pclid"].ToString()),
                            Ctcid = Int32.Parse(ds.Tables[0].Rows[i]["Ctcid"].ToString()),
                            Ccbid = Int32.Parse(ds.Tables[0].Rows[i]["Ccbid"].ToString()),
                            Cdid = Int32.Parse(ds.Tables[0].Rows[i]["Cdid"].ToString()),
                            //ByteArray = (byte[])ds.Tables[0].Rows[i]["Imagen"],
                            //Imagen = byteArrayToImage((byte[])ds.Tables[0].Rows[i]["Imagen"]),
                            NombreArchivoImagen = ds.Tables[0].Rows[i]["Codemp"].ToString() + "_" + ds.Tables[0].Rows[i]["Pclid"].ToString() + "_" + ds.Tables[0].Rows[i]["Ctcid"].ToString() + "_" + ds.Tables[0].Rows[i]["Ccbid"].ToString() + "_" + ds.Tables[0].Rows[i]["Cdid"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConverterEntity RecuperarImagen(int codemp, int pclid, int ctcid, int ccbid, int cdid)
        {
            try
            {
                DataSet ds = new DataSet();
                ConverterEntity obj = new ConverterEntity();
                StoredProcedure sp = new StoredProcedure("_Trae_Imagen_Convertir");
                sp.AgregarParametro("codemp", codemp);
                sp.AgregarParametro("pclid", pclid);
                sp.AgregarParametro("ctcid", ctcid);
                sp.AgregarParametro("ccbid", ccbid);
                sp.AgregarParametro("cdid", cdid);
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        obj = new ConverterEntity()
                        {
                            Codemp = codemp,
                            Pclid = pclid,
                            Ctcid = ctcid,
                            Ccbid = ccbid,
                            Cdid = cdid,
                            ByteArray = (byte[])ds.Tables[0].Rows[i]["Imagen"],
                            Imagen = byteArrayToImage((byte[])ds.Tables[0].Rows[i]["Imagen"]),
                            NombreArchivoImagen = ds.Tables[0].Rows[i]["Codemp"].ToString() + "_" + ds.Tables[0].Rows[i]["Pclid"].ToString() + "_" + ds.Tables[0].Rows[i]["Ctcid"].ToString() + "_" + ds.Tables[0].Rows[i]["Ccbid"].ToString() + "_" + ds.Tables[0].Rows[i]["Cdid"].ToString()
                        };
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GenerarArchivos(string ruta)
        {
            string extension = "";
            foreach (ConverterEntity obj in this.ListaImagenes)
            {
                ConverterEntity objImagen = RecuperarImagen(obj.Codemp,obj.Pclid,obj.Ctcid,obj.Ccbid,obj.Cdid);
                extension = "";
                if (ImageFormat.Jpeg.Equals(objImagen.Imagen.RawFormat))
                {
                    extension = ".jpg";// JPEG
                }
                else if (ImageFormat.Png.Equals(objImagen.Imagen.RawFormat))
                {
                    extension = ".png";// PNG
                }
                else if (ImageFormat.Gif.Equals(objImagen.Imagen.RawFormat))
                {
                    extension = ".gif";// GIF
                }
                obj.NombreArchivoImagen = ruta + obj.NombreArchivoImagen + extension;
                objImagen.Imagen.Save(obj.NombreArchivoImagen);
                ActualizarListaImagenes(obj);
            }
        }

        public void GenerarArchivosEmpleados(string ruta)
        {
            string extension = "";
            foreach (EmpladoConverterEntity obj in this.ListaImagenesEmpleados)
            {
                extension = "";
                if (ImageFormat.Jpeg.Equals(obj.Imagen.RawFormat))
                {
                    extension = ".jpg";// JPEG
                }
                else if (ImageFormat.Png.Equals(obj.Imagen.RawFormat))
                {
                    extension = ".png";// PNG
                }
                else if (ImageFormat.Gif.Equals(obj.Imagen.RawFormat))
                {
                    extension = ".gif";// GIF
                }
                obj.NombreArchivoImagen = ruta + obj.NombreArchivoImagen + extension;
                //obj.Imagen.Save(obj.NombreArchivoImagen);
                ActualizarListaImagenesEmpleados(obj);
            }
        }

        public void ActualizarListaImagenes(ConverterEntity obj)
        {
            try
            {
                Conexion conn = new Conexion();
                conn.SQLConn.Open();
                SqlTransaction trans = conn.SQLConn.BeginTransaction();

                StoredProcedure sp = new StoredProcedure("_Update_Cartera_Clientes_Cpbt_Doc_Imagenes_Ruta");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("pclid", obj.Pclid);
                sp.AgregarParametro("ctcid", obj.Ctcid);
                sp.AgregarParametro("ccbid", obj.Ccbid);
                sp.AgregarParametro("cdid", obj.Cdid);
                sp.AgregarParametro("ruta", obj.NombreArchivoImagen);
                int error = sp.EjecutarProcedimiento(conn, trans);

                if (error > 0)
                {
                    trans.Commit();
                    conn.SQLConn.Close();
                }
                else
                {
                    trans.Rollback();
                    conn.SQLConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GenerarListaImagenesEmpleados()
        {
            try
            {
                DataSet ds = new DataSet();
                StoredProcedure sp = new StoredProcedure("_Listar_Imagenes_Empleado_Convertir");
                ds = sp.EjecutarProcedimiento();

                if (ds.Tables.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        this.ListaImagenesEmpleados.Add(new EmpladoConverterEntity()
                        {
                            Codemp = Int32.Parse(ds.Tables[0].Rows[i]["Codemp"].ToString()),
                            Sucid = Int32.Parse(ds.Tables[0].Rows[i]["Sucid"].ToString()),
                            Emplid = Int32.Parse(ds.Tables[0].Rows[i]["Emplid"].ToString()),
                            Usrid = Int32.Parse(ds.Tables[0].Rows[i]["Usrid"].ToString() == null ? "0" : ds.Tables[0].Rows[i]["Usrid"].ToString()),
                            ByteArray = (byte[])ds.Tables[0].Rows[i]["Imagen"],
                            Imagen = byteArrayToImage((byte[])ds.Tables[0].Rows[i]["Imagen"]),
                            NombreArchivoImagen = ds.Tables[0].Rows[i]["Codemp"].ToString() + "_" + ds.Tables[0].Rows[i]["Sucid"].ToString() + "_" + ds.Tables[0].Rows[i]["Emplid"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActualizarListaImagenesEmpleados(EmpladoConverterEntity obj)
        {
            try
            {
                Conexion conn = new Conexion();
                conn.SQLConn.Open();
                SqlTransaction trans = conn.SQLConn.BeginTransaction();

                StoredProcedure sp = new StoredProcedure("_Update_Empleados_Imagenes_Ruta");
                sp.AgregarParametro("codemp", obj.Codemp);
                sp.AgregarParametro("emplid", obj.Emplid);
                sp.AgregarParametro("ruta", obj.NombreArchivoImagen);
                int error = sp.EjecutarProcedimiento(conn, trans);

                if (error > 0)
                {
                    trans.Commit();
                    conn.SQLConn.Close();
                }
                else
                {
                    trans.Rollback();
                    conn.SQLConn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
