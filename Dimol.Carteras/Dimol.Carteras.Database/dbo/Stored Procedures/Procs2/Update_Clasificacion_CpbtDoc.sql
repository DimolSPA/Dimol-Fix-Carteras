﻿

Create Procedure Update_Clasificacion_CpbtDoc(@clb_codemp integer, @clb_clbid integer, @clb_codigo varchar(4), @clb_tipcpbtdoc char(1),
															  @clb_tipprod smallint, @clb_costos char(1), @clb_selcpbt char(1), @clb_cartcli char(1),
															  @clb_contable char(1), @clb_selapl char(1), @clb_aplica char(1), @clb_cptoctbl char(1),
															  @clb_findeuda char(1), @clb_cancela char(1), @clb_libcompra smallint, @clb_cambiodoc char(1), 
                                                              @clb_remesa char(1), @clb_tipsel smallint, @clb_sinimp char(1), 
                                                              @clb_forpag char(1), @clb_ordcomp char(1)) as

  UPDATE clasificacion_cpbtdoc  
     SET clb_codigo = @clb_codigo,   
         clb_tipcpbtdoc = @clb_tipcpbtdoc,   
         clb_tipprod = @clb_tipprod,   
         clb_costos = @clb_costos,   
         clb_selcpbt = @clb_selcpbt,   
         clb_cartcli = @clb_cartcli,   
         clb_contable = @clb_contable,   
         clb_selapl = @clb_selapl,   
         clb_aplica = @clb_aplica,   
         clb_cptoctbl = @clb_cptoctbl,   
         clb_findeuda = @clb_findeuda,   
         clb_cancela = @clb_cancela,   
         clb_libcompra = @clb_libcompra,  
         clb_cambiodoc = @clb_cambiodoc,
         clb_remesa = @clb_remesa,
         clb_tipsel = @clb_tipsel,
         clb_sinimp = @clb_sinimp,
         clb_forpag = @clb_forpag,
         clb_ordcomp = @clb_ordcomp 
   WHERE ( clasificacion_cpbtdoc.clb_codemp = @clb_codemp ) AND  
         ( clasificacion_cpbtdoc.clb_clbid = @clb_clbid )
