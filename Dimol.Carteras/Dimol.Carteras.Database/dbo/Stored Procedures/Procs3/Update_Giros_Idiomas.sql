

Create Procedure Update_Giros_Idiomas(@gii_codemp integer, @gii_girid integer, @gii_idid integer, @gii_nombre varchar (1500)) as  
  UPDATE giros_idiomas  
     SET gii_nombre = @gii_nombre  
   WHERE ( giros_idiomas.gii_codemp = @gii_codemp ) AND  
         ( giros_idiomas.gii_girid = @gii_girid ) AND  
         ( giros_idiomas.gii_idid = @gii_idid )
