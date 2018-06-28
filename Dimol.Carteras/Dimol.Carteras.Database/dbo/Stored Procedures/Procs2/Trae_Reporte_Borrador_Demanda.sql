

Create Procedure Trae_Reporte_Borrador_Demanda(@rdr_codemp integer, @rdr_rolid integer) as
  SELECT rol_datos_reporte.rdr_materia,   
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
         rol_datos_reporte.rdr_banco
    
    FROM rol_datos_reporte  
   WHERE ( rol_datos_reporte.rdr_codemp = @rdr_codemp ) AND  
         ( rol_datos_reporte.rdr_rolid = @rdr_rolid )
