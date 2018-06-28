CREATE PROCEDURE _Listar_CriteriosFacturacion_Grilla
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
	ccf.PCLID,
	cli.pcl_nomfant Cliente,
	ccf.DESCRIPCION TipoFacturacion,
	ccf.FACTURADO_NOCORRESPONDE NoAplicaFactura,
	ccf.Requiere_Aprueba AplicaAprobacion,
	ccf.imputable Imputable,
	ccf.condicion_id CondicionId,
	ccff.descripcion Condicion,
	ccf.PARAREMESA AplicaRemesa
from CAJA_CRITERIO_FACTURACION ccf
join provcli cli
on ccf.codemp = cli.pcl_codemp
and ccf.pclid = cli.pcl_pclid
left join CAJA_CONDICION_FACTURACION ccff
on ccf.condicion_id = ccff.condicion_id
where ccf.codemp = '+ CONVERT(VARCHAR,@codemp) + ''

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + ' where 1 = 1 ' + @where;
end
 exec(@query)	
END