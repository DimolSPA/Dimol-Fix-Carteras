create Procedure [dbo].[Trae_Cuota_Pago_CpbtDoc](@pcl_rut varchar(10), @ctc_rut varchar(10), @ccb_numero varchar(10)) as  

if @pcl_rut = '700109208'
begin
select ccb_numero,ccb_saldo, mon_nombre, ccb_tipcambio  from cartera_clientes_documentos_cpbt_doc c, deudores d
where pcl_rut = @pcl_rut
and c.ccb_ctcid = d.ctc_ctcid
and d.ctc_rut = @ctc_rut
and c.ccb_numero like '%'+@ccb_numero+'%'
and c.ccb_estcpbt = 'V'          
  end

