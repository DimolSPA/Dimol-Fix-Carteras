CREATE PROCEDURE [dbo].[_Listar_Tribunales_Prefijo] 
(
	@texto varchar(200),
	@codemp as integer,
	@idCompetencia int = 6 -- Por defecto es "Civil"
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @nombre varchar(250) = '%' + @texto + '%'

	SELECT distinct trb.trb_trbid, trb.trb_nombre 
	FROM tribunales trb with(nolock)
	JOIN tribunal_ente tbe on trb.TRB_CODEMP = tbe.CODEMP and trb.TRB_TRBID = tbe.TRBID
	WHERE trb.trb_codemp = @codemp 
	AND trb.trb_nombre like @nombre
	AND trb.ID_COMPETENCIA = @idCompetencia
	ORDER by trb.TRB_NOMBRE ASC
END