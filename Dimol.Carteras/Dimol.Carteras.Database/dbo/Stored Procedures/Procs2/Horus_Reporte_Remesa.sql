
Create Procedure Horus_Reporte_Remesa(@codemp integer, @numero integer, @provcli integer) as
  SELECT remesas.rem_numero,   
         remesas.rem_fecemi,   
         remesas.rem_comentario,   
         remesas.rem_tiphon,   
         remesas.rem_valhon,   
         provcli.pcl_rut,   
         provcli.pcl_nombre,   
         cartera_clientes.ctc_rut,   
         cartera_clientes.ctc_nombre,   
         tipos_doc_diarios_a.tdd_nombre,   
         tipos_comprobante.tpc_nombre,   
         cartera_clientes_cpbt_doc.ccb_numero,   
         cartera_clientes_cpbt_doc.ccb_fecing,   
         cartera_clientes_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_cpbt_doc.ccb_fecvenc,   
         remesas_detalle.rmd_monto,   
         remesas_detalle.rmd_saldo,   
         remesas_detalle.rmd_intereses,   
         remesas_detalle.rmd_honorarios,   
         remesas_detalle.rmd_gastotro,   
         remesas_detalle.rmd_gastjud,   
         tipos_doc_diarios_b.tdd_nombre,   
         documentos_diarios_a.ddi_numcta,   
         tipos_doc_diarios_c.tdd_nombre,   
         documentos_diarios_b.ddi_numcta,   
         documentos_diarios_b.ddi_ctacte,   
         bancos.bco_nombre,
        rmd_valhon,
       apl_fecapl,
       acd_monto, 
      acd_honorarios,
      acd_gastotro,
      acd_gastjud,
      acd_intereses,
      remesas.rem_pagofi,
   sbc_rut,
         sbc_nombre,
documentos_diarios_a.ddi_tipcambio,    
documentos_diarios_b.ddi_tipcambio,
ccb_numesp, ccb_saldo, ccb_saldreal, documentos_diarios_a.ddi_fecvenc,
'COPIA CONTABILIDAD' as copia1,
'COPIA CLIENTE' as copia2,
'COPIA AREA COMERCIAL' as copia3

    FROM {oj cartera_clientes_cpbt_doc 
			LEFT OUTER JOIN tipos_comprobante ON cartera_clientes_cpbt_doc.ccb_codemp = tipos_comprobante.tpc_codemp AND cartera_clientes_cpbt_doc.ccb_tpcid = tipos_comprobante.tpc_tpcid 
		     LEFT OUTER JOIN tipos_doc_diarios tipos_doc_diarios_a ON cartera_clientes_cpbt_doc.ccb_codemp = tipos_doc_diarios_a.tdd_codemp AND cartera_clientes_cpbt_doc.ccb_tddid = tipos_doc_diarios_a.tdd_tddid
               LEFT OUTER JOIN subdeuda ON cartera_clientes_cpbt_doc.ccb_codemp = subdeuda.scd_codemp AND cartera_clientes_cpbt_doc.ccb_pclid = subdeuda.scd_pclid AND cartera_clientes_cpbt_doc.ccb_ctcid = subdeuda.scd_ctcid AND cartera_clientes_cpbt_doc.ccb_ccbid = subdeuda.scd_ccbid 
               LEFT OUTER JOIN subcarteras ON subdeuda.scd_codemp = subcarteras.sbc_codemp AND subdeuda.scd_sbcid = subcarteras.sbc_sbcid}, {oj documentos_diarios documentos_diarios_b LEFT OUTER JOIN bancos ON documentos_diarios_b.ddi_codemp = bancos.bco_codemp AND documentos_diarios_b.ddi_bcoid = bancos.bco_bcoid },  

         remesas,   
         remesas_detalle,   
         cartera_clientes,   
         provcli,   
         aplicaciones_doc_cartcli,   
         aplicaciones,   
         documentos_diarios documentos_diarios_a,   
         tipos_doc_diarios tipos_doc_diarios_b,   
         tipos_doc_diarios tipos_doc_diarios_c
   WHERE ( remesas_detalle.rmd_codemp = remesas.rem_codemp ) and  
         ( remesas_detalle.rmd_pclid = remesas.rem_pclid ) and  
         ( remesas_detalle.rmd_numero = remesas.rem_numero ) and  
         ( cartera_clientes_cpbt_doc.ccb_codemp = remesas_detalle.rmd_codemp ) and  
         ( cartera_clientes_cpbt_doc.ccb_pclid = remesas_detalle.rmd_pclid ) and  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = remesas_detalle.rmd_ctcid ) and  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = remesas_detalle.rmd_ccbid ) and  
         ( cartera_clientes.ctc_codemp = cartera_clientes_cpbt_doc.ccb_codemp ) and  
         ( cartera_clientes.ctc_pclid = cartera_clientes_cpbt_doc.ccb_pclid ) and  
         ( cartera_clientes.ctc_ctcid = cartera_clientes_cpbt_doc.ccb_ctcid ) and  
         ( cartera_clientes_cpbt_doc.ccb_codemp = provcli.pcl_codemp ) and  
         ( cartera_clientes_cpbt_doc.ccb_pclid = provcli.pcl_pclid ) and  
         ( aplicaciones_doc_cartcli.acd_codemp = cartera_clientes_cpbt_doc.ccb_codemp ) and  
         ( aplicaciones_doc_cartcli.acd_pclid = cartera_clientes_cpbt_doc.ccb_pclid ) and  
         ( aplicaciones_doc_cartcli.acd_ctcid = cartera_clientes_cpbt_doc.ccb_ctcid ) and  
         ( aplicaciones_doc_cartcli.acd_ccbid = cartera_clientes_cpbt_doc.ccb_ccbid ) and  
         ( aplicaciones.apl_codemp = aplicaciones_doc_cartcli.acd_codemp ) and  
         ( aplicaciones.apl_sucid = aplicaciones_doc_cartcli.acd_sucid ) and  
         ( aplicaciones.apl_numapl = aplicaciones_doc_cartcli.acd_numapl ) and  
         ( aplicaciones_doc_cartcli.acd_codemp = documentos_diarios_a.ddi_codemp ) and  
         ( aplicaciones_doc_cartcli.acd_sucid = documentos_diarios_a.ddi_sucid ) and  
         ( aplicaciones_doc_cartcli.acd_numdoc = documentos_diarios_a.ddi_numdoc ) and  

         ( aplicaciones_doc_cartcli.acd_codemp = remesas_detalle.rmd_codemp ) and  
         ( aplicaciones_doc_cartcli.acd_numapl = remesas_detalle.rmd_numapl ) and  
         ( aplicaciones_doc_cartcli.acd_item = remesas_detalle.rmd_item ) and  


         ( documentos_diarios_a.ddi_codemp = tipos_doc_diarios_b.tdd_codemp ) and  
         ( documentos_diarios_a.ddi_tddid = tipos_doc_diarios_b.tdd_tddid ) and  
         ( documentos_diarios_b.ddi_codemp = remesas.rem_codemp ) and  
         ( remesas.rem_sucid = documentos_diarios_b.ddi_sucid ) and  
         ( remesas.rem_numdoc = documentos_diarios_b.ddi_numdoc ) and  
         ( documentos_diarios_b.ddi_codemp = tipos_doc_diarios_c.tdd_codemp ) and  
         ( documentos_diarios_b.ddi_tddid = tipos_doc_diarios_c.tdd_tddid ) and  
         ( ( remesas.rem_codemp = @codemp ) AND  
         ( remesas.rem_numero = @numero )    AND  
	   (	remesas.rem_pclid = @provcli )  AND
           
          rem_numero in (select rem_numero from remesas where rem_codemp = @codemp and rem_numero = @numero and rem_pclid = @provcli)
        
)