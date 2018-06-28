

Create Procedure Delete_Producto_Area_Idioma(@pai_codemp integer, @pai_ptaid integer, @pai_idiid integer) as
  DELETE FROM producto_area_idioma  
   WHERE ( producto_area_idioma.pai_codemp = @pai_codemp ) AND  
         ( producto_area_idioma.pai_ptaid = @pai_ptaid ) AND  
         ( producto_area_idioma.pai_idiid = @pai_idiid )
