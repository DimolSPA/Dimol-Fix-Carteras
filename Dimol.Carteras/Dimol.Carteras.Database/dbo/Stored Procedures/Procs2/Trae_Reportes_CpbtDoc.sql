

Create Procedure Trae_Reportes_CpbtDoc(@trc_codemp integer, @trc_tpcid integer) as
  SELECT tipos_cpbtdoc_report.trc_trcid,   
         tipos_cpbtdoc_report.trc_nombre  
    FROM tipos_cpbtdoc_report  
   WHERE ( tipos_cpbtdoc_report.trc_codemp = @trc_codemp ) AND  
         ( tipos_cpbtdoc_report.trc_tpcid = @trc_tpcid )   
order by trc_nombre
