

Create Procedure Insertar_Transporte(@tra_codemp integer, @tra_tptid integer, @tra_traid integer, @tra_nombre varchar(200), @tra_marca varchar(80),
											    @tra_numero varchar(20), @tra_descripcion varchar(500)) as
  INSERT INTO transporte  
         ( tra_codemp,   
           tra_tptid,   
           tra_traid,   
           tra_nombre,   
           tra_marca,   
           tra_numero,   
           tra_descripcion,
           tra_estado )  
  VALUES ( @tra_codemp,   
           @tra_tptid,   
           @tra_traid,   
           @tra_nombre,   
           @tra_marca,   
           @tra_numero,   
           @tra_descripcion,   
           'A' )
