-- =============================================
-- Author:		FM
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Auto_Estados_Cartera] (@texto varchar(200))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @nombre varchar(250) = '%' + @texto + '%'
    select convert(varchar,ECT_ESTID) + ' - ' + ECT_NOMBRE, ECT_ESTID 
	from ESTADOS_CARTERA
	where ECT_CODEMP = 1
	and ECT_NOMBRE like @nombre
    
END
