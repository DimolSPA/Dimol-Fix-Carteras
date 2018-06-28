

Create Procedure Delete_Plan_Cuentas_CCS(@pcc_codemp integer, @pcc_pctid integer, @pcc_ccsid integer) as 
  DELETE FROM plan_cuentas_ccs  
   WHERE ( plan_cuentas_ccs.pcc_codemp = @pcc_codemp ) AND  
         ( plan_cuentas_ccs.pcc_pctid = @pcc_pctid ) AND  
         ( plan_cuentas_ccs.pcc_ccsid = @pcc_ccsid )
