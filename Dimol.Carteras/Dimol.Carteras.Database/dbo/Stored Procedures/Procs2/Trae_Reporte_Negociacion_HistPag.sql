﻿create Procedure Trae_Reporte_Negociacion_HistPag(@neg_codemp integer, @neg_anio integer, @neg_negid integer) as    SELECT view_documentos_diarios.pcl_rut,              view_documentos_diarios.pcl_nomfant,              view_documentos_diarios.ctc_numero,              view_documentos_diarios.ctc_digito,              view_documentos_diarios.ctc_nomfant,              view_documentos_diarios.tci_nombre,              view_documentos_diarios.ddi_numcta,              view_documentos_diarios.edi_nombre,              view_documentos_diarios.ddi_fecvenc,              view_documentos_diarios.ddi_monto,              view_documentos_diarios.ddi_saldo,              negociacion.neg_fecini,              negociacion.neg_fecfin,              negociacion.neg_anio,              negociacion.neg_negid        FROM negociacion,              view_documentos_diarios       WHERE ( negociacion.neg_codemp = view_documentos_diarios.ddi_codemp ) and             ( negociacion.neg_anio = view_documentos_diarios.ddi_anioneg ) and             ( negociacion.neg_negid = view_documentos_diarios.ddi_negid ) and             ( ( negociacion.neg_codemp = @neg_codemp ) AND             ( negociacion.neg_anio = @neg_anio ) AND             ( negociacion.neg_negid = @neg_negid )  AND             ( negociacion.neg_estado in ('A','F'))           )   