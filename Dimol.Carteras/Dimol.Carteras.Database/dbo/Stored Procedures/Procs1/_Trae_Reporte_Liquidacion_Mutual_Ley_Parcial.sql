﻿CREATE Procedure [dbo].[_Trae_Reporte_Liquidacion_Mutual_Ley_Parcial](@ccb_codemp integer, @ccb_pclid integer, @ccb_ctcid integer, @ccb_tipcart integer, @ccb_estcpbt char(1), @idioma integer, @gsc_sucid integer, @ccb_ccbid varchar(8000)) as    

declare @query varchar(8000);

set @query = 'SELECT ccb_pclid,              
ccb_ctcid,              
pcl_rut,              
pcl_nomfant,              
ctc_rut,              
ctc_nomfant,              
tci_nombre,              
ccb_numero,              
ccb_fecdoc,              
ccb_fecvenc,              
eci_nombre,              
mon_nombre,              
ccb_tipcambio,              
ccb_monto,              
ccb_saldo,              
ccb_gastjud,              
ccb_gastotro,              
ccb_intereses,              
ccb_honorarios,              
bco_nombre,              
ccb_numesp,              
isnull(CCB_IDCUENTA, '''') sbc_rut,              
isnull(CCB_DESCCUENTA, '''') sbc_nombre,              
pcc_codigo,              
pcc_nombre,              
mci_nombre,              
pais.pai_nombre,              
region.reg_nombre,              
ciudad.ciu_nombre,              
comuna.com_nombre,              
comuna.com_codpost,              
ctc_direccion,              
gestor.ges_nombre,           
ccb_codmon,
ccb_rutgir,
ccb_nomgir,
ccb_numagrupa
FROM cartera_clientes_documentos_cpbt_doc,              
comuna,              
ciudad,              
region,              
pais,              
gestor,              
gestor_cartera       
WHERE ( ciudad.ciu_ciuid = comuna.com_ciuid ) and             
( region.reg_regid = ciudad.ciu_regid ) and             
( pais.pai_paiid = region.reg_paiid ) and             
( ctc_comid = comuna.com_comid ) and             
( gestor_cartera.gsc_codemp = gestor.ges_codemp ) and             
( gestor_cartera.gsc_sucid = gestor.ges_sucid ) and             
( gestor_cartera.gsc_gesid = gestor.ges_gesid ) and             
( ccb_codemp = gestor_cartera.gsc_codemp ) and             
( ccb_ctcid = gestor_cartera.gsc_ctcid ) and             
( ccb_pclid = gestor_cartera.gsc_pclid ) and             
( ( ccb_codemp = ' + CONVERT(VARCHAR,@ccb_codemp) + ' ) AND             
( ccb_pclid = ' + CONVERT(VARCHAR,@ccb_pclid) + ' ) AND             
( ccb_ctcid = ' + CONVERT(VARCHAR,@ccb_ctcid) + ' ) AND             
( ccb_tipcart = ' + CONVERT(VARCHAR,@ccb_tipcart) + ' ) AND             
( ccb_estcpbt = ''' + CONVERT(VARCHAR,@ccb_estcpbt) + ''' ) AND             
( ccb_fecvenc <= getdate() ) AND             
( tci_idid = ' + CONVERT(VARCHAR,@idioma) + ' ) AND             
( eci_idid = ' + CONVERT(VARCHAR,@idioma) + ' ) AND             
( mci_idid = ' + CONVERT(VARCHAR,@idioma) + ' ) AND  
( ccb_ccbid in (' + CONVERT(VARCHAR(8000),@ccb_ccbid) + ') ) AND            
( gestor_cartera.gsc_sucid = ' + CONVERT(VARCHAR,@gsc_sucid) + ' and eci_estid > 1 )  )       
order by ccb_fecvenc asc'

exec(@query) 