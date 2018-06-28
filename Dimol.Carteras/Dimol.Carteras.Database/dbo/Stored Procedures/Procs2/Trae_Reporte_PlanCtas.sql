

Create procedure Trae_Reporte_PlanCtas(@codemp integer, @idioma integer) as
  SELECT cuentas_padres.ctp_codigo,   
         cuentas_padres_idiomas.cpi_nombre,   
         plan_cuentas.pct_codigo,   
         plan_cuentas_idiomas.pci_nombre,   
         plan_cuentas.pct_ccs,   
         plan_cuentas.pct_activos,   
         plan_cuentas_ccs.pcc_ccsid,   
         centro_costos_idiomas.csi_nombre,   
         plan_cuentas_ccs.pcc_porcentaje  
    FROM {oj plan_cuentas LEFT OUTER JOIN plan_cuentas_ccs ON plan_cuentas.pct_codemp = plan_cuentas_ccs.pcc_codemp AND plan_cuentas.pct_pctid = plan_cuentas_ccs.pcc_pctid LEFT OUTER JOIN centro_costos_idiomas ON plan_cuentas_ccs.pcc_codemp = centro_costos_idiomas.csi_codemp AND plan_cuentas_ccs.pcc_ccsid = centro_costos_idiomas.csi_ccsid},   
         cuentas_padres,   
         cuentas_padres_idiomas,   
         plan_cuentas_idiomas  
   WHERE ( cuentas_padres_idiomas.cpi_codemp = cuentas_padres.ctp_codemp ) and  
         ( cuentas_padres_idiomas.cpi_ctpid = cuentas_padres.ctp_ctpid ) and  
         ( plan_cuentas.pct_codemp = cuentas_padres.ctp_codemp ) and  
         ( plan_cuentas.pct_ctpid = cuentas_padres.ctp_ctpid ) and  
         ( plan_cuentas_idiomas.pci_codemp = plan_cuentas.pct_codemp ) and  
         ( plan_cuentas_idiomas.pci_pctid = plan_cuentas.pct_pctid ) and  
         ( ( cuentas_padres.ctp_codemp = @codemp ) AND  
         ( cuentas_padres_idiomas.cpi_idid = @idioma ) AND  
         ( plan_cuentas_idiomas.pci_idid = @idioma ) )   
ORDER BY cuentas_padres.ctp_codigo ASC,   
         plan_cuentas.pct_codigo ASC,   
         centro_costos_idiomas.csi_nombre ASC
