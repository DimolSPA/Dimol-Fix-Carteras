

Create Procedure Insert_Error(@err_errid integer, @err_codigo varchar(20), @err_descripcion varchar(100)) as
  INSERT INTO errores  
         ( err_errid,   
           err_codigo,   
           err_descripcion )  
  VALUES ( @err_errid,   
           @err_codigo,   
           @err_descripcion )
