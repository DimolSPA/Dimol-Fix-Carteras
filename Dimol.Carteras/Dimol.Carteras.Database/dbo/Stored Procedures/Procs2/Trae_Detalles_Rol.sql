

Create Procedure Trae_Detalles_Rol(@dcr_codemp integer, @dcr_tpcid integer, @dcr_numero integer) as
 SELECT DISTINCT view_cabecera_comprobantes.tci_nombre,   
             view_cabecera_comprobantes.cbc_numprovcli,   
                      insumos.ins_nombre,   
             view_rol_datos.rol_numero,   
             cartera_clientes_documentos_cpbt_doc.ctc_numero,   
             cartera_clientes_documentos_cpbt_doc.ctc_digito,   
             cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
             cartera_clientes_documentos_cpbt_doc.sbc_rut,   
             cartera_clientes_documentos_cpbt_doc.sbc_nombre
             FROM detalle_comprobantes_rol,   
             view_cabecera_comprobantes,   
             view_rol_datos,   
             detalle_comprobantes,   
             insumos,   
             cartera_clientes_documentos_cpbt_doc
             WHERE  detalle_comprobantes.dcc_codemp = detalle_comprobantes_rol.dcr_codemp  and  
             detalle_comprobantes.dcc_sucid = detalle_comprobantes_rol.dcr_sucid  and  
             detalle_comprobantes.dcc_tpcid = detalle_comprobantes_rol.dcr_tpcid  and  
             detalle_comprobantes.dcc_numero = detalle_comprobantes_rol.dcr_numero  and  
             detalle_comprobantes.dcc_item = detalle_comprobantes_rol.dcr_item  and  
             insumos.ins_codemp = detalle_comprobantes.dcc_codemp  and  
             insumos.ins_insid = detalle_comprobantes.dcc_insid  and  
             detalle_comprobantes_rol.dcr_codemp = view_cabecera_comprobantes.cbc_codemp  and  
             detalle_comprobantes_rol.dcr_sucid = view_cabecera_comprobantes.cbc_sucid  and  
             detalle_comprobantes_rol.dcr_tpcid = view_cabecera_comprobantes.cbc_tpcid  and  
             detalle_comprobantes_rol.dcr_numero = view_cabecera_comprobantes.cbc_numero  and  
             detalle_comprobantes_rol.dcr_codemp = view_rol_datos.rol_codemp  and  
             detalle_comprobantes_rol.dcr_rolid = view_rol_datos.rol_rolid  and  
             cartera_clientes_documentos_cpbt_doc.ccb_codemp = view_rol_datos.rol_codemp  and  
             cartera_clientes_documentos_cpbt_doc.ccb_pclid = view_rol_datos.rol_pclid  and  
             cartera_clientes_documentos_cpbt_doc.ccb_ctcid = view_rol_datos.rol_ctcid  and  
             dcr_codemp = @dcr_codemp
             and detalle_comprobantes_rol.dcr_tpcid = @dcr_tpcid
             and detalle_comprobantes_rol.dcr_numero =  @dcr_numero
