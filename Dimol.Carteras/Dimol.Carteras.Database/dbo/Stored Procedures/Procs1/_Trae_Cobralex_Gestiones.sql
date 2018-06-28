 CREATE PROCEDURE [dbo].[_Trae_Cobralex_Gestiones] (@pclid int) 
 AS
 BEGIN
	 select 
	 convert(varchar, d.ctc_numero) + '-' + d.ctc_digito RUT_DEUDOR, 
	 (select top 1 trc_cobid 
	 from COBRALEX_TRIBUNALES trc 
	 where trc.trc_trbid = r.rol_trbid 
	 and trc.trc_pclid = @pclid) CODIGO_TRIBUNAL, -- cobralex 
	 r.rol_numero ROL_TRIBUNAL, 
	 CONVERT(VARCHAR(10),re.rle_fecjud,103) FECHA_GESTION, 
	 (select top 1 gst_cobid 
	 from COBRALEX_GESTIONES gst 
	 where gst.gst_estid = r.rol_estid
	 and gst.gst_pclid = @pclid) CODIGO_GESTION, -- cobralex 
	 re.rle_comentario COMENTARIO 
	 from rol r, deudores d, 
	 --estados_cartera_idiomas e, 
	 rol_estados re 
	 where d.ctc_codemp = r.rol_codemp 
	 and d.ctc_ctcid = r.rol_ctcid 
 
	 --and r.rol_estid = e.eci_estid 
	 --and r.rol_codemp = e.eci_codemp
	 and re.rle_rolid = r.rol_rolid 
	 and re.rle_codemp = r.rol_codemp 

	 and r.ROL_PCLID = @pclid
	 and CAST(re.RLE_FECHA AS DATE) = CAST(GETDATE()-1 AS DATE)
END