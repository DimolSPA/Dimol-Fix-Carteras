CREATE PROCEDURE [dbo].[_Trae_Cobralex_Gestiones_PoderJudicial] (@pclid int) 
 AS
 BEGIN
	select distinct
		convert(varchar, d.ctc_numero) + '-' + d.ctc_digito RUT_DEUDOR, 
		(select top 1 trc_cobid 
		 from COBRALEX_TRIBUNALES trc 
		 where trc.trc_trbid = r.rol_trbid 
		 and trc.trc_pclid = @pclid) CODIGO_TRIBUNAL, -- cobralex 
		 r.rol_numero ROL_TRIBUNAL,
		 CONVERT(VARCHAR(10),pjh.FECHA_TRAMITE,103) FECHA_GESTION,
		pjh.codigo CODIGO_GESTION, -- cobralex 
		pjh.DESC_TRAMITE COMENTARIO
	from rol r with(nolock)
	join deudores d  with(nolock)
	on r.ROL_CODEMP = d.CTC_CODEMP
	and r.ROL_CTCID = d.CTC_CTCID
	join [10.0.1.11].[PoderJudicial].[dbo].[ROL_PODER_JUDICIAL]  rpj with(nolock)
	on r.ROL_CODEMP = rpj.RPJ_CODEMP
	and r.ROL_ROLID = rpj.RPJ_ROLID 
	join [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_HISTORIAL] pjh  with(nolock)
	on rpj.RPJ_ID_CAUSA = pjh.ID_CAUSA 
	where r.ROL_PCLID  = @pclid
	and CAST(pjh.FECHA_HISTORIAL AS DATE)= CAST(GETDATE()-1 AS DATE)
 END