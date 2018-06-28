
/*==============================================================*/
/* View: VIEW_DATOS_GEOGRAFICOS                                 */
/*==============================================================*/
create view VIEW_DATOS_GEOGRAFICOS as
  SELECT pais.pai_paiid,   
         pais.pai_nombre,   
         pais.pai_codtel,   
         region.reg_regid,   
         region.reg_nombre,   
         region.reg_orden,   
         ciudad.ciu_ciuid,   
         ciudad.ciu_nombre,   
         ciudad.ciu_codarea,
         comuna.com_comid,    
         comuna.com_nombre,   
         comuna.com_codpost  
    FROM pais,   
         region,   
         ciudad,   
         comuna  
   WHERE ( region.reg_paiid = pais.pai_paiid ) and  
         ( ciudad.ciu_regid = region.reg_regid ) and  
         ( comuna.com_ciuid = ciudad.ciu_ciuid )
