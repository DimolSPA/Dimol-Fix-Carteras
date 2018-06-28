CREATE PROCEDURE [dbo].[_Listar_Panel_Demanda_GrupoByLlave] (@codemp int, @idioma int, @pclid int, @ctcid int )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @estid int, @quiebra varchar(1) = 'N'
	select @estid = EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = @codemp and EMC_EMCID = 66
	
	select @quiebra = CTC_QUIEBRA from DEUDORES where CTC_CTCID = @ctcid
	if @quiebra = 'S'
		begin
		SELECT cpbt.CCB_PCLID, cpbt.CCB_CTCID, NULL CCB_SBCID, 0 CCB_TPCID
		FROM cartera_clientes_documentos_cpbt_doc cpbt with(nolock)
		JOIN CARTERA_CLIENTES_CPBT_DOC ccc with(nolock)
		ON   cpbt.CCB_CODEMP = ccc.CCB_CODEMP
		AND cpbt.CCB_PCLID = ccc.CCB_PCLID
		AND cpbt.CCB_CTCID = ccc.CCB_CTCID
		AND cpbt.CCB_CCBID = ccc.CCB_CCBID
		JOIN idiomas
		ON  cpbt.tci_idid = idiomas.idi_idid  and  
		cpbt.eci_idid = idiomas.idi_idid  and  
		cpbt.mci_idid = idiomas.idi_idid  and  
		cpbt.ccb_codemp =  @codemp
		and cpbt.ccb_estcpbt = 'V'
		and cpbt.eci_estid =  @estid
		and idiomas.idi_idid =  @idioma
		and cpbt.ccb_pclid = @pclid
		and cpbt.ccb_ctcid = @ctcid
		group by cpbt.CCB_PCLID, cpbt.CCB_CTCID
	end
	else
	begin
		SELECT cpbt.CCB_PCLID, cpbt.CCB_CTCID,ccc.CCB_SBCID, ccc.CCB_TPCID
		FROM cartera_clientes_documentos_cpbt_doc cpbt with(nolock)
		JOIN CARTERA_CLIENTES_CPBT_DOC ccc with(nolock)
		ON   cpbt.CCB_CODEMP = ccc.CCB_CODEMP
		AND cpbt.CCB_PCLID = ccc.CCB_PCLID
		AND cpbt.CCB_CTCID = ccc.CCB_CTCID
		AND cpbt.CCB_CCBID = ccc.CCB_CCBID
		JOIN idiomas
		ON  cpbt.tci_idid = idiomas.idi_idid  and  
		cpbt.eci_idid = idiomas.idi_idid  and  
		cpbt.mci_idid = idiomas.idi_idid  and  
		cpbt.ccb_codemp =  @codemp
		and cpbt.ccb_estcpbt = 'V'
		and cpbt.eci_estid =  @estid
		and idiomas.idi_idid =  @idioma
		and cpbt.ccb_pclid = @pclid
		and cpbt.ccb_ctcid = @ctcid
		group by cpbt.CCB_PCLID, cpbt.CCB_CTCID,ccc.CCB_SBCID, ccc.CCB_TPCID
	end
END
