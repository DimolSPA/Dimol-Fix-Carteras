CREATE procedure [dbo].[_Insertar_Cuenta_Ejecutivo] (@cuenta varchar(200), @idEjecutivo int, @idBanco int) as
 INSERT INTO EJECUTIVO_CUENTA_MUTUAL (CUENTA, ID_EJECUTIVO, ID_TIPO_BANCO)
 VALUES (@cuenta, @idEjecutivo, @idBanco)
