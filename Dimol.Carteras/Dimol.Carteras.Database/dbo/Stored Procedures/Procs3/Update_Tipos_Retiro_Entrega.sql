

Create Procedure Update_Tipos_Retiro_Entrega(@tre_codemp integer, @tre_treid integer, @tre_nombre varchar (80)) as
  UPDATE tipos_retiro_entrega  
     SET tre_nombre = @tre_nombre  
   WHERE ( tipos_retiro_entrega.tre_codemp = @tre_codemp ) AND  
         ( tipos_retiro_entrega.tre_treid = @tre_treid )
