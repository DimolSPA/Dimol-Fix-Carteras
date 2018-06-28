

CREATE Procedure [dbo].[Trae_Reporte_Informe_PreJudicial_CodCarg](@ccb_codemp integer, @ccb_pclid integer, @ccb_tipcart integer, @idi_idid integer, @pcc_codid as integer) as
  SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nombre,   
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         view_datos_geograficos.pai_nombre,   
         view_datos_geograficos.reg_nombre,   
         view_datos_geograficos.ciu_nombre,   
         view_datos_geograficos.com_nombre,   
         cartera_clientes_documentos_cpbt_doc.ctc_direccion,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecing,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecultgest,   
         cartera_clientes_documentos_cpbt_doc.eci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_codmon,   
         cartera_clientes_documentos_cpbt_doc.mon_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastjud,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastotro,   
         cartera_clientes_documentos_cpbt_doc.ccb_intereses,   
         cartera_clientes_documentos_cpbt_doc.ccb_honorarios,   
         cartera_clientes_documentos_cpbt_doc.ccb_numesp,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_codigo,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,
         ccb_ctcid, ccb_pclid, ccb_ccbid
    FROM cartera_clientes_documentos_cpbt_doc,   
         view_datos_geograficos,   
         idiomas  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ctc_comid = view_datos_geograficos.com_comid ) and  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid ) and  
         ( ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_tipcart = @ccb_tipcart ) AND  
         --( cartera_clientes_documentos_cpbt_doc.ccb_fecvenc <= getdate() ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_estcpbt='V' and idi_idid = @idi_idid and pcc_codid = @pcc_codid )   
         )
         order by ctc_numero,ccb_fecvenc
