﻿create Procedure Trae_Reporte_Analisis_Cartera(@ccb_codemp integer, @ccb_pclid integer, @ccb_tipcart smallint) as    SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,              cartera_clientes_documentos_cpbt_doc.pcl_nombre,              cartera_clientes_documentos_cpbt_doc.ctc_rut,              cartera_clientes_documentos_cpbt_doc.ctc_nomfant,              cartera_clientes_documentos_cpbt_doc.tci_nombre,              cartera_clientes_documentos_cpbt_doc.ccb_numero,              cartera_clientes_documentos_cpbt_doc.ccb_fecing,              cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,              cartera_clientes_documentos_cpbt_doc.ccb_asignado,              cartera_clientes_documentos_cpbt_doc.ccb_saldo,              cartera_clientes_documentos_cpbt_doc.ect_agrupa,              cartera_clientes_documentos_cpbt_doc.pcc_nombre,              9999999999999999999999999999999.00000 as Pagos,              9999999999999999999999999999999.00000 as Dev,              9999999999999999999999999999999.00000 as CastPre,              9999999999999999999999999999999.00000 as CastJud,              9999999999999999999999999999999.00000 as Condonacion,              9999999999999999999999999999999.00000 as CobImp,              9999999999999999999999999999999.00000 as Pagare,              9999999999999999999999999999999.00000 as CambDoc,              9999999999999999999999999999999.00000 as CesCred,              9999999999999999999999999999999.00000 as NC,              9999999999999999999999999999999.00000 as ND,              9999999999999999999999999999999.00000 as CertInc,            cartera_clientes_documentos_cpbt_doc.ccb_pclid,              cartera_clientes_documentos_cpbt_doc.ccb_ctcid,              cartera_clientes_documentos_cpbt_doc.ccb_ccbid, mon_nombre Moneda,           datepart(year, ccb_fecing) AnioIng,            datepart(Month, ccb_fecing) MesIng           into #Asignada         FROM cartera_clientes_documentos_cpbt_doc       WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid and ccb_codemp=@ccb_codemp ) AND             ( cartera_clientes_documentos_cpbt_doc.ccb_estcpbt <> 'X' ) AND             ( cartera_clientes_documentos_cpbt_doc.ccb_tipcart = @ccb_tipcart )   