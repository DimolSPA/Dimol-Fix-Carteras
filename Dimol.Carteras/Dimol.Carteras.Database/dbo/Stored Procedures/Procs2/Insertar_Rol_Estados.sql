

Create Procedure Insertar_Rol_Estados(@rle_codemp integer, @rle_rolid integer, @rle_estid smallint, @rle_esjid integer, 
												@rle_usrid integer, @rle_ipred varchar (30), @rle_ipmaquina varchar (30), @rle_comentario text, @rle_fecjud datetime) as  

  INSERT INTO rol_estados  
         ( rle_codemp,   
           rle_rolid,   
           rle_estid,   
           rle_esjid,   
           rle_fecha,   
           rle_usrid,   
           rle_ipred,   
           rle_ipmaquina,   
           rle_comentario,   
           rle_fecjud )  
  VALUES ( @rle_codemp,   
           @rle_rolid,   
           @rle_estid,   
           @rle_esjid,   
           getdate(),   
           @rle_usrid,   
           @rle_ipred,   
           @rle_ipmaquina,   
           @rle_comentario,   
           @rle_fecjud )  


  SELECT max(rle_fecjud) as fecjud
     into #Fecha
    FROM rol_estados  
    where rle_codemp = @rle_codemp and rle_rolid = @rle_rolid


select rol_estados.*
into #RolEst
from rol_estados, #Fecha
where rle_codemp = @rle_codemp and
      rle_rolid = @rle_rolid and
      rle_fecjud =  fecjud 



  UPDATE rol  
     SET rol_estid = rle_estid,   
         rol_esjid = rle_esjid,   
         rol_fecjud = rle_fecjud,   
         rol_fecultgest = getdate()  
from rol, #RolEst
   WHERE  rol.rol_codemp = @rle_codemp  AND  
          rol.rol_rolid =  @rle_rolid  and
          rol_codemp = rle_codemp and
          rol_rolid = rle_rolid
