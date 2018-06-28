

Create Procedure Update_Tipos_CpbtDoc_Report(@trc_codemp integer, @trc_tpcid integer, @trc_trcid integer, @trc_reporte varchar(800), @trc_nombre varchar(200), @trc_reppad varchar(800)) as
  UPDATE tipos_cpbtdoc_report  
     SET trc_reporte = @trc_reporte,   
         trc_nombre = @trc_nombre,
         trc_reppad = @trc_reppad  
   WHERE ( tipos_cpbtdoc_report.trc_codemp = @trc_codemp ) AND  
         ( tipos_cpbtdoc_report.trc_tpcid = @trc_tpcid ) AND  
         ( tipos_cpbtdoc_report.trc_trcid = @trc_trcid )
