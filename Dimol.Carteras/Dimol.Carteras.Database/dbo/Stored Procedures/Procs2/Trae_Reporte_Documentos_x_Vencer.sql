

Create Procedure Trae_Reporte_Documentos_x_Vencer(@ddi_codemp integer, @ddi_sucid integer, @ddi_tipmov char(1), @desde datetime, @hasta datetime) as
  SELECT view_documentos_diarios.pcl_rut,   
         view_documentos_diarios.pcl_nomfant,   
         view_documentos_diarios.ctc_rut,   
         view_documentos_diarios.ctc_nomfant,   
         view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.ddi_numcta,   
         view_documentos_diarios.ddi_custodia,   
         view_documentos_diarios.ddi_pagdir,   
         view_documentos_diarios.ddi_depositar,   
         view_documentos_diarios.ddi_fechadep,   
         view_documentos_diarios.mon_nombre,   
         view_documentos_diarios.ddi_saldo,   
         view_documentos_diarios.bco_nombre,   
         view_documentos_diarios.ddi_fecvenc  
    FROM view_documentos_diarios,   
         estados_documentos_diarios  
   WHERE ( view_documentos_diarios.ddi_codemp = estados_documentos_diarios.edc_codemp ) and  
         ( view_documentos_diarios.ddi_edcid = estados_documentos_diarios.edc_edcid ) and  
         ( ( view_documentos_diarios.ddi_codemp = @ddi_codemp ) AND  
         ( view_documentos_diarios.ddi_sucid = @ddi_sucid ) AND  
         ( view_documentos_diarios.ddi_tipmov = @ddi_tipmov ) AND  
         ( estados_documentos_diarios.edc_estado < 4 ) AND  
         ( ddi_fecvenc >= @desde and ddi_fecvenc <= @hasta ) AND  
         ( view_documentos_diarios.ddi_depositar in ('S', 'N') ) ) 
order by ddi_fecvenc
