

Create procedure Delete_Insumos_Imagenes(@isi_codemp integer, @isi_insid integer, @isi_isiid integer) as

  DELETE FROM insumo_imagenes  
   WHERE ( insumo_imagenes.isi_codemp = @isi_codemp ) AND  
         ( insumo_imagenes.isi_insid = @isi_insid ) AND  
         ( insumo_imagenes.isi_isiid = @isi_isiid )
