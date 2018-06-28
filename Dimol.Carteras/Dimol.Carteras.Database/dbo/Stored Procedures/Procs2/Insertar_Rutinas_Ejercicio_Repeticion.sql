

Create Procedure Insertar_Rutinas_Ejercicio_Repeticion(@rrp_codemp integer, @rrp_rtnid integer, @rrp_ejcid integer, @rrp_repid integer, @rrp_repeticion integer, @rrp_peso decimal(10,2)) as
  INSERT INTO rutinas_ejercicios_repeticion  
         ( rrp_codemp,   
           rrp_rtnid,   
           rrp_ejcid,   
           rrp_repid,   
           rrp_repeticion,   
           rrp_peso )  
  VALUES ( @rrp_codemp,   
           @rrp_rtnid,   
           @rrp_ejcid,   
           @rrp_repid,   
           @rrp_repeticion,   
           @rrp_peso )
