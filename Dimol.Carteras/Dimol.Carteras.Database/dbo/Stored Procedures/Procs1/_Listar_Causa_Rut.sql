CREATE Procedure [dbo].[_Listar_Causa_Rut](@rut varchar(20))

 as   

select R.TIPO,
R.NUMERO,
R.ANIO,
R.FECHA_INGRESO FechaIngreso, 
T.TRIBUNAL, 
L2.NOMBRE DEMANDANTE,
L.NOMBRE DEMANDADO,
R.RUTA_DEMANDA RutaDemanda,
isnull((SELECT top 1 url
  FROM [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_ROL_HTML] h
  where h.id_causa = r.id_causa ),
'http://civil.poderjudicial.cl/CIVILPORWEB/ConsultaDetalleAtPublicoAccion.do?TIP_Consulta=1&TIP_Cuaderno=1&CRR_IdCuaderno='+
convert(varchar,(SELECT TOP 1 ID_CUADERNO FROM [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_CUADERNO WHERE ID_CAUSA = R.ID_CAUSA))+'&ROL_Causa=
'+convert(varchar,R.NUMERO)+'&TIP_Causa='+convert(varchar,R.TIPO)+'&ERA_Causa='+
convert(varchar,R.ANIO)+'&CRR_IdCausa='+convert(varchar,R.ID_CAUSA)+'&COD_Tribunal='+
convert(varchar,R.TRIBUNAL)+'&TIP_Informe=1&') Url
from [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_LITIGANTE L with(nolock)
INNER JOIN [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_ROL R with(nolock)
ON L.ID_CAUSA = R.ID_CAUSA
INNER JOIN [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_TRIBUNAL T with(nolock)
ON R.TRIBUNAL = T.ID_TRIBUNAL
INNER JOIN [10.0.1.11].[PoderJudicial].[dbo].PODER_JUDICIAL_LITIGANTE L2 with(nolock)
ON L.ID_CAUSA = L2.ID_CAUSA
where L.PARTICIPANTE = 'DDO.'
and L.RUT = @rut
AND L2.PARTICIPANTE = 'DTE.'
order by 4


