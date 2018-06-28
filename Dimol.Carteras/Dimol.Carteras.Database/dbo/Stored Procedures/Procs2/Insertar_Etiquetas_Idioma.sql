

  Create Procedure Insertar_Etiquetas_Idioma(@eti_etiid integer, @eti_idid integer, @eti_descripcion varchar(400)) as
  INSERT INTO etiquetas_idiomas  
         ( eti_etiid,   
           eti_idid,   
           eti_descripcion )  
  VALUES ( @eti_etiid,   
           @eti_idid,   
           @eti_descripcion )
