

Create  Procedure Update_Errores(@err_errid integer, @err_codigo varchar (20), @err_descripcion varchar (100)) as  
UPDATE errores  
     SET err_codigo = @err_codigo,   
         err_descripcion = @err_descripcion  
   WHERE errores.err_errid = @err_errid
