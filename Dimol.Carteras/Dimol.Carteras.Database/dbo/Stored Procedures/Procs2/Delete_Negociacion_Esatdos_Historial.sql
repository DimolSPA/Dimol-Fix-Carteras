

Create Procedure Delete_Negociacion_Esatdos_Historial(@ngh_codemp integer, @ngh_anio smallint, @ngh_negid integer, @ngh_fecha datetime, @ngh_estado char (1)) as 
  DELETE FROM negociacion_estados_historial  
   WHERE ( negociacion_estados_historial.ngh_codemp = @ngh_codemp ) AND  
         ( negociacion_estados_historial.ngh_anio = @ngh_anio ) AND  
         ( negociacion_estados_historial.ngh_negid = @ngh_negid ) AND  
         ( negociacion_estados_historial.ngh_fecha = @ngh_fecha ) AND  
         ( negociacion_estados_historial.ngh_estado = @ngh_estado )
