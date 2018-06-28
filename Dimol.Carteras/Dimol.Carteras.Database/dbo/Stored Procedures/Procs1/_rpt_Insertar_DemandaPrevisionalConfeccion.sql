
-- =============================================
-- Author:		César León
-- Create date: 20/03/2018
-- Description:	Guarda el borrador de una demanda previsional. Inserta un registro en DemandaPrevisionalConfección.
-- =============================================
CREATE Procedure [dbo].[_rpt_Insertar_DemandaPrevisionalConfeccion](
	@codemp int,
	@id_borrador int,
	@panelPrevisionalId int,
	@html text,
	@usrid int
)
AS
	DECLARE @id_version int
 
	SELECT TOP 1 @id_version = ID_VERSION FROM BORRADOR_VERSION WHERE CODEMP = @codemp and ID_BORRADOR = @id_borrador ORDER by ID_VERSION desc
  
	INSERT INTO [PANEL_DEMANDA_PREVISIONAL_CONFECCION]
		([CODEMP]
		,[ID_BORRADOR]
		,[ID_VERSION]
		,[ID_PANEL_PREVISIONAL]
		,[HTML]
		,[FECHA_CREACION]
		,[USER_CREACION])
	VALUES
		(@codemp
		,@id_borrador
		,@id_version
		,@panelPrevisionalId
		,@html
		,GETDATE()
		,@usrid)