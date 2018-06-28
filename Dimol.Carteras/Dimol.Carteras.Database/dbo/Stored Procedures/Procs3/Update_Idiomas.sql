

Create  Procedure Update_Idiomas(@idi_idid integer, @idi_nombre varchar (60), @idi_idisrc varchar (20)) as  
  UPDATE idiomas  
     SET idi_nombre = @idi_nombre,   
         idi_idisrc = @idi_idisrc  
   WHERE idiomas.idi_idid = @idi_idid
