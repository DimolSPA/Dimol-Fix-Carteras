

Create Procedure Delete_Estados_Despachos_Idiomas(@edi_codemp integer, @edi_edpid integer, @edi_idid integer) as
  DELETE FROM estados_despachos_idiomas  
   WHERE ( estados_despachos_idiomas.edi_codemp = @edi_codemp ) AND  
         ( estados_despachos_idiomas.edi_edpid = @edi_edpid ) AND  
         ( estados_despachos_idiomas.edi_idid = @edi_idid )
