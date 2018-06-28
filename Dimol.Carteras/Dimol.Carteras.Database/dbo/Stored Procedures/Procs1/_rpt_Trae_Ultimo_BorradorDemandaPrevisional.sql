
CREATE PROCEDURE [dbo].[_rpt_Trae_Ultimo_BorradorDemandaPrevisional](
	@codemp integer,
	@idBorrador integer,
	@idDP integer
) AS

	SELECT TOP 1 isnull([HTML], '') html
	FROM [PANEL_DEMANDA_PREVISIONAL_CONFECCION]
	WHERE CODEMP = @codemp
	AND ID_BORRADOR = @idBorrador
	AND ID_PANEL_PREVISIONAL = @idDP
	ORDER BY FECHA_CREACION DESC