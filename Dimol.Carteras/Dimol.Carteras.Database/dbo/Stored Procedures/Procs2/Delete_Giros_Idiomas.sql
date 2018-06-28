

Create Procedure Delete_Giros_Idiomas(@gii_codemp integer, @gii_girid integer, @gii_idid integer) as  
  DELETE FROM giros_idiomas  
   WHERE ( giros_idiomas.gii_codemp = @gii_codemp ) AND  
         ( giros_idiomas.gii_girid = @gii_girid ) AND  
         ( giros_idiomas.gii_idid = @gii_idid )
