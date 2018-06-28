

Create Procedure Delete_Rutinas_Ejercicio(@rte_codemp integer, @rte_rtnid integer, @rte_ejcid integer) as
     DELETE FROM rutinas_ejercicios_repeticion  
   WHERE ( rutinas_ejercicios_repeticion.rrp_codemp = @rte_codemp ) AND  
         ( rutinas_ejercicios_repeticion.rrp_rtnid = @rte_rtnid ) AND  
         ( rutinas_ejercicios_repeticion.rrp_ejcid = @rte_ejcid )   


  DELETE FROM rutinas_ejercicios  
   WHERE ( rutinas_ejercicios.rte_codemp = @rte_codemp ) AND  
         ( rutinas_ejercicios.rte_rtnid = @rte_rtnid ) AND  
         ( rutinas_ejercicios.rte_ejcid = @rte_ejcid )
