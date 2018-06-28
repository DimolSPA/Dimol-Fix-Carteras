

Create Procedure Update_Negociacion_Estados_Historial(@ngh_codemp integer, @ngh_anio smallint, @ngh_negid integer, 
																		@ngh_estado char (1), @ngh_comentario varchar (1000)) as 
  UPDATE negociacion_estados_historial  
     SET   ngh_estado = @ngh_estado,   
         ngh_comentario = @ngh_comentario  
   WHERE ( negociacion_estados_historial.ngh_codemp = @ngh_codemp ) AND  
         ( negociacion_estados_historial.ngh_anio = @ngh_anio ) AND  
         ( negociacion_estados_historial.ngh_negid = @ngh_negid ) AND  
         ( negociacion_estados_historial.ngh_estado = @ngh_estado )
