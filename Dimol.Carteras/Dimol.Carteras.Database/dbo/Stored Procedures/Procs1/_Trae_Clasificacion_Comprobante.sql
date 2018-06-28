-- =============================================
-- Author:		FMO
-- Create date: 23-05-2014
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Clasificacion_Comprobante] (@codemp int, @tpcid int)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT clb_tipcpbtdoc,   
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
	clb_forpag, 
	tpc_tipdig, 
	clb_ordcomp,
	clb_clbid, 
	clb_sinimp
	FROM tipos_cpbtdoc,   
	clasificacion_cpbtdoc
	WHERE  clb_codemp = tipos_cpbtdoc.tpc_codemp  and  
	clb_clbid = tipos_cpbtdoc.tpc_clbid  and  
	tipos_cpbtdoc.tpc_codemp =  @codemp
	and tipos_cpbtdoc.tpc_tpcid = @tpcid
			
END
