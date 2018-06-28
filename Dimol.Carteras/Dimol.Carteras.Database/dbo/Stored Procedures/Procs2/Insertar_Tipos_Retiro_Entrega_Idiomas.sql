

Create Procedure Insertar_Tipos_Retiro_Entrega_Idiomas(@tri_codemp integer, @tri_treid integer, @tri_idid integer, @tri_nombre varchar (100)) as
  INSERT INTO tipos_retiro_entrega_idiomas  
         ( tri_codemp,   
           tri_treid,   
           tri_idid,   
           tri_nombre )  
  VALUES ( @tri_codemp,   
           @tri_treid,   
           @tri_idid,   
           @tri_nombre )
