CREATE procedure [dbo].[_Trae_Numero_Causas_Rut](@rut int) as

select causas from [10.0.1.11].PoderJudicial.dbo.CAUSAS_POR_CTCID with(nolock) where CTCID = @rut
--select count(IDCAUSA) as causas from (
				
--				select distinct R.IDCAUSA, R.TIPO,
--R.NUMERO,
--R.ANIO,
--R.FECHA_INGRESO FechaIngreso, 
--T.TRIBUNAL, 
--L2.NOMBRE DEMANDANTE,
--L.NOMBRE DEMANDADO
--from [10.0.1.11].PoderJudicial.dbo.PODER_JUDICIAL_LITIGANTE L with(nolock)
--INNER JOIN  [10.0.1.11].PoderJudicial.dbo.PODER_JUDICIAL_ROL R with(nolock)
--ON L.ID_CAUSA = R.ID_CAUSA
--INNER JOIN [10.0.1.11].PoderJudicial.dbo.PODER_JUDICIAL_TRIBUNAL T with(nolock)
--ON R.TRIBUNAL = T.ID_TRIBUNAL
--INNER JOIN [10.0.1.11].PoderJudicial.dbo.PODER_JUDICIAL_LITIGANTE L2 with(nolock)
--ON L.ID_CAUSA = L2.ID_CAUSA
--where L.PARTICIPANTE = 'DDO.'
--and L.RUT = @rut
--AND L2.PARTICIPANTE = 'DTE.'
--) as t
