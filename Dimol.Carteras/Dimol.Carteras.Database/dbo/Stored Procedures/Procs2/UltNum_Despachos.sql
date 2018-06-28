

Create Procedure UltNum_Despachos(@dpc_codemp integer, @dpc_sucid integer) as
  SELECT IsNull(Max(dpc_dpcid)+1, 1)
    FROM despachos  
   WHERE ( despachos.dpc_codemp = @dpc_codemp ) AND  
         ( despachos.dpc_sucid = @dpc_sucid )
