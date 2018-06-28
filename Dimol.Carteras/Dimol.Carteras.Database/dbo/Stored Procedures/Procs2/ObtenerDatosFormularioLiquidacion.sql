CREATE Procedure [dbo].[ObtenerDatosFormularioLiquidacion](
@codemp int,
@conciliacionId int, 
@pclid int, 
@ctcid int
) 
as
begin
	select 
	cmd.NUM_COMPROBANTE NumComprobante,
	cli.PCL_RUT RutCliente,
	cli.PCL_NOMFANT Cliente,
	d.CTC_RUT RutDeudor,
	d.CTC_NOMFANT Deudor,
	ct.MONTO Monto,
	isnull(round(CAST(((ct.MONTO - isnull(Rebajado.Total, 0)) * Porc.CAPITAL) / 100 As Decimal(10,2)), 0), 0) Capital, 
	isnull(round(CAST(((ct.MONTO - isnull(Rebajado.Total, 0)) * Porc.INTERES) / 100 As Decimal(10,2)), 0), 0) Interes,
	isnull(round(CAST(((ct.MONTO - isnull(Rebajado.Total, 0)) * Porc.HONORARIO) / 100 As Decimal(10,2)), 0),0) Honorario,
	isnull(Porc.CAPITAL, 0) CapitalPor,
	isnull(Porc.INTERES, 0) InteresPor,
	isnull(Porc.HONORARIO,0) HonorarioPor, '0' as GastoPre, '0' as GastoJud,
	isnull(round(Rebajado.Total, 0),0) MontoRebajado,
	ce.CONCILIACION_ESTADO_ID EstadoLiquidacionId
from CONCILIACION_MOVIMIENTOS_DOCUMENTOS cmd
join CARTOLA_MOVIMIENTOS ct
on cmd.MOVIMIENTO_ID = ct.MOVIMIENTO_ID
join PROVCLI cli
on cmd.PCLID = cli.PCL_PCLID
and ct.CODEMP = cli.PCL_CODEMP
join DEUDORES d
on cmd.CTCID = d.CTC_CTCID
and ct.CODEMP = d.CTC_CODEMP
join TESORERIA_CRITERIO_IMPUTACION_PORCENTAJE Porc
on cmd.PCLID = Porc.PCLID
join CONCILIACION_ESTADO ce
on cmd.CONCILIACION_ESTADO_ID = ce.CONCILIACION_ESTADO_ID
left join (SELECT ConciliacionId, SUM(subtotal) Total
		FROM (
		select conciliacion_id ConciliacionId, saldo + interes + honorario + gastopre + gastojud as subtotal 
		from CONCILIACION_DOCUMENTO_IMPUTADO) GrandTotal
		group by ConciliacionId) Rebajado
on  cmd.CONCILIACION_ID = Rebajado.ConciliacionId
where ct.CODEMP = @codemp
and cmd.CONCILIACION_ID = @conciliacionId
and cmd.PCLID = @pclid
and cmd.CTCID = @ctcid
end
  
  select * from CONCILIACION_MOVIMIENTOS_DOCUMENTOS
