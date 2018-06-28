

Create Procedure Insertar_Reportes(@rpt_trvid integer, @rpt_rptid integer, @rpt_nombre varchar(200), @rpt_tipo char(1), @rpt_cartera char(1), @rpt_tipcart smallint) as
  INSERT INTO reportes  
         ( rpt_trvid,   
           rpt_rptid,   
           rpt_nombre,   
           rpt_tipo,   
           rpt_cartera,   
           rpt_tipcart )  
  VALUES ( @rpt_trvid,   
           @rpt_rptid,   
           @rpt_nombre,   
           @rpt_tipo,   
           @rpt_cartera,   
           @rpt_tipcart )
