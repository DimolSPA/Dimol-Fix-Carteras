

Create Procedure Update_Tipos_DocCont(@tdc_codemp integer, @tdc_tdcid integer, @tdc_nombre varchar (100)) as  
  UPDATE tipos_doccont  
     SET tdc_nombre = @tdc_nombre  
   WHERE ( tipos_doccont.tdc_codemp = @tdc_codemp ) AND  
         ( tipos_doccont.tdc_tdcid = @tdc_tdcid )
