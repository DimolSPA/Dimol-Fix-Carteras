-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 13-04-2014
-- Description:	Trae supervisor
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_Cpbt_Campana](
	@codemp as integer,
	@estid as varchar(100)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	declare @Query varchar(7000)
	set @Query='SELECT cartera_clientes_cpbt_doc.ccb_pclid,  
	cartera_clientes_cpbt_doc.ccb_ctcid, ccb_ccbid, ccb_estcpbt, ccb_fecplazo, ccb_monto, ccb_saldo, ccb_estid
	FROM cartera_clientes_cpbt_doc
	WHERE  cartera_clientes_cpbt_doc.ccb_codemp = ' + CONVERT(VARCHAR,@codemp)   +
	'and cartera_clientes_cpbt_doc.ccb_estid in ('+ CONVERT(VARCHAR,@estid)  + ')
	and ccb_fecplazo <= getdate() and cartera_clientes_cpbt_doc.ccb_estcpbt in (''V'',''J'') 
	union
	SELECT distinct cartera_clientes_cpbt_doc.ccb_pclid,
	cartera_clientes_cpbt_doc.ccb_ctcid, ccb_ccbid, ccb_estcpbt, ccb_fecplazo, ccb_monto, ccb_saldo, ccb_estid
	FROM cartera_clientes_cpbt_doc,
	negociacion_cpbtdoc,
	negociacion, 
	negociacion_pagos
	WHERE  negociacion_cpbtdoc.ngd_codemp = cartera_clientes_cpbt_doc.ccb_codemp  
	and negociacion_cpbtdoc.ngd_pclid = cartera_clientes_cpbt_doc.ccb_pclid  
	and negociacion_cpbtdoc.ngd_ctcid = cartera_clientes_cpbt_doc.ccb_ctcid  
	and negociacion_cpbtdoc.ngd_ccbid = cartera_clientes_cpbt_doc.ccb_ccbid  
	and cartera_clientes_cpbt_doc.ccb_estcpbt in (''V'',''J'') 
	and  negociacion.neg_codemp = negociacion_cpbtdoc.ngd_codemp  
	and negociacion.neg_anio = negociacion_cpbtdoc.ngd_anio  
	and   negociacion.neg_negid = negociacion_cpbtdoc.ngd_negid  
	and   negociacion_pagos.ngp_codemp = negociacion.neg_codemp  
	and  negociacion_pagos.ngp_anio = negociacion.neg_anio  
	and   negociacion_pagos.ngp_negid = negociacion.neg_negid  
	and   cartera_clientes_cpbt_doc.ccb_codemp = ' + CONVERT(VARCHAR,@codemp)   +
	'and negociacion.neg_estado = ''A''
	and negociacion_pagos.ngp_fechas <= getdate()
	and convert(varchar,ngp_anio) + ''_'' + convert(varchar,ngp_negid) + ''_'' + CONVERT (char(10), ngp_fechas, 112)  in (SELECT convert(varchar, ddi_anioneg) + ''_'' + convert(varchar, ddi_negid) + ''_'' + CONVERT (char(10), ddi_fecvenc, 112)  
		FROM documentos_diarios,    estados_documentos_diarios
		WHERE estados_documentos_diarios.edc_codemp = documentos_diarios.ddi_codemp  
		and estados_documentos_diarios.edc_edcid = documentos_diarios.ddi_edcid  
		and documentos_diarios.ddi_codemp = ' + CONVERT(VARCHAR,@codemp)   +
		' and documentos_diarios.ddi_negid is not null  
		AND estados_documentos_diarios.edc_estado <> 4)'
	
		exec(@query)
END
