-- =============================================
-- Author:		<César León>
-- Create date: <02/03/2018>
-- Description:	<Retorna los documentos correspondientes a una resolucion>
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Cartera_Clientes_Cpbt_Documento_Por_Numero_Resolucion]
	@numResolucion VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT CC.CCB_CCBID as 'Ccbid', CC.CCB_MONTO as 'Monto', CC.CCB_SALDO as 'Saldo', CC.CCB_FECDOC as 'FechaDoc' FROM
	(
		SELECT [CCB_CODEMP],[CCB_PCLID],[CCB_CTCID],[CCB_CCBID],[NUM_RESOLUCION]
		FROM [dbo].[CARTERA_CLIENTES_CPBT_DOC_EXTENDIDO]
		WHERE NUM_RESOLUCION = @numResolucion
	) CC_EXT

	JOIN [dbo].[CARTERA_CLIENTES_CPBT_DOC] CC on 
		CC_EXT.CCB_CODEMP = CC.CCB_CODEMP AND
		CC_EXT.CCB_PCLID = CC.CCB_PCLID AND
		CC_EXT.CCB_CTCID = CC.CCB_CTCID AND
		
		CC_EXT.CCB_CCBID = CC.CCB_CCBID
END
