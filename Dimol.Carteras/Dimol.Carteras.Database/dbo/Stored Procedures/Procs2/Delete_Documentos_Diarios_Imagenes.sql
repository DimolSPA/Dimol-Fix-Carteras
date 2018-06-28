

Create Procedure Delete_Documentos_Diarios_Imagenes(@ddd_codemp integer, @ddd_sucid integer, @ddd_anio smallint,
                                                                                              @ddd_numdoc numeric (15), @ddd_imgid integer) as
  DELETE FROM documentos_diarios_imagenes  
   WHERE ( documentos_diarios_imagenes.ddd_codemp = @ddd_codemp ) AND  
         ( documentos_diarios_imagenes.ddd_sucid = @ddd_sucid ) AND  
         ( documentos_diarios_imagenes.ddd_anio = @ddd_anio ) AND  
         ( documentos_diarios_imagenes.ddd_numdoc = @ddd_numdoc ) AND  
         ( documentos_diarios_imagenes.ddd_imgid = @ddd_imgid )
