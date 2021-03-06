﻿CREATE Procedure [dbo].[_Insertar_Clasificacion_CpbtDoc](@clb_codemp integer, 
@clb_codigo varchar(800), 
@clb_tipcpbtdoc varchar(800), 
@clb_tipprod integer, 
@clb_costos char(1), 
@clb_selcpbt char(1), 
@clb_cartcli char(1), 
@clb_contable char(1), 
@clb_selapl char(1), 
@clb_aplica char(1), 
@clb_cptoctbl varchar(800), 
@clb_findeuda char(1), 
@clb_cancela char(1), 
@clb_libcompra integer, 
@clb_cambiodoc char(1), 
@clb_remesa char(1), 
@clb_tipsel integer, 
@clb_sinimp char(1), 
@clb_forpag char(1), 
@clb_ordcomp char(1), 
@cct_debhab integer, 
@cct_libcomven char(1), 
@cct_honorarios char(1), 
@cct_pctid integer, 
@cct_pctid2 integer, 
@ccs_stock integer, 
@ccs_saldos char(1), 
@ccs_reserva char(1), 
@ccs_transito char(1)) as
declare @clbid int

set @clbid = (select IsNull(Max(clb_clbid)+1, 1) from clasificacion_cpbtdoc where clb_codemp = @clb_codemp)

  INSERT INTO clasificacion_cpbtdoc  
         ( clb_codemp,   
           clb_clbid,   
           clb_codigo,   
           clb_tipcpbtdoc,
		   clb_tipprod,
		   clb_costos,
		   clb_selcpbt,
		   clb_cartcli,
		   clb_contable,
		   clb_selapl,
		   clb_aplica,
		   clb_cptoctbl,
		   clb_findeuda,
		   clb_cancela,
		   clb_libcompra,
		   clb_cambiodoc,
		   clb_remesa,
		   clb_tipsel,
		   clb_sinimp,
		   clb_forpag,
		   clb_ordcomp  )  
  VALUES ( @clb_codemp,
		   @clbid,
           @clb_codigo,   
           @clb_tipcpbtdoc,
		   @clb_tipprod,
		   @clb_costos,
		   @clb_selcpbt,
		   @clb_cartcli,
		   @clb_contable,
		   @clb_selapl,
		   @clb_aplica,
		   @clb_cptoctbl,
		   @clb_findeuda,
		   @clb_cancela,
		   @clb_libcompra,
		   @clb_cambiodoc,
		   @clb_remesa,
		   @clb_tipsel,
		   @clb_sinimp,
		   @clb_forpag,
		   @clb_ordcomp )

  INSERT INTO CLASIFICACION_CPBTDOC_CONTABLE
  (cct_codemp,
  cct_clbid,
  cct_debhab,
  cct_libcomven,
  cct_honorarios,
  cct_pctid,
  cct_pctid2
  )
  VALUES(
  @clb_codemp,
  @clbid,
  @cct_debhab,
  @cct_libcomven,
  @cct_honorarios,
  @cct_pctid,
  @cct_pctid2)

  INSERT INTO CLASIFICACION_CPBTDOC_STOCK
  (ccs_codemp,
  ccs_clbid,
  ccs_stock,
  ccs_saldos,
  ccs_reserva,
  ccs_transito
  )
  VALUES(
  @clb_codemp,
  @clbid,
  @ccs_stock,
  @ccs_saldos,
  @ccs_reserva,
  @ccs_transito
  )
