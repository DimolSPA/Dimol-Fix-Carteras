-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar acciones para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Buscar_Rol_Cliente_Deudor]
(
@codemp int ,
@pclid int ,
@ctcid int 
)
AS
BEGIN
	SET NOCOUNT ON;

		Select  rol.rol_rolid as rolid
		FROM rol
		WHERE  rol.rol_codemp =  @codemp
		and rol.rol_pclid =  @pclid
		and rol.rol_ctcid =  @ctcid
		and datepart(year,rol.rol_fecemi) = datepart(year,getdate())   
		and datepart(month,rol.rol_fecemi) = datepart(month,getdate())   
		and datepart(day,rol.rol_fecemi) = datepart(day,getdate()) 
			

END
