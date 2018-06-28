-- =============================================
-- Author:		FM
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Cpbt_A_Traspasar] (@codemp int, @idioma int, @pclid int, @ctcid int )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @estid int
	select @estid = EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = @codemp and EMC_EMCID = 66

	SELECT ccb_pclid Pclid,   
	ccb_ctcid Ctcid,   
	ccb_ccbid Ccbid,   
	pcl_nomfant Cliente,   
	ctc_rut RutDeudor,   
	ctc_nomfant Deudor,   
	tci_nombre TipoDocumento,   
	ccb_numero Numero,   
	ccb_fecvenc FechaVencimiento,   
	ccb_monto Monto,   
	ccb_saldo Saldo,   
	mci_nombre Motivo
	FROM cartera_clientes_documentos_cpbt_doc,   
	idiomas
	WHERE  tci_idid = idiomas.idi_idid  and  
	eci_idid = idiomas.idi_idid  and  
	mci_idid = idiomas.idi_idid  and  
	ccb_codemp =  @codemp
	and ccb_estcpbt = 'V'
	and eci_estid =  @estid
	and idiomas.idi_idid =  @idioma
	and ccb_pclid = @pclid
	and ccb_ctcid = @ctcid
END
