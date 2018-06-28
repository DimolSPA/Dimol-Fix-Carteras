

Create Procedure Trae_Reporte_Rol_Avenimiento(@rol_codemp integer, @rol_rolid integer) as
 SELECT view_rol.rol_numero,   
         view_rol.rol_total,   
         view_rol.pcl_rut,   
         view_rol.pcl_nomfant,   
         view_rol.ctc_numero,   
         view_rol.ctc_digito,   
         view_rol.ctc_nomfant,   
         view_datos_geograficos.pai_nombre,   
         view_datos_geograficos.reg_nombre,   
         view_datos_geograficos.ciu_nombre,   
         view_datos_geograficos.com_nombre,   
         view_datos_geograficos.com_codpost,   
         deudores.ctc_direccion,   
         rol_avedem.rad_fecave,   
         rol_avedem.rad_cuoave,   
         rol_avedem.rad_monave,   
         rol_avedem.rad_monucouave,   
         rol_avedem.rad_fecpcouave,   
         rol_avedem.rad_fecucouave,   
         rol_avedem.rad_intave,   
         empleados.epl_rut,   
         empleados.epl_nombre,   
         empleados.epl_apepat,   
         empleados.epl_apemat,   
         view_rol.trb_nombre,
         rad_monpcouave,
        view_rol.ctc_nombre,
         view_rol.ctc_apepat,
         view_rol.ctc_apemat   
    FROM view_rol,   
         entejud_rol,   
         entes_judicial,   
         empleados,   
         rol_avedem,   
         deudores,   
         view_datos_geograficos  
   WHERE ( entes_judicial.etj_codemp = entejud_rol.ejr_codemp ) and  
         ( entes_judicial.etj_etjid = entejud_rol.ejr_etjid ) and  
         ( view_rol.rol_codemp = entejud_rol.ejr_codemp ) and  
         ( view_rol.rol_rolid = entejud_rol.ejr_rolid ) and  
         ( empleados.epl_codemp = entes_judicial.etj_codemp ) and  
         ( empleados.epl_emplid = entes_judicial.etj_emplid ) and  
         ( view_rol.rol_codemp = rol_avedem.rad_codemp ) and  
         ( view_rol.rol_rolid = rol_avedem.rad_rolid ) and  
         ( view_rol.rol_codemp = deudores.ctc_codemp ) and  
         ( view_rol.rol_ctcid = deudores.ctc_ctcid ) and  
         ( deudores.ctc_comid = view_datos_geograficos.com_comid ) and  
         ( ( view_rol.rol_codemp = @rol_codemp ) AND  
         ( view_rol.rol_rolid = @rol_rolid ) AND  
         ( entes_judicial.etj_abogado = 'S' ) )
