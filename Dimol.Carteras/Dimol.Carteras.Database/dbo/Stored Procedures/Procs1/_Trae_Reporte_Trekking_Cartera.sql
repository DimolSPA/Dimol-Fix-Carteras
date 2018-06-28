
create procedure [dbo].[_Trae_Reporte_Trekking_Cartera] (@codemp integer, @sucid integer, @tipocartera integer, @gestor integer, @pclid integer, @vencidos integer) as 

SET NOCOUNT ON;

declare @query varchar(8000) = '';


set @query = 'SELECT ges_gesid, '
set @query = @query + 'ges_nombre, '
set @query = @query + 'ccb_pclid, '
set @query = @query + 'pcl_rut, '
set @query = @query + 'pcl_nomfant, '
set @query = @query + 'ctc_ctcid, '
set @query = @query + 'CTC_RUT, '
set @query = @query + 'CTC_NOMBRE, ' 
set @query = @query + 'CCB_CODMON, '
set @query = @query + 'ccb_saldo, '

set @query = @query + '(select isnull(datediff(dd, max(CEA_FECHA), getdate()), 1000) from CARTERA_CLIENTES_ESTADOS_ACCIONES ca where ca.CEA_GESID = GES_GESID and '
set @query = @query + 'ca.CEA_CODEMP = CCB_CODEMP and '
set @query = @query + 'ca.CEA_PCLID = CCB_PCLID and '
set @query = @query + 'ca.CEA_CTCID = CCB_CTCID) as NumDias '

set @query = @query + 'from cartera_clientes_cpbt_doc, gestor_cartera, gestor, PROVCLI, deudores '
set @query = @query + 'where '
set @query = @query + 'ccb_estcpbt = ''V'' and '
set @query = @query + 'gestor_cartera.gsc_codemp = gestor.ges_codemp and '
set @query = @query + 'gestor_cartera.gsc_sucid = gestor.ges_sucid  and '
set @query = @query + 'gestor_cartera.gsc_gesid = gestor.ges_gesid and '
set @query = @query + 'cartera_clientes_cpbt_doc.ccb_codemp = gestor_cartera.gsc_codemp  and '
set @query = @query + 'cartera_clientes_cpbt_doc.ccb_ctcid = gestor_cartera.gsc_ctcid and '
set @query = @query + 'cartera_clientes_cpbt_doc.ccb_pclid = gestor_cartera.gsc_pclid and '
set @query = @query + 'cartera_clientes_cpbt_doc.CCB_PCLID = PROVCLI.PCL_PCLID and '
set @query = @query + 'cartera_clientes_cpbt_doc.CCB_CODEMP = PROVCLI.PCL_CODEMP and '
set @query = @query + 'cartera_clientes_cpbt_doc.CCB_CTCID = deudores.CTC_CTCID and '
set @query = @query + 'cartera_clientes_cpbt_doc.CCB_CODEMP = deudores.CTC_CODEMP and '
set @query = @query + 'GES_ESTADO = ''A'' and '
set @query = @query + 'ccb_codemp = ' + convert(varchar, @codemp) + ' and '
set @query = @query + 'GES_SUCID = ' + convert(varchar, @sucid) + ' and ' 

if @gestor <> 0 
	begin
		set @query = @query + 'gestor.GES_GESID = ' + convert(varchar, @gestor) + ' and '
	end

if @pclid <> 0 
	begin
		set @query = @query + 'cartera_clientes_cpbt_doc.CCB_PCLID = ' + convert(varchar, @pclid) + ' and '
	end

if @vencidos <> 0
	begin
		set @query = @query + 'datediff(dd, cartera_clientes_cpbt_doc.CCB_FECVENC, getdate()) >= 0 and '
	end
	
set @query = @query + 'cartera_clientes_cpbt_doc.CCB_TIPCART = ' + convert(varchar, @tipocartera) 

exec (@query)

