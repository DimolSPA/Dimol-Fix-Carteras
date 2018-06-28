

Create Procedure Find_Error(@err_codigo varchar(20)) as
  SELECT errores.err_errid  
    FROM errores  
   WHERE errores.err_codigo = @err_codigo
