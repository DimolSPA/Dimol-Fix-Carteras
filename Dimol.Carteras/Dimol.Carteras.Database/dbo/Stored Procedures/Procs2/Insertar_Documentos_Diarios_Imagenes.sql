

Create Procedure Insertar_Documentos_Diarios_Imagenes(@ddd_codemp integer, @ddd_sucid integer, @ddd_anio smallint, 
                                                                                                @ddd_numdoc numeric (15), @ddd_imgid integer, @ddd_imagen image) as 
  INSERT INTO documentos_diarios_imagenes  
         ( ddd_codemp,   
           ddd_sucid,   
           ddd_anio,   
           ddd_numdoc,   
           ddd_imgid,   
           ddd_imagen )  
  VALUES ( @ddd_codemp,   
           @ddd_sucid,   
           @ddd_anio,   
           @ddd_numdoc,   
           @ddd_imgid,   
           @ddd_imagen )
