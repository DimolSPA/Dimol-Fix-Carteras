-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar acciones para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Buscar_Rol_Demanda_Avenimiento]
(
@codemp int ,
@rolid int 
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1000 [RAD_CODEMP]
      ,[RAD_ROLID]
      ,[RAD_FECDEM]
      ,[RAD_CUODEM]
      ,[RAD_MONDEM]
      ,[RAD_MONUCOUDEM]
      ,[RAD_FECPCOUDEM]
      ,[RAD_FECUCOUDEM]
      ,[RAD_INTDEM]
      ,[RAD_FECAVE]
      ,[RAD_CUOAVE]
      ,[RAD_MONAVE]
      ,[RAD_MONUCOUAVE]
      ,[RAD_FECPCOUAVE]
      ,[RAD_FECUCOUAVE]
      ,[RAD_INTAVE]
      ,[RAD_MONPCUODEM]
      ,[RAD_MONPCOUAVE]
  FROM [dbo].[ROL_AVEDEM]
  where RAD_CODEMP = @codemp 
  and RAD_ROLID = @rolid
		

END
