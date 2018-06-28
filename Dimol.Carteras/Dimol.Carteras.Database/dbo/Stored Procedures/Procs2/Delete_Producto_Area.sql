

Create Procedure Delete_Producto_Area(@pta_codemp integer, @pta_ptaid integer) as


  DELETE FROM producto_area_idioma  
   WHERE ( producto_area_idioma.pai_codemp = @pta_codemp ) AND  
         ( producto_area_idioma.pai_ptaid = @pta_ptaid )   


  DELETE FROM producto_area  
   WHERE ( producto_area.pta_codemp = @pta_codemp ) AND  
         ( producto_area.pta_ptaid = @pta_ptaid )
