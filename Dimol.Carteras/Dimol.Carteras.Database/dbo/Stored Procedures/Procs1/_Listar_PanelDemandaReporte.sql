CREATE PROCEDURE [dbo].[_Listar_PanelDemandaReporte]
(@tipoReporte int)
AS
BEGIN

SET NOCOUNT ON;
declare @query varchar(8000);

if @tipoReporte = 1 --Total Mes Anterior
begin
set @query = 'SELECT PD.PANEL_ID, p.PCL_NOMFANT CLIENTE, d.CTC_RUT RutDeudor, d.CTC_NOMFANT Deudor, 
    (SELECT 
		STUFF((SELECT ''- '' + CONVERT(VARCHAR,Asegurados.Asegurado)  
				FROM (select distinct PDD.PANEL_ID PANELID,
					(select REPLACE(sbc.SBC_NOMBRE, '','', '';'') from SUBCARTERAS  sbc where sbc.SBC_CODEMP = CPBT.CCB_CODEMP and sbc.SBC_SBCID = CPBT.CCB_SBCID) Asegurado
					from PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
					join CARTERA_CLIENTES_CPBT_DOC CPBT with (nolock)
					ON PDD.CODEMP = CPBT.CCB_CODEMP
					AND PDD.PCLID = cpbt.CCB_PCLID
					and PDD.CTCID = cpbt.CCB_CTCID
					and PDD.CCBID = cpbt.CCB_CCBID
					where PANEL_ID = PD.PANEL_ID
					) Asegurados
				FOR XML PATH('''')), 1, 1, '''') [Asegurado]) As Asegurado,
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
	PD.FEC_REGISTRO As IngresoJudicial, PDDE.FEC_ENVIO FechaEnvio,
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) > 0 ) THEN 
		''S'' ELSE ''N'' END Correcciones, PDDE.FEC_INGRESO_TRIBUNAL as FechaIngresoTribunal, PDDE.FEC_ENTREGA FechaEntrega, NULL Encargado,
	0 DiasTranscurso, 0 CountCorrecciones, PDDE.COMENTARIOS, 0 DiasTranscurso2, 0 TrackingDemanda
FROM PANEL_DEMANDA PD with (nolock)
JOIN PROVCLI p with (nolock)
ON PD.CODEMP = p.PCL_CODEMP
AND PD.PCLID =  p.PCL_PCLID
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
LEFT JOIN PANEL_DEMANDA_DETALLE PDDE
ON PD.PANEL_ID = PDDE.PANEL_ID
WHERE PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
AND (CURSODEMANDA is NULL or CURSODEMANDA = ''NO'')
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 2 --Mes Actual
begin
set @query = 'SELECT PD.PANEL_ID, p.PCL_NOMFANT CLIENTE, d.CTC_RUT RutDeudor, d.CTC_NOMFANT Deudor, 
    (SELECT 
		STUFF((SELECT ''- '' + CONVERT(VARCHAR,Asegurados.Asegurado)  
				FROM (select distinct PDD.PANEL_ID PANELID,
					(select REPLACE(sbc.SBC_NOMBRE, '','', '';'') from SUBCARTERAS  sbc where sbc.SBC_CODEMP = CPBT.CCB_CODEMP and sbc.SBC_SBCID = CPBT.CCB_SBCID) Asegurado
					from PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
					join CARTERA_CLIENTES_CPBT_DOC CPBT with (nolock)
					ON PDD.CODEMP = CPBT.CCB_CODEMP
					AND PDD.PCLID = cpbt.CCB_PCLID
					and PDD.CTCID = cpbt.CCB_CTCID
					and PDD.CCBID = cpbt.CCB_CCBID
					where PANEL_ID = PD.PANEL_ID
					) Asegurados
				FOR XML PATH('''')), 1, 1, '''') [Asegurado]) As Asegurado,
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
	PD.FEC_REGISTRO As IngresoJudicial, PDDE.FEC_ENVIO FechaEnvio,
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) > 0 ) THEN 
		''S'' ELSE ''N'' END Correcciones,
	PDDE.FEC_INGRESO_TRIBUNAL As FechaIngresoTribunal,
	PDDE.FEC_ENTREGA FechaEntrega, NULL Encargado,
	0 DiasTranscurso, 0 CountCorrecciones, PDDE.COMENTARIOS, 0 DiasTranscurso2, 0 TrackingDemanda
FROM PANEL_DEMANDA PD with (nolock)
JOIN PROVCLI p with (nolock)
ON PD.CODEMP = p.PCL_CODEMP
AND PD.PCLID =  p.PCL_PCLID
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
LEFT JOIN PANEL_DEMANDA_DETALLE PDDE
ON PD.PANEL_ID = PDDE.PANEL_ID
WHERE PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 3 --total Demandas
begin
set @query = 'SELECT PD.PANEL_ID, p.PCL_NOMFANT CLIENTE, d.CTC_RUT RutDeudor, d.CTC_NOMFANT Deudor, 
    (SELECT 
		STUFF((SELECT ''- '' + CONVERT(VARCHAR,Asegurados.Asegurado)  
				FROM (select distinct PDD.PANEL_ID PANELID,
					(select REPLACE(sbc.SBC_NOMBRE, '','', '';'') from SUBCARTERAS  sbc where sbc.SBC_CODEMP = CPBT.CCB_CODEMP and sbc.SBC_SBCID = CPBT.CCB_SBCID) Asegurado
					from PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
					join CARTERA_CLIENTES_CPBT_DOC CPBT with (nolock)
					ON PDD.CODEMP = CPBT.CCB_CODEMP
					AND PDD.PCLID = cpbt.CCB_PCLID
					and PDD.CTCID = cpbt.CCB_CTCID
					and PDD.CCBID = cpbt.CCB_CCBID
					where PANEL_ID = PD.PANEL_ID
					) Asegurados
				FOR XML PATH('''')), 1, 1, '''') [Asegurado]) As Asegurado,
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
	PD.FEC_REGISTRO As IngresoJudicial, PDDE.FEC_ENVIO FechaEnvio, PDDE.FEC_INGRESO_TRIBUNAL FechaIngresoTribunal,
	PDDE.FEC_ENTREGA FechaEntrega,
	NULL Encargado, CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) > 0 ) THEN 
		''S'' ELSE ''N'' END Correcciones, 0 DiasTranscurso, 0 CountCorrecciones, PDDE.COMENTARIOS, 0 DiasTranscurso2,
		0 TrackingDemanda
FROM PANEL_DEMANDA PD with (nolock)
JOIN PROVCLI p with (nolock)
ON PD.CODEMP = p.PCL_CODEMP
AND PD.PCLID =  p.PCL_PCLID
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
LEFT JOIN PANEL_DEMANDA_DETALLE PDDE
ON PD.PANEL_ID = PDDE.PANEL_ID
WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = ''NO'') 
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 4 --Demandas no Asignadas
begin
set @query = 'SELECT PD.PANEL_ID, p.PCL_NOMFANT CLIENTE, d.CTC_RUT RutDeudor, d.CTC_NOMFANT Deudor, 
    (SELECT 
		STUFF((SELECT ''- '' + CONVERT(VARCHAR,Asegurados.Asegurado)  
				FROM (select distinct PDD.PANEL_ID PANELID,
					(select REPLACE(sbc.SBC_NOMBRE, '','', '';'') from SUBCARTERAS  sbc where sbc.SBC_CODEMP = CPBT.CCB_CODEMP and sbc.SBC_SBCID = CPBT.CCB_SBCID) Asegurado
					from PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
					join CARTERA_CLIENTES_CPBT_DOC CPBT with (nolock)
					ON PDD.CODEMP = CPBT.CCB_CODEMP
					AND PDD.PCLID = cpbt.CCB_PCLID
					and PDD.CTCID = cpbt.CCB_CTCID
					and PDD.CCBID = cpbt.CCB_CCBID
					where PANEL_ID = PD.PANEL_ID
					) Asegurados
				FOR XML PATH('''')), 1, 1, '''') [Asegurado]) As Asegurado,
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
	PD.FEC_REGISTRO As IngresoJudicial, dbo._Trae_DiasSemana((select top 1 ccb_fecing 
														from cartera_clientes_cpbt_doc with (nolock)
														where ccb_codemp = PD.CODEMP 
														and ccb_pclid = PD.PCLID 
														and ccb_ctcid = PD.CTCID), PD.FEC_REGISTRO) As DiasTranscurso,
	NULL FechaEnvio, NULL FechaIngresoTribunal, NULL FechaEntrega,
	NULL Encargado, NULL Correcciones, 0 CountCorrecciones, PDD.COMENTARIOS, 0 DiasTranscurso2, 0 TrackingDemanda
from PANEL_DEMANDA PD with (nolock)
JOIN PROVCLI p with (nolock)
ON PD.CODEMP = p.PCL_CODEMP
AND PD.PCLID =  p.PCL_PCLID
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
left Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = ''NO'') 
AND PDD.USRID_ENCARGADO is null
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PDD.USRID_ENCARGADO is null
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 5 -- Demandas Asignadas
begin
set @query = 'SELECT PD.PANEL_ID, p.PCL_NOMFANT CLIENTE, d.CTC_RUT RutDeudor, d.CTC_NOMFANT Deudor, 
    (SELECT 
		STUFF((SELECT ''- '' + CONVERT(VARCHAR,Asegurados.Asegurado)  
				FROM (select distinct PDD.PANEL_ID PANELID,
					(select REPLACE(sbc.SBC_NOMBRE, '','', '';'') from SUBCARTERAS  sbc where sbc.SBC_CODEMP = CPBT.CCB_CODEMP and sbc.SBC_SBCID = CPBT.CCB_SBCID) Asegurado
					from PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
					join CARTERA_CLIENTES_CPBT_DOC CPBT with (nolock)
					ON PDD.CODEMP = CPBT.CCB_CODEMP
					AND PDD.PCLID = cpbt.CCB_PCLID
					and PDD.CTCID = cpbt.CCB_CTCID
					and PDD.CCBID = cpbt.CCB_CCBID
					where PANEL_ID = PD.PANEL_ID
					) Asegurados
				FOR XML PATH('''')), 1, 1, '''') [Asegurado]) As Asegurado,
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
	NULL As IngresoJudicial, PDD.FEC_ENVIO FechaEnvio,
	NULL FechaIngresoTribunal, NULL FechaEntrega,
	(select top 1 usu.USR_NOMBRE from USUARIOS usu with (nolock) where usu.USR_USRID = PDD.USRID_ENCARGADO) Encargado , NULL Correcciones, 0 DiasTranscurso, 0 CountCorrecciones, PDD.COMENTARIOS, 0 DiasTranscurso2,
	0 TrackingDemanda
from PANEL_DEMANDA PD with (nolock)
JOIN PROVCLI p with (nolock)
ON PD.CODEMP = p.PCL_CODEMP
AND PD.PCLID =  p.PCL_PCLID
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
left Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
	AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = ''NO'')
	AND PDD.PANEL_ID IS NOT NULL
	OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
	PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
	AND PDD.PANEL_ID IS NOT NULL
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 6 --Demandas sin confeccionar
begin
set @query = 'SELECT PD.PANEL_ID, p.PCL_NOMFANT CLIENTE, d.CTC_RUT RutDeudor, d.CTC_NOMFANT Deudor, 
    (SELECT 
		STUFF((SELECT ''- '' + CONVERT(VARCHAR,Asegurados.Asegurado)  
				FROM (select distinct PDD.PANEL_ID PANELID,
					(select REPLACE(sbc.SBC_NOMBRE, '','', '';'') from SUBCARTERAS  sbc where sbc.SBC_CODEMP = CPBT.CCB_CODEMP and sbc.SBC_SBCID = CPBT.CCB_SBCID) Asegurado
					from PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
					join CARTERA_CLIENTES_CPBT_DOC CPBT with (nolock)
					ON PDD.CODEMP = CPBT.CCB_CODEMP
					AND PDD.PCLID = cpbt.CCB_PCLID
					and PDD.CTCID = cpbt.CCB_CTCID
					and PDD.CCBID = cpbt.CCB_CCBID
					where PANEL_ID = PD.PANEL_ID
					) Asegurados
				FOR XML PATH('''')), 1, 1, '''') [Asegurado]) As Asegurado,
	(select top 1 usu.USR_NOMBRE from USUARIOS usu with (nolock) where usu.USR_USRID = PDD.USRID_ENCARGADO) Encargado,
	PDD.FEC_ENVIO FechaEnvio, 
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) > 0 ) THEN 
		''S'' ELSE ''N'' END Correcciones, dbo._Trae_DiasSemana(PDD.FEC_ENVIO,GETDATE()) As DiasTranscurso,
		NULL FechaAsignacion, NULL FechaAprobacionTraspaso, NULL IngresoJudicial, NULL FechaIngresoTribunal,
		NULL FechaEntrega, 0 CountCorrecciones, PDD.COMENTARIOS, 0 DiasTranscurso2, 0 TrackingDemanda
from PANEL_DEMANDA PD with (nolock)
JOIN PROVCLI p with (nolock)
ON PD.CODEMP = p.PCL_CODEMP
AND PD.PCLID =  p.PCL_PCLID
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = ''NO'') 
AND PDD.FEC_ENTREGA is null
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PDD.FEC_ENTREGA is null
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 7 --Demandas Confeccionadas
begin
set @query = 'SELECT PD.PANEL_ID, p.PCL_NOMFANT CLIENTE, d.CTC_RUT RutDeudor, d.CTC_NOMFANT Deudor, 
    (SELECT 
		STUFF((SELECT ''- '' + CONVERT(VARCHAR,Asegurados.Asegurado)  
				FROM (select distinct PDD.PANEL_ID PANELID,
					(select REPLACE(sbc.SBC_NOMBRE, '','', '';'') from SUBCARTERAS  sbc where sbc.SBC_CODEMP = CPBT.CCB_CODEMP and sbc.SBC_SBCID = CPBT.CCB_SBCID) Asegurado
					from PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
					join CARTERA_CLIENTES_CPBT_DOC CPBT with (nolock)
					ON PDD.CODEMP = CPBT.CCB_CODEMP
					AND PDD.PCLID = cpbt.CCB_PCLID
					and PDD.CTCID = cpbt.CCB_CTCID
					and PDD.CCBID = cpbt.CCB_CCBID
					where PANEL_ID = PD.PANEL_ID
					) Asegurados
				FOR XML PATH('''')), 1, 1, '''') [Asegurado]) As Asegurado,
	PDD.FEC_ENVIO FechaEnvio, PDD.FEC_ENTREGA FechaEntrega,
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) > 0 ) THEN 
		''S'' ELSE ''N'' END Correcciones,
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) = -1 ) THEN 
		0 ELSE 
		(select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) END CountCorrecciones,
	NULL FechaAsignacion, NULL FechaAprobacionTraspaso, NULL IngresoJudicial, NULL FechaIngresoTribunal, NULL Encargado,
	0 DiasTranscurso, PDD.COMENTARIOS, 0 DiasTranscurso2, 0 TrackingDemanda
from PANEL_DEMANDA PD with (nolock)
JOIN PROVCLI p with (nolock)
ON PD.CODEMP = p.PCL_CODEMP
AND PD.PCLID =  p.PCL_PCLID
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = ''NO'')
AND PDD.FEC_ENTREGA IS NOT NULL
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PDD.FEC_ENTREGA IS NOT NULL
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 8 --Demandas Ingresadas a Tribunal
begin
set @query = 'SELECT PD.PANEL_ID, p.PCL_NOMFANT CLIENTE, d.CTC_RUT RutDeudor, d.CTC_NOMFANT Deudor, 
    (SELECT 
		STUFF((SELECT ''- '' + CONVERT(VARCHAR,Asegurados.Asegurado)  
				FROM (select distinct PDD.PANEL_ID PANELID,
					(select REPLACE(sbc.SBC_NOMBRE, '','', '';'') from SUBCARTERAS  sbc where sbc.SBC_CODEMP = CPBT.CCB_CODEMP and sbc.SBC_SBCID = CPBT.CCB_SBCID) Asegurado
					from PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
					join CARTERA_CLIENTES_CPBT_DOC CPBT with (nolock)
					ON PDD.CODEMP = CPBT.CCB_CODEMP
					AND PDD.PCLID = cpbt.CCB_PCLID
					and PDD.CTCID = cpbt.CCB_CTCID
					and PDD.CCBID = cpbt.CCB_CCBID
					where PANEL_ID = PD.PANEL_ID
					) Asegurados
				FOR XML PATH('''')), 1, 1, '''') [Asegurado]) As Asegurado,
	PDD.FEC_ENVIO FechaEnvio, PDD.FEC_ENTREGA FechaEntrega, PDD.FEC_INGRESO_TRIBUNAL FechaIngresoTribunal,
	NULL FechaAsignacion, NULL FechaAprobacionTraspaso, PD.FEC_REGISTRO IngresoJudicial, NULL Encargado, NULL Correcciones,
	dbo._Trae_DiasSemana(PDD.FEC_ENVIO, PDD.FEC_ENTREGA) DiasTranscurso, 0 CountCorrecciones, PDD.COMENTARIOS,
	dbo._Trae_DiasSemana(PDD.FEC_ENTREGA, PDD.FEC_INGRESO_TRIBUNAL) DiasTranscurso2,
	dbo._Trae_DiasSemana(PD.FEC_REGISTRO, PDD.FEC_INGRESO_TRIBUNAL) TrackingDemanda
from PANEL_DEMANDA PD with (nolock)
JOIN PROVCLI p with (nolock)
ON PD.CODEMP = p.PCL_CODEMP
AND PD.PCLID =  p.PCL_PCLID
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = ''NO'')
AND PDD.FEC_ENTREGA IS NOT NULL
AND PDD.FEC_INGRESO_TRIBUNAL IS NOT NULL
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PDD.FEC_ENTREGA IS NOT NULL
AND PDD.FEC_INGRESO_TRIBUNAL IS NOT NULL
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 9 --Demandas No Ingresadas a Tribunal
begin
set @query = 'SELECT PD.PANEL_ID, p.PCL_NOMFANT CLIENTE, d.CTC_RUT RutDeudor, d.CTC_NOMFANT Deudor, 
    (SELECT 
		STUFF((SELECT ''- '' + CONVERT(VARCHAR,Asegurados.Asegurado)  
				FROM (select distinct PDD.PANEL_ID PANELID,
					(select REPLACE(sbc.SBC_NOMBRE, '','', '';'') from SUBCARTERAS  sbc where sbc.SBC_CODEMP = CPBT.CCB_CODEMP and sbc.SBC_SBCID = CPBT.CCB_SBCID) Asegurado
					from PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
					join CARTERA_CLIENTES_CPBT_DOC CPBT with (nolock)
					ON PDD.CODEMP = CPBT.CCB_CODEMP
					AND PDD.PCLID = cpbt.CCB_PCLID
					and PDD.CTCID = cpbt.CCB_CTCID
					and PDD.CCBID = cpbt.CCB_CCBID
					where PANEL_ID = PD.PANEL_ID
					) Asegurados
				FOR XML PATH('''')), 1, 1, '''') [Asegurado]) As Asegurado,
	PDD.FEC_ENTREGA FechaEntrega, dbo._Trae_DiasSemana(PDD.FEC_ENTREGA, GETDATE()) As DiasTranscurso,
	NULL FechaAsignacion, NULL FechaAprobacionTraspaso, NULL IngresoJudicial, PDD.FEC_ENVIO FechaEnvio,
	NULL FechaIngresoTribunal, NULL Encargado, NULL Correcciones, 0 CountCorrecciones, PDD.COMENTARIOS,
	0 DiasTranscurso2, 0 TrackingDemanda
from PANEL_DEMANDA PD with (nolock)
JOIN PROVCLI p with (nolock)
ON PD.CODEMP = p.PCL_CODEMP
AND PD.PCLID =  p.PCL_PCLID
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = ''NO'')
AND PDD.FEC_ENTREGA IS NOT NULL
AND PDD.FEC_INGRESO_TRIBUNAL IS NULL
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PDD.FEC_ENTREGA IS NOT NULL
AND PDD.FEC_INGRESO_TRIBUNAL IS NULL
order by PD.FEC_REGISTRO'
end
--print @query
exec(@query)	
END