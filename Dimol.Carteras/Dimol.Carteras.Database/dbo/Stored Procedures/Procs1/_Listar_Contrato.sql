-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Listar_Contrato]
(
@codemp int,
@pclid int,
@tipoCartera int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	 SELECT cc.cct_cctid Id,   
              cc.cct_nombre as Nombre
             FROM contratos_cartera cc,   
             contratos_clientes ccl
             WHERE  ccl.ctc_codemp = cc.cct_codemp  and  
             ccl.ctc_cctid = cc.cct_cctid  and  
             cc.cct_codemp = @codemp
             and cc.cct_tipo = @tipoCartera
             and ctc_pclid = @pclid
             order by nombre

END
