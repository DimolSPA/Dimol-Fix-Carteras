CREATE Procedure [dbo].[_rpt_Trae_Ultimo_Borrador](@codemp integer, @id_borrador integer, @rolid integer) as

SELECT TOP 1 isnull([HTML], '') html
  FROM [ROL BORRADOR]
  where CODEMP = @codemp
  AND ID_BORRADOR = @id_borrador
  AND ROLID = @rolid
  ORDER BY FECHA_CREACION DESC
