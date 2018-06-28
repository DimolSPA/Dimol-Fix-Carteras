

Create Procedure UltNum_Isapres(@isp_codemp integer) as
  SELECT IsNull(Max(isapres.isp_ispid)+1, 1) 
    FROM isapres  
   WHERE ( isapres.isp_codemp = @isp_codemp )
