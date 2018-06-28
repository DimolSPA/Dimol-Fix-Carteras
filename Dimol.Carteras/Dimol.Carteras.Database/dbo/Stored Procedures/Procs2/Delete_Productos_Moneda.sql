

Create Procedure Delete_Productos_Moneda(@pdm_codemp integer, @pdm_prodid numeric (15), @pdm_codmon integer) as
  DELETE FROM productos_moneda  
   WHERE ( productos_moneda.pdm_codemp = @pdm_codemp ) AND  
         ( productos_moneda.pdm_prodid = @pdm_prodid ) AND  
         ( productos_moneda.pdm_codmon = @pdm_codmon )
