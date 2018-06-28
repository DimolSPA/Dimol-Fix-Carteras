

Create Procedure Trae_Reporte_Asientos_Contables(@ast_codemp integer, @ast_anio integer, @ast_tipo char(1), @ast_numero integer, @pci_idid integer) as
  SELECT asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         asientos_contables.ast_numfin,   
         asientos_contables.ast_mes,   
         asientos_contables.ast_fecemision,   
         asientos_contables.ast_fecperiodo,   
         asientos_contables.ast_estado,   
         asientos_contables.ast_glosa,   
         plan_cuentas_idiomas.pci_nombre,   
         asientos_contables_detalle.acd_debe,   
         asientos_contables_detalle.acd_haber,   
         plan_cuentas.pct_activos,   
         asientos_contables_detalle.acd_glosa,
         pct_codigo   
    FROM asientos_contables,   
         asientos_contables_detalle,   
         plan_cuentas_idiomas,   
         plan_cuentas  
   WHERE ( asientos_contables_detalle.acd_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_detalle.acd_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_detalle.acd_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_detalle.acd_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_detalle.acd_codemp = plan_cuentas_idiomas.pci_codemp ) and  
         ( asientos_contables_detalle.acd_pctid = plan_cuentas_idiomas.pci_pctid ) and  
         ( plan_cuentas.pct_codemp = plan_cuentas_idiomas.pci_codemp ) and  
         ( plan_cuentas.pct_pctid = plan_cuentas_idiomas.pci_pctid ) and  
         ( ( asientos_contables.ast_codemp = @ast_codemp ) AND  
         ( asientos_contables.ast_anio = @ast_anio ) AND  
         ( asientos_contables.ast_tipo = @ast_tipo ) AND  
         ( asientos_contables.ast_numero = @ast_numero ) AND  
         ( plan_cuentas_idiomas.pci_idid = @pci_idid ) ) 
order by acd_debe desc, acd_haber desc, pct_codigo
