

Create Procedure Trae_Reporte_Negociacion_Documentos(@neg_codemp integer, @neg_anio smallint, @neg_negid integer, @tci_idid smallint) as
  SELECT provcli.pcl_rut,   
         provcli.pcl_nomfant,   
         deudores.ctc_numero,   
         deudores.ctc_digito,   
         deudores.ctc_nomfant,   
         negociacion.neg_anio,   
         negociacion.neg_negid,   
         negociacion.neg_fecini,   
         negociacion.neg_fecfin,   
         negociacion.neg_estado,   
         pais.pai_nombre,   
         region.reg_nombre,   
         ciudad.ciu_nombre,   
         comuna.com_nombre,   
         deudores.ctc_direccion,   
         tipos_cpbtdoc_idiomas.tci_nombre,   
         cartera_clientes_cpbt_doc.ccb_numero,   
         cartera_clientes_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_cpbt_doc.ccb_fecvenc,   
         negociacion_cpbtdoc.ngd_monto,   
         negociacion_cpbtdoc.ngd_monto_n,   
         negociacion_cpbtdoc.ngd_intereses,   
         negociacion_cpbtdoc.ngd_intereses_n,   
         negociacion_cpbtdoc.ngd_honorarios,   
         negociacion_cpbtdoc.ngd_honorarios_n,   
         negociacion_cpbtdoc.ngd_gastjud,   
         negociacion_cpbtdoc.ngd_gastjud_n,   
         negociacion_cpbtdoc.ngd_gastotro,   
         negociacion_cpbtdoc.ngd_gastotro_n,
         mon_codmon,
         mon_nombre,
         ges_nombre,
         usr_nombre
    FROM negociacion,   
         deudores,   
         negociacion_cpbtdoc,   
         tipos_cpbtdoc_idiomas,   
         cartera_clientes_cpbt_doc,   
         provcli,   
         comuna,   
         ciudad,   
         region,   
         pais,
         monedas,
         gestor_cartera,
         gestor,
         usuarios   
   WHERE ( deudores.ctc_codemp = negociacion.neg_codemp ) and  
         ( deudores.ctc_ctcid = negociacion.neg_ctcid ) and  
         ( negociacion_cpbtdoc.ngd_codemp = negociacion.neg_codemp ) and  
         ( negociacion_cpbtdoc.ngd_anio = negociacion.neg_anio ) and  
         ( negociacion_cpbtdoc.ngd_negid = negociacion.neg_negid ) and  
         ( cartera_clientes_cpbt_doc.ccb_codemp = negociacion_cpbtdoc.ngd_codemp ) and  
         ( cartera_clientes_cpbt_doc.ccb_pclid = negociacion_cpbtdoc.ngd_pclid ) and  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = negociacion_cpbtdoc.ngd_ctcid ) and  
         ( cartera_clientes_cpbt_doc.ccb_ccbid = negociacion_cpbtdoc.ngd_ccbid ) and  
         ( cartera_clientes_cpbt_doc.ccb_codemp = tipos_cpbtdoc_idiomas.tci_codemp ) and  
         ( cartera_clientes_cpbt_doc.ccb_tpcid = tipos_cpbtdoc_idiomas.tci_tpcid ) and  
         ( cartera_clientes_cpbt_doc.ccb_codemp = provcli.pcl_codemp ) and  
         ( cartera_clientes_cpbt_doc.ccb_pclid = provcli.pcl_pclid ) and  
         ( cartera_clientes_cpbt_doc.ccb_codemp = mon_codemp ) and  
         ( cartera_clientes_cpbt_doc.ccb_codmon = mon_codmon ) and  
         ( neg_codemp = usr_codemp) and  
         ( neg_usrid = usr_usrid) and  
         ( ccb_codemp = gsc_codemp ) and  
         ( ccb_ctcid = gsc_ctcid ) and  
         ( ccb_pclid = gsc_pclid ) and  
         ( gsc_codemp = ges_codemp ) and  
         ( gsc_sucid  = ges_sucid) and  
         ( gsc_gesid = ges_gesid) and  
         ( comuna.com_comid = deudores.ctc_comid ) and  
         ( ciudad.ciu_ciuid = comuna.com_ciuid ) and  
         ( region.reg_regid = ciudad.ciu_regid ) and  
         ( pais.pai_paiid = region.reg_paiid ) and  
         ( ( negociacion.neg_codemp = @neg_codemp ) AND  
         ( negociacion.neg_anio = @neg_anio ) AND  
         ( negociacion.neg_negid = @neg_negid ) AND  
         ( tipos_cpbtdoc_idiomas.tci_idid = @tci_idid )   
         )
