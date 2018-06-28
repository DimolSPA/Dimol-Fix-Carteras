-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar acciones para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Existe_Ente_Rol]
(
@codemp int ,
@rolid int ,
@etjid int 
)
AS
BEGIN
	SET NOCOUNT ON;
		select  count(ejr_etjid)
		from entejud_rol 
		where ejr_codemp=@codemp
		and ejr_rolid=@rolid 
		and ejr_etjid=@etjid
		
			

END
