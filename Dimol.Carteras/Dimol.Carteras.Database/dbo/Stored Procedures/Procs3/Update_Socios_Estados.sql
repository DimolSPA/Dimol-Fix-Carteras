

Create Procedure Update_Socios_Estados(@soc_codemp integer, @soc_socid integer, @soc_estado char(1), @soc_fecfin datetime ) as
  
  UPDATE socios  
     SET soc_estado = @soc_estado,   
         soc_fecfin = @soc_fecfin  
   WHERE ( socios.soc_codemp = @soc_codemp ) AND  
         ( socios.soc_socid = @soc_socid )
