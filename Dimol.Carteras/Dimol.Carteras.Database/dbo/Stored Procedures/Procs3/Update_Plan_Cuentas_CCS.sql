

Create Procedure Update_Plan_Cuentas_CCS(@pcc_codemp integer, @pcc_pctid integer, @pcc_ccsid integer, @pcc_porcentaje decimal (5,2)) as  
  UPDATE plan_cuentas_ccs  
     SET pcc_porcentaje = @pcc_porcentaje  
   WHERE ( plan_cuentas_ccs.pcc_codemp = @pcc_codemp ) AND  
         ( plan_cuentas_ccs.pcc_pctid = @pcc_pctid ) AND  
         ( plan_cuentas_ccs.pcc_ccsid = @pcc_ccsid )
