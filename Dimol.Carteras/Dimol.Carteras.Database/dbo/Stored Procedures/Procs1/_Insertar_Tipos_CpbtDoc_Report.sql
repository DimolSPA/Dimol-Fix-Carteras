CREATE Procedure [dbo].[_Insertar_Tipos_CpbtDoc_Report](@trc_codemp integer, @trc_tpcid integer, @trc_reporte varchar(800), @trc_nombre varchar(200), @trc_reppad varchar(800)) as
declare @tpcid int

set @tpcid = (select IsNull(Max(trc_trcid)+1, 1) from tipos_cpbtdoc_report where trc_codemp = @trc_codemp)

  INSERT INTO tipos_cpbtdoc_report  
         ( trc_codemp,   
           trc_tpcid,   
           trc_trcid,   
           trc_reporte,
		   trc_nombre,
		   trc_reppad )  
  VALUES ( @trc_codemp,
		   @trc_tpcid,
           @tpcid,   
           @trc_reporte,
		   @trc_nombre,
		   @trc_reppad )
