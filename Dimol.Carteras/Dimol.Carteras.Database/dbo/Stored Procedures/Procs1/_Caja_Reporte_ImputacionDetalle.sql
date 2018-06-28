CREATE PROCEDURE _Caja_Reporte_ImputacionDetalle
(
--declare
@codemp int,
@conciliacionId int
)
AS
BEGIN
	SET NOCOUNT ON;
	select cdi.conciliacion_id ConciliacionId, 
		CASE WHEN dc.FEC_DOC is null THEN cm.FEC_MOVIMIENTO ELSE dc.FEC_DOC END FecDoc,
		(select TDOC.TPC_NOMBRE from TIPOS_CPBTDOC TDOC with (nolock) 
			where TDOC.TPC_CODEMP = cdi.codemp AND TDOC.TPC_TPCID = cpbt.ccb_tpcid) TipoDocumento,
		cpbt.ccb_Numero Numero,
		 cdi.saldo Capital,
		 cdi.interes Interes,
		 cdi.honorario Honorario,
		 cdi.gastopre GastoPre,
		 cdi.gastojud GastoJud,
		 cdi.saldo + cdi.interes + cdi.honorario + cdi.gastopre + cdi.gastojud Total
	from CONCILIACION_DOCUMENTO_IMPUTADO cdi with (nolock)
	join CONCILIACION_MOVIMIENTOS_DOCUMENTOS cmd with (nolock)
	on  cdi.CONCILIACION_ID = cmd.CONCILIACION_ID
	left join DOCUMENTOS_CUSTODIA dc with (nolock)
	on cmd.CUSTODIA_ID  = dc.CUSTODIA_ID
	left join CARTOLA_MOVIMIENTOS cm with (nolock)
	on cmd.movimiento_id = cm.movimiento_id
	join CARTERA_CLIENTES_CPBT_DOC cpbt with (nolock)
	on cpbt.ccb_codemp = cdi.codemp 
	and cpbt.ccb_pclid = cmd.pclid
	and cpbt. ccb_ctcid = cmd.ctcid
	and cpbt.ccb_ccbid = cdi.ccbid
	where cdi.codemp = @codemp
	and cdi.conciliacion_id = @conciliacionId

END