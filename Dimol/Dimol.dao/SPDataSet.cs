using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Dimol.dao
{
    public class StoredProcedureParameter
    {
        public string Variable  { get; set; }
        public Object Valor { get; set; }

        public StoredProcedureParameter(string variable, Object valor)
        {
            try
            {
                Variable = variable;
                Valor = valor;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la creacion del Parametro: " + variable + " Error: " + ex.Message);
            }
        }

        public SqlDbType GetProperty()
        {
            switch (this.Valor.GetType().ToString())
            {
                case "System.String":
                    return SqlDbType.VarChar;
                case "System.Int16":
                case "System.Int32":
                    return SqlDbType.Int;
                case "System.Int64":
                    return SqlDbType.BigInt;
                case "System.Decimal":
                    return SqlDbType.Decimal;
                case "System.Double":
                    return SqlDbType.BigInt;
                case "System.DateTime":
                    return SqlDbType.DateTime;
                case "System.Byte":
                case "System.Byte[]":
                    return SqlDbType.Image;
                default:
                    return SqlDbType.Char;
            }
        }

    }
    public class StoredProcedure
    {
        #region " Variables "
        public string NombreProcedimiento { get; set; }
        public List<StoredProcedureParameter> Parametros { get; set; }
        #endregion

        public StoredProcedure(string Nombre)
        {
            NombreProcedimiento = Nombre;
            Parametros = new List<StoredProcedureParameter>();
        }

        public StoredProcedure()
        {
        }

        public void AgregarParametro(string variable, Object valor)
        {
            try
            {
                StoredProcedureParameter objParametro = new StoredProcedureParameter("@" + variable, valor);
                Parametros.Add(objParametro);
            }
            catch (Exception ex)
            {

            }

        }

        public DataSet EjecutarProcedimiento()
        {
            Conexion conn = new Conexion();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(this.NombreProcedimiento, conn.SQLConn);
                sqlCmd.CommandTimeout = 120;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                

                foreach (StoredProcedureParameter sppParametros in this.Parametros )
                {
                    SqlParameter sqlPar = new SqlParameter(sppParametros.Variable, sppParametros.GetProperty());
                    sqlPar.Direction = ParameterDirection.Input;
                    sqlPar.Value = sppParametros.Valor;
                    sqlCmd.Parameters.Add(sqlPar);
                }
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.SQLConn.Close();
                return ds;
            }
            catch (Exception ex)
            {
                conn.SQLConn.Close();
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: " + this.NombreProcedimiento, 0);
                return new DataSet();
            }
        }

        public int EjecutarProcedimiento(int opc)
        {
            Conexion conn = new Conexion();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(this.NombreProcedimiento, conn.SQLConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;


                foreach (StoredProcedureParameter sppParametros in this.Parametros)
                {
                    SqlParameter sqlPar = new SqlParameter(sppParametros.Variable, sppParametros.GetProperty());
                    sqlPar.Direction = ParameterDirection.Input;
                    sqlPar.Value = sppParametros.Valor;
                    sqlCmd.Parameters.Add(sqlPar);
                }
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.SQLConn.Close();
                return 1;// sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                conn.SQLConn.Close();
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: " + this.NombreProcedimiento, 0);
                return -1;
            }
        }

        public int EjecutarProcedimiento(Conexion conn,SqlTransaction trans)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand(this.NombreProcedimiento, conn.SQLConn, trans);
                sqlCmd.CommandType = CommandType.StoredProcedure;


                foreach (StoredProcedureParameter sppParametros in this.Parametros)
                {
                    SqlParameter sqlPar = new SqlParameter(sppParametros.Variable, sppParametros.GetProperty());
                    sqlPar.Direction = ParameterDirection.Input;
                    sqlPar.Value = sppParametros.Valor;
                    sqlCmd.Parameters.Add(sqlPar);
                }
                int salida = sqlCmd.ExecuteNonQuery();
                return salida;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: " + this.NombreProcedimiento, 0);
                return -1;
            }
        }

        public int EjecutarProcedimientoNoTrans()
        {
            Conexion conn = new Conexion();
            conn.SQLConn.Open();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(this.NombreProcedimiento, conn.SQLConn, null);
                sqlCmd.CommandType = CommandType.StoredProcedure;


                foreach (StoredProcedureParameter sppParametros in this.Parametros)
                {
                    SqlParameter sqlPar = new SqlParameter(sppParametros.Variable, sppParametros.GetProperty());
                    sqlPar.Direction = ParameterDirection.Input;
                    sqlPar.Value = sppParametros.Valor;
                    sqlCmd.Parameters.Add(sqlPar);
                }
                int salida = sqlCmd.ExecuteNonQuery();
                conn.SQLConn.Close();
                return salida;
            }
            catch (Exception ex)
            {
                conn.SQLConn.Close();
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: EjecutarProcedimientoNoTrans =>" + this.NombreProcedimiento, 0);
                return -1;
            }
        }

        public int EjecutarProcedimientoTrans()
        {
            try
            {
                Conexion conn = new Conexion();
                conn.SQLConn.Open();
                SqlTransaction trans = conn.SQLConn.BeginTransaction();

                SqlCommand sqlCmd = new SqlCommand(this.NombreProcedimiento, conn.SQLConn, trans);
                sqlCmd.CommandType = CommandType.StoredProcedure;


                foreach (StoredProcedureParameter sppParametros in this.Parametros)
                {
                    SqlParameter sqlPar = new SqlParameter(sppParametros.Variable, sppParametros.GetProperty());
                    sqlPar.Direction = ParameterDirection.Input;
                    sqlPar.Value = sppParametros.Valor;
                    sqlCmd.Parameters.Add(sqlPar);
                }
                 int error = sqlCmd.ExecuteNonQuery();

                if (error >= 0)
                {
                    trans.Commit();
                    conn.SQLConn.Close();
                }
                else
                {
                    trans.Rollback();
                    conn.SQLConn.Close();
                }

                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: " + this.NombreProcedimiento, 0);
                return -1;
            }
        }

        public int EjecutarProcedimientoTransLargo()
        {
            try
            {
                Conexion conn = new Conexion();
                conn.SQLConn.OpenAsync();
                SqlTransaction trans = conn.SQLConn.BeginTransaction();

                SqlCommand sqlCmd = new SqlCommand(this.NombreProcedimiento, conn.SQLConn, trans);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;

                foreach (StoredProcedureParameter sppParametros in this.Parametros)
                {
                    SqlParameter sqlPar = new SqlParameter(sppParametros.Variable, sppParametros.GetProperty());
                    sqlPar.Direction = ParameterDirection.Input;
                    sqlPar.Value = sppParametros.Valor;
                    sqlCmd.Parameters.Add(sqlPar);
                }
                int error = sqlCmd.ExecuteNonQuery();

                if (error >= 0)
                {
                    trans.Commit();
                    conn.SQLConn.Close();
                }
                else
                {
                    trans.Rollback();
                    conn.SQLConn.Close();
                }

                return error;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: " + this.NombreProcedimiento, 0);
                return -1;
            }
        }

        public int EjecutarProcedimiento(Conexion conn)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand(this.NombreProcedimiento, conn.SQLConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;


                foreach (StoredProcedureParameter sppParametros in this.Parametros)
                {
                    SqlParameter sqlPar = new SqlParameter(sppParametros.Variable, sppParametros.GetProperty());
                    sqlPar.Direction = ParameterDirection.Input;
                    sqlPar.Value = sppParametros.Valor;
                    sqlCmd.Parameters.Add(sqlPar);
                }
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.SQLConn.Close();
                return sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                conn.SQLConn.Close();
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: " + this.NombreProcedimiento, 0);
                return -1;
            }
        }

        public DataSet EjecProdEspecial(Conexion conn, SqlTransaction trans)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand(this.NombreProcedimiento, conn.SQLConn, trans);
                sqlCmd.CommandType = CommandType.StoredProcedure;


                foreach (StoredProcedureParameter sppParametros in this.Parametros)
                {
                    SqlParameter sqlPar = new SqlParameter(sppParametros.Variable, sppParametros.GetProperty());
                    sqlPar.Direction = ParameterDirection.Input;
                    sqlPar.Value = sppParametros.Valor;
                    sqlCmd.Parameters.Add(sqlPar);
                }
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.SQLConn.Close();
                return ds;
            }
            catch (Exception ex)
            {
                trans.Rollback();
                conn.SQLConn.Close();
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: " + this.NombreProcedimiento, 0);
                return new DataSet();
            }
        }

        public DataSet EjecProdEspecial(Conexion conn)
        {
            try
            {
                SqlCommand sqlCmd = new SqlCommand(this.NombreProcedimiento, conn.SQLConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;


                foreach (StoredProcedureParameter sppParametros in this.Parametros)
                {
                    SqlParameter sqlPar = new SqlParameter(sppParametros.Variable, sppParametros.GetProperty());
                    sqlPar.Direction = ParameterDirection.Input;
                    sqlPar.Value = sppParametros.Valor;
                    sqlCmd.Parameters.Add(sqlPar);
                }
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.SQLConn.Close();
                return ds;
            }
            catch (Exception ex)
            {
                conn.SQLConn.Close();
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: " + this.NombreProcedimiento, 0);
                return new DataSet();
            }
        }

        public SqlDataAdapter EjecutarProcedimiento(string sd)
        {
            Conexion conn = new Conexion();
            try
            {
                SqlCommand sqlCmd = new SqlCommand(this.NombreProcedimiento, conn.SQLConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;


                foreach (StoredProcedureParameter sppParametros in this.Parametros)
                {
                    SqlParameter sqlPar = new SqlParameter(sppParametros.Variable, sppParametros.GetProperty());
                    sqlPar.Direction = ParameterDirection.Input;
                    sqlPar.Value = sppParametros.Valor;
                    sqlCmd.Parameters.Add(sqlPar);
                }
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                conn.SQLConn.Close();
                return sda;
            }
            catch (Exception ex)
            {
                conn.SQLConn.Close();
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: " + this.NombreProcedimiento, 0);
                return new SqlDataAdapter();
            }
        }

        public DataSet EjecutarScript()
        {
            try
            {
                Conexion conn = new Conexion();
                conn.SQLConn.OpenAsync();
                SqlTransaction trans = conn.SQLConn.BeginTransaction();

                SqlCommand sqlCmd = new SqlCommand(this.NombreProcedimiento, conn.SQLConn, trans);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandTimeout = 0;

                //foreach (StoredProcedureParameter sppParametros in this.Parametros)
                //{
                //    SqlParameter sqlPar = new SqlParameter(sppParametros.Variable, sppParametros.GetProperty());
                //    sqlPar.Direction = ParameterDirection.Input;
                //    sqlPar.Value = sppParametros.Valor;
                //    sqlCmd.Parameters.Add(sqlPar);
                //}
                SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.SQLConn.Close();
                return ds;
            }
            catch (Exception ex)
            {
                Funciones.InsertarError(ex.Message, ex.StackTrace, "Ejecutando: query " + this.NombreProcedimiento, 0);
                return new DataSet();
            }
        }
    }
    public class DatSet
    {
        public DatSet()
        {
        }
    }
    /*

    Public Class DatSet
        'Consulta simple a la base de datos, utilizando un query directo.
        Public Function ConsultaBD(ByVal pQuery As String) As DataSet
            Dim ds As New DataSet
            Try
                Return CreateDataSet(pQuery)
            Catch ex As Exception
                'Throw ex
            End Try
            Return ds
        End Function

        Public Function ConsultaBD(ByVal conn As Conexion, ByVal trans As SqlTransaction, ByVal pQuery As String) As DataSet
            Dim ds As New DataSet
            Try
                Return CreateDataSet(conn, trans, pQuery)
            Catch ex As Exception
                'Throw ex
            End Try
            Return ds
        End Function

        Public Function ConsultaBD(ByVal conn As Conexion, ByVal pQuery As String) As DataSet
            Dim ds As New DataSet
            Try
                Return CreateDataSet(conn, pQuery)
            Catch ex As Exception
                'Throw ex
            End Try
            Return ds
        End Function


        Public Function ConsultaBDOri(ByVal pQuery As String) As DataSet
            Dim ds As New DataSet
            Try
                Return CreateDataSetOri(pQuery)
            Catch ex As Exception
                'Throw ex
            End Try
            Return ds
        End Function

        Public Function CreateDataSetOri(ByVal strSQL As String) As DataSet
            Dim ds2 As New DataSet
            Dim Conn As New ConexionOri
            'SqlCommand es utilizado para ejecutar los comandos SQL
            Dim sqlCmd As New SqlCommand(strSQL, Conn.SQLConn)

            Try
               

                'Se le define el tiempo de espera en segundos para la consulta,
                'el valor default es 30 segundos.
                sqlCmd.CommandTimeout = 3600

                'SqlAdapter utiliza el SqlCommand para llenar el Dataset
                Dim sda As New SqlDataAdapter(sqlCmd)

                'Se llena el dataset
                Dim ds As New DataSet
                sda.Fill(ds)

                Conn.SQLConn.Close()

                Return ds
            Catch ex As Exception
                Conn.SQLConn.Close()
                Return ds2
            End Try
        End Function

        Public Function CreateDataSet(ByVal conn As Conexion, ByVal trans As SqlTransaction, ByVal strSQL As String) As DataSet
            Dim ds2 As New DataSet
            'SqlCommand es utilizado para ejecutar los comandos SQL
            Dim sqlCmd As New SqlCommand(strSQL, conn.SQLConn, trans)

            Try
              

                'Se le define el tiempo de espera en segundos para la consulta,
                'el valor default es 30 segundos.
                sqlCmd.CommandTimeout = 3600

                'SqlAdapter utiliza el SqlCommand para llenar el Dataset
                Dim sda As New SqlDataAdapter(sqlCmd)

                'Se llena el dataset
                Dim ds As New DataSet
                sda.Fill(ds)
                ' conn.SQLConn.Close()

                Return ds
            Catch ex As Exception
                ' conn.SQLConn.Close()
                Return ds2
            End Try
        End Function

        Public Function CreateDataSet(ByVal conn As Conexion, ByVal strSQL As String) As DataSet
            Dim ds2 As New DataSet
            'SqlCommand es utilizado para ejecutar los comandos SQL
            Dim sqlCmd As New SqlCommand(strSQL, conn.SQLConn)

            Try


                'Se le define el tiempo de espera en segundos para la consulta,
                'el valor default es 30 segundos.
                sqlCmd.CommandTimeout = 3600

                'SqlAdapter utiliza el SqlCommand para llenar el Dataset
                Dim sda As New SqlDataAdapter(sqlCmd)

                'Se llena el dataset
                Dim ds As New DataSet
                sda.Fill(ds)
                ' conn.SQLConn.Close()

                Return ds
            Catch ex As Exception
                ' conn.SQLConn.Close()
                Return ds2
            End Try
        End Function

        Public Function CreateDataSet(ByVal strSQL As String) As DataSet
            Dim ds2 As New DataSet
            Dim Conn As New Conexion
            'SqlCommand es utilizado para ejecutar los comandos SQL
            Dim sqlCmd As New SqlCommand(strSQL, Conn.SQLConn)

            Try
            

                'Se le define el tiempo de espera en segundos para la consulta,
                'el valor default es 30 segundos.
                sqlCmd.CommandTimeout = 3600

                'SqlAdapter utiliza el SqlCommand para llenar el Dataset
                Dim sda As New SqlDataAdapter(sqlCmd)

                'Se llena el dataset
                Dim ds As New DataSet
                sda.Fill(ds)

                Conn.SQLConn.Close()

                Return ds
            Catch ex As Exception
                Conn.SQLConn.Close()
                Return ds2
            End Try
        End Function


        Private Function CreateDataSet(ByVal strSQL As String, ByVal strNombeTabla As String) As DataSet
            Dim ds2 As New DataSet
            Dim Conn As New Conexion
            'SqlCommand es utilizado para ejecutar los comandos SQL
            Dim sqlCmd As New SqlCommand(strSQL, Conn.SQLConn)

            Try


                'Se le define el tiempo de espera en segundos para la consulta,
                'el valor default es 30 segundos.
                sqlCmd.CommandTimeout = 3600

                'SqlAdapter utiliza el SqlCommand para llenar el Dataset
                Dim sda As New SqlDataAdapter(sqlCmd)

                'Se llena el dataset
                Dim ds As New DataSet
                sda.Fill(ds, strNombeTabla)
                Conn.SQLConn.Close()

                Return ds
            Catch ex As Exception
                Conn.SQLConn.Close()
                Return ds2
            End Try
        End Function

        Public Function EjecutaComando(ByVal StrSql As String, ByVal conn As Conexion, ByVal trans As SqlTransaction) As String
            Try
                Dim sqlCmd As New SqlCommand(StrSql, conn.SQLConn)
                sqlCmd.CommandType = CommandType.Text
                sqlCmd.Transaction = trans
                sqlCmd.ExecuteNonQuery()

                Conn.SQLConn.Close()

            Catch ex As Exception
                ' Throw ex
                Return ex.Message
            End Try

            Return ""
        End Function
    End Class


   
  
End Namespace



     */
}
