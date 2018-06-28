

Create Procedure Insertar_Rutinas(@rtn_codemp integer, @rtn_rtnid integer, @rtn_nombre varchar(200), @rtn_nivel smallint) as
  INSERT INTO rutinas  
         ( rtn_codemp,   
           rtn_rtnid,   
           rtn_nombre,   
           rtn_nivel )  
  VALUES ( @rtn_codemp,   
           @rtn_rtnid,   
           @rtn_nombre,   
           @rtn_nivel )
