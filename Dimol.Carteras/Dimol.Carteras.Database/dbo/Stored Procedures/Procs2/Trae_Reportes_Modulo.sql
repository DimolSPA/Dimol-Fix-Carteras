

Create Procedure Trae_Reportes_Modulo(@rpt_trvid integer, @tpt_tipo char(1), @rti_idid integer) as
  SELECT reportes_idiomas.rti_nombre,   
         reportes_idiomas.rti_rptid  
    FROM reportes,   
         reportes_idiomas  
   WHERE ( reportes_idiomas.rti_trvid = reportes.rpt_trvid ) and  
         ( reportes_idiomas.rti_rptid = reportes.rpt_rptid ) and  
         ( ( reportes.rpt_trvid = @rpt_trvid ) AND  
         ( reportes.rpt_tipo = @tpt_tipo ) AND  
         ( reportes_idiomas.rti_idid = @rti_idid )   
         )   
ORDER BY reportes_idiomas.rti_nombre ASC
