CREATE Procedure [dbo].[_rpt_Insertar_DemandaMasivaConfeccion](
	@codemp int,
	@id_borrador int,
	@panelMasivoId int,
	@html text,
	@usrid int
)
AS
	DECLARE @id_version int
 
	SELECT TOP 1 @id_version = ID_VERSION FROM BORRADOR_VERSION WHERE CODEMP = @codemp and ID_BORRADOR = @id_borrador ORDER by ID_VERSION desc
  
	INSERT INTO [PANEL_DEMANDA_MASIVA_CONFECCION]
		([CODEMP]
		,[ID_BORRADOR]
		,[ID_VERSION]
		,[ID_PANEL_MASIVO]
		,[HTML]
		,[FECHA_CREACION]
		,[USER_CREACION])
	VALUES
		(@codemp
		,@id_borrador
		,@id_version
		,@panelMasivoId
		,@html
		,GETDATE()
		,@usrid)
