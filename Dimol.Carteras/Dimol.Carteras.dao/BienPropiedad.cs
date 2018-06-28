using Dimol.dao;
using System;
using System.Collections.Generic;
using System.Data;
using Dimol.dto;

namespace Dimol.Carteras.dao
{
	public class BienPropiedad
	{
		public static List<dto.BienPropiedad> ListarBienesRaicesGrilla(int ctcid, string where, string sidx, string sord, int inicio, int limite)
		{
			List<dto.BienPropiedad> lst = new List<dto.BienPropiedad>();
			try
			{
				DataSet ds = new DataSet();
				StoredProcedure sp = new StoredProcedure("Listar_Bienes_Raices_Grilla");
				sp.AgregarParametro("ctcid", ctcid);
				sp.AgregarParametro("where", where);
				sp.AgregarParametro("sidx", sidx);
				sp.AgregarParametro("sord", sord);
				sp.AgregarParametro("inicio", inicio);
				sp.AgregarParametro("limite", limite);
				ds = sp.EjecutarProcedimiento();

				if (ds.Tables.Count > 0)
				{
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						lst.Add(new dto.BienPropiedad()
						{
							BienesRaicesId = int.Parse(ds.Tables[0].Rows[i]["BIENESRAICESID"].ToString()),
							Conservador = ds.Tables[0].Rows[i]["CONSERVADOR"].ToString(),
							Rol = ds.Tables[0].Rows[i]["ROL"].ToString(),
							Foja = ds.Tables[0].Rows[i]["FOJA"].ToString(),
							Anio = string.IsNullOrEmpty(ds.Tables[0].Rows[i]["ANIO"].ToString()) ? new int() : int.Parse(ds.Tables[0].Rows[i]["ANIO"].ToString()),
							Direccion = ds.Tables[0].Rows[i]["DIRECCION"].ToString(),
							Propietario = ds.Tables[0].Rows[i]["PROPIETARIO"].ToString() == "S" ? true : false,
							EvaluoFiscal = ds.Tables[0].Rows[i]["EVALUOFISCAL"].ToString() == "S" ? true : false,
							Verificado = ds.Tables[0].Rows[i]["VERIFICADO"].ToString() == "S" ? true : false,
							Hipotecado = ds.Tables[0].Rows[i]["HIPOTECADO"].ToString() == "S" ? true : false,
							Embargo = ds.Tables[0].Rows[i]["EMBARGO"].ToString() == "S" ? true : false,
							ConservadorId = int.Parse(ds.Tables[0].Rows[i]["CONSERVADORID"].ToString()),
							ArchivoCertificado = ds.Tables[0].Rows[i]["ArchivoCertificado"].ToString()
						});
					}
				}

				return lst;
			}
			catch (Exception ex)
			{
				Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienPropiedad.ListarBienesRaicesGrilla", 0);

				return lst;
			}
		}

		public static int ListarBienesRaicesGrillaCount(int ctcid, string where, string sidx, string sord, int inicio, int limite)
		{
			int count = 0;
			try
			{
				DataSet ds = new DataSet();
				StoredProcedure sp = new StoredProcedure("_Listar_Bienes_Raices_Grilla_Count");
				sp.AgregarParametro("ctcid", ctcid);
				sp.AgregarParametro("where", where);
				sp.AgregarParametro("sidx", sidx);
				sp.AgregarParametro("sord", sord);
				sp.AgregarParametro("inicio", inicio);
				sp.AgregarParametro("limite", limite);
				ds = sp.EjecutarProcedimiento();

				if (ds.Tables.Count > 0)
				{
					count = int.Parse(ds.Tables[0].Rows[0]["count"].ToString());
				}

				return count;
			}
			catch (Exception ex)
			{
				Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienPropiedad.ListarBienesRaicesGrillaCount", 0);
				return count;
			}
		}
		public static dto.BienDetalle DetelleBienes(int ctcid)
		{
			var objeto = new dto.BienDetalle();
			try
			{
				
				DataSet ds = new DataSet();
				StoredProcedure sp = new StoredProcedure("_Obtener_Bienes_Detalle");
				sp.AgregarParametro("ctcid", ctcid);
			  
				ds = sp.EjecutarProcedimiento();

				if (ds.Tables.Count > 0)
				{
					
					objeto.Observacion = ds.Tables[0].Rows[0]["Observacion"].ToString();
				}

				return objeto;
			}
			catch (Exception ex)
			{
				Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienDetalle.DetelleBienes", 0);
				return objeto;
			}
		}
		public static List<Combobox> ListarConservadorBienes()
		{
			List<Combobox> lst = new List<Combobox>();
			try
			{
				DataSet ds = new DataSet();
				StoredProcedure sp = new StoredProcedure("_Listar_Conservador_Bienes");

				ds = sp.EjecutarProcedimiento();

				if (ds.Tables.Count > 0)
				{
					for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
					{
						lst.Add(new Dimol.dto.Combobox()
						{
							Text = ds.Tables[0].Rows[i]["Descripcion"].ToString(),
							Value = ds.Tables[0].Rows[i]["Id"].ToString()
						});
					}
				}

				return lst;
			}
			catch (Exception ex)
			{
				Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienPropiedad.ListarConservadorBienes", 0);
				return lst;
			}
		}

		public static int InsertarBienPropiedad(dto.BienPropiedad model, int ctcid, int user)
		{
			int id = -1;

			try
			{

				Funciones func = new Funciones();
				DataSet ds = new DataSet();
				StoredProcedure sp = new StoredProcedure("_Insertar_BienPropiedad");
				sp.AgregarParametro("ctcid", ctcid);
				sp.AgregarParametro("conservadorId", model.ConservadorId);
				sp.AgregarParametro("rol", model.Rol);
				sp.AgregarParametro("foja", model.Foja);
				sp.AgregarParametro("anio", model.Anio);
				sp.AgregarParametro("direccion", model.Direccion);
				sp.AgregarParametro("propietario", model.Propietario ? "S" : "N");
				sp.AgregarParametro("evaluoFical", model.EvaluoFiscal ? "S" : "N");
				sp.AgregarParametro("hipotecado", model.Hipotecado ? "S" : "N");
				sp.AgregarParametro("verificado", model.Verificado ? "S" : "N");
				sp.AgregarParametro("embargo", model.Embargo ? "S" : "N");
				sp.AgregarParametro("userId", user);

				ds = sp.EjecutarProcedimiento();
				if (ds.Tables.Count > 0)
					if (ds.Tables[0].Rows.Count > 0)
					{
						id = Int32.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
					}

			}
			catch (Exception ex)
			{
				Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienPropiedad.InsertarBienPropiedad", user);

				return id;
			}
			return id;
		}

		public static int ActualizarBienPropiedad(dto.BienPropiedad model, int user)
		{
			int id = -1;

			try
			{

				Funciones func = new Funciones();
				StoredProcedure sp = new StoredProcedure("_Actualizar_BienPropiedad");
				sp.AgregarParametro("Id", model.BienesRaicesId);
				sp.AgregarParametro("conservadorId", model.ConservadorId);
				sp.AgregarParametro("rol", model.Rol);
				sp.AgregarParametro("foja", model.Foja);
				sp.AgregarParametro("anio", model.Anio);
				sp.AgregarParametro("direccion", model.Direccion);
				sp.AgregarParametro("propietario", model.Propietario ? "S" : "N");
				sp.AgregarParametro("evaluoFical", model.EvaluoFiscal ? "S" : "N");
				sp.AgregarParametro("hipotecado", model.Hipotecado ? "S" : "N");
				sp.AgregarParametro("verificado", model.Verificado ? "S" : "N");
				sp.AgregarParametro("embargo", model.Embargo ? "S" : "N");
				sp.AgregarParametro("userId", user);

				id = sp.EjecutarProcedimientoTrans();


			}
			catch (Exception ex)
			{
				Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienPropiedad.ActualizarBienPropiedad", user);

				return id;
			}
			return id;
		}

		public static int ExisteRegistro(int propiedadId)
		{
			int id = -1;

			try
			{

				Funciones func = new Funciones();
				DataSet ds = new DataSet();
				StoredProcedure sp = new StoredProcedure("_Exist_BienPropiedad");
				sp.AgregarParametro("Id", propiedadId);

				ds = sp.EjecutarProcedimiento();
				if (ds.Tables.Count > 0)
					if (ds.Tables[0].Rows.Count > 0)
					{
						id = Int32.Parse(ds.Tables[0].Rows[0]["propiedadId"].ToString());
					}

			}
			catch (Exception ex)
			{
				Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienPropiedad.ExisteRegistro", 0);

				return id;
			}
			return id;
		}

		public static int InsertarPropiedadArchivo(int propiedadId, string rutaArchivo, int user)
		{
			int id = -1;

			try
			{

				Funciones func = new Funciones();
				StoredProcedure sp = new StoredProcedure("_Insertar_Bien_Popiedad_Certificado");				sp.AgregarParametro("propiedadId", propiedadId);
				sp.AgregarParametro("rutaArchivo", rutaArchivo);
				sp.AgregarParametro("userId", user);

				id = sp.EjecutarProcedimientoTrans();


			}
			catch (Exception ex)
			{
				Funciones.InsertarError(ex.Message, ex.StackTrace, "Dimol.Carteras.dao.BienPropiedad.InsertarPropiedadArchivo", user);

				return id;
			}
			return id;
		}
	}
}
