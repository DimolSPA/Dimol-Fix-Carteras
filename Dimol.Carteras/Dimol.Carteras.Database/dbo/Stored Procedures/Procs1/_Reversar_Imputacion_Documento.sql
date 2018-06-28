CREATE PROCEDURE _Reversar_Imputacion_Documento(
@codemp int,
@conciliacionId int,
@userId int)
AS
BEGIN

	UPDATE
		cpbt
	SET
		cpbt.ccb_estcpbt = docs.estado,
		cpbt.ccb_saldo = cpbt.ccb_saldo + docs.saldo,
		cpbt.ccb_intereses = cpbt.ccb_intereses + docs.interes,
		cpbt.ccb_honorarios = cpbt.ccb_honorarios + docs.honorario,
		cpbt.ccb_gastjud = cpbt.ccb_gastjud + docs.gastojud,
		cpbt.ccb_gastotro = cpbt.ccb_gastotro + docs.gastopre
	--select cpbt.*
	FROM
		CARTERA_CLIENTES_CPBT_DOC cpbt
	INNER JOIN
		(select
			cdi.codemp, 
			cmd.pclid, 
			cmd.ctcid, 
			cdi.ccbid, 
			cdi.saldo, 
			cdi.interes, 
			cdi.honorario, 
			cdi.gastopre, 
			cdi.gastojud, cdi.estado
		from conciliacion_movimientos_documentos cmd
		join CONCILIACION_DOCUMENTO_IMPUTADO cdi
		on cmd.conciliacion_id = cdi.conciliacion_id
		where cmd.conciliacion_id  = @conciliacionId) docs
	ON 
		cpbt.CCB_CODEMP = docs.codemp
		and cpbt.ccb_pclid = docs.pclid
		and cpbt.ccb_ctcid = docs.ctcid
		and cpbt.ccb_ccbid = docs. ccbid
END

