CREATE PROCEDURE _Listar_CriteriosRemesa_Grilla
(
@codemp int,
@pclid int,
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
	tcg.id,
	tcg.desdediasvencido DesdeDiasVencido,
	tcg.HASTADIASVENCIDO HastaDiasVencido,
	case tcg.CODIGOREGION when 6 then ''S'' else ''N'' end RegionMetropolitana,
	tcg.codigocarga,
	ccarga.pcc_codigo + '' -'' + ccarga.pcc_nombre CodigoDeCarga,
	tcg.tipocambiocapital TipoCambioCapital,
	(select top 1 SIMBOLO_ID from CAJA_CRITERIO_SIMBOLO where DESCRIPCION = tcg.tipocambiocapital) SimboloId,
	tcg.CAPITAL Capital,
	tcg.INTERES Interes,
	tcg.HONORARIO Honorario,
	tcg.CONCILIACION_TIPO_ID TipoConciliacionId,
	ct.DESCRIPCION TipoConciliacion,
	tcg.condicion_id CondicionId,
	ccf.DESCRIPCION CondicionAnticipo
from TESORERIA_CRITERIO_GANANCIA_PORCENTAJE tcg
left join provcli_codigo_carga ccarga
on tcg.CODEMP = ccarga.pcc_codemp
and tcg.CODIGOCARGA = ccarga.pcc_codid
join CONCILIACION_TIPO ct
on tcg.CONCILIACION_TIPO_ID = ct.CONCILIACION_TIPO_ID
left join CAJA_CONDICION_FACTURACION ccf
on tcg.condicion_id = ccf.condicion_id
where tcg.codemp = '+ CONVERT(VARCHAR,@codemp) + '
and tcg.pclid = '+ CONVERT(VARCHAR,@pclid) + ''

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + ' where 1 = 1 ' + @where;
end
 exec(@query)	
END