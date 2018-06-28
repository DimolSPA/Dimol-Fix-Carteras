

Create Procedure Insertar_Tipos_CpbtDoc_Report(@trc_codemp integer, @trc_tpcid integer, @trc_trcid integer, @trc_reporte varchar(800), @trc_nombre varchar(200), @trc_reppad varchar(800)) as
  INSERT INTO tipos_cpbtdoc_report  
         ( trc_codemp,   
           trc_tpcid,   
           trc_trcid,   
           trc_reporte,   
           trc_nombre,
           trc_reppad )  
  VALUES ( @trc_codemp,   
           @trc_tpcid,   
           @trc_trcid,   
           @trc_reporte,   
           @trc_nombre,
           @trc_reppad )
