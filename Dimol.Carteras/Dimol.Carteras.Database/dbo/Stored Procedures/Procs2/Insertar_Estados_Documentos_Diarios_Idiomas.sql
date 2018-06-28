

Create Procedure Insertar_Estados_Documentos_Diarios_Idiomas(@edi_codemp integer, @edi_edcid integer,
                                                                                                           @edi_idiid integer, @edi_nombre varchar (30)) as 
 INSERT INTO estados_documentos_diarios_idiomas  
         ( edi_codemp,   
           edi_edcid,   
           edi_idiid,   
           edi_nombre )  
  VALUES ( @edi_codemp,   
           @edi_edcid,   
           @edi_idiid,   
           @edi_nombre )
