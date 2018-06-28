CREATE PROCEDURE [dbo].[_Insertar_Visita_Terreno_Formulario](
@fecEnvio datetime,
@formularioNombre varchar(100),
@gestor varchar(100),
@cliente varchar(100),
@position varchar(100),
@direccion varchar(100),
@foto1 varchar(100),
@foto2 varchar(100),
@foto3 varchar(100),
@foto4 varchar(100),
@estado varchar(20),
@visita varchar(20),
@direccionActual varchar(2),
@comentarios varchar(1000),
@direccion1 varchar(100),
@comuna1 varchar(30),
@direccion2 varchar(100),
@comuna2 varchar(30),
@userId int,
@idCarga int,
@formId varchar(500))

AS
BEGIN
declare 
@formularioId int = 0,
@formularioClienteExist int = 0

	--set @formularioClienteExist = (SELECT count(formularioId) 
	--								FROM VISITA_TERRENO_FORMULARIO
	--								WHERE RTRIM(LTRIM(CLIENTE)) = RTRIM(LTRIM(@cliente)))
	IF @formularioClienteExist = 0
	BEGIN
		
		set @formularioId = (SELECT IsNull(Max(FORMULARIOID)+1, 1)
						FROM VISITA_TERRENO_FORMULARIO)
		
		INSERT INTO [dbo].[VISITA_TERRENO_FORMULARIO]
				   ([FEC_ENVIO]
				   ,[FORMULARIOID]
				   ,[NOMBRE]
				   ,[GESTOR]
				   ,[CLIENTE]
				   ,[POSICION]
				   ,[DIRECCION]
				   ,[FOTO1]
				   ,[FOTO2]
				   ,[FOTO3]
				   ,[FOTO4]
				   ,[ESTADO]
				   ,[VISITA]
				   ,[DIRECCIONACTUAL]
				   ,[COMENTARIOS]
				   ,[DIRECCION1]
				   ,[COMUNA1]
				   ,[DIRECCION2]
				   ,[COMUNA2]
				   ,[USRID_DESCARGA]
				   ,[IDCARGA]
				   ,[FORMID])
			 VALUES
				   (@fecEnvio
				   ,@formularioId
				   ,@formularioNombre
				   ,@gestor
				   ,@cliente
				   ,@position
				   ,@direccion
				   ,@foto1
				   ,@foto2
				   ,@foto3
				   ,@foto4
				   ,@estado
				   ,@visita
				   ,@direccionActual
				   ,@comentarios
				   ,@direccion1
				   ,@comuna1
				   ,@direccion2
				   ,@comuna2
				   ,@userId
				   ,@idCarga
				   ,@formId)

		select @formularioId formularioId
	END
	ELSE
	BEGIN
		select 0 formularioId
	END
END
