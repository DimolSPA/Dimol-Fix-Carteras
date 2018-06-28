

Create Procedure Delete_Rutinas_Ejercicios_Repeticion(@rrp_codemp integer, @rrp_rtnid integer, @rrp_ejcid integer) as
  DELETE FROM rutinas_ejercicios_repeticion  
   WHERE ( rutinas_ejercicios_repeticion.rrp_codemp = @rrp_codemp ) AND  
         ( rutinas_ejercicios_repeticion.rrp_rtnid = @rrp_rtnid ) AND  
         ( rutinas_ejercicios_repeticion.rrp_ejcid = @rrp_ejcid )
