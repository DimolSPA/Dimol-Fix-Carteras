

create Procedure Trae_Reporte_Cartera_Clientes_RetEnt(@ccb_codemp integer, @desde datetime, @hasta datetime, @idioma integer) as    
SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,
       cartera_clientes_documentos_cpbt_doc.pcl_nomfant,      
       cartera_clientes_documentos_cpbt_doc.ctc_numero,             
       cartera_clientes_documentos_cpbt_doc.ctc_digito,              cartera_clientes_documentos_cpbt_doc.ctc_nomfant,
       cartera_clientes_documentos_cpbt_doc.tci_nombre,       
       cartera_clientes_documentos_cpbt_doc.ccb_numero,              cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,
       cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,      
       cartera_clientes_documentos_cpbt_doc.ccb_monto,              cartera_clientes_documentos_cpbt_doc.ccb_saldo,
       cartera_clientes_documentos_cpbt_doc.mon_nombre,       
       view_datos_geograficos.pai_nombre,              view_datos_geograficos.reg_nombre,
       view_datos_geograficos.ciu_nombre,              view_datos_geograficos.com_nombre,
       view_datos_geograficos.com_codpost,              cartera_clientes_retiros_entrega.cre_direccion,
       cartera_clientes_retiros_entrega.cre_contacto,         
       cartera_clientes_retiros_entrega.cre_comentario,              cartera_clientes_retiros_entrega.cre_telefono,
       cartera_clientes_retiros_entrega.cre_copia,            
       cartera_clientes_retiros_entrega.cre_fecha,              cartera_clientes_retiros_entrega.cre_horini,
       cartera_clientes_retiros_entrega.cre_horfin        
FROM cartera_clientes_documentos_cpbt_doc,              cartera_clientes_retiros_entrega,
             view_datos_geograficos       
WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp= cartera_clientes_retiros_entrega.cre_codemp ) and             
      ( cartera_clientes_documentos_cpbt_doc.ccb_pclid= cartera_clientes_retiros_entrega.cre_pclid ) and
      ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid= cartera_clientes_retiros_entrega.cre_ctcid ) and             
      ( cartera_clientes_documentos_cpbt_doc.ccb_ccbid= cartera_clientes_retiros_entrega.cre_ccbid ) and             
      ( cartera_clientes_retiros_entrega.cre_comid= view_datos_geograficos.com_comid ) and             
      ( ( cartera_clientes_documentos_cpbt_doc.ccb_codemp= @ccb_codemp ) AND             
      ( cartera_clientes_retiros_entrega.cre_fecha >=@desde ) AND             
      ( cartera_clientes_retiros_entrega.cre_fecha<= @hasta ) AND             
      ( cartera_clientes_documentos_cpbt_doc.tci_idid= @idioma ) AND             
      ( cartera_clientes_documentos_cpbt_doc.eci_idid= @idioma ) AND             
      ( cartera_clientes_documentos_cpbt_doc.mci_idid= @idioma ))       
order by cre_fecha, cre_horini
