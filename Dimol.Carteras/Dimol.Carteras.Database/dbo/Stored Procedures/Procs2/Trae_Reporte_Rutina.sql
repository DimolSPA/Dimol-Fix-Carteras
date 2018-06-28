

Create Procedure Trae_Reporte_Rutina(@rtn_codemp integer, @rtn_rtnid integer, @eji_idid integer) as
   SELECT rutinas.rtn_nombre,   
         rutinas.rtn_nivel,   
         rutinas_ejercicios.rte_dia1,   
         rutinas_ejercicios.rte_dia2,   
         rutinas_ejercicios.rte_dia3,   
         rutinas_ejercicios.rte_dia4,   
         rutinas_ejercicios.rte_dia5,   
         rutinas_ejercicios.rte_dia6,   
         rutinas_ejercicios.rte_dia7,   
         rutinas_ejercicios.rte_orden,   
         rutinas_ejercicios.rte_cantrep,   
         rutinas_ejercicios_repeticion.rrp_repid,   
         rutinas_ejercicios_repeticion.rrp_repeticion,   
         rutinas_ejercicios_repeticion.rrp_peso,   
         ejercicios_idiomas.eji_nombre,   
         tipos_ejercicios_idiomas.tji_nombre,   
         tipos_ejercicios.tej_tipo,
          ejc_imagen1, rte_tiempo  
    FROM {oj rutinas_ejercicios_repeticion RIGHT OUTER JOIN rutinas_ejercicios ON rutinas_ejercicios_repeticion.rrp_codemp = rutinas_ejercicios.rte_codemp AND rutinas_ejercicios_repeticion.rrp_rtnid = rutinas_ejercicios.rte_rtnid AND rutinas_ejercicios_repeticion.rrp_ejcid = rutinas_ejercicios.rte_ejcid},   
         rutinas,   
         ejercicios,   
         ejercicios_idiomas,   
         tipos_ejercicios,   
         tipos_ejercicios_idiomas  
   WHERE ( rutinas_ejercicios.rte_codemp = rutinas.rtn_codemp ) and  
         ( rutinas_ejercicios.rte_rtnid = rutinas.rtn_rtnid ) and  
         ( ejercicios.ejc_codemp = rutinas_ejercicios.rte_codemp ) and  
         ( ejercicios.ejc_ejcid = rutinas_ejercicios.rte_ejcid ) and  
         ( ejercicios_idiomas.eji_codemp = ejercicios.ejc_codemp ) and  
         ( ejercicios_idiomas.eji_ejcid = ejercicios.ejc_ejcid ) and  
         ( tipos_ejercicios.tej_codemp = ejercicios.ejc_codemp ) and  
         ( tipos_ejercicios.tej_tejid = ejercicios.ejc_tejid ) and  
         ( tipos_ejercicios_idiomas.tji_codemp = tipos_ejercicios.tej_codemp ) and  
         ( tipos_ejercicios_idiomas.tji_tejid = tipos_ejercicios.tej_tejid ) and  
         ( ( rutinas.rtn_codemp = @rtn_codemp ) AND  
         ( rutinas.rtn_rtnid = @rtn_rtnid ) AND  
         ( ejercicios_idiomas.eji_idid = @eji_idid ) AND  
         ( tipos_ejercicios_idiomas.tji_idid = @eji_idid )   
         )
