CREATE PROCEDURE [dbo].[_Listar_PanelAlerta]
(
@codemp int
)
AS
BEGIN
	SET NOCOUNT ON;
	---Alerta Cabecera
; WITH DemandaConfeccion AS (SELECT PD.PANEL_ID, dbo._Trae_DiasSemana(PDD.FEC_ENVIO, ISNULL(PDD.FEC_ENTREGA, GETDATE())) As DiasTranscurso,
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) = -1 ) THEN 
		0 ELSE 
		(select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) END CountCorrecciones
	
from PANEL_DEMANDA PD with (nolock)
Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = 'NO') 
AND PD.CODEMP = @codemp
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PD.CODEMP = @codemp)
SELECT 
	(select count (CCB_CTCID) Traspasos 
		from (SELECT distinct CCB_CTCID
				FROM cartera_clientes_documentos_cpbt_doc,
					 idiomas
				WHERE tci_idid = idiomas.idi_idid
				AND eci_idid = idiomas.idi_idid
				AND mci_idid = idiomas.idi_idid
				AND ccb_codemp = 1
				AND ccb_estcpbt = 'V'
				AND ESTIJ = 226
				AND idiomas.idi_idid = 1                             
				UNION
				SELECT distinct CCB_CTCID
				FROM cartera_clientes_documentos_cpbt_doc,
					 idiomas
				WHERE tci_idid = idiomas.idi_idid
				AND eci_idid = idiomas.idi_idid
				AND mci_idid = idiomas.idi_idid
				AND ccb_codemp = 1
				AND ccb_estcpbt = 'V'
				AND ECI_ESTID = 226
				AND idiomas.idi_idid = 1) traspasos) TotalTraspaso,
	(SELECT COUNT(PANEL_ID)
	FROM PANEL_DEMANDA with (nolock)
	WHERE FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
	AND (CURSODEMANDA is NULL or CURSODEMANDA = 'NO')
	AND CODEMP =  @codemp
	OR FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
	FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
	AND CODEMP =  @codemp) DemandasProceso,
	Cast(ROUND(AVG(CAST(DiasTranscurso AS FLOAT)),0)as varchar(5)) + ' dias' as PromedioConfeccionDias, 
	SUM(CountCorrecciones) CantCorrecciones, 
	Cast(Cast((SUM(CountCorrecciones) * 100.00)/(select count(*) from  DemandaConfeccion)as decimal(18,2)) as varchar(5)) + ' %' PromedioCorrecciones
FROM DemandaConfeccion
END