
/*==============================================================*/
/* View: VIEW_ENTES_JUDICIALES                                  */
/*==============================================================*/
create view VIEW_ENTES_JUDICIALES as
  SELECT etj_codemp,
            entes_judicial.etj_etjid,   
            provcli.pcl_nomfant,
            etj_sindico,
            etj_abogado,
            etj_procurador,
            etj_receptor
            FROM entes_judicial,   
            provcli
            WHERE  provcli.pcl_codemp = entes_judicial.etj_codemp  and  
            provcli.pcl_pclid = entes_judicial.etj_pclid  and  
            pcl_estado='V'
            UNION
            SELECT etj_codemp, entes_judicial.etj_etjid,   
            epl_nombre + ' ' + epl_apepat,
	       etj_sindico,
            etj_abogado,
            etj_procurador,
            etj_receptor    
            FROM entes_judicial,   
            empleados, estados_empleado
            WHERE  empleados.epl_codemp = entes_judicial.etj_codemp  and  
            empleados.epl_emplid = entes_judicial.etj_emplid  and  
            epl_codemp = eem_codemp  and  
            epl_eemid = eem_eemid  and  
            empleados.epl_emplid = entes_judicial.etj_emplid  and  
            eem_accion='A'
