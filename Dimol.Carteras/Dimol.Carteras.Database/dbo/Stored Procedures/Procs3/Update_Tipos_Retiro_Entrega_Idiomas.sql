

Create Procedure Update_Tipos_Retiro_Entrega_Idiomas(@tri_codemp integer, @tri_treid integer, @tri_idid integer, @tri_nombre varchar (100)) as
   UPDATE tipos_retiro_entrega_idiomas  
     SET tri_nombre = @tri_nombre  
   WHERE ( tipos_retiro_entrega_idiomas.tri_codemp = @tri_codemp ) AND  
         ( tipos_retiro_entrega_idiomas.tri_treid = @tri_treid ) AND  
         ( tipos_retiro_entrega_idiomas.tri_idid = @tri_idid )
