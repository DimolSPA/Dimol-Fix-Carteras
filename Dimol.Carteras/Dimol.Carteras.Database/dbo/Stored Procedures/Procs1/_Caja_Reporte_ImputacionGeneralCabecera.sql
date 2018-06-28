CREATE PROCEDURE _Caja_Reporte_ImputacionGeneralCabecera
(
--declare
@codemp int,
@conciliacionId int
)
AS
BEGIN
	SET NOCOUNT ON;
	select 
		cmd.NUM_COMPROBANTE NumComprobante,
		cmd.FEC_REGISTRO FecDoc,
		cli.PCL_RUT RutCliente,
		cli.PCL_NOMFANT Cliente,
		d.CTC_RUT RutDeudor,
		d.CTC_NOMFANT Deudor,
		CASE WHEN dc.MONTO is null THEN cm.MONTO ELSE dc.MONTO END Monto,
		ges.GES_NOMBRE Gestor
	from CONCILIACION_MOVIMIENTOS_DOCUMENTOS cmd
	join PROVCLI cli
	on cmd.PCLID = cli.PCL_PCLID
	and cli.PCL_CODEMP = @codemp
	join DEUDORES d
	on cmd.CTCID = d.CTC_CTCID
	and d.CTC_CODEMP= @codemp
	join GESTOR ges
	on ges.GES_CODEMP = @codemp
	and cmd.GESTORID = ges.GES_GESID
	left join DOCUMENTOS_CUSTODIA dc
	on cmd.CUSTODIA_ID  = dc.CUSTODIA_ID
	left join CARTOLA_MOVIMIENTOS cm
	on cmd.movimiento_id = cm.movimiento_id
	where cmd.conciliacion_id = @conciliacionId
	
END