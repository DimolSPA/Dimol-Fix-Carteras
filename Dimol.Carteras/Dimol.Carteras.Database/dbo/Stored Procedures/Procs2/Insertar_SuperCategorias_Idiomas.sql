

Create Procedure Insertar_SuperCategorias_Idiomas(@sci_codemp integer, @sci_spcid integer, @sci_idiid integer, @sci_nombre varchar(150)) as
  INSERT INTO supercategorias_idioma  
         ( sci_codemp,   
           sci_spcid,   
           sci_idiid,   
           sci_nombre )  
  VALUES ( @sci_codemp,   
           @sci_spcid,   
           @sci_idiid,   
           @sci_nombre )
