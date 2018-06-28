

Create procedure Update_Insumos_Imagenes(@isi_codemp integer, @isi_insid integer, @isi_isiid integer, @isi_default char(1), @isi_orden integer, @isi_imagen image) as

  UPDATE insumo_imagenes  
     SET isi_default = @isi_default,   
         isi_orden = @isi_orden,
         isi_imagen = @isi_imagen   
   WHERE ( insumo_imagenes.isi_codemp = @isi_codemp ) AND  
         ( insumo_imagenes.isi_insid = @isi_insid ) AND  
         ( insumo_imagenes.isi_isiid = @isi_isiid )
