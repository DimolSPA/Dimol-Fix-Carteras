

Create Procedure Trae_Plan_Ctas_DD(@pct_codemp integer, @pci_idid integer) as
  SELECT plan_cuentas.pct_pctid,   
         plan_cuentas.pct_codigo,   
         plan_cuentas_idiomas.pci_nombre  
    FROM plan_cuentas,   
         plan_cuentas_idiomas  
   WHERE ( plan_cuentas_idiomas.pci_codemp = plan_cuentas.pct_codemp ) and  
         ( plan_cuentas_idiomas.pci_pctid = plan_cuentas.pct_pctid ) and  
         ( ( plan_cuentas.pct_codemp =@pct_codemp ) AND  
         ( plan_cuentas_idiomas.pci_idid = @pci_idid ) AND  
         ( plan_cuentas.pct_codigo not in (  SELECT cuentas_padres.ctp_codigo  
                                               FROM cuentas_padres  
                                              WHERE cuentas_padres.ctp_codemp =@pct_codemp   
                                                     )) )   
ORDER BY plan_cuentas.pct_codigo ASC
