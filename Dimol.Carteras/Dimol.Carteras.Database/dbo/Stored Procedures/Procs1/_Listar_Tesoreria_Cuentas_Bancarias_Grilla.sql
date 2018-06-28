CREATE PROCEDURE [dbo].[_Listar_Tesoreria_Cuentas_Bancarias_Grilla]
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
	ctas.CUENTA_ID CuentaId,
	ctas.NUM_CUENTA NumCuenta,
	tipo.DESCRIPCION TipoCuenta,
	ban.BCO_NOMBRE Banco,
	isnull((select sum(ct.MONTO) from CARTOLA_MOVIMIENTOS ct
	where ct.NUM_CUENTA = ctas.NUM_CUENTA
	and ct.ESTATUS_ID = 1
	and ct.MONTO > 0
	and ct.TIPO_ESTADO_BANCO_ID IN (1,2)), 0) MontoConciliar
from 
TESORERIA_CUENTAS_BANCARIAS ctas
join BANCOS ban
on ctas.CODEMP = ban.BCO_CODEMP
and ctas.BANCO_ID = ban.BCO_BCOID
join TESORERIA_CUENTAS_TIPO tipo
on ctas.TIPO_CUENTA_ID = tipo.TIPO_CUENTA_ID
where ctas.CODEMP = '+ CONVERT(VARCHAR,@codemp) + '
and ctas.NUM_CUENTA != ''1'''

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
