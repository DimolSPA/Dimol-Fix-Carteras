

CREATE Procedure [dbo].[Trae_Reporte_Cancelaciones_Deudor](@apl_codemp integer, @apl_sucid integer, @api_ctcid numeric(15), @idi_idid integer) as
  SELECT aplicaciones.apl_anio,   
         aplicaciones.apl_mes,   
         CONVERT(VARCHAR(24),aplicaciones.apl_fecapl,121)   apl_fecapl,   
         view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.ddi_numcta,   
         cartera_clientes_documentos_cpbt_doc.ctc_rut,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         CONVERT(VARCHAR(24),cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,121)  ccb_fecvenc,   
         aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones.apl_accion,
        cartera_clientes_documentos_cpbt_doc.tci_nombre as TipCpbt  ,
         cartera_clientes_documentos_cpbt_doc.pcl_nomfant   
    FROM aplicaciones,   
         aplicaciones_items,   
         cartera_clientes_documentos_cpbt_doc,   
         view_documentos_diarios,   
         idiomas  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( aplicaciones_items.api_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( aplicaciones_items.api_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( aplicaciones_items.api_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( aplicaciones_items.api_codemp = view_documentos_diarios.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = view_documentos_diarios.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc = view_documentos_diarios.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc = view_documentos_diarios.ddi_numdoc ) and  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid ) and  
         ( view_documentos_diarios.edi_idiid = idiomas.idi_idid ) and  
         ( ( aplicaciones.apl_codemp = @apl_codemp ) AND  
         ( aplicaciones.apl_sucid = @apl_sucid ) AND  
         ( aplicaciones_items.api_ctcid = @api_ctcid ) AND  
         ( idiomas.idi_idid = @idi_idid ) )
         
         order by apl_fecapl
