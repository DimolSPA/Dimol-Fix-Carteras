create procedure [dbo].[_Trae_Tipo_Bancos] as
 select B.ID_TIPO_BANCO, B.NOMBRE 
 from TIPO_BANCO B with (nolock)
 order by B.ID_TIPO_BANCO
