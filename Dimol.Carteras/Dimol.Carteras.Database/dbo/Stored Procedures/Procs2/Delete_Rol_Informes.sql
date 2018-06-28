

Create Procedure Delete_Rol_Informes(@rif_codemp integer, @rif_rolid integer, @rif_item integer) as
  DELETE FROM rol_informes  
   WHERE ( rol_informes.rif_codemp = @rif_codemp ) AND  
         ( rol_informes.rif_rolid = @rif_rolid ) AND  
         ( rol_informes.rif_item = @rif_item )
