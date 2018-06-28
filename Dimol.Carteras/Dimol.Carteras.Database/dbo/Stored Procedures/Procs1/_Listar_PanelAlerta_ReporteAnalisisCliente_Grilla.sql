CREATE PROCEDURE [dbo].[_Listar_PanelAlerta_ReporteAnalisisCliente_Grilla]
(
@codemp int,
@pclid int,
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
  
set @query = @query + 'SELECT PD.PANEL_ID PANELID, PD.PCLID, p.PCL_NOMFANT CLIENTE, 
	d.CTC_NOMFANT Deudor, 
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
	(select max(cea.cea_fecha) from CARTERA_CLIENTES_ESTADOS_ACCIONES cea with (nolock)
	where cea.CEA_CODEMP = PD.CODEMP and cea.CEA_PCLID = PD.PCLID
	and cea.CEA_CTCID = PD.CTCID and cea.CEA_ACCID = 7 )as FechaAprobacionTraspaso,
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
		PDDE.COMENTARIOS, NULL TipoDocumento,
	sum(cpbt.CCB_SALDO) SALDO
	FROM PANEL_DEMANDA PD with (nolock)
	JOIN PROVCLI p with (nolock)
	ON PD.CODEMP = p.PCL_CODEMP
	AND PD.PCLID =  p.PCL_PCLID
	JOIN DEUDORES  d  with (nolock)
	ON PD.CTCID = d.CTC_CTCID
	AND PD.CODEMP = d.CTC_CODEMP
	LEFT JOIN PANEL_DEMANDA_DETALLE PDDE
	ON PD.PANEL_ID = PDDE.PANEL_ID
	JOIN PANEL_DEMANDA_DOCUMENTOS PDDO with (nolock)
	ON PD.PANEL_ID = PDDO.PANEL_ID
	JOIN CARTERA_CLIENTES_CPBT_DOC cpbt with (nolock)
	ON PDDO.CODEMP = CPBT.CCB_CODEMP
	AND PDDO.PCLID = cpbt.CCB_PCLID
	AND PDDO.CTCID = cpbt.CCB_CTCID
	AND PDDO.CCBID = cpbt.CCB_CCBID
	WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
	AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = ''NO'')
	AND PDDO.CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
	AND PDDO.ESTADO = ''ACT''
	AND PD.PCLID = ' + CONVERT(VARCHAR,@pclid) +'
	OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
	PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
	AND PDDO.CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
	AND PDDO.ESTADO = ''ACT''
	AND PD.PCLID = ' + CONVERT(VARCHAR,@pclid) +'
group by PD.PANEL_ID, PD.PCLID, p.PCL_NOMFANT, 
		d.CTC_NOMFANT, PD.CODEMP, PD.SBCID,PD.CTCID, 
		PD.FEC_REGISTRO, PDDE.FEC_ENVIO,PDDE.USRID_ENCARGADO, 
		PDDE.FEC_ENTREGA,PDDE.FEC_INGRESO_TRIBUNAL, 
		PDDE.COMENTARIOS'
set @query = @query + ') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end
 
 exec(@query)
 	
END
