

create procedure Trae_Reporte_Castigo_Judicial(@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer,  @cbc_desde numeric(15), @cbc_hasta numeric(15), @idi_idid integer) as
 SELECT DISTINCT view_cabecera_comprobantes.cbc_pclid,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.cbc_feccpbt,   
         view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nomfant,   
         view_datos_geograficos_a.pai_nombre,   
         view_datos_geograficos_a.reg_nombre,   
         view_datos_geograficos_a.ciu_nombre,   
         view_datos_geograficos_a.com_nombre,   
         provcli_sucursal.pcs_direccion,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.mon_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         detalle_comprobantes.dcc_saldo,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_docori,   
         cartera_clientes_documentos_cpbt_doc.ccb_docant,   
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.pcc_codigo,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numesp,   
         view_datos_geograficos_b.pai_nombre,   
         view_datos_geograficos_b.reg_nombre,   
         view_datos_geograficos_b.ciu_nombre,   
         view_datos_geograficos_b.com_nombre,   
         cartera_clientes_documentos_cpbt_doc.ctc_direccion,   
         view_rol_datos.rol_numero,   
         view_rol_datos.trb_nombre,
         cbc_pcsid,
         detalle_comprobantes.dcc_ctcid  
    FROM view_cabecera_comprobantes,   
         provcli_sucursal,   
         view_datos_geograficos view_datos_geograficos_a,   
         detalle_comprobantes,   
         cartera_clientes_documentos_cpbt_doc,   
         view_datos_geograficos view_datos_geograficos_b,   
         rol_documentos,   
         view_rol_datos  
   WHERE ( provcli_sucursal.pcs_comid = view_datos_geograficos_a.com_comid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = provcli_sucursal.pcs_codemp ) and
         ( view_cabecera_comprobantes.cbc_pclid = provcli_sucursal.pcs_pclid ) and  
         ( view_cabecera_comprobantes.cbc_pcsid = provcli_sucursal.pcs_pcsid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = detalle_comprobantes.dcc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_sucid = detalle_comprobantes.dcc_sucid ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = detalle_comprobantes.dcc_tpcid ) and  
         ( view_cabecera_comprobantes.cbc_numero = detalle_comprobantes.dcc_numero ) and  
         ( detalle_comprobantes.dcc_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( detalle_comprobantes.dcc_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( detalle_comprobantes.dcc_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( detalle_comprobantes.dcc_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( cartera_clientes_documentos_cpbt_doc.ctc_comid = view_datos_geograficos_b.com_comid ) and  
         ( view_cabecera_comprobantes.idi_idid = cartera_clientes_documentos_cpbt_doc.tci_idid ) and  
         ( view_cabecera_comprobantes.idi_idid = cartera_clientes_documentos_cpbt_doc.eci_idid ) and  
         ( view_cabecera_comprobantes.idi_idid = cartera_clientes_documentos_cpbt_doc.mci_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = rol_documentos.rdc_codemp ) and  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = rol_documentos.rdc_pclid ) and  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = rol_documentos.rdc_ctcid ) and  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ccbid = rol_documentos.rdc_ccbid ) and  
         ( rol_documentos.rdc_codemp = view_rol_datos.rol_codemp ) and  
         ( rol_documentos.rdc_rolid = view_rol_datos.rol_rolid ) and  
         ( view_cabecera_comprobantes.idi_idid = view_rol_datos.eci_idid ) and  
         ( view_cabecera_comprobantes.idi_idid = view_rol_datos.tci_idid ) and  
         ( view_cabecera_comprobantes.idi_idid = view_rol_datos.mji_idid ) and  
         ( view_cabecera_comprobantes.idi_idid = view_rol_datos.tipidi ) and  
         ( view_cabecera_comprobantes.idi_idid = view_rol_datos.eciidi ) and  
         ( view_cabecera_comprobantes.idi_idid = view_rol_datos.mci_idid ) and  
         ( ( view_cabecera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( view_cabecera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
         ( view_cabecera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
         ( view_cabecera_comprobantes.cbc_numero >= @cbc_desde ) AND  
         ( view_cabecera_comprobantes.cbc_numero <= @cbc_hasta ) AND  
         ( view_cabecera_comprobantes.cbt_estado in ( 'A','F' ) ) AND  
         ( view_cabecera_comprobantes.idi_idid = @idi_idid ) )
