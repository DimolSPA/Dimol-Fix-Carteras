

Create Procedure Insertar_Unidades_Medidas(@unm_unmid integer, @unm_nombre varchar(20), @unm_simbolo varchar(5), @unm_agrupa smallint) as
  INSERT INTO unidades_medida  
         ( unm_unmid,   
           unm_nombre,   
           unm_simbolo,   
           unm_agrupa )  
  VALUES ( @unm_unmid,   
           @unm_nombre,   
           @unm_simbolo,   
           @unm_agrupa )
