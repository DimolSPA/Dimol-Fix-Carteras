

Create Procedure Update_Pais(@id integer, @nombre varchar(150), @codtel smallint) as
  UPDATE pais  
     SET pai_nombre = @nombre,   
         pai_codtel = @codtel  
   WHERE pais.pai_paiid = @id
