

Create Procedure Delete_Talonario_CpbtDoc(@tac_codemp integer, @tac_tacid integer) as
 
  DELETE FROM tipos_cpbtdoc_talonario  
   WHERE ( tipos_cpbtdoc_talonario.tct_codemp = @tac_codemp ) AND  
         ( tipos_cpbtdoc_talonario.tct_tacid = @tac_tacid )   


  DELETE FROM talonario_cpbtdoc  
   WHERE ( talonario_cpbtdoc.tac_codemp = @tac_codemp ) AND  
         ( talonario_cpbtdoc.tac_tacid = @tac_tacid )
