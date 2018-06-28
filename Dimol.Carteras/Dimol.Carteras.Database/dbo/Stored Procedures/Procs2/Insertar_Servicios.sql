

Create Procedure Insertar_Servicios(@sve_codemp integer, @sve_sveid integer, @sve_nombre varchar(200), @sve_orden integer, @sve_foto varchar(200)) as
  INSERT INTO servicios  
         ( sve_codemp,   
           sve_sveid,   
           sve_nombre,   
           sve_orden,   
           sve_foto )  
  VALUES ( @sve_codemp,   
           @sve_sveid,   
           @sve_nombre,   
           @sve_orden,   
           @sve_foto )
