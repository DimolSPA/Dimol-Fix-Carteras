

Create Procedure Delete_Impuestos(@ipt_codemp integer, @ipt_iptid smallint) as  
DELETE FROM impuestos  
   WHERE ( impuestos.ipt_codemp = @ipt_codemp ) AND  
         ( impuestos.ipt_iptid = @ipt_iptid )
