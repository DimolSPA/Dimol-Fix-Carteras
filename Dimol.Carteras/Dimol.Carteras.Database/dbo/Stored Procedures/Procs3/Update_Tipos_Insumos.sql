

Create Procedure Update_Tipos_Insumos(@tpi_codemp integer, @tpi_tipid integer, @tpi_nombre varchar(80)) as
   UPDATE tipos_insumo  
     SET tpi_nombre = @tpi_nombre 
   WHERE ( tipos_insumo.tpi_codemp = @tpi_codemp ) AND  
         ( tipos_insumo.tpi_tipid = @tpi_tipid )
