

Create Procedure Insertar_Menu_Idiomas(@mni_menid integer, @mni_idid integer, @mni_nombre varchar(400)) as

  INSERT INTO menu_idiomas  
         ( mni_menid,   
           mni_idid,   
           mni_nombre )  
  VALUES ( @mni_menid,   
           @mni_idid,   
           @mni_nombre )
