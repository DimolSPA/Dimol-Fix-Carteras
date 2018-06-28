

Create Procedure Delete_Tipos_Retiro_Entrega_Idiomas(@tri_codemp integer, @tri_treid integer, @tri_idid integer) as
  DELETE FROM tipos_retiro_entrega_idiomas  
   WHERE ( tipos_retiro_entrega_idiomas.tri_codemp = @tri_codemp ) AND  
         ( tipos_retiro_entrega_idiomas.tri_treid = @tri_treid ) AND  
         ( tipos_retiro_entrega_idiomas.tri_idid = @tri_idid )
