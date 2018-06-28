-- =============================================
-- Author:		FM
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[_Listar_Tribunal_Auto] (@nombre varchar(200))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @like varchar(250) = '%' + @nombre + '%'  
    select TRB_TRBID, TRB_NOMBRE from TRIBUNALES p where p.TRB_NOMBRE like @like
END