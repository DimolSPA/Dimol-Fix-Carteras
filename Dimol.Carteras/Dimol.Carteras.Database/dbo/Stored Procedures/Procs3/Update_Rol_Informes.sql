

Create Procedure Update_Rol_Informes(@rif_codemp integer, @rif_rolid integer, @rif_item integer, @rif_tifid integer,
												@rif_nombre varchar (800), @rif_ubicacion varchar (1200)) as  
  UPDATE rol_informes  
     SET rif_codemp = @rif_codemp,   
         rif_rolid = @rif_rolid,   
         rif_item = @rif_item,   
         rif_tifid = @rif_tifid,   
         rif_nombre = @rif_nombre,   
         rif_ubicacion = @rif_nombre  
   WHERE ( rol_informes.rif_codemp = @rif_codemp ) AND  
         ( rol_informes.rif_rolid = @rif_rolid ) AND  
         ( rol_informes.rif_item = @rif_item )
