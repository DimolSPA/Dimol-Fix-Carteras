

Create Procedure Delete_Rutinas(@rtn_codemp integer, @rtn_rtnid integer) as
  DELETE FROM rutinas_ejercicios_repeticion  
   WHERE ( rutinas_ejercicios_repeticion.rrp_codemp = @rtn_codemp ) AND  
         ( rutinas_ejercicios_repeticion.rrp_rtnid = @rtn_rtnid )   
           

  DELETE FROM rutinas_ejercicios  
   WHERE ( rutinas_ejercicios.rte_codemp = @rtn_codemp ) AND  
         ( rutinas_ejercicios.rte_rtnid = @rtn_rtnid )   


  DELETE FROM rutinas  
   WHERE ( rutinas.rtn_codemp = @rtn_codemp ) AND  
         ( rutinas.rtn_rtnid = @rtn_rtnid )
