

Create Procedure UltNum_Socios(@soc_codemp integer) as
  SELECT IsNull(Max(soc_socid)+1, 1)  
    FROM socios  
   WHERE socios.soc_codemp = @soc_codemp
