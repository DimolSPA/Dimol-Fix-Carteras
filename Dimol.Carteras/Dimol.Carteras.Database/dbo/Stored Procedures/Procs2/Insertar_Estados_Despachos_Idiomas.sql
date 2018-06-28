

Create Procedure Insertar_Estados_Despachos_Idiomas(@edi_codemp integer, @edi_edpid integer,
																		@edi_idid integer, @edi_nombre varchar (150)) as
  INSERT INTO estados_despachos_idiomas  
         ( edi_codemp,   
           edi_edpid,   
           edi_idid,   
           edi_nombre )  
  VALUES ( @edi_codemp,   
           @edi_edpid,   
           @edi_idid,   
           @edi_nombre )
