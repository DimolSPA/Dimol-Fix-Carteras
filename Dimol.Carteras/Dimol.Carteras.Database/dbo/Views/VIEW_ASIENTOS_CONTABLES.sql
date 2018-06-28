
/*==============================================================*/
/* View: VIEW_ASIENTOS_CONTABLES                                */
/*==============================================================*/
create view VIEW_ASIENTOS_CONTABLES as
  SELECT asientos_contables.ast_codemp,   
         asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         asientos_contables.ast_numero,   
         asientos_contables.ast_numfin,   
         asientos_contables.ast_mes,   
         asientos_contables.ast_fecemision,   
         asientos_contables.ast_fecperiodo,   
         asientos_contables.ast_estado,   
         asientos_contables.ast_glosa,   
         asientos_contables.ast_tot_debe,   
         asientos_contables.ast_tot_haber,   
         tipos_cpbtdoc_idiomas.tci_nombre,   
         asientos_contables_cpbtdoc_apl.ada_item,   
         documentos_diarios.ddi_numdoc,   
         documentos_diarios.ddi_numcta,   
         documentos_diarios.ddi_fecing,   
         documentos_diarios.ddi_fecdoc,   
         documentos_diarios.ddi_fecvenc,   
         documentos_diarios.ddi_monto,   
         documentos_diarios.ddi_tipcambio,   
         asientos_contables_cpbtdoc_apl.ada_tpcid,   
         cabacera_comprobantes.cbc_numero,   
         cabacera_comprobantes.cbc_numprovcli,   
         cabacera_comprobantes.cbc_pclid,   
         documentos_diarios.ddi_ctcid,   
         documentos_diarios.ddi_emplid,   
         documentos_diarios.ddi_pclid,   
         cabacera_comprobantes.cbc_fecemi,   
         cabacera_comprobantes.cbc_feccpbt,   
         cabacera_comprobantes.cbc_fecvenc,   
         cabacera_comprobantes.cbc_fecent,   
         cabacera_comprobantes.cbc_tipcambio
    FROM {oj asientos_contables_cpbtdoc_apl RIGHT OUTER JOIN asientos_contables ON asientos_contables_cpbtdoc_apl.ada_codemp = asientos_contables.ast_codemp AND asientos_contables_cpbtdoc_apl.ada_anio = asientos_contables.ast_anio AND asientos_contables_cpbtdoc_apl.ada_tipo = asientos_contables.ast_tipo AND asientos_contables_cpbtdoc_apl.ada_numero = asientos_contables.ast_numero LEFT OUTER JOIN cabacera_comprobantes ON asientos_contables_cpbtdoc_apl.ada_codemp = cabacera_comprobantes.cbc_codemp AND asientos_contables_cpbtdoc_apl.ada_sucid = cabacera_comprobantes.cbc_sucid AND asientos_contables_cpbtdoc_apl.ada_tpcid = cabacera_comprobantes.cbc_tpcid AND asientos_contables_cpbtdoc_apl.ada_numcpbt = cabacera_comprobantes.cbc_numero LEFT OUTER JOIN documentos_diarios ON asientos_contables_cpbtdoc_apl.ada_codemp = documentos_diarios.ddi_codemp AND asientos_contables_cpbtdoc_apl.ada_sucid = documentos_diarios.ddi_sucid AND asientos_contables_cpbtdoc_apl.ada_tpcid = documentos_diarios.ddi_tpcid AND asientos_contables_cpbtdoc_apl.ada_numdoc = documentos_diarios.ddi_numdoc LEFT OUTER JOIN tipos_cpbtdoc ON asientos_contables_cpbtdoc_apl.ada_codemp = tipos_cpbtdoc.tpc_codemp AND asientos_contables_cpbtdoc_apl.ada_tpcid = tipos_cpbtdoc.tpc_tpcid LEFT OUTER JOIN tipos_cpbtdoc_idiomas ON tipos_cpbtdoc.tpc_codemp = tipos_cpbtdoc_idiomas.tci_codemp AND tipos_cpbtdoc.tpc_tpcid = tipos_cpbtdoc_idiomas.tci_tpcid LEFT OUTER JOIN clasificacion_cpbtdoc ON tipos_cpbtdoc.tpc_codemp = clasificacion_cpbtdoc.clb_codemp AND tipos_cpbtdoc.tpc_clbid = clasificacion_cpbtdoc.clb_clbid}
