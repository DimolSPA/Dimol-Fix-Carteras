CREATE PROCEDURE _Obtener_Documentos_Custodia
(
@conciliacionId int

)
AS
BEGIN
	SET NOCOUNT ON;
	select 
		dc.num_documento NumDoc, 
		ban.bco_nombre Banco, 
		dc.recibe GiradoA,
		dc.fec_doc FecDoc,
		dc.monto MontoDoc
	from CONCILIACION_MOVIMIENTOS_DOCUMENTOS cmd with(nolock)
	join DOCUMENTOS_CUSTODIA dc with(nolock)
	on cmd.CUSTODIA_ID = dc.CUSTODIA_ID
	join BANCOS ban with(nolock)
	on dc.codemp = ban.bco_codemp
	and dc.banco_id = ban.bco_bcoid
	where cmd.CONCILIACION_ID = @conciliacionId
END