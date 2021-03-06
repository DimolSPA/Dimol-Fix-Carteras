﻿

 Create Procedure Insertar_Periodos_Contables_Meses(@pcm_codemp integer, @pcm_anio integer, @pcm_mes numeric (2), @pcm_inicio datetime, @pcm_fin datetime,
                                                                                           @pcm_apeini integer, @pcm_apefin integer, @pcm_ingini integer, @pcm_ingfin integer, @pcm_egreini integer,
                                                                                           @pcm_egrefin integer, @pcm_trasini integer, @pcm_trasfin integer, @pcm_habilitado char (1), @pcm_finalizado char (1)) as
  INSERT INTO periodos_contables_meses  
         ( pcm_codemp,   
           pcm_anio,   
           pcm_mes,   
           pcm_inicio,   
           pcm_fin,   
           pcm_apeini,   
           pcm_apefin,   
           pcm_ingini,   
           pcm_ingfin,   
           pcm_egreini,   
           pcm_egrefin,   
           pcm_trasini,   
           pcm_trasfin,   
           pcm_habilitado,   
           pcm_finalizado )  
  VALUES ( @pcm_codemp,   
           @pcm_anio,   
           @pcm_mes,   
           @pcm_inicio,   
           @pcm_fin,   
           @pcm_apeini,   
           @pcm_apefin,   
           @pcm_ingini,   
           @pcm_ingfin,   
           @pcm_egreini,   
           @pcm_egrefin,   
           @pcm_trasini,   
           @pcm_trasfin,   
           @pcm_habilitado,   
           @pcm_finalizado )
