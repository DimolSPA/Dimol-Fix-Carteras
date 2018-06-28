CREATE PROCEDURE [dbo].[_Listar_PanelAlerta_TipoReporte_Grilla]
(
@codemp int,
@tipoReporte int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN

	SET NOCOUNT ON;
declare @query varchar(7000);
set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'select * from (SELECT PD.PANEL_ID PANELID,
	p.PCL_NOMFANT CLIENTE,
	d.CTC_NOMFANT Deudor, 
	(select sbc.SBC_NOMBRE 
	from SUBCARTERAS  sbc where sbc.SBC_CODEMP = PD.CODEMP and sbc.SBC_SBCID = PD.SBCID)As Asegurado,
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
	(select top 1 usu.USR_NOMBRE from USUARIOS usu with (nolock) where usu.USR_USRID = PDDE.USRID_ENCARGADO) Encargado,
	PDDE.FEC_ENTREGA FechaEntrega,
	PDDE.FEC_INGRESO_TRIBUNAL FechaIngresoTribunal,
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) > 0 ) THEN 
		''S'' ELSE ''N'' END Correcciones, 
	CASE 
		WHEN ((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) = -1 ) THEN 
		0 ELSE 
		(select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) END CountCorrecciones, 
		PDDE.COMENTARIOS,'
if @tipoReporte = 1 --Aprobación Traspaso v/s Ingreso Judicial
begin
set @query = @query + ' CASE WHEN  
				(dbo._Trae_DiasSemana((select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
				where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
				and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 ), PD.FEC_REGISTRO))
				< 0 THEN (dbo._Trae_DiasSemana((select max(CEH_FECHA)-1 from cartera_clientes_estados_historial 
							where ceh_codemp = PD.CODEMP and ceh_pclid = PD.PCLID 
							and ceh_ctcid = PD.CTCID and ceh_estid = 27), PD.FEC_REGISTRO))
				ELSE (dbo._Trae_DiasSemana((select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
				where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
				and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 ), PD.FEC_REGISTRO)) END As DiasTranscurso,
	CASE WHEN 
					(dbo._Trae_DiasSemana((select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
					where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
					and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 ), PD.FEC_REGISTRO) -1)
				< 0 THEN (dbo._Trae_DiasSemana((select max(CEH_FECHA)-1 from cartera_clientes_estados_historial 
							where ceh_codemp = PD.CODEMP and ceh_pclid = PD.PCLID 
							and ceh_ctcid = PD.CTCID and ceh_estid = 27), PD.FEC_REGISTRO) -1) ELSE 
				(dbo._Trae_DiasSemana((select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
				where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
				and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 ), PD.FEC_REGISTRO) -1)END As DiasAtraso'
end
if @tipoReporte = 2 --Preparación de Datos de Confección
begin
set @query = @query + ' CASE WHEN 
	dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDDE.FEC_ENVIO, GETDATE())) 
	< 0 THEN 0 ELSE dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDDE.FEC_ENVIO, GETDATE())) END As DiasTranscurso,
	CASE WHEN 
		dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDDE.FEC_ENVIO, GETDATE()))-5
	< 0 THEN 0 ELSE dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDDE.FEC_ENVIO, GETDATE()))-5 END  As DiasAtraso'
end
if @tipoReporte = 3 --Tiempo de confeccion
begin
set @query = @query + ' dbo._Trae_DiasSemana(PDDE.FEC_ENVIO, ISNULL(PDDE.FEC_ENTREGA, GETDATE())) As DiasTranscurso,
	CASE WHEN 
		dbo._Trae_DiasSemana(PDDE.FEC_ENVIO, ISNULL(PDDE.FEC_ENTREGA, GETDATE()))-5
	< 0 THEN 0 ELSE dbo._Trae_DiasSemana(PDDE.FEC_ENVIO, ISNULL(PDDE.FEC_ENTREGA, GETDATE()))-5 END  As DiasAtraso'
end
if @tipoReporte = 4 --Demanda Producción
begin
set @query = @query + ' CASE WHEN 
	dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDDE.FEC_INGRESO_TRIBUNAL, GETDATE()))
	< 0 THEN 0 ELSE dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDDE.FEC_INGRESO_TRIBUNAL, GETDATE())) END As DiasTranscurso,
	CASE WHEN 
		dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDDE.FEC_INGRESO_TRIBUNAL, GETDATE()))-14
	< 0 THEN 0 ELSE dbo._Trae_DiasSemana(PD.FEC_REGISTRO, ISNULL(PDDE.FEC_INGRESO_TRIBUNAL, GETDATE()))-14 END  As DiasAtraso '
end
set @query = @query + '
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
AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = ''NO'')'

if @tipoReporte = 3 --Tiempo de confeccion
begin
set @query = @query + ' AND PDDE.USRID_ENCARGADO IS NOT NULL '
end

set @query = @query + '
AND PD.CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
AND PD.CODEMP = ' + CONVERT(VARCHAR,@codemp) +''

if @tipoReporte = 3 --Tiempo de confeccion
begin
set @query = @query + ' AND PDDE.USRID_ENCARGADO IS NOT NULL '
end

set @query = @query + ') Panel '
if @tipoReporte = 1 --Aprobación Traspaso v/s Ingreso Judicial
begin
set @query = @query + 'where DiasAtraso > 0'
end
if @tipoReporte = 2 --Preparación de Datos de Confección
begin
set @query = @query + 'where DiasAtraso > 0'
end
if @tipoReporte = 3 --Tiempo de confeccion
begin
set @query = @query + 'where DiasAtraso > 0'
end
if @tipoReporte = 4 --Demanda Produccion
begin
set @query = @query + 'where DiasAtraso > 0'
end
set @query = @query + ') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

-- select @query
 exec(@query)	

END