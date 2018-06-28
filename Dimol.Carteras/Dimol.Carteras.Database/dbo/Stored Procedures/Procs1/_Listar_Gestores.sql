-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Gestores]
(
@codemp int,
@codsuc int
)
AS
BEGIN
	SET NOCOUNT ON;
	
select GES_GESID, GES_NOMBRE 
from GESTOR
where GES_CODEMP = @codemp
and GES_SUCID = @codsuc
and GES_ESTADO = 'A'
order by GES_NOMBRE

end
