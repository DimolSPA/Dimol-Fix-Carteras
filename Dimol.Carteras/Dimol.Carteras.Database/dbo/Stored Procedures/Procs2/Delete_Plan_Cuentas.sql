

Create Procedure Delete_Plan_Cuentas(@pct_codemp integer, @pct_pctid integer) as 

 DELETE FROM plan_cuentas_idiomas 
   WHERE ( plan_cuentas_idiomas.pci_codemp = @pct_codemp ) AND  
         ( plan_cuentas_idiomas.pci_pctid = @pct_pctid )   

 DELETE FROM plan_cuentas  
   WHERE ( plan_cuentas.pct_codemp = @pct_codemp ) AND  
         ( plan_cuentas.pct_pctid = @pct_pctid )
