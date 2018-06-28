

Create Procedure Delete_Rol_Datos_Reporte(@rdr_codemp integer, @rdr_rolid integer) as  
   DELETE FROM rol_datos_reporte  
   WHERE ( rol_datos_reporte.rdr_codemp = @rdr_codemp ) AND  
         ( rol_datos_reporte.rdr_rolid = @rdr_rolid )
