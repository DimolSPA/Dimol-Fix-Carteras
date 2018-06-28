

Create Procedure Trae_Reporte_Cpbt_Caja(@ddi_codemp integer, @ddi_sucid integer, @ddi_anio integer, @ddi_numdoc integer, @edi_idiid integer) as
  SELECT view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.ddi_numcta,   
         view_documentos_diarios.bco_nombre,   
         view_documentos_diarios.ddi_ctacte,   
         view_documentos_diarios.ddi_fecdoc,   
         view_documentos_diarios.ddi_fecvenc,   
         view_documentos_diarios.ddi_tipcambio,   
         view_documentos_diarios.pcl_rut,   
         view_documentos_diarios.pcl_nomfant,   
         view_documentos_diarios.epl_rut,   
         view_documentos_diarios.epl_nombre,   
         view_documentos_diarios.epl_apepat,   
         view_documentos_diarios.ctc_numero,   
         view_documentos_diarios.ctc_digito,   
         view_documentos_diarios.ddi_comentario,   
         view_documentos_diarios.ctc_nomfant,   
         view_documentos_diarios.edi_nombre,   
         view_documentos_diarios.mon_nombre,  
         view_documentos_diarios.ddi_monto
    FROM view_documentos_diarios,   
         estados_documentos_diarios  
   WHERE ( view_documentos_diarios.ddi_codemp = estados_documentos_diarios.edc_codemp ) and  
         ( view_documentos_diarios.ddi_edcid = estados_documentos_diarios.edc_edcid ) and  
         ( ( view_documentos_diarios.ddi_codemp = @ddi_codemp ) AND  
         ( view_documentos_diarios.ddi_sucid = @ddi_sucid ) AND  
         ( view_documentos_diarios.ddi_tipmov = 'E' ) AND  
         ( view_documentos_diarios.ddi_anio = @ddi_anio ) AND  
         ( view_documentos_diarios.ddi_numdoc = @ddi_numdoc ) AND  
         ( view_documentos_diarios.edi_idiid = @edi_idiid ) AND  
         ( view_documentos_diarios.tci_idid = @edi_idiid ) AND  
         ( estados_documentos_diarios.edc_estado not in ( 3 ) )   
         )
