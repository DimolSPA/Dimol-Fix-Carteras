

Create Procedure Trae_Reporte_Libro_Balance(@ctp_codemp integer, @desde datetime, @hasta datetime, @idi_idid integer) as
  SELECT cuentas_padres.ctp_codigo,   
         cuentas_padres_idiomas.cpi_nombre,   
         cuentas_padres.ctp_agrupa,   
         plan_cuentas.pct_codigo,   
         plan_cuentas_idiomas.pci_nombre,   
         plan_cuentas.pct_activos,   
         asientos_contables_detalle.acd_debe,   
         asientos_contables_detalle.acd_haber,   
         asientos_contables_detalle.acd_exento,   
         asientos_contables.ast_fecperiodo  
    FROM cuentas_padres,   
         cuentas_padres_idiomas,   
         plan_cuentas,   
         plan_cuentas_idiomas,   
         asientos_contables,   
         asientos_contables_detalle,   
         idiomas  
   WHERE ( cuentas_padres_idiomas.cpi_codemp = cuentas_padres.ctp_codemp ) and  
         ( cuentas_padres_idiomas.cpi_ctpid = cuentas_padres.ctp_ctpid ) and  
         ( plan_cuentas.pct_codemp = cuentas_padres.ctp_codemp ) and  
         ( plan_cuentas.pct_ctpid = cuentas_padres.ctp_ctpid ) and  
         ( plan_cuentas_idiomas.pci_codemp = plan_cuentas.pct_codemp ) and  
         ( plan_cuentas_idiomas.pci_pctid = plan_cuentas.pct_pctid ) and  
         ( asientos_contables_detalle.acd_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_detalle.acd_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_detalle.acd_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_detalle.acd_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_detalle.acd_codemp = plan_cuentas.pct_codemp ) and  
         ( asientos_contables_detalle.acd_pctid = plan_cuentas.pct_pctid ) and  
         ( cuentas_padres_idiomas.cpi_idid = idiomas.idi_idid ) and  
         ( plan_cuentas_idiomas.pci_idid = idiomas.idi_idid ) and  
         ( ( cuentas_padres.ctp_codemp = @idi_idid ) AND  
         ( asientos_contables.ast_fecperiodo >= @desde ) AND  
         ( asientos_contables.ast_fecperiodo <= @hasta ) AND  
         ( idiomas.idi_idid = @idi_idid and ast_estado <> 'N'  )   
         )   
ORDER BY cuentas_padres.ctp_agrupa ASC,   
         plan_cuentas.pct_codigo ASC
