CREATE procedure [dbo].[SII_Listar_Actividades_Economicas] (@ctcid int) as 
	select ca.ACTIVIDAD, 
	ca.CODIGO_ACTIVIDAD, 
	--c.CATEGORIA, 
	case ca.CATEGORIA_TRIBUTARIA 	
	WHEN 1 THEN 'Primera'
	WHEN 2 THEN 'Segunda'
	ELSE 'ND' END AS CATEGORIA,
	ca.AFECTO_IVA 
	from sii..ACTIVIDAD_ECONOMICA_RUT a with (nolock),
	sii..CATEGORIA_ACTIVIDAD_ECONOMICA ca with (nolock)--,
	--sii..CATEGORIA c with (nolock) 
	where a.CODIGO_ACTIVIDAD = ca.CODIGO_ACTIVIDAD 
	--and ca.CATEGORIA_TRIBUTARIA = c.CODIGO_CATEGORIA
	and a.CTCID = @ctcid
