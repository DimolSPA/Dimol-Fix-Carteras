CREATE PROCEDURE _Listar_Movimientos_Protestados_Grilla
(
@codemp int,
@numCuenta varchar(60),
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
	ct.MOVIMIENTO_ID MovimientoId,
	ct.NUM_CUENTA NumCuenta,
	ct.FEC_MOVIMIENTO FecMovimiento,
	ct.MONTO Monto,
	ct.SUCURSAL,
	ct.NUM_COMPROBANTE_REF NumComprobante,
	tm.DESCRIPCION Movimiento,
	cartola.DESCRIPCION Motivo,
	mot.DESCRIPCION MotivoSistema,
	--est.DESCRIPCION Estado
	ct.TIPO_ESTADO_BANCO_ID Estado,
	ct.TIPO_MOTIVO_BANCO_ID MotivoSistemaId
from CARTOLA_MOVIMIENTOS ct
join TESORERIA_TIPO_MOVIMIENTO_BANCO tm
on ct.TIPO_MOVIMIENTO_BANCO_ID = tm.TIPO_MOVIMIENTO_BANCO_ID
join TESORERIA_TIPO_MOTIVO_BANCO mot
on ct.TIPO_MOTIVO_BANCO_ID = mot.TIPO_MOTIVO_BANCO_ID
join TESORERIA_TIPO_ESTADO_BANCO est
on ct.TIPO_ESTADO_BANCO_ID = est.TIPO_ESTADO_BANCO_ID
join CARTOLA_MOVIMIENTOS_TIPO_ESTATUS sts
on ct.ESTATUS_ID = sts.ESTATUS_ID
join CARTOLA_BANCO_EXCEL cartola
on ct.EXCEL_ROW_ID = cartola.EXCEL_ROW_ID
where ct.CODEMP = ' + CONVERT(VARCHAR,@codemp) + '
and ct.NUM_CUENTA = ''' + CONVERT(VARCHAR,@numCuenta) + '''
and ct.ESTATUS_ID = 1
and ct.TIPO_ESTADO_BANCO_ID = 3 
and ct.MONTO > 0'

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END