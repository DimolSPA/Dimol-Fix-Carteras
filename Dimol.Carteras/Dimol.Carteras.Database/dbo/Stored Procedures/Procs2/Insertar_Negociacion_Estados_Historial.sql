

Create Procedure Insertar_Negociacion_Estados_Historial(@ngh_codemp integer, @ngh_anio smallint, @ngh_negid integer,
																		@ngh_estado char (1), @ngh_comentario varchar (1000)) as 
  INSERT INTO negociacion_estados_historial  
         ( ngh_codemp,   
           ngh_anio,   
           ngh_negid,   
           ngh_fecha,   
           ngh_estado,   
           ngh_comentario )  
  VALUES ( @ngh_codemp,   
           @ngh_anio,   
           @ngh_negid,   
           getdate(),   
           @ngh_estado,   
           @ngh_comentario )
