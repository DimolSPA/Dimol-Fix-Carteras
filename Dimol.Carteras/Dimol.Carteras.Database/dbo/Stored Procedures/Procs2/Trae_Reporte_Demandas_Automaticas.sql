

Create Procedure Trae_Reporte_Demandas_Automaticas(@rdr_codemp integer, @rdr_rolid integer, @tbi_idid integer) as
  SELECT DISTINCT rol_datos_reporte.rdr_materia,   
         rol_datos_reporte.rdr_rutdem,   
         rol_datos_reporte.rdr_dvdem,   
         rol_datos_reporte.rdr_nomdem,   
         rol_datos_reporte.rdr_rutabo,   
         rol_datos_reporte.rdr_dvabo,   
         rol_datos_reporte.rdr_nomabo,   
         rol_datos_reporte.rdr_telabo,   
         rol_datos_reporte.rdr_mailabo,   
         rol_datos_reporte.rdr_dirabo,   
         rol_datos_reporte.rdr_rutdeu,   
         rol_datos_reporte.rdr_dvdeu,   
         rol_datos_reporte.rdr_nomdeu,   
         rol_datos_reporte.rdr_dirdeu,   
         rol_datos_reporte.rdr_rutgir,   
         rol_datos_reporte.rdr_dvgir,   
         rol_datos_reporte.rdr_nomgir,   
         rol_datos_reporte.rdr_tipcpbt,   
         rol_datos_reporte.rdr_numero,   
         rol_datos_reporte.rdr_monto,   
         rol_datos_reporte.rdr_moneda,   
         rol_datos_reporte.rdr_fecpro,   
         rol_datos_reporte.rdr_motivo,   
         rol_datos_reporte.rdr_monesc,   
         rol_datos_reporte.rdr_totdeu,   
         rol_datos_reporte.rdr_totesc,   
         rol_datos_reporte.rdr_banco,   
         rol_datos_reporte.rdr_item,   
         view_rol_datos.trb_nombre,   
         view_rol_datos.tci_nombre,   
         view_rol_datos.mji_nombre,   
         tipos_tribunal_idiomas.tbi_nombre,   
         tribunales.trb_nombre,   
         view_rol_datos.rol_numero,
         rdr_saldo,rdr_feccpbt   
    FROM rol_datos_reporte,   
         view_rol_datos,   
         tipos_tribunal_idiomas,   
         tribunales  
   WHERE ( rol_datos_reporte.rdr_codemp = view_rol_datos.rol_codemp ) and  
         ( rol_datos_reporte.rdr_rolid = view_rol_datos.rol_rolid ) and  
         ( view_rol_datos.rol_codemp = tribunales.trb_codemp ) and  
         ( view_rol_datos.rol_trbid = tribunales.trb_trbid ) and  
         ( tribunales.trb_ttbid = tipos_tribunal_idiomas.tbi_ttbid ) and  
         ( tribunales.trb_codemp = tipos_tribunal_idiomas.tbi_codemp ) and  
         ( tipos_tribunal_idiomas.tbi_idid = view_rol_datos.eci_idid ) and  
         ( ( rol_datos_reporte.rdr_codemp = @rdr_codemp ) AND  
         ( rol_datos_reporte.rdr_rolid = @rdr_rolid ) AND  
         ( tipos_tribunal_idiomas.tbi_idid = @tbi_idid )   
         )
