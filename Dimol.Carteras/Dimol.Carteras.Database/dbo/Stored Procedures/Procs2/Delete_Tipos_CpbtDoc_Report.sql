

Create Procedure Delete_Tipos_CpbtDoc_Report(@trc_codemp integer, @trc_tpcid integer, @trc_trcid integer) as
  DELETE FROM tipos_cpbtdoc_report  
   WHERE ( tipos_cpbtdoc_report.trc_codemp = @trc_codemp ) AND  
         ( tipos_cpbtdoc_report.trc_tpcid =  @trc_tpcid ) AND  
         ( tipos_cpbtdoc_report.trc_trcid =  @trc_trcid )
