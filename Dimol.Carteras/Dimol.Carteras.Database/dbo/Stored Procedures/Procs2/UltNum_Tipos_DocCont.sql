

Create Procedure UltNum_Tipos_DocCont(@tdc_codemp integer) as
  SELECT IsNull(Max(tipos_doccont.tdc_tdcid)+1, 1)
    FROM tipos_doccont  
   WHERE ( tipos_doccont.tdc_codemp = @tdc_codemp )
