

Create Procedure Insertar_Unidades_Medidas_Idiomas(@umi_unmid integer, @umi_idiid integer, @umi_nombre varchar(50)) as
   INSERT INTO unidades_medida_idiomas  
         ( umi_unmid,   
           umi_idiid,   
           umi_nombre )  
  VALUES ( @umi_unmid,   
           @umi_idiid,   
           @umi_nombre )
