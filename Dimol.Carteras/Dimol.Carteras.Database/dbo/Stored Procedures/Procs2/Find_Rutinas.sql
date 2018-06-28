

Create Procedure Find_Rutinas(@rtn_codemp integer, @rtn_rtnid integer) as
  SELECT count(rutinas.rtn_rtnid)  
    FROM rutinas  
   WHERE ( rutinas.rtn_codemp = @rtn_codemp ) AND  
         ( rutinas.rtn_rtnid = @rtn_rtnid )
