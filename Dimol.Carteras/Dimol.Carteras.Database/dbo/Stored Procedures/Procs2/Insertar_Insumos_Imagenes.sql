

Create procedure Insertar_Insumos_Imagenes(@isi_codemp integer, @isi_insid integer, @isi_isiid integer, @isi_default char(1), @isi_orden integer, @isi_imagen image) as
  INSERT INTO insumo_imagenes  
         ( isi_codemp,   
           isi_insid,   
           isi_isiid,   
           isi_default,   
           isi_orden,   
           isi_imagen )  
  VALUES ( @isi_codemp,   
           @isi_insid,   
           @isi_isiid,   
           @isi_default,   
           @isi_orden,   
           @isi_imagen )
