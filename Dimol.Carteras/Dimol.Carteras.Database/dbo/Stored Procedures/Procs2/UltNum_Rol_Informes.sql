

Create Procedure UltNum_Rol_Informes(@rif_codemp integer, @rif_rolid integer) as
  SELECT IsNull(Max(rif_item)+1, 1) 
    FROM rol_informes  
   WHERE ( rol_informes.rif_codemp = @rif_codemp ) AND  
         ( rol_informes.rif_rolid = @rif_rolid )
