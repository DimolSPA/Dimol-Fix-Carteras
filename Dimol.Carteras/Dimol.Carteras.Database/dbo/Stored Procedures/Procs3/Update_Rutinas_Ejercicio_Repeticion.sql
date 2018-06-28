

Create Procedure Update_Rutinas_Ejercicio_Repeticion(@rrp_codemp integer, @rrp_rtnid integer, @rrp_ejcid integer, @rrp_repid integer, @rrp_repeticion integer, @rrp_peso decimal(10,2)) as
   UPDATE rutinas_ejercicios_repeticion  
     SET rrp_repeticion = @rrp_repeticion,   
         rrp_peso = @rrp_peso  
   WHERE ( rutinas_ejercicios_repeticion.rrp_codemp = @rrp_codemp ) AND  
         ( rutinas_ejercicios_repeticion.rrp_rtnid = @rrp_rtnid ) AND  
         ( rutinas_ejercicios_repeticion.rrp_ejcid = @rrp_ejcid ) AND  
         ( rutinas_ejercicios_repeticion.rrp_repid = @rrp_repid )
