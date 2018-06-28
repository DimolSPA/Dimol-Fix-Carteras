

Create Procedure UltNum_Insumos_Imagenes(@isi_codemp integer, @isi_insid integer) as
  SELECT IsNull(Max(isi_isiid)+1, 1)
    FROM insumo_imagenes  
   WHERE ( insumo_imagenes.isi_codemp = 1 ) AND  
         ( insumo_imagenes.isi_insid = 1 )
