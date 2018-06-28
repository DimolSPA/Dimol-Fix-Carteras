CREATE Procedure [dbo].[_rpt_Trae_Ultimo_Template](@codemp integer, @id_borrador integer) as

/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1 isnull(v.[TEMPLATE],'') template , t.DESC_TIPO_BORRADOR tipo
  FROM [BORRADOR_VERSION] v, BORRADOR b, BORRADOR_TIPO t
  where v.CODEMP = @codemp
  and v.ID_BORRADOR = @id_borrador
  and v.ESTADO = 'V'
  and b.CODEMP = v.CODEMP
  and b.ID_BORRADOR = v.ID_BORRADOR
  and b.CODEMP = t.CODEMP
  and b.TIPO_BORRADOR = t.TIPO_BORRADOR
  order by ID_VERSION desc
