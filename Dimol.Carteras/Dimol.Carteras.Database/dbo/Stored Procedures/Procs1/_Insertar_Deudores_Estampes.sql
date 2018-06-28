
CREATE procedure [dbo].[_Insertar_Deudores_Estampes] (@codemp int, @pclid numeric(15), @ctcid numeric(15), @rolid int, @path varchar(300), @nombre varchar(300), @ext varchar(5)) as
		
	INSERT INTO DEUDORES_ESTAMPES (DDE_CODEMP, DDE_PCLID, DDE_CTCID, DDE_ROLID, DDE_PATH, DDE_NOMBRE, DDE_EXT) 
	VALUES(@codemp, @pclid, @ctcid, @rolid, @path, @nombre, @ext)

	select SCOPE_IDENTITY() DDE_DDEID
