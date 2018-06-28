

Create Procedure Insertar_Tipos_Insumos_Idiomas(@tii_codemp integer, @tii_tipid integer, @tii_idid integer, @tii_nombre varchar(80)) as
   INSERT INTO tipos_insumo_idiomas  
         ( tii_codemp,   
           tii_tipid,   
           tii_idid,   
           tii_nombre )  
  VALUES ( @tii_codemp,   
           @tii_tipid,   
           @tii_idid,   
           @tii_nombre )
