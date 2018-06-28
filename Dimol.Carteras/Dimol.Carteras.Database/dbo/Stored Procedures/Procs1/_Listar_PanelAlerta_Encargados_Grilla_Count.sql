CREATE PROCEDURE [dbo].[_Listar_PanelAlerta_Encargados_Grilla_Count]
(
@codemp int ,
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
 
set @query = 'WITH DemandaEncargados AS (
		SELECT PD.PANEL_ID,
			PDDE.USRID_ENCARGADO  USR,
			(select top 1 usu.USR_NOMBRE from USUARIOS usu with (nolock) 
			where usu.USR_USRID = PDDE.USRID_ENCARGADO) Encargado, 
			sum(cpbt.CCB_SALDO) SALDO,
			CASE WHEN 
				((select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) = -1 ) THEN 
			0 ELSE 
			(select (count(PANEL_ID)-1) from PANEL_DEMANDA_CORRECCION_HISTORIAL where PANEL_ID = PD.PANEL_ID) END CountCorrecciones,
			CASE WHEN 
				DATEDIFF(day, PDDE.FEC_ENVIO, isnull(PDDE.FEC_ENTREGA, GETDATE())) 
				< 0 THEN 0 ELSE DATEDIFF(day, PDDE.FEC_ENVIO, isnull(PDDE.FEC_ENTREGA, GETDATE())) END As DiasTranscurso
			FROM PANEL_DEMANDA PD with (nolock)
			JOIN PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
			ON PD.PANEL_ID = PDD.PANEL_ID
			JOIN CARTERA_CLIENTES_CPBT_DOC cpbt with (nolock)
			ON PDD.CODEMP = CPBT.CCB_CODEMP
			AND PDD.PCLID = cpbt.CCB_PCLID
			AND PDD.CTCID = cpbt.CCB_CTCID
			AND PDD.CCBID = cpbt.CCB_CCBID
			left Join PANEL_DEMANDA_DETALLE PDDE with (nolock)
			on PD.PANEL_ID = PDDE.PANEL_ID
			WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
			AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = ''NO'')
			AND PDD.CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
			AND PDD.ESTADO = ''ACT''
			--AND PDDE.FEC_ENVIO IS NOT NULL
			OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
			PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
			AND PDD.CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
			AND PDD.ESTADO = ''ACT''
			--AND PDDE.FEC_ENVIO IS NOT NULL
			group by PD.PANEL_ID, PDDE.USRID_ENCARGADO, PDDE.FEC_ENVIO, PDDE.FEC_ENTREGA)  
select count(Encargado) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('
  
set @query = @query + '
SELECT
	CASE WHEN USR IS NULL THEN 0 ELSE USR END as USR,
	CASE WHEN Encargado IS NULL THEN ''NO ASIGNADO'' ELSE Encargado END as Encargado,
	count(PANEL_ID)CANTDEMANDAS,
	SUM(SALDO) SALDO,
	SUM(CountCorrecciones) CantCorrecciones,
	Cast(isnull(ROUND(AVG(DiasTranscurso),0), 0) as varchar(5)) + '' dias'' TiempoEntrega
FROM DemandaEncargados
group by USR,Encargado'


   set @query = @query +')as tabla ) as t
  where  row > 0'

if @where is not null
begin
set @query = @query + @where;
end

-- select @query
 exec(@query)	
	

END