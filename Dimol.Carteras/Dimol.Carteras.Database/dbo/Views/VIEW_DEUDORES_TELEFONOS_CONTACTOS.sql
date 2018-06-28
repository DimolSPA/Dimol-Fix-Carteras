
/*==============================================================*/
/* View: VIEW_DEUDORES_TELEFONOS_CONTACTOS                      */
/*==============================================================*/
create view VIEW_DEUDORES_TELEFONOS_CONTACTOS as
SELECT deudores_contactos_telefonos.dct_codemp,   
         deudores_contactos_telefonos.dct_ctcid,   
         tipos_contacto_idiomas.tci_ticid,   
         tipos_contacto_idiomas.tci_nombre,   
         deudores_contactos.ddc_nombre,   
         deudores_contactos_telefonos.dct_numero,   
         deudores_contactos_telefonos.dct_tipo,   
         deudores_contactos_telefonos.dct_estado,
         ddc_estado,
         ddc_ddcid,
         ddc_estdir,
         ddc_comid, 
         ddc_ctcid,
         case dct_tipo when 'M' then '' else convert(varchar,ciu_codarea)  end as ciu_codarea,
         ddc_direccion        
    FROM deudores_contactos_telefonos,   
         deudores_contactos,   
         tipos_contacto_idiomas, 
         ciudad,
         comuna  
   WHERE ( deudores_contactos.ddc_codemp = deudores_contactos_telefonos.dct_codemp ) and  
         ( deudores_contactos.ddc_ctcid = deudores_contactos_telefonos.dct_ctcid ) and  
         ( deudores_contactos.ddc_ddcid = deudores_contactos_telefonos.dct_ddcid ) and  
         ( deudores_contactos.ddc_codemp = tipos_contacto_idiomas.tci_codemp ) and  
         ( deudores_contactos.ddc_ticid = tipos_contacto_idiomas.tci_ticid )   and
         ( deudores_contactos.ddc_comid = com_comid )   and
         ( com_ciuid = ciu_ciuid )   
   UNION   
  SELECT deudores_telefonos.ddt_codemp,   
         deudores_telefonos.ddt_ctcid, 
         0, 
         '' as tipo,   
         ctc_nomfant as contacto,   
         deudores_telefonos.ddt_numero,   
         deudores_telefonos.ddt_tipo,   
         deudores_telefonos.ddt_estado,
         'A', 
         0,
         1,
         0,
         ddt_ctcid,
         case ddt_tipo when 'M' then '' else convert(varchar,ciu_codarea)  end as ciu_codarea,
         ''          
    FROM deudores_telefonos, deudores, comuna, ciudad
    where ctc_codemp = ddt_codemp and 
               ctc_ctcid = ddt_ctcid  and
               ctc_comid = com_comid and
               com_ciuid = ciu_ciuid
