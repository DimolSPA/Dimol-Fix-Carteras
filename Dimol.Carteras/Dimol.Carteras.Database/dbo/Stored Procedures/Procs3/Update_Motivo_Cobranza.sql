

Create Procedure Update_Motivo_Cobranza(@mtc_codemp integer, @mtc_mtcid integer, @mtc_nombre varchar (80)) as  
  UPDATE motivo_cobranza  
     SET mtc_nombre = @mtc_nombre  
   WHERE ( motivo_cobranza.mtc_codemp = @mtc_codemp ) AND  
         ( motivo_cobranza.mtc_mtcid = @mtc_mtcid )
