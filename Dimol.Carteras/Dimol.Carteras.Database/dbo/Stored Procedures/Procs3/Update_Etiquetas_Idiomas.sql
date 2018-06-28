

Create  Procedure Update_Etiquetas_Idiomas(@eti_etiid integer, @eti_idid integer, @eti_descripcion varchar (400)) as  
  UPDATE etiquetas_idiomas  
     SET eti_descripcion = @eti_descripcion  
   WHERE ( etiquetas_idiomas.eti_etiid = @eti_etiid ) AND  
         ( etiquetas_idiomas.eti_idid = @eti_idid )
