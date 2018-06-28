

Create Procedure Trae_Reporte_Analitico_Cuentas(@ast_codemp integer, @desde datetime, @hasta datetime, @pci_pctid integer, @pci_idid integer) as
 SELECT distinct view_asientos_contables.ast_tipo,   
         view_asientos_contables.ast_numero,   
         view_asientos_contables.ast_numfin,   
         view_asientos_contables.ast_fecperiodo,   
         substring(view_asientos_contables.ast_glosa, 1, 1000) as ast_glosa,   
         provcli.pcl_rut,   
         provcli.pcl_nomfant,   
         view_asientos_contables.tci_nombre,   
         view_asientos_contables.ddi_numcta,   
         view_asientos_contables.ddi_fecdoc,   
         view_asientos_contables.ddi_fecvenc,   
         view_asientos_contables.ddi_tipcambio,   
         plan_cuentas_idiomas.pci_nombre,   
         asientos_contables_detalle.acd_debe,   
         asientos_contables_detalle.acd_haber,   
         asientos_contables_detalle.acd_exento,   
         substring(asientos_contables_detalle.acd_glosa, 1, 1000) as acd_glosa,   
         plan_cuentas_idiomas.pci_pctid,
        pct_codigo  
    FROM view_asientos_contables,   
         provcli,   
         plan_cuentas_idiomas,   
         asientos_contables_detalle,
         plan_cuentas  
   WHERE ( view_asientos_contables.ast_codemp = provcli.pcl_codemp ) and  
         ( view_asientos_contables.ddi_pclid = provcli.pcl_pclid ) and  
         ( view_asientos_contables.ast_codemp = asientos_contables_detalle.acd_codemp ) and  
         ( view_asientos_contables.ast_anio = asientos_contables_detalle.acd_anio ) and  
         ( view_asientos_contables.ast_tipo = asientos_contables_detalle.acd_tipo ) and  
         ( view_asientos_contables.ast_numero = asientos_contables_detalle.acd_numero ) and  
         ( asientos_contables_detalle.acd_codemp = plan_cuentas_idiomas.pci_codemp ) and  
         ( asientos_contables_detalle.acd_pctid = plan_cuentas_idiomas.pci_pctid ) and  

         ( pct_codemp = pci_codemp ) and
         ( pct_pctid = pci_pctid ) and

         ( ( view_asientos_contables.ast_codemp = @ast_codemp ) AND  
         ( view_asientos_contables.ast_fecperiodo >= @desde ) AND  
         ( view_asientos_contables.ast_fecperiodo <= @hasta ) AND  
         ( view_asientos_contables.ast_estado <> 'N' ) AND  
         ( plan_cuentas_idiomas.pci_idid = @pci_idid ) AND  
         ( plan_cuentas_idiomas.pci_pctid = @pci_pctid )   

         )    

Union

  SELECT distinct view_asientos_contables.ast_tipo,   
         view_asientos_contables.ast_numero,   
         view_asientos_contables.ast_numfin,   
         view_asientos_contables.ast_fecperiodo,   
          substring(view_asientos_contables.ast_glosa, 1, 1000) as ast_glosa,   
         provcli.pcl_rut,   
         provcli.pcl_nomfant,   
         view_asientos_contables.tci_nombre,  
         view_asientos_contables.cbc_numprovcli,   
         view_asientos_contables.cbc_feccpbt,   
         view_asientos_contables.cbc_fecvenc,   
         view_asientos_contables.cbc_tipcambio,  
         plan_cuentas_idiomas.pci_nombre,   
         asientos_contables_detalle.acd_debe,   
         asientos_contables_detalle.acd_haber,   
         asientos_contables_detalle.acd_exento,   
         substring(asientos_contables_detalle.acd_glosa, 1, 1000) as acd_glosa,   
         plan_cuentas_idiomas.pci_pctid,
         pct_codigo  
    FROM view_asientos_contables,   
         provcli,   
         plan_cuentas_idiomas,   
         asientos_contables_detalle,plan_cuentas  
   WHERE ( view_asientos_contables.ast_codemp = asientos_contables_detalle.acd_codemp ) and  
         ( view_asientos_contables.ast_anio = asientos_contables_detalle.acd_anio ) and  
         ( view_asientos_contables.ast_tipo = asientos_contables_detalle.acd_tipo ) and  
         ( view_asientos_contables.ast_numero = asientos_contables_detalle.acd_numero ) and  
         ( asientos_contables_detalle.acd_codemp = plan_cuentas_idiomas.pci_codemp ) and  
         ( asientos_contables_detalle.acd_pctid = plan_cuentas_idiomas.pci_pctid ) and  
         ( view_asientos_contables.ast_codemp = provcli.pcl_codemp ) and  
         ( view_asientos_contables.cbc_pclid = provcli.pcl_pclid ) and  
	    ( pct_codemp = pci_codemp ) and
         ( pct_pctid = pci_pctid ) and
         ( ( view_asientos_contables.ast_codemp = @ast_codemp ) AND  
         ( view_asientos_contables.ast_fecperiodo >= @desde ) AND  
         ( view_asientos_contables.ast_fecperiodo <= @hasta ) AND  
         ( view_asientos_contables.ast_estado <> 'N' ) AND  
         ( plan_cuentas_idiomas.pci_idid = @pci_idid ) AND  
         ( plan_cuentas_idiomas.pci_pctid = @pci_pctid )   
         )    

Union

 SELECT distinct view_asientos_contables.ast_tipo,   
         view_asientos_contables.ast_numero,   
         view_asientos_contables.ast_numfin,   
         view_asientos_contables.ast_fecperiodo,   
           substring(view_asientos_contables.ast_glosa, 1, 1000) as ast_glosa,   
         empleados.epl_rut,   
         epl_nombre + ' ' +  epl_apepat as nombre,  
         view_asientos_contables.tci_nombre,   
         view_asientos_contables.ddi_numcta,   
         view_asientos_contables.ddi_fecdoc,   
         view_asientos_contables.ddi_fecvenc,   
         view_asientos_contables.ddi_tipcambio,   
         plan_cuentas_idiomas.pci_nombre,   
         asientos_contables_detalle.acd_debe,   
         asientos_contables_detalle.acd_haber,   
         asientos_contables_detalle.acd_exento,   
         substring(asientos_contables_detalle.acd_glosa, 1, 1000) as acd_glosa,   
         plan_cuentas_idiomas.pci_pctid,
         pct_codigo   
    FROM view_asientos_contables,   
         plan_cuentas_idiomas,   
         asientos_contables_detalle,   
         empleados,plan_cuentas  
   WHERE ( view_asientos_contables.ast_codemp = asientos_contables_detalle.acd_codemp ) and  
         ( view_asientos_contables.ast_anio = asientos_contables_detalle.acd_anio ) and  
         ( view_asientos_contables.ast_tipo = asientos_contables_detalle.acd_tipo ) and  
         ( view_asientos_contables.ast_numero = asientos_contables_detalle.acd_numero ) and  
         ( asientos_contables_detalle.acd_codemp = plan_cuentas_idiomas.pci_codemp ) and  
         ( asientos_contables_detalle.acd_pctid = plan_cuentas_idiomas.pci_pctid ) and  
         ( view_asientos_contables.ast_codemp = empleados.epl_codemp ) and  
         ( view_asientos_contables.ddi_emplid = empleados.epl_emplid ) and  
         ( pct_codemp = pci_codemp ) and
         ( pct_pctid = pci_pctid ) and
         ( ( view_asientos_contables.ast_codemp = @ast_codemp ) AND  
         ( view_asientos_contables.ast_fecperiodo >= @desde ) AND  
         ( view_asientos_contables.ast_fecperiodo <= @hasta ) AND  
         ( view_asientos_contables.ast_estado <> 'N' ) AND  
         ( plan_cuentas_idiomas.pci_idid = @pci_idid ) AND  
         ( plan_cuentas_idiomas.pci_pctid = @pci_pctid )   
         )    

Union

SELECT distinct  view_asientos_contables.ast_tipo,   
         view_asientos_contables.ast_numero,   
         view_asientos_contables.ast_numfin,   
         view_asientos_contables.ast_fecperiodo,   
           substring(view_asientos_contables.ast_glosa, 1, 1000) as ast_glosa,   
         '' as Rut,   
         '' as nombre,  
        '',   
         '',   
        ast_fecperiodo,   
         ast_fecperiodo,   
        0,   
         plan_cuentas_idiomas.pci_nombre,   
         asientos_contables_detalle.acd_debe,   
         asientos_contables_detalle.acd_haber,   
         asientos_contables_detalle.acd_exento,   
         substring(asientos_contables_detalle.acd_glosa, 1, 1000) as acd_glosa,     
         plan_cuentas_idiomas.pci_pctid,
         pct_codigo
   FROM view_asientos_contables,   
         plan_cuentas_idiomas,   
         asientos_contables_detalle,plan_cuentas  
   WHERE ( view_asientos_contables.ast_codemp = asientos_contables_detalle.acd_codemp ) and  
         ( view_asientos_contables.ast_anio = asientos_contables_detalle.acd_anio ) and  
         ( view_asientos_contables.ast_tipo = asientos_contables_detalle.acd_tipo ) and  
         ( view_asientos_contables.ast_numero = asientos_contables_detalle.acd_numero ) and  
         ( asientos_contables_detalle.acd_codemp = plan_cuentas_idiomas.pci_codemp ) and  
         ( asientos_contables_detalle.acd_pctid = plan_cuentas_idiomas.pci_pctid ) and  
         ( pct_codemp = pci_codemp ) and
         ( pct_pctid = pci_pctid ) and
         ( ( view_asientos_contables.ast_codemp = @ast_codemp ) AND  
         ( view_asientos_contables.ast_fecperiodo >= @desde ) AND  
         ( view_asientos_contables.ast_fecperiodo <= @hasta ) AND  
         ( view_asientos_contables.ast_estado <> 'N' ) AND  
         ( plan_cuentas_idiomas.pci_idid = @pci_idid ) AND  
         ( plan_cuentas_idiomas.pci_pctid = @pci_pctid ) AND  
         ( view_asientos_contables.cbc_pclid is null ) AND  
         ( view_asientos_contables.ddi_emplid is null ) AND  
         ( view_asientos_contables.ddi_pclid is null )   
         )
