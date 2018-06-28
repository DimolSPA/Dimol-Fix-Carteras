

Create Procedure Find_Rutinas_Ejercicios(@rte_codemp integer, @rte_rtnid integer, @rte_ejcid integer) as
  SELECT count(rutinas_ejercicios.rte_codemp)  
    FROM rutinas_ejercicios  
   WHERE ( rutinas_ejercicios.rte_codemp = @rte_codemp ) AND  
         ( rutinas_ejercicios.rte_rtnid = @rte_rtnid ) AND  
         ( rutinas_ejercicios.rte_ejcid = @rte_ejcid )
