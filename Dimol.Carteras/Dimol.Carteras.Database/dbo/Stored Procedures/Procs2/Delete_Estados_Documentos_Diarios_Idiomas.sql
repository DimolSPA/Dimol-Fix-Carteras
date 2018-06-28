

Create Procedure Delete_Estados_Documentos_Diarios_Idiomas(@edi_codemp integer, @edi_edcid integer, @edi_idiid integer) as
  DELETE FROM estados_documentos_diarios_idiomas  
   WHERE ( estados_documentos_diarios_idiomas.edi_codemp = @edi_codemp ) AND  
         ( estados_documentos_diarios_idiomas.edi_edcid = @edi_edcid ) AND  
         ( estados_documentos_diarios_idiomas.edi_idiid = @edi_idiid )
