

Create Procedure Insertar_Producto_Area_Idioma(@pai_codemp integer, @pai_ptaid integer, @pai_idiid integer, @pai_nombre varchar (150)) as
  INSERT INTO producto_area_idioma  
         ( pai_codemp,   
           pai_ptaid,   
           pai_idiid,   
           pai_nombre )  
  VALUES ( @pai_codemp,   
           @pai_ptaid,   
           @pai_idiid,   
           @pai_nombre )
