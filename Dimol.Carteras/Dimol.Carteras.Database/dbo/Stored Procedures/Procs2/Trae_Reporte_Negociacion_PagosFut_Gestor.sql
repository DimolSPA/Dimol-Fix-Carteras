﻿create Procedure Trae_Reporte_Negociacion_PagosFut_Gestor(@neg_codemp integer, @desde datetime, @hasta datetime, @gestid as integer) as    SELECT view_documentos_diarios.pcl_rut,              view_documentos_diarios.pcl_nomfant,              view_documentos_diarios.ctc_numero,              view_documentos_diarios.ctc_digito,              view_documentos_diarios.ctc_nomfant,              view_documentos_diarios.tci_nombre,              view_documentos_diarios.ddi_numcta,              view_documentos_diarios.edi_nombre,              view_documentos_diarios.ddi_fecvenc,              view_documentos_diarios.ddi_monto,              view_documentos_diarios.ddi_saldo,              negociacion.neg_fecini,              negociacion.neg_fecfin,              negociacion.neg_anio,              negociacion.neg_negid,           ges_nombre        FROM negociacion,              view_documentos_diarios,           gestor_cartera,           gestor       WHERE ( negociacion.neg_codemp = view_documentos_diarios.ddi_codemp ) and             ( negociacion.neg_anio = view_documentos_diarios.ddi_anioneg ) and             ( negociacion.neg_negid = view_documentos_diarios.ddi_negid ) and             ( view_documentos_diarios.ddi_codemp = gsc_codemp) and             ( view_documentos_diarios.ddi_pclid = gsc_pclid) and             ( view_documentos_diarios.ddi_ctcid = gsc_ctcid) and             (gsc_codemp = ges_codemp) and             (gsc_sucid = ges_sucid) and             (gsc_gesid = ges_gesid) and             ( negociacion.neg_estado in ('A','F')) and  ddi_saldo > 0 and           neg_codemp = @neg_codemp and           ddi_fecvenc between @desde and @hasta and            ges_gesid = @gestid 