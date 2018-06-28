CREATE Procedure [dbo].[_Trae_Reporte_Devolucion_Documentos_Deudor](@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer, @cbc_numero decimal(15), @idi_idid integer) as
 
 SELECT  'CASTIGO JUDICIAL'   tci_nombre ,   
         @cbc_numero cbc_numero,   
         '876705001' pcl_rut,   
         cc.pcl_nomfant,   
         '2016-12-30' cbc_feccpbt,   
         cc.ccb_estcpbt  cbt_estado,   
         cc.ctc_rut,   
         cc.ctc_nomfant,   
         cc.tci_nombre tip_doc,   
         cc.ccb_numero,   
         cc.ccb_asignado dcc_prereal,   
         cc.ccb_asignado dcc_saldo,    
         cc.ccb_fecdoc,   
         cc.ccb_fecvenc,   
         cc.ccb_numesp,   
         dg.pai_nombre,   
         dg.reg_nombre,   
         dg.ciu_nombre,   
         dg.com_nombre,   
         dg.com_codpost,   
         cc.ctc_direccion,   
         cc.mon_nombre,   
         @cbc_tpcid cbc_tpcid,
         pcc_codigo, 
         pcc_nombre,
         sbc_rut,
         sbc_nombre,
         ccb_docori,
         cc.ccb_pclid cbc_pclid,
         1 cbc_pcsid,
         cc.ccb_ctcid dcc_ctcid, ccb_docant 
    FROM cartera_clientes_documentos_cpbt_doc cc,   
         view_datos_geograficos  dg
   WHERE cc.ccb_codemp = @cbc_codemp
     and cc.ccb_ctcid = @idi_idid 
     and cc.ctc_comid = dg.com_comid 
     and cc.eci_estid = 21
     and cc.ccb_pclid = 78

 
 ORDER BY cc.sbc_rut
 -- SELECT view_cabecera_comprobantes.tci_nombre,   
 --        view_cabecera_comprobantes.cbc_numero,   
 --        view_cabecera_comprobantes.pcl_rut,   
 --        view_cabecera_comprobantes.pcl_nomfant,   
 --        view_cabecera_comprobantes.cbc_feccpbt,   
 --        view_cabecera_comprobantes.cbt_estado,   
 --        cartera_clientes_documentos_cpbt_doc.ctc_rut,   
 --        cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
 --        cartera_clientes_documentos_cpbt_doc.tci_nombre as tip_doc,   
 --        cartera_clientes_documentos_cpbt_doc.ccb_numero,   
 --        detalle_comprobantes.dcc_prereal,   
 --        detalle_comprobantes.dcc_saldo,   
 --        cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
 --        cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
 --        cartera_clientes_documentos_cpbt_doc.ccb_numesp,   
 --        view_datos_geograficos.pai_nombre,   
 --        view_datos_geograficos.reg_nombre,   
 --        view_datos_geograficos.ciu_nombre,   
 --        view_datos_geograficos.com_nombre,   
 --        view_datos_geograficos.com_codpost,   
 --        cartera_clientes_documentos_cpbt_doc.ctc_direccion,   
 --        cartera_clientes_documentos_cpbt_doc.mon_nombre,   
 --        view_cabecera_comprobantes.cbc_tpcid,
 --        pcc_codigo, 
 --        pcc_nombre,
 --        sbc_rut,
 --        sbc_nombre,
 --        ccb_docori,
 --        cbc_pclid,
 --        cbc_pcsid,
 --        detalle_comprobantes.dcc_ctcid, ccb_docant    
 --   FROM view_cabecera_comprobantes,   
 --        detalle_comprobantes,   
 --        cartera_clientes_documentos_cpbt_doc,   
 --        view_datos_geograficos  
 --  WHERE ( view_cabecera_comprobantes.cbc_codemp = detalle_comprobantes.dcc_codemp ) and  
 --        ( view_cabecera_comprobantes.cbc_sucid = detalle_comprobantes.dcc_sucid ) and  
 --        ( view_cabecera_comprobantes.cbc_tpcid = detalle_comprobantes.dcc_tpcid ) and  
 --        ( view_cabecera_comprobantes.cbc_numero = detalle_comprobantes.dcc_numero ) and  
 --        ( detalle_comprobantes.dcc_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
 --        ( detalle_comprobantes.dcc_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
 --        ( detalle_comprobantes.dcc_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
 --        ( cartera_clientes_documentos_cpbt_doc.ctc_comid = view_datos_geograficos.com_comid ) and  
 --        ( cartera_clientes_documentos_cpbt_doc.tci_idid = view_cabecera_comprobantes.idi_idid ) and  
 --        ( cartera_clientes_documentos_cpbt_doc.eci_idid = view_cabecera_comprobantes.idi_idid ) and  
 --        ( cartera_clientes_documentos_cpbt_doc.mci_idid = view_cabecera_comprobantes.idi_idid ) and  
 --        ( ( view_cabecera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
 --        ( view_cabecera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
 --        ( view_cabecera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
 --        ( view_cabecera_comprobantes.cbc_numero = @cbc_numero ) AND  
 --        ( view_cabecera_comprobantes.idi_idid = @idi_idid )   
 --        )
	--ORDER BY cartera_clientes_documentos_cpbt_doc.ccb_fecvenc
