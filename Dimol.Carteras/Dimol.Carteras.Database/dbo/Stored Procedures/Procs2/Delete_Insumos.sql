

Create procedure Delete_Insumos(@ins_codemp integer, @ins_insid integer) as

  DELETE FROM insumo_equivalencias  
   WHERE ( insumo_equivalencias.ieq_codemp = @ins_codemp ) AND  
         ( insumo_equivalencias.ieq_insid = @ins_insid )   


  DELETE FROM insumo_especificaciones_idiomas  
   WHERE ( insumo_especificaciones_idiomas.iei_codemp = @ins_codemp ) AND  
         ( insumo_especificaciones_idiomas.iei_insid = @ins_insid )   

  DELETE FROM insumo_especificaciones  
   WHERE ( insumo_especificaciones.ise_codemp = @ins_codemp ) AND  
         ( insumo_especificaciones.ise_insid = @ins_insid )   


  DELETE FROM insumo_imagenes  
   WHERE ( insumo_imagenes.isi_codemp = @ins_codemp ) AND  
         ( insumo_imagenes.isi_insid = @ins_insid )   


  DELETE FROM insumos  
   WHERE ( insumos.ins_codemp = @ins_codemp ) AND  
         ( insumos.ins_insid = @ins_insid )
