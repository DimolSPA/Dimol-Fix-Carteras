

Create Procedure Insertar_Rol_Informes(@rif_codemp integer, @rif_rolid integer, @rif_item integer, @rif_tifid integer,
												@rif_nombre varchar (800), @rif_ubicacion varchar (1200)) as  
  INSERT INTO rol_informes  
         ( rif_codemp,   
           rif_rolid,   
           rif_item,   
           rif_tifid,   
           rif_nombre,   
           rif_ubicacion )  
  VALUES ( @rif_codemp,   
           @rif_rolid,   
           @rif_item,   
           @rif_tifid,   
           @rif_nombre,   
           @rif_ubicacion )
