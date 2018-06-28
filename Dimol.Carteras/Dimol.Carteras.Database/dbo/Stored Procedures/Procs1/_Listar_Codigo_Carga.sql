-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Listar_Codigo_Carga]
(
@codemp int,
@pclid int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT  pcc_codid Id,
			pcc_codigo + ' - '+ pcc_nombre Nombre
             FROM provcli_codigo_carga
             WHERE pcc_codemp = @codemp
             and pcc_pclid =  @pclid
             order by pcc_codigo
	


END
