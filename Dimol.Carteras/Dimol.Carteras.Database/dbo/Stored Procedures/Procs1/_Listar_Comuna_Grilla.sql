-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Comuna_Grilla]

AS
BEGIN
	SET NOCOUNT ON;
	
select COM_COMID id,
com_nombre nombre
from COMUNA
order by nombre

	

END
