CREATE procedure [dbo].[_Trae_Informe_Bajas] (@pclid int) as

select convert(varchar,B.FECHA_RECLAMO, 105) as FECHA_RECLAMO, 
	'P' + d.CTC_RUT as RUT, d.CTC_NOMFANT as EMPRESA, 
	b.NUMERO as FACTURA, b.SALDO as MONTO, u.USR_NOMBRE as GESTOR,
	convert(varchar,b.FECHA_PAGO, 105) as FECHA_PAGO,
	tb.NOMBRE as BANCO, ec.CUENTA, b.OBSERVACIONES as COMENTARIO, ISNULL(b.COMENTARIO,'') as HISTORIAL  
	from BAJAS_CPBT_DOC b, (
		select b.pclid, b.CTCID, b.ccbid, max(b.FECHA) as Fecha  
		from BAJAS_CPBT_DOC b 
		group by b.pclid, b.CTCID, b.ccbid  
		)  bajas, CARTERA_CLIENTES_CPBT_DOC c, DEUDORES d, USUARIOS u, TIPO_BANCO tb,
		EJECUTIVO_CUENTA_MUTUAL ec 
	where b.pclid = bajas.pclid and b.CTCID = bajas.CTCID and b.CCBID = bajas.CCBID 
	and b.FECHA = bajas.Fecha and b.pclid = c.CCB_PCLID and b.CTCID = c.CCB_CTCID 
	and b.CCBID = c.CCB_CCBID 
	and c.CCB_CTCID = d.CTC_CTCID 
	and u.USR_USRID = b.USRID 
	and b.TIPO_BANCO = tb.ID_TIPO_BANCO
	and b.ID_CUENTA = ec.ID_CUENTA_EJECUTIVO
	and c.CCB_ESTCPBT = 'V'
	and c.CCB_PCLID = @pclid
