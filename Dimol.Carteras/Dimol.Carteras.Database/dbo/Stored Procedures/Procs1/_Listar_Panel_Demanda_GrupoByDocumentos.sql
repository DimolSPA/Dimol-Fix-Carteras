CREATE PROCEDURE [dbo].[_Listar_Panel_Demanda_GrupoByDocumentos] (@codemp int, @idioma int,
																@pclid int, @ctcid int, 
																@sbcid int,@tpcid int)
AS
BEGIN
	declare @estid int, @quiebra varchar(1) = 'N'
	select @estid = EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = @codemp and EMC_EMCID = 66
	select @quiebra = CTC_QUIEBRA from DEUDORES where CTC_CTCID = @ctcid
	
	declare @query varchar(7000);
	set @query =
	'SELECT cpbt.CCB_PCLID, cpbt.CCB_CTCID, cpbt.CCB_CCBID, ccc.CCB_SBCID, ccc.CCB_TPCID
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
	cpbt.ccb_codemp = ' + CONVERT(VARCHAR,@codemp) + '
	and cpbt.ccb_estcpbt = ''V''
	and cpbt.eci_estid =  ' + CONVERT(VARCHAR,@estid) +'
	and idiomas.idi_idid =  ' + CONVERT(VARCHAR,@idioma) + '
	and cpbt.ccb_pclid = ' + CONVERT(VARCHAR,@pclid) + '
	and cpbt.ccb_ctcid = ' + CONVERT(VARCHAR,@ctcid) + ''
if @quiebra = 'S'
begin
	set @query = @query + ' '
end
else
begin
	set @query = @query + ' and ccc.ccb_tpcid = ' + CONVERT(VARCHAR,@tpcid) + ''
end
if @quiebra = 'S'
begin
	set @query = @query + ''
end
else
begin
	if @sbcid = 0
	begin  
		set @query = @query + ' and ccc.ccb_sbcid is null';
	end 
	else
	begin 
		set @query = @query + ' and ccc.ccb_sbcid = ' + CONVERT(VARCHAR,@sbcid) + '';
	end 
end	
 
	
exec(@query)	
	
END
