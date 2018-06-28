﻿
CREATE PROCEDURE [dbo].[_Listar_Panel_Demandas_Excel]
(
	@codemp int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT DISTINCT
	(select top 1 usu.USR_NOMBRE from USUARIOS usu with (nolock) where usu.USR_USRID = PD.USRID_REGISTRO) as Responsable,
	(select top 1 ccb_fecing from cartera_clientes_cpbt_doc with (nolock) 
	where ccb_codemp = PD.CODEMP and ccb_pclid = PD.PCLID and ccb_ctcid = PD.CTCID)as FechaAsignacion,
	CASE WHEN 
	(select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
	where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
	and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 )
	> PD.FEC_REGISTRO THEN (select max(CEH_FECHA)-1 from cartera_clientes_estados_historial 
							where ceh_codemp = PD.CODEMP and ceh_pclid = PD.PCLID 
							and ceh_ctcid = PD.CTCID and ceh_estid = 27)
	ELSE (select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
	where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
	and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 ) END as FechaAprobacionTraspaso, 
	PD.FEC_REGISTRO as FechaIngresaJudicial, p.PCL_NOMFANT Cliente, d.CTC_RUT RutDeudor, d.CTC_NOMFANT Deudor,
	(SELECT 
		STUFF((SELECT '- ' + CONVERT(VARCHAR,Asegurados.Asegurado)  
				FROM (select distinct PDD.PANEL_ID PANELID,
					(select REPLACE(sbc.SBC_NOMBRE, ',', ';') from SUBCARTERAS  sbc where sbc.SBC_CODEMP = CPBT.CCB_CODEMP and sbc.SBC_SBCID = CPBT.CCB_SBCID) Asegurado
					from PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
					join CARTERA_CLIENTES_CPBT_DOC CPBT with (nolock)
					ON PDD.CODEMP = CPBT.CCB_CODEMP
					AND PDD.PCLID = cpbt.CCB_PCLID
					and PDD.CTCID = cpbt.CCB_CTCID
					and PDD.CCBID = cpbt.CCB_CCBID
					where PANEL_ID = PD.PANEL_ID
					) Asegurados
				FOR XML PATH('')), 1, 1, '') [Asegurado]) Asegurado, 
	 (select TDOC.TPC_NOMBRE from TIPOS_CPBTDOC TDOC with (nolock) where TDOC.TPC_CODEMP = PD.CODEMP AND TDOC.TPC_TPCID = PD.TPCID) TipoDocumento, 
	 com.COM_NOMBRE Comuna, reg.REG_NOMBRE Region,
	 (select top 1 usu.USR_NOMBRE from USUARIOS usu with (nolock) where usu.USR_USRID = PDDE.USRID_ENCARGADO) as EncargadoCofeccion, 
	 PDDE.FEC_ENVIO FechaEnvioConfeccion, PDDE.FEC_ENTREGA FechaEntrega, 
	 PDDE.FEC_INGRESO_TRIBUNAL FechaIngresoTribunal, PDDE.COMENTARIOS,
	 CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) > 0 ) THEN 
		'S' ELSE 'N' END Correcciones,
	 CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) = -1 ) THEN 
		0 ELSE 
		(select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) END countCorrecciones,

		R.ROL_NUMERO, T.TRB_NOMBRE
		
FROM PANEL_DEMANDA PD with (nolock)

JOIN PANEL_DEMANDA_DOCUMENTOS PDD with (nolock) ON PD.PANEL_ID = PDD.PANEL_ID
JOIN CARTERA_CLIENTES_CPBT_DOC cpbt with (nolock) ON 
	PDD.CODEMP = CPBT.CCB_CODEMP
	AND PDD.PCLID = cpbt.CCB_PCLID
	and PDD.CTCID = cpbt.CCB_CTCID
	and PDD.CCBID = cpbt.CCB_CCBID
JOIN PROVCLI p with (nolock) ON 
	cpbt.CCB_CODEMP = p.PCL_CODEMP
	AND cpbt.CCB_PCLID =  p.PCL_PCLID
JOIN DEUDORES  d  with (nolock) ON 
	cpbt.CCB_CTCID = d.CTC_CTCID
	AND cpbt.CCB_CODEMP = d.CTC_CODEMP
JOIN COMUNA com ON
	d.CTC_COMID = com.COM_COMID
JOIN CIUDAD ciu ON
	com.COM_CIUID = ciu.CIU_CIUID
JOIN REGION reg ON 
	ciu.CIU_REGID = reg.REG_REGID
LEFT JOIN PANEL_DEMANDA_DETALLE PDDE ON 
	PD.PANEL_ID = PDDE.PANEL_ID
LEFT JOIN ROL R on R.ROL_ROLID = PDDE.ROLID
LEFT JOIN TRIBUNALES T on T.TRB_TRBID = R.ROL_TRBID

WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = 'NO')
AND PDD.CODEMP = @codemp
AND PDD.ESTADO = 'ACT'
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PDD.CODEMP = @codemp
AND PDD.ESTADO = 'ACT'
END