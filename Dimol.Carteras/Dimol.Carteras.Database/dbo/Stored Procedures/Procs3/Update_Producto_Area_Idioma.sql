

Create Procedure Update_Producto_Area_Idioma(@pai_codemp integer, @pai_ptaid integer, @pai_idiid integer, @pai_nombre varchar (150)) as
  UPDATE producto_area_idioma  
     SET pai_codemp = @pai_codemp,   
         pai_ptaid = @pai_ptaid,   
         pai_idiid = @pai_idiid,   
         pai_nombre = @pai_nombre  
   WHERE ( producto_area_idioma.pai_codemp = @pai_codemp ) AND  
         ( producto_area_idioma.pai_ptaid = @pai_ptaid ) AND  
         ( producto_area_idioma.pai_idiid = @pai_idiid )
