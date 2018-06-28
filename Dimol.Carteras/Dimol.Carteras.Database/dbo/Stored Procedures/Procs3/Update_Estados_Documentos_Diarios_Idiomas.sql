

Create Procedure Update_Estados_Documentos_Diarios_Idiomas(@edi_codemp integer, @edi_edcid integer,
                                                                                                            @edi_idiid integer, @edi_nombre varchar (30)) as 
  UPDATE estados_documentos_diarios_idiomas  
     SET edi_nombre = @edi_nombre  
   WHERE ( estados_documentos_diarios_idiomas.edi_codemp = @edi_codemp ) AND  
         ( estados_documentos_diarios_idiomas.edi_edcid = @edi_edcid ) AND  
         ( estados_documentos_diarios_idiomas.edi_idiid = @edi_idiid )
