

Create Procedure Delete_Tipos_Retiro_Entrega(@tre_codemp integer, @tre_treid integer) as

  DELETE FROM tipos_retiro_entrega_idiomas  
   WHERE ( tipos_retiro_entrega_idiomas.tri_codemp = @tre_codemp ) AND  
         ( tipos_retiro_entrega_idiomas.tri_treid = @tre_treid ) 


  DELETE FROM tipos_retiro_entrega  
   WHERE ( tipos_retiro_entrega.tre_codemp = @tre_codemp ) AND  
         ( tipos_retiro_entrega.tre_treid = @tre_treid )
