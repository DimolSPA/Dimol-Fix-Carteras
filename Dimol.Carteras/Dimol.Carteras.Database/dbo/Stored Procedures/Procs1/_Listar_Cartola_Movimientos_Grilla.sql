CREATE PROCEDURE [dbo].[_Listar_Cartola_Movimientos_Grilla]
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
	ct.TIPO_MOTIVO_BANCO_ID MotivoSistemaId,
	ct.OBSERVACION,
	(select CUENTA_ID
	from TESORERIA_CUENTAS_BANCARIAS 
	where NUM_CUENTA = ct.NUM_CUENTA) cuentaId
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
and ct.TIPO_ESTADO_BANCO_ID != 3 
and ct.MOVIMIENTO_ID not in (select MOVIMIENTO_ID from CARTOLA_MOVIMIENTOS
							where CODEMP  = ' + CONVERT(VARCHAR,@codemp) + '
							and NUM_CUENTA = ''' + CONVERT(VARCHAR,@numCuenta) + '''
							and ESTATUS_ID = 1
							and TIPO_ESTADO_BANCO_ID != 3
							and FEC_REGISTRO < DATEADD(day, DATEDIFF(day, 0, GETDATE() -1), 0) 
							and TIPO_ESTADO_BANCO_ID = 4)'

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END