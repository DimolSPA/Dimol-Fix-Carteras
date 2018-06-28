

Create Procedure Trae_Error(@error varchar(40), @idioma integer) as
  SELECT errores_idiomas.eri_descripcion  
    FROM errores,   
         errores_idiomas  
   WHERE ( errores_idiomas.eri_errid = errores.err_errid ) and  
         ( ( errores.err_codigo = @error ) AND  
         ( errores_idiomas.eri_idid = @idioma )   
         )
