



CREATE Procedure [dbo].[_Trae_Cartera_Clientes_CpbtDoc_Ingresos](@ccb_codemp integer, @ccb_pclid integer, @idi_idid integer, @desde datetime, @hasta datetime, @ccb_tipcart smallint) as
  SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nombre,   
         cartera_clientes_documentos_cpbt_doc.ctc_numero,   
         cartera_clientes_documentos_cpbt_doc.ctc_digito,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.mon_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_asignado, 
		 cartera_clientes_documentos_cpbt_doc.sbc_rut,		 
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,
         mci_nombre,
         ccb_docori,
         ccb_saldo,
         ccb_asignado - ccb_saldo   as dif,
		 cartera_clientes_documentos_cpbt_doc.ccb_fecing 
    FROM cartera_clientes_documentos_cpbt_doc,   
         idiomas  
   WHERE ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid ) and  
         ( ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( idiomas.idi_idid = @idi_idid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_estcpbt <> 'X' and (convert(date, ccb_fecing) between @desde and @hasta) and ccb_tipcart = @ccb_tipcart  )   
         ) 
order by ctc_numero, ccb_fecvenc, ccb_numero


