

Create Procedure Delete_Tipos_DocCont(@tdc_codemp integer, @tdc_tdcid integer) as  

  DELETE FROM tipos_doccont_idiomas  
   WHERE ( tipos_doccont_idiomas.tci_codemp = @tdc_codemp ) AND  
         ( tipos_doccont_idiomas.tci_tdcid = @tdc_tdcid ) 

  DELETE FROM tipos_doccont  
   WHERE ( tipos_doccont.tdc_codemp = @tdc_codemp ) AND  
         ( tipos_doccont.tdc_tdcid = @tdc_tdcid )
