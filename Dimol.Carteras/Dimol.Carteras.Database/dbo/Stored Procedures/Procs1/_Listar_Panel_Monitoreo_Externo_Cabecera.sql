CREATE PROCEDURE [dbo].[_Listar_Panel_Monitoreo_Externo_Cabecera]
AS
BEGIN
	SET NOCOUNT ON;
;WITH CausasRecolectadas AS (select  COUNT(distinct ID_CAUSA) CantCausas
							from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_litigante with(nolock)
							where FECHA > (DATEADD(DD, -1, GETDATE()))
							)
SELECT 
	CASE WHEN CantCausas > 0 THEN 'SI' ELSE 'NO' END Recolecto, 
	CantCausas,
	(select  COUNT(distinct ID_CAUSA) CantCausas
							from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_litigante with(nolock)
							where FECHA >= (DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)) and --Mes actual
								FECHA < (DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0))) CantMesActual
FROM CausasRecolectadas
END
