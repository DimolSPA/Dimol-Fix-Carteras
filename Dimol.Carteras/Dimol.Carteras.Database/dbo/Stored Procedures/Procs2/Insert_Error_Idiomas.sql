

Create Procedure Insert_Error_Idiomas(@eri_errid integer, @eri_idid integer, @eri_descripcion varchar(400)) as
   INSERT INTO errores_idiomas  
         ( eri_errid,   
           eri_idid,   
           eri_descripcion )  
  VALUES ( @eri_errid,   
           @eri_idid,   
           @eri_descripcion )
