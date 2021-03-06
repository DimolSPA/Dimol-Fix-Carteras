﻿

CREATE Procedure [dbo].[Trae_Reporte_Liquidacion_Todo](@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid integer, @ccb_tipcart integer, @ccb_estcpbt char(1), @idioma integer, @gsc_sucid integer) as
  SELECT cartera_clientes_documentos_cpbt_doc.ccb_pclid,   
         cartera_clientes_documentos_cpbt_doc.ccb_ctcid,   
         cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nomfant,   
         cartera_clientes_documentos_cpbt_doc.ctc_rut,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecdoc,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.eci_nombre,   
         cartera_clientes_documentos_cpbt_doc.mon_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_tipcambio,   
         cartera_clientes_documentos_cpbt_doc.ccb_monto,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastjud,   
         cartera_clientes_documentos_cpbt_doc.ccb_gastotro,   
         cartera_clientes_documentos_cpbt_doc.ccb_intereses,   
         cartera_clientes_documentos_cpbt_doc.ccb_honorarios,   
         cartera_clientes_documentos_cpbt_doc.bco_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numesp,   
         cartera_clientes_documentos_cpbt_doc.sbc_rut,   
         cartera_clientes_documentos_cpbt_doc.sbc_nombre,   
         cartera_clientes_documentos_cpbt_doc.pcc_codigo,   
         cartera_clientes_documentos_cpbt_doc.pcc_nombre,   
         cartera_clientes_documentos_cpbt_doc.mci_nombre,   
         pais.pai_nombre,   
         region.reg_nombre,   
         ciudad.ciu_nombre,   
         comuna.com_nombre,   
         comuna.com_codpost,   
         cartera_clientes_documentos_cpbt_doc.ctc_direccion,   
         gestor.ges_nombre,
         ccb_codmon  
    FROM cartera_clientes_documentos_cpbt_doc,   
         comuna,   
         ciudad,   
         region,   
         pais,   
         gestor,   
         gestor_cartera  
   WHERE ( ciudad.ciu_ciuid = comuna.com_ciuid ) and  
         ( region.reg_regid = ciudad.ciu_regid ) and  
         ( pais.pai_paiid = region.reg_paiid ) and  
         ( cartera_clientes_documentos_cpbt_doc.ctc_comid = comuna.com_comid ) and  
         ( gestor_cartera.gsc_codemp = gestor.ges_codemp ) and  
         ( gestor_cartera.gsc_sucid = gestor.ges_sucid ) and  
         ( gestor_cartera.gsc_gesid = gestor.ges_gesid ) and  
         ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = gestor_cartera.gsc_codemp ) and  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = gestor_cartera.gsc_ctcid ) and  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = gestor_cartera.gsc_pclid ) and  
         ( ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_tipcart = @ccb_tipcart ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_estcpbt = @ccb_estcpbt ) AND  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = @idioma ) AND  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = @idioma ) AND  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = @idioma ) AND  
         ( gestor_cartera.gsc_sucid = @gsc_sucid and eci_estid > 1 )   
         )
         --and (cartera_clientes_documentos_cpbt_doc.CCB_SALDO > 500 and cartera_clientes_documentos_cpbt_doc.MON_NOMBRE = 'PESOS')  -- sacar saldos menores a $500
order by ccb_fecvenc