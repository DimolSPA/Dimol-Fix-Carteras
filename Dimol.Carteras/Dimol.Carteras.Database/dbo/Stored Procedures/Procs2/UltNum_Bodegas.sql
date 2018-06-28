

Create Procedure UltNum_Bodegas(@bod_codemp integer) as
  SELECT IsNull(Max(bod_bodid)+1, 1)
    FROM bodegas  
   WHERE bodegas.bod_codemp = @bod_codemp
