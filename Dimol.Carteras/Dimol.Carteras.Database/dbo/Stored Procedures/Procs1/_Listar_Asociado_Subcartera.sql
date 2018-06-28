-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Listar_Asociado_Subcartera]
(
@codemp int
)
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT subcarteras.sbc_sbcid id,  
subcarteras.sbc_nombre Nombre
FROM subcarteras
WHERE subcarteras.sbc_codemp  = @codemp
order by sbc_nombre


END
