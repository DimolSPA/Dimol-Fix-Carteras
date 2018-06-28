

Create Procedure Insertar_Giros(@gir_codemp integer, @gir_girid integer, @gir_nombre varchar (1200)) as
  INSERT INTO giros  
         ( gir_codemp,   
           gir_girid,   
           gir_nombre )  
  VALUES ( @gir_codemp,   
           @gir_girid,   
           @gir_nombre )
