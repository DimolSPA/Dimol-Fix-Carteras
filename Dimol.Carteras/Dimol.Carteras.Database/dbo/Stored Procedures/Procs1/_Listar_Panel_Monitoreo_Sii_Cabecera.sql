CREATE PROCEDURE [dbo].[_Listar_Panel_Monitoreo_Sii_Cabecera]
AS
BEGIN
	SET NOCOUNT ON;
;WITH SiiRecolectadas AS (select  COUNT(distinct CTCID) CantRut
							from sii..CABECERA with(nolock)
							where FECHA_CONSULTA >= DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), -1)
							)
SELECT 
	CASE WHEN CantRut > 0 THEN 'SI' ELSE 'NO' END Recolecto, 
	CantRut,
	(select  COUNT(distinct CTCID) CantRut
							from sii..CABECERA with(nolock)
							where FECHA_CONSULTA >= (DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)) and --Mes actual
								FECHA_CONSULTA < (DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0))) CantMesActual,
	(select  COUNT(distinct CTCID) CantRut
							from sii..CABECERA with(nolock)) Acumulativas,
	(select top 1  CAST(FECHA_CONSULTA AS DATE) from sii..CABECERA order by FECHA_CONSULTA desc) FecUtimaActualizacion,
	CAST(DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), 0)AS DATE) FecProximaActualizacion
FROM SiiRecolectadas
END
