

Create Procedure Update_Aplicaciones_Especial(@apl_codemp integer, @apl_sucid integer, @apl_anio integer, @apl_numapl integer, @apl_fecapl datetime) as
  UPDATE aplicaciones  
     SET apl_fecapl = @apl_fecapl  
   WHERE ( aplicaciones.apl_codemp = @apl_codemp ) AND  
         ( aplicaciones.apl_sucid = @apl_sucid ) AND  
         ( aplicaciones.apl_anio = @apl_anio ) AND  
         ( aplicaciones.apl_numapl = @apl_numapl )
