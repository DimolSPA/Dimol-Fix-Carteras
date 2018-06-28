

Create Procedure Update_Transporte(@tra_codemp integer, @tra_tptid integer, @tra_traid integer, @tra_nombre varchar(200), @tra_marca varchar(80),
											    @tra_numero varchar(20), @tra_descripcion varchar(500), @tra_estado char(1)) as
 
  UPDATE transporte  
     SET tra_tptid = @tra_tptid,   
         tra_nombre = @tra_nombre,   
         tra_marca = @tra_marca,   
         tra_numero = @tra_numero,   
         tra_descripcion = @tra_descripcion,   
         tra_estado = @tra_estado  
   WHERE ( transporte.tra_codemp = @tra_codemp ) AND  
         ( transporte.tra_traid = @tra_traid )
