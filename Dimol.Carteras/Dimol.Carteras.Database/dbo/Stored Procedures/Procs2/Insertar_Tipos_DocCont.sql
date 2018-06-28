

Create Procedure Insertar_Tipos_DocCont(@tdc_codemp integer, @tdc_tdcid integer, @tdc_nombre varchar(100)) as
     INSERT INTO tipos_doccont  
         ( tdc_codemp,   
           tdc_tdcid,   
           tdc_nombre )  
  VALUES ( @tdc_codemp,   
           @tdc_tdcid,   
           @tdc_nombre )
