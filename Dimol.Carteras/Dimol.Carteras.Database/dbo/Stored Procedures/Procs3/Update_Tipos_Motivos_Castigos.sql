

Create Procedure Update_Tipos_Motivos_Castigos(@tmc_codemp integer, @tmc_tmcid integer, @tmc_nombre varchar(200)) as
  UPDATE tipos_motivos_castigos  
     SET tmc_nombre = @tmc_nombre
   WHERE ( tipos_motivos_castigos.tmc_codemp = @tmc_codemp ) AND  
         ( tipos_motivos_castigos.tmc_tmcid = @tmc_tmcid )
