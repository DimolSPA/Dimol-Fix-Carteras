-- =============================================
-- Author:		César León
-- Create date: 07-02-2018
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Historia_BorradoresDemandaMasiva] (@codEmp int, @idDM int, @idBorrador int)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @creacion varchar(50), @ultimo varchar(50)


	/****** Script for SelectTopNRows command from SSMS  ******/
	SELECT top 1 @creacion = ISNULL((convert(varchar(10),FECHA_CREACION,103) + ' ' + convert(varchar(10),FECHA_CREACION,108) + ' por ' + u.usr_nombre),'')
	FROM [PANEL_DEMANDA_MASIVA_CONFECCION], USUARIOS u
	WHERE codemp = @codemp
		AND ID_PANEL_MASIVO = @idDM
		AND id_borrador = @idBorrador
		AND u.usr_codemp = codemp
		AND u.usr_usrid = user_creacion
	ORDER by fecha_creacion ASC


	SELECT top 1 @ultimo = ISNULL((convert(varchar(10),FECHA_CREACION ,103) + ' ' + convert(varchar(10),FECHA_CREACION,108) +  ' por ' + u.usr_nombre),'')
	FROM [PANEL_DEMANDA_MASIVA_CONFECCION], USUARIOS u
	WHERE codemp = @codemp
		AND ID_PANEL_MASIVO = @idDM
		AND id_borrador = @idBorrador
		AND u.usr_codemp = codemp
		AND u.usr_usrid = user_creacion
	ORDER by fecha_creacion DESC

	SELECT @creacion Creacion, @ultimo Ultimo  
END
