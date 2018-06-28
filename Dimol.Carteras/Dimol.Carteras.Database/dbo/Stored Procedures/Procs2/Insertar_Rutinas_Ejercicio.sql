

Create Procedure Insertar_Rutinas_Ejercicio(@rte_codemp integer, @rte_rtnid integer, @rte_ejcid integer, @rte_tiempo integer, 
														@rte_dia1 char(1), @rte_dia2 char(1), @rte_dia3 char(1), @rte_dia4 char(1),
													     @rte_dia5 char(1), @rte_dia6 char(1), @rte_dia7 char(1), @rte_orden smallint, @rte_cantrep smallint) as
  INSERT INTO rutinas_ejercicios  
         ( rte_codemp,   
           rte_rtnid,   
           rte_ejcid,   
           rte_tiempo,   
           rte_dia1,   
           rte_dia2,   
           rte_dia3,   
           rte_dia4,   
           rte_dia5,   
           rte_dia6,   
           rte_dia7,   
           rte_orden,   
           rte_cantrep )  
  VALUES ( @rte_codemp,   
           @rte_rtnid,   
           @rte_ejcid,   
           @rte_tiempo,   
           @rte_dia1,   
           @rte_dia2,   
           @rte_dia3,   
           @rte_dia4,   
           @rte_dia5,   
           @rte_dia6,   
           @rte_dia7,   
           @rte_orden,   
           @rte_cantrep )
