

Create Procedure Update_Servicios(@sve_codemp integer, @sve_sveid integer, @sve_nombre varchar(200), @sve_orden integer, @sve_foto varchar(200)) as
   UPDATE servicios  
     SET sve_nombre = @sve_nombre,   
         sve_orden = @sve_orden,   
         sve_foto = @sve_foto  
   WHERE ( servicios.sve_codemp = @sve_codemp ) AND  
         ( servicios.sve_sveid = @sve_sveid )
