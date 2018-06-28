


/*==============================================================*/
/* View: VIEW_DEUDORES_MAIL_CONTACTOS                           */
/*==============================================================*/
CREATE VIEW [dbo].[VIEW_DEUDORES_MAIL_CONTACTOS_PROV] as
SELECT deudores_contactos.ddc_codemp,   
         deudores_contactos.ddc_ctcid,   
         tipos_contacto_idiomas.tci_nombre,   
         deudores_contactos.ddc_nombre,   
         deudores_contactos_mail_prov.mail,   
         deudores_contactos_mail_prov.tipo,   
         tipos_contacto_idiomas.tci_idid,
         ddc_estado,
         ddc_ddcid,
         ddc_estdir,
         ddc_comid,
         tci_ticid,
         ddc_direccion,
         deudores_contactos_mail_prov.masivo
   FROM  [dbo].[DEUDORES_CONTACTOS_MAIL_PROV],   
         [dbo].[DEUDORES_CONTACTOS],   
         [dbo].[TIPOS_CONTACTO_IDIOMAS],
        ciudad,
         comuna    
   WHERE ( [dbo].[DEUDORES_CONTACTOS].ddc_codemp = [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].codemp ) and  
         ( [dbo].[DEUDORES_CONTACTOS].ddc_ctcid = [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].ctcid ) and  
         ( [dbo].[DEUDORES_CONTACTOS].ddc_ddcid = [dbo].[DEUDORES_CONTACTOS_MAIL_PROV].ddcid ) and  
         ( [dbo].[DEUDORES_CONTACTOS].ddc_codemp = [dbo].[TIPOS_CONTACTO_IDIOMAS].tci_codemp ) and  
         ( [dbo].[DEUDORES_CONTACTOS].ddc_ticid = [dbo].[TIPOS_CONTACTO_IDIOMAS].tci_ticid )   and
         ( [dbo].[DEUDORES_CONTACTOS].ddc_comid = com_comid )   and
         ( com_ciuid = ciu_ciuid )   
 --  UNION   


 -- SELECT deudores_mail.ddm_codemp,   
 --        deudores_mail.ddm_ctcid,   
 --        '' as tci_nombre,   
 --        '' as ddc_nombre,   
 --        deudores_mail.ddm_mail,   
 --        deudores_mail.ddm_tipo,   
 --        0,
 --       'A', 
 --        0,
 --        1,
 --        0,
 --        0,
 --        '',
 --        deudores_mail.ddm_masivo
 --FROM deudores_mail, deudores, comuna, ciudad
 --   where ctc_codemp = ddm_codemp and 
 --              ctc_ctcid = ddm_ctcid  and
 --              ctc_comid = com_comid and
 --              com_ciuid = ciu_ciuid
