

Create Procedure Update_Talonario_Numero(@tct_codemp integer, @tct_tpcid integer, @tct_sucid integer) as
  UPDATE talonario_cpbtdoc  
     SET tac_numero = tac_numero + 1
    FROM talonario_cpbtdoc,   
         tipos_cpbtdoc_talonario  
   WHERE ( tipos_cpbtdoc_talonario.tct_codemp = talonario_cpbtdoc.tac_codemp ) and  
         ( tipos_cpbtdoc_talonario.tct_tacid = talonario_cpbtdoc.tac_tacid ) and  
         ( ( tipos_cpbtdoc_talonario.tct_codemp = @tct_codemp ) AND  
         ( tipos_cpbtdoc_talonario.tct_tpcid = @tct_tpcid ) AND  
         ( tipos_cpbtdoc_talonario.tct_sucid = @tct_sucid )   
         )
