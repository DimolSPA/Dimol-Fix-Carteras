

Create Procedure Insertar_Idiomas(@idi_idid integer, @idi_nombre varchar(60), @idi_idisrc varchar(20)) as
  INSERT INTO idiomas  
         ( idi_idid,   
           idi_nombre,   
           idi_idisrc )  
  VALUES ( @idi_idid,   
           @idi_nombre,   
           @idi_idisrc )
