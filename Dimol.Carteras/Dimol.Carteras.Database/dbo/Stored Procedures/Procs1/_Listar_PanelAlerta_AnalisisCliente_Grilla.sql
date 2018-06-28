CREATE PROCEDURE [dbo].[_Listar_PanelAlerta_AnalisisCliente_Grilla]
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
declare @query varchar(7000);
  
set @query = 'WITH DemandaClientes AS (
		SELECT PD.PANEL_ID, PD.PCLID, p.PCL_NOMFANT CLIENTE, 
			sum(cpbt.CCB_SALDO) SALDO
			FROM PANEL_DEMANDA PD with (nolock)
			JOIN PROVCLI p with (nolock)
			ON PD.CODEMP = p.PCL_CODEMP
			AND PD.PCLID =  p.PCL_PCLID
			JOIN PANEL_DEMANDA_DOCUMENTOS PDD with (nolock)
			ON PD.PANEL_ID = PDD.PANEL_ID
			JOIN CARTERA_CLIENTES_CPBT_DOC cpbt with (nolock)
			ON PDD.CODEMP = CPBT.CCB_CODEMP
			AND PDD.PCLID = cpbt.CCB_PCLID
			AND PDD.CTCID = cpbt.CCB_CTCID
			AND PDD.CCBID = cpbt.CCB_CCBID
			WHERE PD.FEC_REGISTRO  < DATEADD(DAY,1,DATEADD(MONTH, DATEDIFF(MONTH, -1, GETDATE())-1, -1))
			AND (PD.CURSODEMANDA is NULL or PD.CURSODEMANDA = ''NO'') 
			AND PDD.CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
			AND PDD.ESTADO = ''ACT''
			OR PD.FEC_REGISTRO >= DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()), 0) and --Mes actual
			PD.FEC_REGISTRO < DATEADD(MONTH, DATEDIFF(MONTH, 0, GETDATE()) + 1, 0)
			AND PDD.CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
			AND PDD.ESTADO = ''ACT''
		group by PD.PANEL_ID, PD.PCLID, p.PCL_NOMFANT)  
select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + '
SELECT PCLID, 
	CLIENTE, 
	count(PANEL_ID)CANTDEMANDAS,
	cast(round(SUM(SALDO),0) as int) SALDO,
	CONVERT(decimal(5,0), SUM(SALDO)* 100.00/(select sum(saldo) from DemandaClientes)) Percentage,
	Cast(Cast((SUM(SALDO)/(select sum(saldo) from DemandaClientes))*100 as decimal(18,2)) as varchar(5)) + ''%'' as PORCENTAJE
FROM DemandaClientes 
group by PCLID,CLIENTE'
 
set @query = @query + ') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

-- select @query
 exec(@query)	
	

END
