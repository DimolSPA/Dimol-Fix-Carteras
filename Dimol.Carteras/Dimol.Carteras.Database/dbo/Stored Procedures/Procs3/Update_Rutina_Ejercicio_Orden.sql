

Create Procedure Update_Rutina_Ejercicio_Orden(@rte_codemp integer, @rte_rtnid integer, @rte_ejcid integer, @rte_orden integer) as
  UPDATE rutinas_ejercicios  
     SET rte_orden = @rte_orden  
   WHERE ( rutinas_ejercicios.rte_codemp = @rte_codemp ) AND  
         ( rutinas_ejercicios.rte_rtnid = @rte_rtnid ) AND  
         ( rutinas_ejercicios.rte_ejcid = @rte_ejcid )
