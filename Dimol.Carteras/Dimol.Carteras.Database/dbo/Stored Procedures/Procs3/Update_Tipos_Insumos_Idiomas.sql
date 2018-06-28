

Create Procedure Update_Tipos_Insumos_Idiomas(@tii_codemp integer, @tii_tipid integer, @tii_idid integer, @tii_nombre varchar(80)) as
    UPDATE tipos_insumo_idiomas  
     SET tii_nombre = @tii_nombre  
   WHERE ( tipos_insumo_idiomas.tii_codemp = @tii_codemp ) AND  
         ( tipos_insumo_idiomas.tii_tipid = @tii_tipid ) AND  
         ( tipos_insumo_idiomas.tii_idid = @tii_idid )
