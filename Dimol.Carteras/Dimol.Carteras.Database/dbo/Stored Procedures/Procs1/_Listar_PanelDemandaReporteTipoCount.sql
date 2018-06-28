CREATE PROCEDURE [dbo].[_Listar_PanelDemandaReporteTipoCount]
(@tipoReporte int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int)
AS
BEGIN
SET NOCOUNT ON;
declare @query varchar(8000);
set @query = '  select count(Deudor) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('
if @tipoReporte = 1 --Total Mes Anterior
begin
set @query = @query + 'SELECT PD.PANEL_ID, d.CTC_NOMFANT Deudor, 
(select sbc.SBC_NOMBRE 
	from SUBCARTERAS  sbc where sbc.SBC_CODEMP = PD.CODEMP and sbc.SBC_SBCID = PD.SBCID)As Asegurado,
	(select top 1 ccb_fecing from cartera_clientes_cpbt_doc with (nolock) 
	where ccb_codemp = PD.CODEMP and ccb_pclid = PD.PCLID and ccb_ctcid = PD.CTCID)as FechaAsignacion,
	(select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
	where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
	and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 )as FechaAprobacionTraspaso,
	PD.FEC_REGISTRO As IngresoJudicial, PDDE.FEC_ENVIO FechaEnvio,
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) > 0 ) THEN 
		''S'' ELSE ''N'' END Correcciones, NULL as FechaIngresoTribunal, NULL FechaEntrega, NULL Encargado,
	0 DiasTranscurso, 0 CountCorrecciones
FROM PANEL_DEMANDA PD with (nolock)
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
LEFT JOIN PANEL_DEMANDA_DETALLE PDDE
ON PD.PANEL_ID = PDDE.PANEL_ID
WHERE PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-2, 0)  
AND PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -2, GETDATE())-2, -1))
AND PD.PROCESADA = ''N'' 
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0)
AND PROCESADA = ''N''
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 2 --Mes Actual
begin
set @query = @query + 'SELECT PD.PANEL_ID, d.CTC_NOMFANT Deudor, 
(select sbc.SBC_NOMBRE 
	from SUBCARTERAS  sbc where sbc.SBC_CODEMP = PD.CODEMP and sbc.SBC_SBCID = PD.SBCID)As Asegurado,
	(select top 1 ccb_fecing from cartera_clientes_cpbt_doc with (nolock) 
	where ccb_codemp = PD.CODEMP and ccb_pclid = PD.PCLID and ccb_ctcid = PD.CTCID)as FechaAsignacion,
	(select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
	where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
	and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 )as FechaAprobacionTraspaso,
	PD.FEC_REGISTRO As IngresoJudicial, PDDE.FEC_ENVIO FechaEnvio,
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) > 0 ) THEN 
		''S'' ELSE ''N'' END Correcciones,
	PDDE.FEC_INGRESO_TRIBUNAL As FechaIngresoTribunal,
	NULL FechaEntrega, NULL Encargado,
	0 DiasTranscurso, 0 CountCorrecciones
FROM PANEL_DEMANDA PD with (nolock)
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
set @query = @query + 'SELECT PD.PANEL_ID, d.CTC_NOMFANT Deudor, 
(select sbc.SBC_NOMBRE 
	from SUBCARTERAS  sbc where sbc.SBC_CODEMP = PD.CODEMP and sbc.SBC_SBCID = PD.SBCID)As Asegurado,
	(select top 1 ccb_fecing from cartera_clientes_cpbt_doc with (nolock) 
	where ccb_codemp = PD.CODEMP and ccb_pclid = PD.PCLID and ccb_ctcid = PD.CTCID)as FechaAsignacion,
	(select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
	where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
	and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 )as FechaAprobacionTraspaso,
	PD.FEC_REGISTRO As IngresoJudicial, NULL FechaEnvio, NULL FechaIngresoTribunal, NULL FechaEntrega,
	NULL Encargado, NULL Correcciones, 0 DiasTranscurso, 0 CountCorrecciones 
FROM PANEL_DEMANDA PD with (nolock)
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
WHERE PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  --Mes anterior
AND PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND PD.PROCESADA = ''N'' 
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 4 --Demandas no Asignadas
begin
set @query = @query + 'SELECT PD.PANEL_ID, d.CTC_NOMFANT Deudor, 
(select sbc.SBC_NOMBRE 
	from SUBCARTERAS  sbc where sbc.SBC_CODEMP = PD.CODEMP and sbc.SBC_SBCID = PD.SBCID)As Asegurado,
	(select top 1 ccb_fecing from cartera_clientes_cpbt_doc with (nolock) 
	where ccb_codemp = PD.CODEMP and ccb_pclid = PD.PCLID and ccb_ctcid = PD.CTCID)as FechaAsignacion,
	(select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
	where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
	and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 )as FechaAprobacionTraspaso,
	PD.FEC_REGISTRO As IngresoJudicial, DATEDIFF(day,(select top 1 ccb_fecing 
														from cartera_clientes_cpbt_doc with (nolock)
														where ccb_codemp = PD.CODEMP 
														and ccb_pclid = PD.PCLID 
														and ccb_ctcid = PD.CTCID), PD.FEC_REGISTRO) As DiasTranscurso,
	NULL FechaEnvio, NULL FechaIngresoTribunal, NULL FechaEntrega,
	NULL Encargado, NULL Correcciones, 0 CountCorrecciones
from PANEL_DEMANDA PD with (nolock)
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
left Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
WHERE PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  
AND PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND PD.PROCESADA = ''N'' 
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PDD.USRID_ENCARGADO is null
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 5 -- Demandas Asignadas
begin
set @query = @query + 'SELECT PD.PANEL_ID, d.CTC_NOMFANT Deudor, 
(select sbc.SBC_NOMBRE 
	from SUBCARTERAS  sbc where sbc.SBC_CODEMP = PD.CODEMP and sbc.SBC_SBCID = PD.SBCID)As Asegurado,
	(select top 1 ccb_fecing from cartera_clientes_cpbt_doc with (nolock) 
	where ccb_codemp = PD.CODEMP and ccb_pclid = PD.PCLID and ccb_ctcid = PD.CTCID)as FechaAsignacion,
	(select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
	where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
	and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 )as FechaAprobacionTraspaso,
	PD.FEC_REGISTRO As IngresoJudicial, PDD.FEC_ENVIO FechaEnvio,
	NULL FechaIngresoTribunal, NULL FechaEntrega,
	NULL Encargado, NULL Correcciones, 0 DiasTranscurso, 0 CountCorrecciones
from PANEL_DEMANDA PD with (nolock)
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
WHERE PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  
AND PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND PD.PROCESADA = ''N'' 
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 6 --Demandas sin confeccionar
begin
set @query = @query + 'SELECT PD.PANEL_ID, d.CTC_NOMFANT Deudor, 
(select sbc.SBC_NOMBRE 
	from SUBCARTERAS  sbc where sbc.SBC_CODEMP = PD.CODEMP and sbc.SBC_SBCID = PD.SBCID)As Asegurado,
	usu.USR_NOMBRE Encargado,
	PDD.FEC_ENVIO FechaEnvio, 
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) > 0 ) THEN 
		''S'' ELSE ''N'' END Correcciones, DATEDIFF(day, PDD.FEC_ENVIO, GETDATE()) As DiasTranscurso,
		NULL FechaAsignacion, NULL FechaAprobacionTraspaso, NULL IngresoJudicial, NULL FechaIngresoTribunal,
		NULL FechaEntrega, 0 CountCorrecciones
from PANEL_DEMANDA PD with (nolock)
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
LEFT JOIN  USUARIOS usu with (nolock)
ON PDD.USRID_ENCARGADO = usu.USR_USRID
WHERE PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  
AND PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND PD.PROCESADA = ''N'' 
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PDD.FEC_ENTREGA is null
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 7 --Demandas Confeccionadas
begin
set @query = @query + 'SELECT PD.PANEL_ID, d.CTC_NOMFANT Deudor, 
(select sbc.SBC_NOMBRE 
	from SUBCARTERAS  sbc where sbc.SBC_CODEMP = PD.CODEMP and sbc.SBC_SBCID = PD.SBCID)As Asegurado,
	PDD.FEC_ENVIO FechaEnvio, PDD.FEC_ENTREGA FechaEntrega,
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) > 0 ) THEN 
		''S'' ELSE ''N'' END Correcciones,
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) = -1 ) THEN 
		0 ELSE 
		(select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) END CountCorrecciones,
	NULL FechaAsignacion, NULL FechaAprobacionTraspaso, NULL IngresoJudicial, NULL FechaIngresoTribunal, NULL Encargado,
	0 DiasTranscurso
from PANEL_DEMANDA PD with (nolock)
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
WHERE PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  
AND PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND PD.PROCESADA = ''N'' 
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PDD.FEC_ENTREGA IS NOT NULL
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 8 --Demandas Ingresadas a Tribunal
begin
set @query = @query + 'SELECT PD.PANEL_ID, d.CTC_NOMFANT Deudor, 
(select sbc.SBC_NOMBRE 
	from SUBCARTERAS  sbc where sbc.SBC_CODEMP = PD.CODEMP and sbc.SBC_SBCID = PD.SBCID)As Asegurado,
	PDD.FEC_ENVIO FechaEnvio, PDD.FEC_ENTREGA FechaEntrega, PDD.FEC_INGRESO_TRIBUNAL FechaIngresoTribunal,
	NULL FechaAsignacion, NULL FechaAprobacionTraspaso, NULL IngresoJudicial, NULL Encargado, NULL Correcciones,
	0 DiasTranscurso, 0 CountCorrecciones
from PANEL_DEMANDA PD with (nolock)
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
WHERE PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  
AND PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND PD.PROCESADA = ''N'' 
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PDD.FEC_INGRESO_TRIBUNAL IS NOT NULL
order by PD.FEC_REGISTRO'
end
if @tipoReporte = 9 --Demandas No Ingresadas a Tribunal
begin
set @query = @query + 'SELECT PD.PANEL_ID, d.CTC_NOMFANT Deudor, 
(select sbc.SBC_NOMBRE 
	from SUBCARTERAS  sbc where sbc.SBC_CODEMP = PD.CODEMP and sbc.SBC_SBCID = PD.SBCID)As Asegurado,
	PDD.FEC_ENTREGA FechaEntrega, DATEDIFF(day, PDD.FEC_ENTREGA, GETDATE()) As DiasTranscurso,
	NULL FechaAsignacion, NULL FechaAprobacionTraspaso, NULL IngresoJudicial, NULL FechaEnvio,
	NULL FechaIngresoTribunal, NULL Encargado, NULL Correcciones, 0 CountCorrecciones
from PANEL_DEMANDA PD with (nolock)
JOIN DEUDORES  d  with (nolock)
ON PD.CTCID = d.CTC_CTCID
AND PD.CODEMP = d.CTC_CODEMP
Join PANEL_DEMANDA_DETALLE PDD with (nolock)
on PD.PANEL_ID = PDD.PANEL_ID
WHERE PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE())-1, 0)  
AND PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND PD.PROCESADA = ''N'' 
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and 
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PDD.FEC_ENTREGA IS NOT NULL
AND PDD.FEC_INGRESO_TRIBUNAL IS NULL
order by PD.FEC_REGISTRO'
end
set @query = @query +')as tabla ) as t
  where  row > 0'

if @where is not null
begin
set @query = @query + @where;
end
--print @query
exec(@query)	
END
