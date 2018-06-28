CREATE procedure [dbo].[_Trae_Datos_CpbtDoc] (@codemp int, @tpcid int, @numero int) as
SELECT rol_documentos.rdc_pclid,   
       rol_documentos.rdc_ctcid,   
       rol_documentos.rdc_ccbid, 
	   DETALLE_COMPROBANTES_ROL.DCR_MONTO --,   
       --cartera_clientes_cpbt_doc.ccb_estid,   
       --cartera_clientes_cpbt_doc.ccb_estcpbt
FROM rol_documentos,   
     cartera_clientes_cpbt_doc, 
	 DETALLE_COMPROBANTES_ROL
WHERE  cartera_clientes_cpbt_doc.ccb_codemp = rol_documentos.rdc_codemp  and  
	   cartera_clientes_cpbt_doc.ccb_pclid = rol_documentos.rdc_pclid  and  
       cartera_clientes_cpbt_doc.ccb_ctcid = rol_documentos.rdc_ctcid  and  
       cartera_clientes_cpbt_doc.ccb_ccbid = rol_documentos.rdc_ccbid  and  
	   rol_documentos.rdc_rolid = DETALLE_COMPROBANTES_ROL.DCR_ROLID and 
	   DETALLE_COMPROBANTES_ROL.DCR_TPCID = @tpcid and
	   DETALLE_COMPROBANTES_ROL.DCR_NUMERO = @numero and
       rol_documentos.rdc_codemp = @codemp 
       --and rol_documentos.rdc_rolid in (select DCR_ROLID 
		--								from DETALLE_COMPROBANTES_ROL 
		--								WHERE  DCR_TPCID = @tpcid AND 
		--								DCR_NUMERO = @numero)
