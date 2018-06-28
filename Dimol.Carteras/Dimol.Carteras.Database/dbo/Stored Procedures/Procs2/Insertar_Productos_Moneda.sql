

Create Procedure Insertar_Productos_Moneda(@pdm_codemp integer, @pdm_prodid numeric (15), @pdm_codmon integer, @pdm_precio decimal (25,2)) as
  INSERT INTO productos_moneda  
         ( pdm_codemp,   
           pdm_prodid,   
           pdm_codmon,   
           pdm_precio )  
  VALUES ( @pdm_codemp,   
           @pdm_prodid,   
           @pdm_codmon,   
           @pdm_precio )
