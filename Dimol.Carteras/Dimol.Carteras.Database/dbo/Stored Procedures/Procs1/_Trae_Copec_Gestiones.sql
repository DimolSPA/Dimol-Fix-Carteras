

CREATE procedure [dbo].[_Trae_Copec_Gestiones] (@pclid int) as


DECLARE @intervalo int = -1

if datename(dw,getdate()) = 'Monday' begin set @intervalo = -3 end

  select 
  ISNULL(d.ctc_rut, ' ') Rut_Cliente, 
  ISNULL(r.rol_numero, ' ') ROL,  
  ISNULL(trc.trc_cobid, ' ') TRIBUNAL, -- cobralex 
  ISNULL(cg.CGS_CGSID, ' ') Gestion_Realizada, 
  ISNULL(CONVERT(VARCHAR(10),re.rle_fecjud,103), ' ') Fecha_Gestion , 
  --re.rle_comentario COMENTARIO  
  ISNULL(( select TOP 1 CCB_IDCUENTA 
	  FROM CARTERA_CLIENTES_CPBT_DOC ccd, ROL_DOCUMENTOS rd
	  where r.ROL_CODEMP = rd.RDC_CODEMP 
	  and r.ROL_ROLID = rd.RDC_ROLID 
	  and r.ROL_PCLID = rd.RDC_PCLID 
	  and r.ROL_CTCID = rd.RDC_CTCID 
	  and ccd.CCB_CODEMP = rd.RDC_CODEMP
	  and ccd.CCB_PCLID = rd.RDC_PCLID 
	  and ccd.CCB_CTCID = rd.RDC_CTCID 
	  and ccd.CCB_CCBID = rd.RDC_CCBID), ' ') Rut_Gestion 
  from rol r with(nolock), 
  deudores d with(nolock), 
  rol_estados re with(nolock), 
  COPEC_GESTIONES cg with(nolock), 
  COBRALEX_TRIBUNALES trc with(nolock) 
  where d.ctc_codemp = r.rol_codemp 
  and d.ctc_ctcid = r.rol_ctcid 
 
  and re.rle_rolid = r.rol_rolid 
  and re.rle_codemp = r.rol_codemp 

  and r.ROL_CODEMP = cg.CGS_CODEMP 
  and R.ROL_ESTID = cg.CGS_ESTID 

  and r.rol_trbid = trc.TRC_TRBID 
  and r.ROL_CODEMP = trc.TRC_CODEMP 
  and r.ROL_PCLID = trc.TRC_PCLID 

  and r.ROL_PCLID = @pclid 
  and CAST(re.RLE_FECHA AS DATE) = CAST(GETDATE()+@intervalo AS DATE) 

  union 

  select 
  ISNULL(d.ctc_rut, ' ') Rut_Cliente, 
  ISNULL(r.rol_numero, ' ') ROL, 
  ISNULL(trc.trc_cobid, ' ') TRIBUNAL, -- cobralex 
  ISNULL(cg.CGS_CGSID, ' ') Gestion_Realizada, 
  ISNULL(CONVERT(VARCHAR(10),pjh.FECHA_TRAMITE,103), ' ') Fecha_Gestion , 
  --re.rle_comentario COMENTARIO  
  ISNULL(( select TOP 1 CCB_IDCUENTA 
	  FROM CARTERA_CLIENTES_CPBT_DOC ccd, ROL_DOCUMENTOS rd 
	  where r.ROL_CODEMP = rd.RDC_CODEMP 
	  and r.ROL_ROLID = rd.RDC_ROLID 
	  and r.ROL_PCLID = rd.RDC_PCLID 
	  and r.ROL_CTCID = rd.RDC_CTCID 
	  and ccd.CCB_CODEMP = rd.RDC_CODEMP 
	  and ccd.CCB_PCLID = rd.RDC_PCLID 
	  and ccd.CCB_CTCID = rd.RDC_CTCID 
	  and ccd.CCB_CCBID = rd.RDC_CCBID), ' ') Rut_Gestion 
  from rol r with(nolock), 
  deudores d with(nolock), 
  [10.0.1.11].[PoderJudicial].[dbo].[ROL_PODER_JUDICIAL]  rpj with(nolock), 
  [10.0.1.11].[PoderJudicial].[dbo].[PODER_JUDICIAL_HISTORIAL] pjh with(nolock), 
  --[PoderJudicial].[dbo].[ROL_PODER_JUDICIAL]  rpj with(nolock), 
  --[PoderJudicial].[dbo].[PODER_JUDICIAL_HISTORIAL] pjh with(nolock), 
  COPEC_GESTIONES cg with(nolock), 
  COBRALEX_TRIBUNALES trc with(nolock) 
  where d.ctc_codemp = r.rol_codemp 
  and d.ctc_ctcid = r.rol_ctcid 

  and r.ROL_CODEMP = cg.CGS_CODEMP 
  and R.ROL_ESTID = cg.CGS_ESTID 

  and r.rol_trbid = trc.TRC_TRBID 
  and r.ROL_CODEMP = trc.TRC_CODEMP 
  and r.ROL_PCLID = trc.TRC_PCLID 

  and r.ROL_CODEMP = rpj.RPJ_CODEMP 
  and r.ROL_ROLID = rpj.RPJ_ROLID 
  
  and rpj.RPJ_ID_CAUSA = pjh.ID_CAUSA 

  and r.ROL_PCLID = @pclid 
  and CAST(pjh.FECHA_HISTORIAL AS DATE)= CAST(GETDATE()+@intervalo AS DATE) 

