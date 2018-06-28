Create Procedure Trae_Reporte_Torta_Generica_Judicial(@rol_codemp integer, @rol_pclid integer, @ccb_tipcart smallint, @mji_idid smallint) as
  SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nomfant,   
         cartera_clientes_documentos_cpbt_doc.ctc_rut,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecing,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.ccb_tipcambio,   
         materia_judicial_idiomas.mji_nombre,   
         materia_judicial.esj_orden,   
         estados_cartera_idiomas.eci_nombre,   
         rol_documentos.rdc_monto,   
         rol_documentos.rdc_saldo,   
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.pcc_codigo,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,  
         cartera_clientes_documentos_cpbt_doc.ccb_codmon,
         cartera_clientes_documentos_cpbt_doc.ccb_compromiso,
         ect_agrupa
     FROM rol,   
         rol_documentos,   
         cartera_clientes_documentos_cpbt_doc,   
         materia_estados,   
         materia_judicial,   
         materia_judicial_idiomas,   
         estados_cartera_idiomas  
   WHERE ( rol_documentos.rdc_codemp = rol.rol_codemp ) and  
         ( rol_documentos.rdc_rolid = rol.rol_rolid ) and  
         ( rol_documentos.rdc_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( rol_documentos.rdc_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( rol_documentos.rdc_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( rol_documentos.rdc_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( materia_estados.mej_codemp = rol.rol_codemp ) and  
         ( materia_estados.mej_esjid = rol.rol_esjid ) and  
         ( materia_estados.mej_estid = rol.rol_estid ) and  
         ( materia_judicial.esj_codemp = materia_estados.mej_codemp ) and  
         ( materia_judicial.esj_esjid = materia_estados.mej_esjid ) and  
         ( materia_judicial_idiomas.mji_codemp = materia_judicial.esj_codemp ) and  
         ( materia_judicial_idiomas.mji_esjid = materia_judicial.esj_esjid ) and  
         ( materia_estados.mej_codemp = estados_cartera_idiomas.eci_codemp ) and  
         ( materia_estados.mej_estid = estados_cartera_idiomas.eci_estid ) and  
         ( ( rol.rol_codemp = @rol_codemp ) AND  
         ( rol.rol_pclid = @rol_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_tipcart = @ccb_tipcart ) AND  
         ( materia_judicial_idiomas.mji_idid = @mji_idid ) AND  
         ( estados_cartera_idiomas.eci_idid = @mji_idid )   
         )  

union

 SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nomfant,   
         cartera_clientes_documentos_cpbt_doc.ctc_rut,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecing,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.ccb_tipcambio,   
         'NO DEMANDADO',   
         -1,   
         'SIN GESTIONA',   
         ccb_monto,   
         ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.pcc_codigo,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,  
         cartera_clientes_documentos_cpbt_doc.ccb_codmon,
         cartera_clientes_documentos_cpbt_doc.ccb_compromiso,
         -1
     FROM cartera_clientes_documentos_cpbt_doc  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @rol_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @rol_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = @mji_idid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_tipcart = @ccb_tipcart ) AND  
         ( convert(varchar,ccb_pclid) + '_' + convert(varchar,ccb_ctcid) + '_' + convert(varchar, ccb_ccbid) not in (  SELECT convert(varchar,rdc_pclid) + '_' + convert(varchar,rdc_ctcid) + '_' + convert(varchar, rdc_ccbid)  
                                                                                                                         FROM rol_documentos  
                                                                                                                        WHERE ( rol_documentos.rdc_codemp = @rol_codemp ) AND  
                                                                                                                              ( rol_documentos.rdc_pclid = @rol_pclid )   
                                                                                                                               )) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_estcpbt = 'J' )   


order by esj_orden, eci_nombre ;