-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Usuarios]
(
@codemp int
)
AS
BEGIN
	SET NOCOUNT ON;
	
select USR_USRID, USR_NOMBRE 
from USUARIOS
where USR_CODEMP = @codemp
order by USR_NOMBRE

end
