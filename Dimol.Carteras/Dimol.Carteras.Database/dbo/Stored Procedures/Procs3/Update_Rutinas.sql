

Create Procedure Update_Rutinas(@rtn_codemp integer, @rtn_rtnid integer, @rtn_nombre varchar(200), @rtn_nivel smallint) as
   UPDATE rutinas  
     SET rtn_nombre = @rtn_nombre,   
         rtn_nivel = @rtn_nivel  
   WHERE ( rutinas.rtn_codemp = @rtn_codemp ) AND  
         ( rutinas.rtn_rtnid = @rtn_rtnid )
