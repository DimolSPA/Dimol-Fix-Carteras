
 CREATE procedure [dbo].[_Insert_Bajas_Cpbt_Doc](@pclid numeric(15), @ctcid numeric(15), @ccbid int, @numero varchar(30), @fecha_reclamo datetime, @saldo decimal(15,2), @usrid int, @fecha_pago datetime, @tipo_banco int, @id_cuenta int, @observaciones text, @codmon int, @codid int, @estid int, @fecha datetime) as 
 INSERT INTO BAJAS_CPBT_DOC (PCLID,	CTCID, CCBID, NUMERO, FECHA_RECLAMO, SALDO, USRID, FECHA_PAGO, TIPO_BANCO, ID_CUENTA, OBSERVACIONES, CODMON, CODID, ESTID, FECHA) 
 VALUES (@pclid, @ctcid, @ccbid, @numero, @fecha_reclamo, @saldo, @usrid, @fecha_pago, @tipo_banco, @id_cuenta, @observaciones, @codmon, @codid, @estid, @fecha) 

