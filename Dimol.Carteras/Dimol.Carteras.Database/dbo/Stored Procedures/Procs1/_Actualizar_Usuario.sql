-- =============================================
-- Author:		César León
-- Create date: 05-01-2018
-- Description:	Procedimiento para actualizar un usuario
-- =============================================
CREATE PROCEDURE [dbo].[_Actualizar_Usuario]
(
	 @USR_USRID int
	--,@USR_CODEMP int -- No se debe actualizar el com_empleado, se genera automàticamente
	,@USR_NOMBRE varchar(200)
	,@USR_LOGIN varchar(15)
	,@USR_PASSWORD varchar(25)

	--,@USR_FECING datetime -- No debe poderse actualizar la fecha de ingreso
	,@USR_GODLOG int
	,@USR_BADLOG int
	,@USR_FECULTLOG datetime
	,@USR_FECBLOCK datetime

	,@USR_MAIL varchar(80)
	,@USR_TIPQUEST smallint
	,@USR_ANSWER varchar(200)
	--,@USR_SUCID int
	 ,@USR_PRFID int

	,@USR_PERMISOS smallint
	,@USR_ESTADO char(1)
	,@USR_CAMPASS char(1)
	,@USR_FECCAMBIO datetime
	--,@USR_PERMREMOTO char(1)
)
AS
BEGIN
	UPDATE [dbo].[USUARIOS]
	SET
	   --[USR_CODEMP]		= @USR_CODEMP -- No se debe actualizar el com_empleado, se genera automàticamente
       [USR_NOMBRE]		= @USR_NOMBRE
      ,[USR_LOGIN]		= @USR_LOGIN
      ,[USR_PASSWORD]   = @USR_PASSWORD
      --,[USR_FECING]		= @USR_FECING -- No debe poderse actualizar la fecha de ingreso
      ,[USR_GODLOG]		= @USR_GODLOG
      ,[USR_BADLOG]		= @USR_BADLOG
      ,[USR_FECULTLOG]  = @USR_FECULTLOG
      ,[USR_FECBLOCK]   = @USR_FECBLOCK
      ,[USR_MAIL]		= @USR_MAIL
      ,[USR_TIPQUEST]   = @USR_TIPQUEST
      ,[USR_ANSWER]     = @USR_ANSWER
      --,[USR_SUCID]      = @USR_SUCID
      ,[USR_PRFID]      = @USR_PRFID
      ,[USR_PERMISOS]   = @USR_PERMISOS
      ,[USR_ESTADO]     = @USR_ESTADO
      ,[USR_CAMPASS]    = @USR_CAMPASS
      ,[USR_FECCAMBIO]  = @USR_FECCAMBIO
      --,[USR_PERMREMOTO] = @USR_PERMREMOTO
	WHERE USR_USRID = @USR_USRID;
END
