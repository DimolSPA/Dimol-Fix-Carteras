-- =============================================
-- Author:		César León
-- Create date: 04-01-2018
-- Description:	Procedimiento para buscar un usuario por idUsuario
-- =============================================
CREATE PROCEDURE [dbo].[_Buscar_UsuarioPorIdUsuario]
(
	@USRID int
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT U.[USR_CODEMP]
      ,U.[USR_USRID]
	  ,E.[EPL_RUT]
      ,U.[USR_NOMBRE]
      ,U.[USR_LOGIN]
      ,U.[USR_PASSWORD]
      ,U.[USR_FECING]
      ,U.[USR_GODLOG]
      ,U.[USR_BADLOG]
      ,U.[USR_FECULTLOG]
      ,U.[USR_FECBLOCK]
      ,U.[USR_MAIL]
      ,U.[USR_TIPQUEST]
      ,U.[USR_ANSWER]
      ,U.[USR_SUCID]
      ,U.[USR_PRFID]
      ,U.[USR_PERMISOS]
      ,U.[USR_ESTADO]
      ,U.[USR_CAMPASS]
      ,U.[USR_FECCAMBIO]
      ,U.[USR_PERMREMOTO]
	FROM [dbo].[USUARIOS] U
	JOIN [dbo].[EMPLEADOS] E ON U.USR_USRID = E.EPL_USRID
	WHERE USR_USRID = @USRID
END
