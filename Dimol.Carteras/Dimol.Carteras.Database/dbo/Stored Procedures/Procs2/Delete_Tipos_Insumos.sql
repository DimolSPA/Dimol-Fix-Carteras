

Create Procedure Delete_Tipos_Insumos(@tii_codemp integer, @tii_tipid integer) as
  DELETE FROM tipos_insumo_idiomas  
   WHERE ( tipos_insumo_idiomas.tii_codemp = @tii_codemp ) AND  
         ( tipos_insumo_idiomas.tii_tipid = @tii_tipid )   


  DELETE FROM tipos_insumo  
   WHERE ( tipos_insumo.tpi_codemp = @tii_codemp ) AND  
         ( tipos_insumo.tpi_tipid = @tii_tipid )
