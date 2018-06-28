

Create Procedure Trae_Reporte_Documentos_Protestados(@ddi_codemp integer, @ddi_edcid integer, @idi_idid integer, @desde datetime, @hasta datetime) as
  SELECT view_documentos_diarios.pcl_rut,   
         view_documentos_diarios.pcl_nomfant,   
         view_documentos_diarios.ctc_rut,   
         view_documentos_diarios.ctc_nomfant,   
         view_documentos_diarios.bco_nombre,   
         view_documentos_diarios.mon_nombre,   
         view_documentos_diarios.ddi_fecvenc,   
         view_documentos_diarios.ddi_monto,   
         documentos_diarios_estados.dde_fecproc,   
         documentos_diarios_estados.dde_comentario,   
         view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.ddi_numcta,   
         view_documentos_diarios.edi_nombre  
    FROM view_documentos_diarios,   
         documentos_diarios_estados,   
         idiomas  
   WHERE ( view_documentos_diarios.ddi_codemp = documentos_diarios_estados.dde_codemp ) and  
         ( view_documentos_diarios.ddi_sucid = documentos_diarios_estados.dde_sucid ) and  
         ( view_documentos_diarios.ddi_anio = documentos_diarios_estados.dde_anio ) and  
         ( view_documentos_diarios.ddi_numdoc = documentos_diarios_estados.dde_numdoc ) and  
         ( view_documentos_diarios.ddi_edcid = documentos_diarios_estados.dde_edcid ) and  
         ( view_documentos_diarios.tci_idid = idiomas.idi_idid ) and  
         ( view_documentos_diarios.edi_idiid = idiomas.idi_idid ) and  
         ( ( view_documentos_diarios.ddi_codemp = @ddi_codemp ) AND  
         ( view_documentos_diarios.ddi_edcid = @ddi_edcid and ddi_tipmov = 'I' ) AND  
         ( idiomas.idi_idid = @idi_idid ) AND  
         ( documentos_diarios_estados.dde_fecproc >=@desde ) AND  
         ( documentos_diarios_estados.dde_fecproc <= @hasta ) )
