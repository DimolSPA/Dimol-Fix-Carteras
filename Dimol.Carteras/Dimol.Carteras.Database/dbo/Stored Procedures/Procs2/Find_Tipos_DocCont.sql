

Create Procedure Find_Tipos_DocCont(@tdc_codemp integer, @tdc_tdcid integer) as
  SELECT count(tipos_doccont.tdc_tdcid)  
    FROM tipos_doccont  
   WHERE ( tipos_doccont.tdc_codemp = @tdc_codemp ) AND  
         ( tipos_doccont.tdc_tdcid = @tdc_tdcid )
