CREATE PROCEDURE _Listar_CriteriosImputacion_Grilla
(
@codemp int,
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
	tci.pclid,
	 cli.pcl_nomfant Cliente,
	 tci.Capital,
	 tci.Interes,
	 tci.Honorario
from TESORERIA_CRITERIO_IMPUTACION_PORCENTAJE tci
join provcli cli
on tci.codemp = cli.pcl_codemp
and tci.pclid = cli.pcl_pclid
where tci.codemp = '+ CONVERT(VARCHAR,@codemp) + ''

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + ' where 1 = 1 ' + @where;
end
 exec(@query)	
END