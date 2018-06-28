

Create Procedure Delete_Tipos_Documentos_Deudores(@tdd_codemp integer, @tdd_tddid integer) as
   DELETE FROM tipos_documentos_deudores_idiomas  
   WHERE ( tipos_documentos_deudores_idiomas.tdi_codemp = @tdd_codemp ) AND  
         ( tipos_documentos_deudores_idiomas.tdi_tddid = @tdd_tddid ) 


  DELETE FROM tipos_documentos_deudores  
   WHERE ( tipos_documentos_deudores.tdd_codemp = @tdd_codemp ) AND  
         ( tipos_documentos_deudores.tdd_tddid = @tdd_tddid )
