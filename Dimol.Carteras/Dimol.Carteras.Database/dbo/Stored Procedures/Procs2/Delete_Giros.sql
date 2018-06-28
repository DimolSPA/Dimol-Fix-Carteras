

Create Procedure Delete_Giros(@gir_codemp integer, @gir_girid integer) as  
  
    DELETE FROM giros_idiomas  
   WHERE ( giros_idiomas.gii_codemp = @gir_codemp ) AND  
         ( giros_idiomas.gii_girid = @gir_girid )   
           
  DELETE FROM giros  
   WHERE ( giros.gir_codemp = @gir_codemp ) AND  
         ( giros.gir_girid = @gir_girid )
