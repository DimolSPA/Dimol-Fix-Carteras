

 Create Procedure Insertar_Plan_Cuentas_CCS(@pcc_codemp integer, @pcc_pctid integer, @pcc_ccsid integer, @pcc_porcentaje decimal (5,2)) as
  INSERT INTO plan_cuentas_ccs  
         ( pcc_codemp,   
           pcc_pctid,   
           pcc_ccsid,   
           pcc_porcentaje )  
  VALUES ( @pcc_codemp,   
           @pcc_pctid,   
           @pcc_ccsid,   
           @pcc_porcentaje )
