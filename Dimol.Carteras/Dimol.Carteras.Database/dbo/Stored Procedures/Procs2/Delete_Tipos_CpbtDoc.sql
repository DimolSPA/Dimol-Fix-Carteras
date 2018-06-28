

Create Procedure Delete_Tipos_CpbtDoc(@tpc_codemp integer, @tpc_tpcid integer) as
  DELETE FROM tipos_cpbtdoc_idiomas  
   WHERE ( tipos_cpbtdoc_idiomas.tci_codemp = @tpc_codemp ) AND  
         ( tipos_cpbtdoc_idiomas.tci_tpcid = @tpc_tpcid )   
    
  DELETE FROM tipos_cpbtdoc_report  
   WHERE ( tipos_cpbtdoc_report.trc_codemp = @tpc_codemp ) AND  
         ( tipos_cpbtdoc_report.trc_tpcid = @tpc_tpcid )   
           
  DELETE FROM tipos_cpbtdoc_talonario  
   WHERE ( tipos_cpbtdoc_talonario.tct_codemp = @tpc_codemp ) AND  
         ( tipos_cpbtdoc_talonario.tct_tpcid = @tpc_tpcid )   


  DELETE FROM tipos_cpbtdoc  
   WHERE ( tipos_cpbtdoc.tpc_codemp = @tpc_codemp ) AND  
         ( tipos_cpbtdoc.tpc_tpcid = @tpc_tpcid )
