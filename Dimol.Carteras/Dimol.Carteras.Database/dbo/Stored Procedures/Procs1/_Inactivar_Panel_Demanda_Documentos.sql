CREATE PROCEDURE [dbo].[_Inactivar_Panel_Demanda_Documentos](
@codemp int,
@pclid numeric(15,0),
@ctcid numeric(15,0),
@ccbid int)

AS
BEGIN
	
	-- INSERTAR DATOS DE DOCUMENTOS
	UPDATE PANEL_DEMANDA_DOCUMENTOS
	SET ESTADO = 'INA'
	WHERE CODEMP = @codemp
	AND PCLID = @pclid
	AND CTCID = @ctcid
	AND CCBID = @ccbid
	
END
