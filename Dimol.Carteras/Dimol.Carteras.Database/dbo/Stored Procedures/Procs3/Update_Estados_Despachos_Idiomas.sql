

Create Procedure Update_Estados_Despachos_Idiomas(@edi_codemp integer, @edi_edpid integer,
																	@edi_idid integer, @edi_nombre varchar (150)) as
  UPDATE estados_despachos_idiomas  
     SET edi_codemp = @edi_codemp,   
         edi_edpid = @edi_edpid,   
         edi_idid = @edi_idid,   
         edi_nombre = @edi_nombre  
   WHERE ( estados_despachos_idiomas.edi_codemp = @edi_codemp ) AND  
         ( estados_despachos_idiomas.edi_edpid = @edi_edpid ) AND  
         ( estados_despachos_idiomas.edi_idid = @edi_idid )
