﻿create Procedure Trae_Reporte_Recuperacion_Gestor(@apl_codemp integer, @apl_sucid integer, @desde datetime, @hasta datetime, @api_gesid integer) as    SELECT view_aplicaciones_doc_cartera_clientes.apl_fecapl,              view_aplicaciones_doc_cartera_clientes.pcl_rut,              view_aplicaciones_doc_cartera_clientes.pcl_nomfant,              view_aplicaciones_doc_cartera_clientes.ctc_numero,              view_aplicaciones_doc_cartera_clientes.ctc_digito,              view_aplicaciones_doc_cartera_clientes.ctc_nomfant,              view_aplicaciones_doc_cartera_clientes.apl_accion,              view_aplicaciones_doc_cartera_clientes.api_capital,              view_aplicaciones_doc_cartera_clientes.api_interes,              view_aplicaciones_doc_cartera_clientes.api_honorario,              view_aplicaciones_doc_cartera_clientes.api_gastpre,              view_aplicaciones_doc_cartera_clientes.api_gastjud,              gestor.ges_nombre,              view_aplicaciones_doc_cartera_clientes.ddi_tipcambio,           ctc_nomfantP        FROM view_aplicaciones_doc_cartera_clientes,              gestor,              view_tipos_cpbtdoc_clasificacion       WHERE ( view_aplicaciones_doc_cartera_clientes.apl_codemp = gestor.ges_codemp ) and             ( view_aplicaciones_doc_cartera_clientes.apl_sucid = gestor.ges_sucid ) and             ( view_aplicaciones_doc_cartera_clientes.api_gesid = gestor.ges_gesid ) and             ( view_aplicaciones_doc_cartera_clientes.apl_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and             ( view_aplicaciones_doc_cartera_clientes.ddi_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and             ( ( view_aplicaciones_doc_cartera_clientes.apl_codemp = apl_codemp ) AND             ( view_aplicaciones_doc_cartera_clientes.apl_sucid =apl_sucid ) AND             ( view_aplicaciones_doc_cartera_clientes.apl_fecapl >= @desde) AND             ( view_aplicaciones_doc_cartera_clientes.apl_fecapl <= @hasta ) AND             ( view_aplicaciones_doc_cartera_clientes.api_gesid = @api_gesid ) ) 