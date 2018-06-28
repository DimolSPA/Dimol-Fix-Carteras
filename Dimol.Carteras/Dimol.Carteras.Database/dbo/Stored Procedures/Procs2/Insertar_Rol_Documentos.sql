

CREATE Procedure [dbo].[Insertar_Rol_Documentos](
	@rdc_codemp integer,
	@rdc_rolid integer,
	@rdc_pclid numeric (15),
	@rdc_ctcid numeric (15),
	@rdc_ccbid integer,
	@rdc_monto decimal (15,2),
	@rdc_saldo decimal (15,2)
) AS
	INSERT INTO rol_documentos
	(
		rdc_codemp,
		rdc_rolid,
		rdc_pclid,
		rdc_ctcid,
		rdc_ccbid,
		rdc_monto,
		rdc_saldo
	) VALUES (
		@rdc_codemp,
		@rdc_rolid,
		@rdc_pclid,
		@rdc_ctcid,
		@rdc_ccbid,
		@rdc_monto,
		@rdc_saldo
	)
	
	DECLARE @ccb_estidj int
	SELECT @ccb_estidj = rol_estid FROM ROL
	WHERE rol_rolid = @rdc_rolid

	UPDATE cartera_clientes_cpbt_doc
	SET ccb_estidj = @ccb_estidj
	WHERE
		( cartera_clientes_cpbt_doc.ccb_codemp = @rdc_codemp ) AND 
		( cartera_clientes_cpbt_doc.ccb_pclid = @rdc_pclid ) AND 
		( cartera_clientes_cpbt_doc.ccb_ctcid = @rdc_ctcid ) AND 
		( cartera_clientes_cpbt_doc.ccb_ccbid = @rdc_ccbid )