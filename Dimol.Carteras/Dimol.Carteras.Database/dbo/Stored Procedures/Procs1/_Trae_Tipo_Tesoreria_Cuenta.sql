﻿CREATE PROCEDURE [dbo].[_Trae_Tipo_Tesoreria_Cuenta]
AS
BEGIN
	SELECT TIPO_CUENTA_ID, DESCRIPCION FROM TESORERIA_CUENTAS_TIPO
END
