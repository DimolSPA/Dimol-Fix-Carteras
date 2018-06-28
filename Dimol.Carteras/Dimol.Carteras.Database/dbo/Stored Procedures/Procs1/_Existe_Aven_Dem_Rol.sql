-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar acciones para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Existe_Aven_Dem_Rol]
(
@codemp int ,
@rolid int
)
AS
BEGIN
	SET NOCOUNT ON;
	select count(rad_codemp) cantidad
	from rol_avedem 
	where rad_codemp=@codemp
	and rad_rolid=@rolid 	
			

END
