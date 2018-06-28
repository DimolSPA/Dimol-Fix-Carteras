create PROCEDURE [dbo].[_Trae_Documentos_cpbt_doc](@codemp int,@ctcid int, @pclid numeric(15,0), @estcpbt char(1))
AS
BEGIN
	SET NOCOUNT ON;
	SELECT
	  tci_nombre + ' - ' + ccb_numero DOCUMENTO, 
	  CCB_CCBID
	FROM cartera_clientes_documentos_cpbt_doc
	WHERE ccb_codemp = @codemp
	AND ccb_ctcid = @ctcid
	AND ccb_pclid = @pclid
	AND ccb_estcpbt = @estcpbt
	ORDER BY ccb_ccbid DESC
END
