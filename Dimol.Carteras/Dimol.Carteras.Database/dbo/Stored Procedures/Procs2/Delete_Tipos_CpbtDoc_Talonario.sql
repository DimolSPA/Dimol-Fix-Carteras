

Create Procedure Delete_Tipos_CpbtDoc_Talonario(@tct_codemp integer, @tct_tacid integer, @tct_tpcid integer, @tct_sucid integer) as
  DELETE FROM tipos_cpbtdoc_talonario  
   WHERE ( tipos_cpbtdoc_talonario.tct_codemp = @tct_codemp ) AND  
         ( tipos_cpbtdoc_talonario.tct_tacid = @tct_tacid ) AND  
         ( tipos_cpbtdoc_talonario.tct_tpcid = @tct_tpcid ) AND  
         ( tipos_cpbtdoc_talonario.tct_sucid = @tct_sucid )
