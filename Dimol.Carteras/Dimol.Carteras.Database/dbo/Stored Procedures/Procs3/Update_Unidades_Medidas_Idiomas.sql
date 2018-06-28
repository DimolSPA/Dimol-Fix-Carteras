

Create Procedure Update_Unidades_Medidas_Idiomas(@umi_unmid integer, @umi_idiid integer, @umi_nombre varchar(50)) as
   UPDATE unidades_medida_idiomas  
     SET umi_nombre = @umi_nombre  
   WHERE ( unidades_medida_idiomas.umi_unmid = @umi_unmid ) AND  
         ( unidades_medida_idiomas.umi_idiid = @umi_idiid )
