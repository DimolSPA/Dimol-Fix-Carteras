

Create Procedure Update_Productos_Moneda(@pdm_codemp integer, @pdm_prodid numeric (15), @pdm_codmon integer, @pdm_precio decimal (25,2)) as
  UPDATE productos_moneda  
     SET pdm_codemp = @pdm_codemp,   
         pdm_prodid = @pdm_prodid,   
         pdm_codmon = @pdm_codmon,   
         pdm_precio = @pdm_precio  
   WHERE ( productos_moneda.pdm_codemp = @pdm_codemp ) AND  
         ( productos_moneda.pdm_prodid = @pdm_prodid ) AND  
         ( productos_moneda.pdm_codmon = @pdm_codmon )
