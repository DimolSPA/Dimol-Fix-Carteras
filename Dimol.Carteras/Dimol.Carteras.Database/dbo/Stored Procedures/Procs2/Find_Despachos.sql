

Create Procedure Find_Despachos(@dpc_codemp integer, @dpc_sucid integer, @dpc_dpcid numeric(15)) as
  SELECT count(despachos.dpc_dpcid)  
    FROM despachos  
   WHERE ( despachos.dpc_codemp = @dpc_codemp ) AND  
         ( despachos.dpc_sucid = @dpc_sucid )   AND
         ( despachos.dpc_dpcid = @dpc_dpcid )
