

Create Procedure Update_Rol_Estados(@rle_codemp integer, @rle_rolid integer, @rle_estid smallint, @rle_esjid integer, @rle_fecha datetime,
												@rle_usrid integer, @rle_ipred varchar (30), @rle_ipmaquina varchar (30), @rle_comentario text, @rle_fecjud datetime) as  
  UPDATE rol_estados  
     SET rle_codemp = @rle_codemp,   
         rle_rolid = @rle_rolid,   
         rle_estid = @rle_estid,   
         rle_esjid = @rle_esjid,   
         rle_fecha = @rle_fecha,   
         rle_usrid = @rle_usrid,   
         rle_ipred = @rle_ipred,   
         rle_ipmaquina = @rle_ipmaquina,   
         rle_comentario = @rle_ipmaquina,   
         rle_fecjud = @rle_fecjud  
   WHERE ( rol_estados.rle_codemp = @rle_codemp ) AND  
         ( rol_estados.rle_rolid = @rle_rolid ) AND  
         ( rol_estados.rle_estid = @rle_estid ) AND  
         ( rol_estados.rle_esjid = @rle_esjid ) AND  
         ( rol_estados.rle_fecha = @rle_fecha )
