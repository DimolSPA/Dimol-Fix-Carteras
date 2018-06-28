

Create Procedure Insertar_Treeview_Idiomas(@tvi_trvid integer, @tvi_idid integer, @tvi_idiomas varchar(400)) as


   INSERT INTO treeview_idiomas  
         ( tvi_trvid,   
           tvi_idid,   
           tvi_idiomas )  
  VALUES ( @tvi_trvid,   
           @tvi_idid,   
           @tvi_idiomas )
