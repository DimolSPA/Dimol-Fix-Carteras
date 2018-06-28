CREATE PROCEDURE [dbo].[_rpt_Trae_Ultimo_BorradorDemandaMasiva](
	@codemp integer,
	@idBorrador integer,
	@idDM integer
) AS
	SELECT TOP 1 isnull([HTML], '') html
	FROM [PANEL_DEMANDA_MASIVA_CONFECCION]
	WHERE CODEMP = @codemp
	AND ID_BORRADOR = @idBorrador
	AND ID_PANEL_MASIVO = @idDM
	ORDER BY FECHA_CREACION DESC
