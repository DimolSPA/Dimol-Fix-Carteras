CREATE PROCEDURE [dbo].[_Listar_Movimientos_Conciliados_Apr_Grilla]
(
--declare
@codemp int,
@fechaConciliacion varchar(20),
@pclid int,
@ctcid int,
@numcomprobante int,
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
	cmd.CONCILIACION_ID ConciliacionId,
	cmd.MOVIMIENTO_ID MovimientoId,
	cmd.CUSTODIA_ID CustodiaId,
	cmd.PCLID Pclid,
	cmd.CTCID Ctcid,
	cmd.GESTORID GestorId,
	cmd.NUM_COMPROBANTE NumComprobante,
	cli.PCL_RUT RutCliente,
	cli.PCL_NOMFANT Cliente,
	d.CTC_RUT RutDeudor,
	d.CTC_NOMFANT Deudor,
	mot.DESCRIPCION MotivoSistema,
	ct.MONTO Monto,
	ct.MONTO - isnull(Rebajado.Total, 0) Saldo,
	ctipo.DESCRIPCION Tipoconciliacion,
	ce.DESCRIPCION EstadoLiquidacion,
	cmd.FEC_REGISTRO FechaConciliacion,
	(select  count(conciliacion_id) from REMESA_DETALLE where conciliacion_id = cmd.CONCILIACION_ID) Remesa
from CONCILIACION_MOVIMIENTOS_DOCUMENTOS cmd
join CARTOLA_MOVIMIENTOS ct
on cmd.MOVIMIENTO_ID = ct.MOVIMIENTO_ID
join PROVCLI cli
on cmd.PCLID = cli.PCL_PCLID
and ct.CODEMP = cli.PCL_CODEMP
join DEUDORES d
on cmd.CTCID = d.CTC_CTCID
and ct.CODEMP = d.CTC_CODEMP
join TESORERIA_TIPO_MOTIVO_BANCO mot
on ct.TIPO_MOTIVO_BANCO_ID = mot.TIPO_MOTIVO_BANCO_ID
join CONCILIACION_ESTADO ce
on cmd.CONCILIACION_ESTADO_ID = ce.CONCILIACION_ESTADO_ID
join CONCILIACION_TIPO ctipo
on cmd.CONCILIACION_TIPO_ID = ctipo.CONCILIACION_TIPO_ID
left join (SELECT ConciliacionId, SUM(subtotal) Total
		FROM (
		select conciliacion_id ConciliacionId, saldo + interes + honorario + gastopre + gastojud as subtotal 
		from CONCILIACION_DOCUMENTO_IMPUTADO) GrandTotal
		group by ConciliacionId) Rebajado
on  cmd.CONCILIACION_ID = Rebajado.ConciliacionId
where ct.CODEMP = ' + CONVERT(VARCHAR,@codemp) +'
and ce.CONCILIACION_ESTADO_ID = 2'
if @fechaConciliacion is not null
begin
set @query = @query + ' and Cast(cmd.FEC_REGISTRO AS DATE) = convert(date,''' + CONVERT(varchar(10),@fechaConciliacion,120) + ''', 103)'
end
if @pclid is not null
begin
set @query = @query + ' and cmd.PCLID = ' + CONVERT(VARCHAR,@pclid) +''
end
if @ctcid is not null
begin
set @query = @query + ' and cmd.CTCID = ' + CONVERT(VARCHAR,@ctcid) +''
end
if @numcomprobante is not null
begin
set @query = @query + ' and cmd.NUM_COMPROBANTE = ' + CONVERT(VARCHAR,@numcomprobante) +''
end

set @query = @query + ') as tabla  ) as t'
 
if @where is not null
begin
set @query = @query + @where;
end
 exec(@query)	
END
