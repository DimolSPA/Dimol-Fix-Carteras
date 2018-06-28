

Create  Procedure Update_Errores_Idiomas(@eri_errid integer, @eri_idid integer, @eri_descripcion varchar (400)) as  
  UPDATE errores_idiomas  
     SET eri_descripcion = @eri_descripcion  
   WHERE ( errores_idiomas.eri_errid = @eri_errid ) AND  
         ( errores_idiomas.eri_idid = @eri_idid )
