

Create Procedure UltNum_Documentos_Diarios_Imagenes(@ddd_codemp integer, @ddd_sucid integer, @ddd_anio smallint, @ddd_numdoc integer) as
  SELECT IsNull(Max(ddd_imgid)+1, 1)
    FROM documentos_diarios_imagenes  
   WHERE ( documentos_diarios_imagenes.ddd_codemp = @ddd_codemp ) AND  
         ( documentos_diarios_imagenes.ddd_sucid = @ddd_sucid ) AND  
         ( documentos_diarios_imagenes.ddd_anio = @ddd_anio ) AND  
         ( documentos_diarios_imagenes.ddd_numdoc = @ddd_numdoc )
