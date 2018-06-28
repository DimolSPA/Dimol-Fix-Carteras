
/*==============================================================*/
/* View: VIEW_DEUDORES_MAIL_CONTACTOS                           */
/*==============================================================*/
create view VIEW_DEUDORES_MAIL_CONTACTOS as
SELECT deudores_contactos.ddc_codemp,   
         deudores_contactos.ddc_ctcid,   
         tipos_contacto_idiomas.tci_nombre,   
         deudores_contactos.ddc_nombre,   
         deudores_contactos_mail.dcm_mail,   
         deudores_contactos_mail.dcm_tipo,   
         tipos_contacto_idiomas.tci_idid,
         ddc_estado,
         ddc_ddcid,
         ddc_estdir,
         ddc_comid,
         tci_ticid,
         ddc_direccion,
         deudores_contactos_mail.dcm_masivo
   FROM deudores_contactos_mail,   
         deudores_contactos,   
         tipos_contacto_idiomas,
        ciudad,
         comuna    
   WHERE ( deudores_contactos.ddc_codemp = deudores_contactos_mail.dcm_codemp ) and  
         ( deudores_contactos.ddc_ctcid = deudores_contactos_mail.dcm_ctcid ) and  
         ( deudores_contactos.ddc_ddcid = deudores_contactos_mail.dcm_ddcid ) and  
         ( deudores_contactos.ddc_codemp = tipos_contacto_idiomas.tci_codemp ) and  
         ( deudores_contactos.ddc_ticid = tipos_contacto_idiomas.tci_ticid )   and
         ( deudores_contactos.ddc_comid = com_comid )   and
         ( com_ciuid = ciu_ciuid )   
   UNION   


  SELECT deudores_mail.ddm_codemp,   
         deudores_mail.ddm_ctcid,   
         '' as tci_nombre,   
         '' as ddc_nombre,   
         deudores_mail.ddm_mail,   
         deudores_mail.ddm_tipo,   
         0,
        'A', 
         0,
         1,
         0,
         0,
         '',
         deudores_mail.ddm_masivo
 FROM deudores_mail, deudores, comuna, ciudad
    where ctc_codemp = ddm_codemp and 
               ctc_ctcid = ddm_ctcid  and
               ctc_comid = com_comid and
               com_ciuid = ciu_ciuid
