-- =============================================
-- Author:		FM
-- Create date: 12-05-2014
-- Description:	Lista regiones segun pais
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Historia_Borradores] (@codemp int, @rolid int, @id_borrador int)
AS
BEGIN
	SET NOCOUNT ON;
	declare @creacion varchar(50), @ultimo varchar(50)
	/****** Script for SelectTopNRows command from SSMS  ******/
SELECT top 1 @creacion = isnull((convert(varchar(10),FECHA_CREACION,103) + ' ' + convert(varchar(10),FECHA_CREACION,108)  + ' por ' + u.usr_nombre),'')
  FROM [ROL BORRADOR], usuarios u
  where codemp = @codemp
  and rolid = @rolid
  and id_borrador = @id_borrador
  and u.usr_codemp = codemp
  and u.usr_usrid = user_creacion
  order by fecha_creacion asc
  
  SELECT top 1 @ultimo = isnull((convert(varchar(10),FECHA_CREACION ,103)+ ' ' + convert(varchar(10),FECHA_CREACION,108)  +  ' por ' + u.usr_nombre),'')
  FROM [ROL BORRADOR], usuarios u
  where codemp = @codemp
  and rolid = @rolid
  and id_borrador = @id_borrador
  and u.usr_codemp = codemp
  and u.usr_usrid = user_creacion
  order by fecha_creacion desc
  
select @creacion Creacion, @ultimo Ultimo  
  
END
