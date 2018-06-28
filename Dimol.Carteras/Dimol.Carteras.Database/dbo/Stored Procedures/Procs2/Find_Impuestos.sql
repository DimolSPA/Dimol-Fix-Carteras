

Create Procedure Find_Impuestos(@ipt_codemp integer, @ipt_iptid integer) as
  SELECT Count(impuestos.ipt_iptid)  
    FROM impuestos  
   WHERE ( impuestos.ipt_codemp = @ipt_codemp ) AND  
         ( impuestos.ipt_iptid = @ipt_iptid )
