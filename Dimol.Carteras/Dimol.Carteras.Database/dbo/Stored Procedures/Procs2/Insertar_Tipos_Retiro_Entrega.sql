

Create Procedure Insertar_Tipos_Retiro_Entrega(@tre_codemp integer, @tre_treid integer, @tre_nombre varchar (80)) as
  INSERT INTO tipos_retiro_entrega  
         ( tre_codemp,   
           tre_treid,   
           tre_nombre )  
  VALUES ( @tre_codemp,   
           @tre_treid,   
           @tre_nombre )
