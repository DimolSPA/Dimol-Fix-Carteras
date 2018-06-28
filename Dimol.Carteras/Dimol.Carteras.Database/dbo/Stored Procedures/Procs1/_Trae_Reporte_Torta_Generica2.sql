CREATE Procedure [dbo].[_Trae_Reporte_Torta_Generica2](@cbc_codemp integer, @ccb_pclid integer, @ccb_estcpbt char(1),  @idi_idid integer) as  
--CREATE Procedure [dbo].[_Trae_Reporte_Torta_Generica2](@cbc_codemp integer, @ccb_pclid integer, @ccb_estcpbt char(1),  @pcc_codigo int, @idi_idid integer) as  
SELECT cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
		 cartera_clientes_documentos_cpbt_doc.pcl_rut,
		 cartera_clientes_documentos_cpbt_doc.pcl_nomfant,
         cartera_clientes_documentos_cpbt_doc.ctc_rut,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         --cartera_clientes_documentos_cpbt_doc.,   
         cartera_clientes_documentos_cpbt_doc.ccb_estcpbt,   
         cartera_clientes_documentos_cpbt_doc.ctc_rut,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre as tip_doc,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.ccb_numesp,   
         view_datos_geograficos.pai_nombre,   
         view_datos_geograficos.reg_nombre,   
         view_datos_geograficos.ciu_nombre,   
         view_datos_geograficos.com_nombre,   
         view_datos_geograficos.com_codpost,   
         cartera_clientes_documentos_cpbt_doc.ctc_direccion,   
         cartera_clientes_documentos_cpbt_doc.mon_nombre,   
         cartera_clientes_documentos_cpbt_doc.tci_tpcid,
         pcc_codigo, 
         pcc_nombre,
         sbc_rut,
         sbc_nombre,
         ccb_docori,
         cartera_clientes_documentos_cpbt_doc.ccb_pclid,
		 cartera_clientes_documentos_cpbt_doc.ccb_ctcid
         --cbc_pcsid,
         --detalle_comprobantes.dcc_ctcid, ccb_docant    
    FROM-- view_cabecera_comprobantes,   
         --detalle_comprobantes,   
         cartera_clientes_documentos_cpbt_doc,   
         view_datos_geograficos  
   WHERE --( view_cabecera_comprobantes.cbc_codemp = detalle_comprobantes.dcc_codemp ) and  
   --      ( view_cabecera_comprobantes.cbc_sucid = detalle_comprobantes.dcc_sucid ) and  
   --      ( view_cabecera_comprobantes.cbc_tpcid = detalle_comprobantes.dcc_tpcid ) and  
   --      ( view_cabecera_comprobantes.cbc_numero = detalle_comprobantes.dcc_numero ) and  
   --      ( detalle_comprobantes.dcc_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
   --      ( detalle_comprobantes.dcc_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
   --      ( detalle_comprobantes.dcc_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( cartera_clientes_documentos_cpbt_doc.ctc_comid = view_datos_geograficos.com_comid ) and  
         --( cartera_clientes_documentos_cpbt_doc.tci_idid = view_cabecera_comprobantes.idi_idid ) and  
         --( cartera_clientes_documentos_cpbt_doc.eci_idid = view_cabecera_comprobantes.idi_idid ) and  
         --( cartera_clientes_documentos_cpbt_doc.mci_idid = view_cabecera_comprobantes.idi_idid ) and  
         ( ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @cbc_codemp ) AND  
       --  ( cartera_clientes_documentos_cpbt_doc. = 1 ) AND  
         --( cartera_clientes_documentos_cpbt_doc.tci_tpcid = 31 ) AND  
		( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND --85
		( cartera_clientes_documentos_cpbt_doc.ccb_estcpbt = @ccb_estcpbt ) AND --V
		--( cartera_clientes_documentos_cpbt_doc.pcc_codigo = @pcc_codigo ) AND
         --( view_cabecera_comprobantes.cbc_numero = @cbc_numero ) AND  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = @idi_idid )   
         )
	ORDER BY cartera_clientes_documentos_cpbt_doc.ccb_fecvenc
