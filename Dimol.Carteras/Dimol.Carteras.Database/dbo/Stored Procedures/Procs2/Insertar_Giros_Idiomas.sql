

Create Procedure Insertar_Giros_Idiomas(@gii_codemp integer, @gii_girid integer, @gii_idid integer, @gii_nombre varchar (1500)) as
  INSERT INTO giros_idiomas  
         ( gii_codemp,   
           gii_girid,   
           gii_idid,   
           gii_nombre )  
  VALUES ( @gii_codemp,   
           @gii_girid,   
           @gii_idid,   
           @gii_nombre )
