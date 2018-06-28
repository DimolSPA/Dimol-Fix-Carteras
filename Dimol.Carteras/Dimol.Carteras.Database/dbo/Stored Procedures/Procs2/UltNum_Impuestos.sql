

Create Procedure UltNum_Impuestos(@ipt_codemp integer) as
  SELECT IsNull(Max(ipt_iptid)+1, 1) 
    FROM impuestos  
   WHERE ( impuestos.ipt_codemp = @ipt_codemp )
