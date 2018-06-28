

Create Procedure Update_Unidades_Medidas(@unm_unmid integer, @unm_nombre varchar(20), @unm_simbolo varchar(5), @unm_agrupa smallint) as
  UPDATE unidades_medida  
     SET unm_nombre = @unm_nombre,   
         unm_simbolo = @unm_simbolo,   
         unm_agrupa = @unm_agrupa  
   WHERE unidades_medida.unm_unmid = @unm_unmid
