

Create Procedure Delete_Estados_Documentos_Diarios(@edc_codemp integer, @edc_edcid integer) as

  DELETE FROM estados_documentos_diarios_idiomas  
   WHERE ( estados_documentos_diarios_idiomas.edi_codemp = @edc_codemp ) AND  
         ( estados_documentos_diarios_idiomas.edi_edcid = @edc_edcid ) 


  DELETE FROM estados_documentos_diarios  
   WHERE ( estados_documentos_diarios.edc_codemp = @edc_codemp ) AND  
         ( estados_documentos_diarios.edc_edcid = @edc_edcid )
