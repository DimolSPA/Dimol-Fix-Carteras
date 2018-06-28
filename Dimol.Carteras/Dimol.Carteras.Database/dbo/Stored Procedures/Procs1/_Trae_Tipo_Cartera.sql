-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar documentos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Tipo_Cartera]
(
@codemp int,
@pclid integer, 
@ctcid integer, 
@estcpbt char(1)
)
AS
BEGIN
	SET NOCOUNT ON;
declare @masiva int = (
SELECT count(cartera_clientes_documentos_cpbt_doc.ccb_tipcart)
    FROM cartera_clientes_documentos_cpbt_doc  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ctcid  and ccb_estcpbt = @estcpbt and ccb_tipcart = 1))
         
declare @dura int = (
SELECT count(cartera_clientes_documentos_cpbt_doc.ccb_tipcart)
    FROM cartera_clientes_documentos_cpbt_doc  
   WHERE ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_ctcid = @ctcid  and ccb_estcpbt = @estcpbt and ccb_tipcart = 2))         
	
if @dura = 0 and @masiva >0
begin
	select 1 as cartera
end
else
	select 2 as cartera


END
