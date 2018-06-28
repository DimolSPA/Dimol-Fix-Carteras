

Create Procedure Insertar_Documentos_Diarios_OP(@ddo_codemp integer, @ddo_sucid integer, @ddo_anio integer, @ddo_numdoc numeric(15)) as
  DELETE FROM documentos_diarios_op  
   WHERE ( documentos_diarios_op.ddo_codemp = @ddo_codemp ) AND  
         ( documentos_diarios_op.ddo_sucid = @ddo_sucid ) AND  
         ( documentos_diarios_op.ddo_anio = @ddo_anio ) AND  
         ( documentos_diarios_op.ddo_numdoc = @ddo_numdoc )   
           

insert into documentos_diarios_op
  SELECT view_documentos_diarios.ddi_codemp,   
         view_documentos_diarios.ddi_sucid,   
         view_documentos_diarios.ddi_anio,   
         view_documentos_diarios.ddi_numdoc,   
         view_documentos_diarios.ddi_tpcid,   
         view_documentos_diarios.ddi_tipmov,   
         view_documentos_diarios.ddi_pclid,   
         view_documentos_diarios.ddi_propio,   
         view_documentos_diarios.ddi_bcoid,   
         view_documentos_diarios.ddi_ctacte,   
         view_documentos_diarios.ddi_fecing,   
         view_documentos_diarios.ddi_fecdoc,   
         view_documentos_diarios.ddi_fecvenc,   
         view_documentos_diarios.ddi_edcid,   
         view_documentos_diarios.ddi_numcta,   
         view_documentos_diarios.ddi_monto,   
         view_documentos_diarios.ddi_saldo,   
         view_documentos_diarios.ddi_codmon,   
         view_documentos_diarios.ddi_tipcambio,   
         view_documentos_diarios.ddi_titular,   
         view_documentos_diarios.ddi_rutpag,   
         view_documentos_diarios.ddi_nompag,   
         view_documentos_diarios.ddi_ctcid,   
         view_documentos_diarios.ddi_empleado,   
         view_documentos_diarios.ddi_emplid,   
         view_documentos_diarios.ddi_custodia,   
         view_documentos_diarios.ddi_docemp,   
         view_documentos_diarios.ddi_pagdir,   
         view_documentos_diarios.ddi_vecesdep,   
         view_documentos_diarios.ddi_fechadep,   
         view_documentos_diarios.ddi_depositar,   
         view_documentos_diarios.ddi_rutdep,   
         view_documentos_diarios.ddi_nomdep,   
         view_documentos_diarios.ddi_nrodep,   
         view_documentos_diarios.ddi_fecdep,   
         view_documentos_diarios.ddi_pendiente,   
         view_documentos_diarios.tci_idid,   
         view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.pcl_rut,   
         view_documentos_diarios.pcl_nomfant,   
         view_documentos_diarios.bco_rut,   
         view_documentos_diarios.bco_nombre,   
         view_documentos_diarios.edi_idiid,   
         view_documentos_diarios.edi_nombre,   
         view_documentos_diarios.mon_nombre,   
         view_documentos_diarios.ctc_rut,   
         view_documentos_diarios.ctc_nomfant,   
         view_documentos_diarios.epl_rut,   
         view_documentos_diarios.epl_nombre,   
         view_documentos_diarios.epl_apepat,   
         view_documentos_diarios.ddi_anioneg,   
         view_documentos_diarios.ddi_negid,   
         view_documentos_diarios.ctc_numero,   
         view_documentos_diarios.ctc_digito  
    FROM view_documentos_diarios  
   WHERE ( view_documentos_diarios.ddi_codemp = @ddo_codemp ) AND  
         ( view_documentos_diarios.ddi_sucid = @ddo_sucid ) AND  
         ( view_documentos_diarios.ddi_anio = @ddo_anio ) AND  
         ( view_documentos_diarios.ddi_numdoc = @ddo_numdoc )
