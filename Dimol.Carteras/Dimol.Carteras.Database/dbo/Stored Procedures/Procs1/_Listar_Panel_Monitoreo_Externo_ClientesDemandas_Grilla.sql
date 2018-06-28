CREATE PROCEDURE [dbo].[_Listar_Panel_Monitoreo_Externo_ClientesDemandas_Grilla]
(
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;
declare @query varchar(7000);
set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'select 
	pje.PCLID,
	pv.PCL_NOMFANT Cliente, 
	pje.TOTAL_CARTERA SaldoCartera, 
	pje.TOTAL_SINDEMANDA SaldoSinDemanda,
	Cast(CONVERT(decimal(5,0), pje.TOTAL_SINDEMANDA * 100.00/pje.TOTAL_CARTERA) as varchar(5)) + ''%'' PorSaldoSinDemanda,
	pje.TOTAL_DEMANDADO SaldoDemandado,
	Cast(CONVERT(decimal(5,0), pje.TOTAL_DEMANDADO * 100.00/pje.TOTAL_CARTERA) as varchar(5)) + ''%'' PorSaldoDemandado,
	pje.DEMANDADO_DOSANIOS SaldoDemandadoDosAnios,
	Cast(CONVERT(decimal(5,0), pje.DEMANDADO_DOSANIOS * 100.00/pje.TOTAL_CARTERA) as varchar(5)) + ''%'' PorSaldoDemandadoDosAnios
from PODER_JUDICIAL_MONITOREO_EXTERNO pje
join PROVCLI pv
on pje.PCLID = pv.PCL_PCLID'

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
