

Create Procedure Delete_Errores_Idiomas(@eri_errid integer, @eri_idid integer) as 
  DELETE FROM errores_idiomas  
   WHERE ( errores_idiomas.eri_errid = @eri_errid ) AND  
         ( errores_idiomas.eri_idid = @eri_idid )
