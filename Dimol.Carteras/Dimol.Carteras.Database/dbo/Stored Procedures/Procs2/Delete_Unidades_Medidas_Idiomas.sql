

Create Procedure Delete_Unidades_Medidas_Idiomas(@umi_unmid integer, @umi_idiid integer) as
 
  DELETE FROM unidades_medida_idiomas  
   WHERE ( unidades_medida_idiomas.umi_unmid = @umi_unmid ) AND  
         ( unidades_medida_idiomas.umi_idiid = @umi_idiid )
