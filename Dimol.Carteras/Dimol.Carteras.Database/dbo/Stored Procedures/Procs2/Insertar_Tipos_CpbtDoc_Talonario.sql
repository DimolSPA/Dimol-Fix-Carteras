

Create Procedure Insertar_Tipos_CpbtDoc_Talonario(@tct_codemp integer, @tct_tacid integer, @tct_tpcid integer, @tct_sucid integer) as
 
  INSERT INTO tipos_cpbtdoc_talonario  
         ( tct_codemp,   
           tct_tacid,   
           tct_tpcid,   
           tct_sucid )  
  VALUES ( @tct_codemp,   
           @tct_tacid,   
           @tct_tpcid,   
           @tct_sucid )
