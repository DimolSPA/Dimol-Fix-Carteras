-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 09-04-2014
-- Description:	Trae casos con compromisos
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Casos_Compromiso] (@codemp as int, @estid as int) 
AS
BEGIN
	SET NOCOUNT ON;

    SELECT DISTINCT cartera_clientes_cpbt_doc.ccb_pclid,
    
cartera_clientes_cpbt_doc.ccb_ctcid, ccb_ccbid
FROM cartera_clientes_cpbt_doc
 WHERE  cartera_clientes_cpbt_doc.ccb_codemp =@codemp
 and cartera_clientes_cpbt_doc.ccb_estid = @estid
and cartera_clientes_cpbt_doc.ccb_fecplazo < getdate()
END
