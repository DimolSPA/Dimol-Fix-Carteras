﻿

Create Procedure Trae_Reporte_Documentos_Tercero(@ddi_codemp integer, @desde datetime, @hasta datetime, @idi_idid integer) as
  SELECT view_documentos_diarios.pcl_rut,   
         view_documentos_diarios.pcl_nomfant,   
         view_documentos_diarios.ctc_rut,   
         view_documentos_diarios.ctc_nomfant,   
         view_documentos_diarios.ddi_rutpag,   
         view_documentos_diarios.ddi_nompag,   
         view_documentos_diarios.mon_nombre,   
         view_documentos_diarios.edi_nombre,   
         view_documentos_diarios.ddi_monto,   
         view_documentos_diarios.ddi_tipcambio,   
         view_documentos_diarios.ddi_fecvenc,
         tci_nombre,
         ddi_numcta   
    FROM view_documentos_diarios,   
         idiomas  
   WHERE ( view_documentos_diarios.tci_idid = idiomas.idi_idid ) and  
         ( view_documentos_diarios.edi_idiid = idiomas.idi_idid ) and  
         ( ( view_documentos_diarios.ddi_codemp = @ddi_codemp ) AND  
         ( view_documentos_diarios.ddi_fecing >=@desde ) AND  
         ( view_documentos_diarios.ddi_fecing <= @hasta ) AND  
         ( view_documentos_diarios.ddi_titular = 'N' ) AND  
         ( idiomas.idi_idid = @idi_idid )   
         )   
ORDER BY view_documentos_diarios.ddi_fecvenc ASC
