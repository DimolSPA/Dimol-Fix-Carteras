-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 13-04-2014
-- Description:	Trae casos con compromisos
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Casos_Vencidos] (@codemp as int, @estid as int, @pclid as varchar(100)) 
AS
BEGIN
	SET NOCOUNT ON;
	declare @Query varchar(7000)
set @Query='SELECT DISTINCT cartera_clientes_cpbt_doc.ccb_pclid,
	cartera_clientes_cpbt_doc.ccb_ctcid
	FROM cartera_clientes_cpbt_doc
	WHERE  cartera_clientes_cpbt_doc.ccb_codemp =' + CONVERT(VARCHAR,@codemp)   +
	'and cartera_clientes_cpbt_doc.ccb_estid ='+ CONVERT(VARCHAR,@estid) +
	'and ccb_pclid in ('+ CONVERT(VARCHAR,@pclid)  + ')'+
	'and cartera_clientes_cpbt_doc.ccb_fecvenc < getdate()'

 exec(@query)
	

END
