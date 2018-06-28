

Create Procedure Update_Socios_Foto(@soc_codemp integer, @soc_socid integer, @soc_foto image) as
  UPDATE socios  
     SET soc_foto = @soc_foto
   WHERE ( soc_codemp = @soc_codemp ) AND  
         ( soc_socid = @soc_socid )
