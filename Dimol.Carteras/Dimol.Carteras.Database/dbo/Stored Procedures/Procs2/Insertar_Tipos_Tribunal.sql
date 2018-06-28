

Create Procedure Insertar_Tipos_Tribunal(@ttb_codemp integer, @ttb_ttbid integer, @ttb_nombre varchar (50)) as  
  INSERT INTO tipos_tribunal  
         ( ttb_codemp,   
           ttb_ttbid,   
           ttb_nombre )  
  VALUES ( @ttb_codemp,   
           @ttb_ttbid,   
           @ttb_nombre )
