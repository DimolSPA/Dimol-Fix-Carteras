

Create Procedure Insertar_Pais(@id integer, @nombre varchar(150), @codtel smallint) as
  INSERT INTO pais  
         ( pai_paiid,   
           pai_nombre,   
           pai_codtel )  
  VALUES ( @id,   
           @nombre,   
           @codtel )
