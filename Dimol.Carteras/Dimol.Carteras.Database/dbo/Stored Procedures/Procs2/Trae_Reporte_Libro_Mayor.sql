

Create Procedure Trae_Reporte_Libro_Mayor(@ast_codemp integer, @desde datetime, @hasta datetime, @cpi_idid integer) as
  SELECT asientos_contables.ast_tipo,   
         asientos_contables.ast_numero,   
         asientos_contables.ast_numfin,   
         asientos_contables.ast_fecemision,   
         asientos_contables.ast_fecperiodo,   
         asientos_contables.ast_estado,   
         asientos_contables.ast_glosa,   
         asientos_contables_detalle.acd_debe,   
         asientos_contables_detalle.acd_haber,   
         asientos_contables_detalle.acd_exento,   
         asientos_contables_detalle.acd_glosa,   
         cuentas_padres.ctp_codigo,   
         cuentas_padres_idiomas.cpi_nombre,   
         cuentas_padres.ctp_agrupa,   
         plan_cuentas.pct_codigo,   
         plan_cuentas_idiomas.pci_nombre,   
         plan_cuentas.pct_ccs,   
         plan_cuentas.pct_activos  
    FROM asientos_contables,   
         asientos_contables_detalle,   
         plan_cuentas_idiomas,   
         plan_cuentas,   
         cuentas_padres,   
         cuentas_padres_idiomas  
   WHERE ( asientos_contables_detalle.acd_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_detalle.acd_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_detalle.acd_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_detalle.acd_numero = asientos_contables.ast_numero ) and  
         ( plan_cuentas.pct_codemp = plan_cuentas_idiomas.pci_codemp ) and  
         ( plan_cuentas.pct_pctid = plan_cuentas_idiomas.pci_pctid ) and  
         ( plan_cuentas.pct_codemp = asientos_contables_detalle.acd_codemp ) and  
         ( plan_cuentas.pct_pctid = asientos_contables_detalle.acd_pctid ) and  
         ( cuentas_padres.ctp_codemp = plan_cuentas.pct_codemp ) and  
         ( cuentas_padres.ctp_ctpid = plan_cuentas.pct_ctpid ) and  
         ( cuentas_padres_idiomas.cpi_codemp = cuentas_padres.ctp_codemp ) and  
         ( cuentas_padres_idiomas.cpi_ctpid = cuentas_padres.ctp_ctpid )   and
         ast_codemp = @ast_codemp and
         ast_fecperiodo between @desde and @hasta and
         ast_estado <> 'N' and
         cpi_idid = @cpi_idid and
         pci_idid = @cpi_idid
  ORDER BY cuentas_padres.ctp_agrupa ASC,   
         plan_cuentas.pct_codigo ASC,   
         asientos_contables.ast_tipo ASC,   
         asientos_contables.ast_numfin ASC
