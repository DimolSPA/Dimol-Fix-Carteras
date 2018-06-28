

Create Procedure Find_Idiomas(@idi_idid integer)  as
  SELECT idiomas.idi_idid  
    FROM idiomas  
   WHERE idiomas.idi_idid = @idi_idid
