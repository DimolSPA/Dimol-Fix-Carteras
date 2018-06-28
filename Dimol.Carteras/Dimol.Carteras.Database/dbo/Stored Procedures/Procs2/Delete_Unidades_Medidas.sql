

Create Procedure Delete_Unidades_Medidas(@umi_unmid integer) as
 
  DELETE FROM unidades_medida_idiomas  
   WHERE ( unidades_medida_idiomas.umi_unmid = @umi_unmid ) 

  DELETE FROM unidades_medida  
   WHERE unidades_medida.unm_unmid = @umi_unmid
