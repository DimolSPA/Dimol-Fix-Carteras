

Create Procedure Insertar_Tipos_Motivos_Castigos(@tmc_codemp integer, @tmc_tmcid integer, @tmc_nombre varchar(200)) as
  INSERT INTO tipos_motivos_castigos  
         ( tmc_codemp,   
           tmc_tmcid,   
           tmc_nombre )  
  VALUES ( @tmc_codemp,   
           @tmc_tmcid,   
           @tmc_nombre )
