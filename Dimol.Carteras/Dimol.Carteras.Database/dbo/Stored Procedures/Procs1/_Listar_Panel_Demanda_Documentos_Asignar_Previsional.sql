CREATE PROCEDURE [dbo].[_Listar_Panel_Demanda_Documentos_Asignar_Previsional]
(
	@codemp int,
	@panelId int,
	@pclid int,
	@ctcid int
)
AS 
BEGIN
	SELECT cpbt.ccb_ccbid Ccbid,   
		cpbt.ccb_numero Numero,   
		cpbt.ccb_monto Monto, 
		cpbt.ccb_saldo Saldo 
	FROM cartera_clientes_cpbt_doc cpbt
	JOIN PANEL_DEMANDA_PREVISIONAL_DOCUMENTOS PDD
	ON cpbt.CCB_CODEMP = PDD.CODEMP
	AND cpbt.CCB_PCLID = PDD.PCLID
	AND cpbt.CCB_CTCID= PDD.CTCID
	AND cpbt.CCB_CCBID = PDD.CCBID
	WHERE PDD.PANEL_ID = @panelId
	AND PDD.ESTADO = 'ACT'
	AND PDD.CODEMP = @codemp
	AND cpbt.ccb_estcpbt = 'J'
	AND cpbt.ccb_ccbid not in (SELECT rol_documentos.rdc_ccbid  
								FROM rol_documentos
								WHERE  rol_documentos.rdc_codemp =  @codemp
								and rol_documentos.rdc_pclid =  @pclid
								and rol_documentos.rdc_ctcid =  @ctcid)
END