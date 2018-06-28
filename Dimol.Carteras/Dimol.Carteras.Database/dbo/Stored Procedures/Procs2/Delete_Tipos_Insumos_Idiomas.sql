

Create Procedure Delete_Tipos_Insumos_Idiomas(@tii_codemp integer, @tii_tipid integer, @tii_idid integer) as
  DELETE FROM tipos_insumo_idiomas  
   WHERE ( tipos_insumo_idiomas.tii_codemp = @tii_codemp ) AND  
         ( tipos_insumo_idiomas.tii_tipid = @tii_tipid ) AND  
         ( tipos_insumo_idiomas.tii_idid = @tii_idid )
