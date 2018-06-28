

Create Procedure Insertar_Tipos_Insumos(@tpi_codemp integer, @tpi_tipid integer, @tpi_nombre varchar(80)) as
  INSERT INTO tipos_insumo  
         ( tpi_codemp,   
           tpi_tipid,   
           tpi_nombre )  
  VALUES ( @tpi_codemp,   
           @tpi_tipid,   
           @tpi_nombre )
