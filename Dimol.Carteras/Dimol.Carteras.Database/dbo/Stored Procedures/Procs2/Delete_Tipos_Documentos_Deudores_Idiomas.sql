

Create Procedure Delete_Tipos_Documentos_Deudores_Idiomas(@tdi_codemp integer, @tdi_tddid integer, @tdi_idid integer) as
   DELETE FROM tipos_documentos_deudores_idiomas  
   WHERE ( tipos_documentos_deudores_idiomas.tdi_codemp = @tdi_codemp ) AND  
         ( tipos_documentos_deudores_idiomas.tdi_tddid = @tdi_tddid ) AND  
         ( tipos_documentos_deudores_idiomas.tdi_idid = @tdi_idid )
