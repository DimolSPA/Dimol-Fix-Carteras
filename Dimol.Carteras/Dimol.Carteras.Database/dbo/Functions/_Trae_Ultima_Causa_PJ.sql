-- =============================================
-- Author:		FM
-- Create date: 20140509
-- Description:	TRae descripcion de agrupa accion
-- =============================================
CREATE FUNCTION [dbo].[_Trae_Ultima_Causa_PJ] 
(
	@rut varchar(20)
)
RETURNS varchar(500)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @causa varchar(500)

	-- Add the T-SQL statements to compute the return value here

	set @causa =(
	    select top 1 R.TIPO + '-'+ CONVERT(VARCHAR, R.NUMERO) + '-' + CONVERT(VARCHAR, R.ANIO) + '/' + T.TRIBUNAL
		from [10.0.1.11].[PoderJudicial].dbo.PODER_JUDICIAL_LITIGANTE L with(nolock)
		INNER JOIN  [10.0.1.11].[PoderJudicial].dbo.PODER_JUDICIAL_ROL R with(nolock)
		ON L.ID_CAUSA = R.ID_CAUSA
		INNER JOIN [10.0.1.11].[PoderJudicial].dbo.PODER_JUDICIAL_TRIBUNAL T with(nolock)
		ON R.TRIBUNAL = T.ID_TRIBUNAL
		INNER JOIN [10.0.1.11].[PoderJudicial].dbo.PODER_JUDICIAL_LITIGANTE L2 with(nolock)
		ON L.ID_CAUSA = L2.ID_CAUSA
		where L.PARTICIPANTE = 'DDO.'
		and L.RUT = @rut
		AND L2.PARTICIPANTE = 'DTE.'
		order by r.FECHA_INGRESO desc)

	-- Return the result of the function
	RETURN @causa

END
