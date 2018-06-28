﻿CREATE PROCEDURE [dbo].[_Listar_Panel_Demandas_Masivas_Grilla_Count]
(
@codemp int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(8000) = ''

set @query = '  select count(PanelId) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' 
set @query = @query + 'SELECT DISTINCT PD.ID_PANEL_MASIVO PanelId, PD.PROCESADA,
	(select top 1 ccb_fecing from cartera_clientes_cpbt_doc with (nolock) 
	where ccb_codemp = PD.CODEMP and ccb_pclid = PD.PCLID and ccb_ctcid = PD.CTCID)as FechaAsignacion,
	(select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
	where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
	and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 )as FechaAprobacionTraspaso, 
	PD.FECHA_REG as FechaIngresaJudicial, p.PCL_NOMFANT Cliente, d.CTC_RUT RutDeudor, d.CTC_NOMFANT Deudor,
	null Asegurado, 
	 null TipoDocumento, com.COM_NOMBRE Comuna, reg.REG_NOMBRE Region,
	 PDDE.USRID_ENCARGADO as UsridEncargado, (select top 1 usu.USR_NOMBRE from USUARIOS usu with (nolock) where usu.USR_USRID = PDDE.USRID_ENCARGADO) as EncargadoCofeccion, 
	 PDDE.FEC_ENVIO FechaEnvioConfeccion, PDDE.FEC_ENTREGA FechaEntrega, 
	 PDDE.FEC_INGRESO_TRIBUNAL FechaIngresoTribunal, PDDE.COMENTARIOS,
	 PD.PCLID, PD.CTCID, p.PCL_RUT, 
	 (select (count(ID_PANEL_MASIVO)) from PANEL_DEMANDA_MASIVA_CORRECCION_HISTORIAL where ID_PANEL_MASIVO = PD.ID_PANEL_MASIVO) countFechaEntrega,
	 CASE 
		WHEN ((select (count(ID_PANEL_MASIVO)-1) from PANEL_DEMANDA_MASIVA_CORRECCION_HISTORIAL where ID_PANEL_MASIVO = PD.ID_PANEL_MASIVO) > 0 ) THEN 
		''S'' ELSE ''N'' END Correcciones,
	 CASE 
		WHEN ((select (count(ID_PANEL_MASIVO)-1) from PANEL_DEMANDA_MASIVA_CORRECCION_HISTORIAL where ID_PANEL_MASIVO = PD.ID_PANEL_MASIVO) = -1 ) THEN 
		0 ELSE 
		(select (count(ID_PANEL_MASIVO)-1) from PANEL_DEMANDA_MASIVA_CORRECCION_HISTORIAL where ID_PANEL_MASIVO = PD.ID_PANEL_MASIVO) END countCorrecciones
FROM PANEL_DEMANDA_MASIVA PD with (nolock)
JOIN PANEL_DEMANDA_MASIVA_DOCUMENTOS PDD with (nolock)
ON PD.ID_PANEL_MASIVO = PDD.ID_PANEL_MASIVO
JOIN CARTERA_CLIENTES_CPBT_DOC cpbt with (nolock)
ON PDD.CODEMP = CPBT.CCB_CODEMP
AND PDD.PCLID = cpbt.CCB_PCLID
and PDD.CTCID = cpbt.CCB_CTCID
and PDD.CCBID = cpbt.CCB_CCBID
JOIN PROVCLI p with (nolock)
ON cpbt.CCB_CODEMP = p.PCL_CODEMP
AND cpbt.CCB_PCLID =  p.PCL_PCLID
JOIN DEUDORES  d  with (nolock)
ON cpbt.CCB_CTCID = d.CTC_CTCID
AND cpbt.CCB_CODEMP = d.CTC_CODEMP
JOIN COMUNA com
ON d.CTC_COMID = com.COM_COMID
JOIN CIUDAD ciu
ON com.COM_CIUID = ciu.CIU_CIUID
JOIN REGION reg
ON ciu.CIU_REGID = reg.REG_REGID
LEFT JOIN PANEL_DEMANDA_MASIVA_DETALLE PDDE
ON PD.ID_PANEL_MASIVO = PDDE.ID_PANEL_MASIVO
WHERE PD.FECHA_REG < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = ''NO'')
AND PDD.CODEMP = '+ convert(varchar,@codemp) +'
AND PDD.ESTADO = ''ACT''
OR PD.FECHA_REG >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
PD.FECHA_REG < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PDD.CODEMP = '+ convert(varchar,@codemp) +'
AND PDD.ESTADO = ''ACT'''


set @query = @query +') as tabla  ) as t
  where  row > 0' 

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END