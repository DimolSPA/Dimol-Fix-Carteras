

Create Procedure Insertar_Rol_Estados_Especial(@rle_codemp integer, @rle_rolid integer, @rle_estid smallint, @rle_esjid integer, @rle_fecha datetime, 
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
           @rle_fecha,   
           @rle_usrid,   
           @rle_ipred,   
           @rle_ipmaquina,   
           @rle_comentario,   
           @rle_fecjud )
