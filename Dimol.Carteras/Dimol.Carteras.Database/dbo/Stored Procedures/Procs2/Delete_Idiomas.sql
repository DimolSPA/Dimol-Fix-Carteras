

Create Procedure Delete_Idiomas(@idi_idid integer) as 
  DELETE FROM idiomas  
   WHERE idiomas.idi_idid = @idi_idid
