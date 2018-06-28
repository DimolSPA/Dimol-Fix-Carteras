

Create Procedure Delete_Etiquetas_Idiomas(@eti_etiid integer, @eti_idid integer) as 
  DELETE FROM etiquetas_idiomas  
   WHERE ( etiquetas_idiomas.eti_etiid = @eti_etiid ) AND  
         ( etiquetas_idiomas.eti_idid = @eti_idid )
