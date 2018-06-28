-- =============================================
-- Author:		César León
-- Create date: 14-02-2018
-- Description:	Lista los tribunales según idCompetencia
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_TribunalesPorIdCompetencia]
(
	@codEmp int,
	@idCompetencia int = 3 -- Por defecto es "Civil"
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT T.TRB_TRBID, T.TRB_NOMBRE
	FROM TRIBUNALES as T 
	whERE t.ID_COMPETENCIA = @idCompetencia
	AND T.TRB_CODEMP = @codEmp
	ORDER by T.TRB_NOMBRE ASC
	-- original cesar
	--SELECT T.TRB_TRBID, T.TRB_NOMBRE
	--FROM COMPETENCIA as COM
	--JOIN CORTE as COR on COM.ID_COMPETENCIA = COR.ID_COMPETENCIA
	--JOIN TRIBUNALES as T on COR.ID_CORTE = T.ID_CORTE
	--WHERE COM.ID_COMPETENCIA = @idCompetencia
	--AND T.TRB_CODEMP = @codEmp
	--ORDER by T.TRB_NOMBRE desc
END
