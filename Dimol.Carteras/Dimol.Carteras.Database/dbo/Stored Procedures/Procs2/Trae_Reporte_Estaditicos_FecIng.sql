

Create Procedure Trae_Reporte_Estaditicos_FecIng(@ccb_codemp integer, @ccb_pclid integer, @desde datetime, @hasta datetime, 
												 @ccb_tipcart smallint, @idi_idid integer, @monPes integer, @monUF integer,
												 @CambUF decimal(10,2), @monDol integer, @CambDol decimal(10,2),
												 @TipCP integer, @TipCJ integer, @TipDV integer) as	 	
SELECT cartera_clientes_documentos_cpbt_doc.pcl_rut,   
         cartera_clientes_documentos_cpbt_doc.pcl_nombre,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecing,   
         cartera_clientes_documentos_cpbt_doc.ccb_fecvenc,   
         cartera_clientes_documentos_cpbt_doc.ccb_asignado,   
         cartera_clientes_documentos_cpbt_doc.ccb_saldo,   
         cartera_clientes_documentos_cpbt_doc.ctc_rut,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant,   
         cartera_clientes_documentos_cpbt_doc.ccb_codmon,   
         cartera_clientes_documentos_cpbt_doc.mon_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_estcpbt,   
         cartera_clientes_documentos_cpbt_doc.tci_tpcid,  
         datepart(year,  ccb_fecing) as AnioIng, 
         datepart(month,  ccb_fecing) as MesIng
         into #DocOri 
    FROM cartera_clientes_documentos_cpbt_doc,   
         idiomas  
   WHERE ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid ) and  
         ( ( cartera_clientes_documentos_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_fecing >= @desde ) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_fecing <= @hasta) AND  
         ( cartera_clientes_documentos_cpbt_doc.ccb_estcpbt <> 'X' and ccb_tipcart = @ccb_tipcart  ) AND  
         ( idiomas.idi_idid = @idi_idid )   
         )    


select AnioIng, MesIng, sum(ccb_asignado * 1) as Asignado
into #Asignado
from #DocOri
where ccb_codmon = @monPes
group by AnioIng, MesIng

insert into #Asignado
select AnioIng, MesIng, sum(ccb_asignado * @CambUF) as Asignado
from #DocOri
where ccb_codmon = @monUF
group by AnioIng, MesIng

insert into #Asignado
select AnioIng, MesIng, sum(ccb_asignado * @CambDol) as Asignado
from #DocOri
where ccb_codmon = @monDol
group by AnioIng, MesIng


SELECT view_aplicaciones_cpbt_cartera_clientes.pcl_rut,   
         view_aplicaciones_cpbt_cartera_clientes.pcl_nomfant,   
         view_aplicaciones_cpbt_cartera_clientes.apl_fecapl,   
         view_aplicaciones_cpbt_cartera_clientes.apl_accion,   
         view_aplicaciones_cpbt_cartera_clientes.tci_nombre,   
         view_aplicaciones_cpbt_cartera_clientes.tci_nombrep,   
         view_aplicaciones_cpbt_cartera_clientes.ccb_numero,   
         view_aplicaciones_cpbt_cartera_clientes.ccb_fecing,   
         view_aplicaciones_cpbt_cartera_clientes.ccb_fecvenc,   
         view_aplicaciones_cpbt_cartera_clientes.mon_nombre,   
         view_aplicaciones_cpbt_cartera_clientes.cbc_tipcambio,   
         view_aplicaciones_cpbt_cartera_clientes.api_capital,   
         view_aplicaciones_cpbt_cartera_clientes.api_interes,   
         view_aplicaciones_cpbt_cartera_clientes.api_honorario,   
         view_aplicaciones_cpbt_cartera_clientes.api_gastpre,   
         view_aplicaciones_cpbt_cartera_clientes.api_gastjud,
         datepart(year,  ccb_fecing) as AnioIng, 
         datepart(month,  ccb_fecing) as MesIng,
         tpc_tpcid
         into #AplCpbt
    FROM view_aplicaciones_cpbt_cartera_clientes,   
         view_tipos_cpbtdoc_clasificacion,   
         tipos_cpbtdoc_idiomas  
   WHERE ( view_aplicaciones_cpbt_cartera_clientes.apl_codemp = tipos_cpbtdoc_idiomas.tci_codemp ) and  
         ( view_aplicaciones_cpbt_cartera_clientes.tci_nombre = tipos_cpbtdoc_idiomas.tci_nombre ) and  
         ( tipos_cpbtdoc_idiomas.tci_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( tipos_cpbtdoc_idiomas.tci_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( ( view_aplicaciones_cpbt_cartera_clientes.apl_codemp = @ccb_codemp ) AND  
         ( view_aplicaciones_cpbt_cartera_clientes.cbc_pclid = @ccb_pclid ) AND  
         ( view_aplicaciones_cpbt_cartera_clientes.apl_fecapl >= @desde ) AND  
         ( view_aplicaciones_cpbt_cartera_clientes.apl_fecapl <= @hasta ) AND  
         ( view_aplicaciones_cpbt_cartera_clientes.ccb_tipcart = @ccb_tipcart ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_findeuda = 'S' and tci_idid = 1)   
         )    


select AnioIng, MesIng, sum(((api_capital * apl_accion) * -1)  * cbc_tipcambio) as Recuperado
into #RecCP
from #AplCpbt
where tpc_tpcid = @TipCP
group by AnioIng, MesIng

select AnioIng, MesIng, sum(((api_capital * apl_accion) * -1)  * cbc_tipcambio) as Recuperado
into #RecCJ
from #AplCpbt
where tpc_tpcid = @TipCJ
group by AnioIng, MesIng

select AnioIng, MesIng, sum(((api_capital * apl_accion) * -1)  * cbc_tipcambio) as Recuperado
into #RecDev
from #AplCpbt
where tpc_tpcid = @TipDV
group by AnioIng, MesIng


  SELECT view_aplicaciones_doc_cartera_clientes.pcl_rut,   
         view_aplicaciones_doc_cartera_clientes.pcl_nomfant,   
         view_aplicaciones_doc_cartera_clientes.apl_fecapl,   
         view_aplicaciones_doc_cartera_clientes.apl_accion,   
         view_aplicaciones_doc_cartera_clientes.tci_nombre,   
         view_aplicaciones_doc_cartera_clientes.tci_nombrep,   
         view_aplicaciones_doc_cartera_clientes.ccb_numero,   
         view_aplicaciones_doc_cartera_clientes.ccb_fecing,   
         view_aplicaciones_doc_cartera_clientes.ccb_fecvenc,   
         view_aplicaciones_doc_cartera_clientes.mon_nombre,   
         view_aplicaciones_doc_cartera_clientes.ddi_tipcambio,   
         view_aplicaciones_doc_cartera_clientes.api_capital,   
         view_aplicaciones_doc_cartera_clientes.api_interes,   
         view_aplicaciones_doc_cartera_clientes.api_honorario,   
         view_aplicaciones_doc_cartera_clientes.api_gastpre,   
         view_aplicaciones_doc_cartera_clientes.api_gastjud,   
         tipos_cpbtdoc_idiomas.tci_tpcid,   
         view_tipos_cpbtdoc_clasificacion.clb_cambiodoc,   
         view_tipos_cpbtdoc_clasificacion.clb_aplica,
         datepart(year,  ccb_fecing) as AnioIng,
         datepart(month,  ccb_fecing) as MesIng
         into #AplDoc  
    FROM view_tipos_cpbtdoc_clasificacion,   
         tipos_cpbtdoc_idiomas,   
         view_aplicaciones_doc_cartera_clientes  
   WHERE ( tipos_cpbtdoc_idiomas.tci_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( tipos_cpbtdoc_idiomas.tci_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_aplicaciones_doc_cartera_clientes.apl_codemp = tipos_cpbtdoc_idiomas.tci_codemp ) and  
         ( view_aplicaciones_doc_cartera_clientes.tci_nombre = tipos_cpbtdoc_idiomas.tci_nombre ) and  
         ( ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = @ccb_codemp ) AND  
         ( view_aplicaciones_doc_cartera_clientes.ccb_pclid = @ccb_pclid ) AND  
         ( view_aplicaciones_doc_cartera_clientes.apl_fecapl >= @desde ) AND  
         ( view_aplicaciones_doc_cartera_clientes.apl_fecapl <= @hasta ) AND  
         ( view_aplicaciones_doc_cartera_clientes.ccb_tipcart = @ccb_tipcart and clb_remesa ='S' ) )    


  SELECT view_aplicaciones_doc_cartera_clientes.pcl_rut,   
         view_aplicaciones_doc_cartera_clientes.pcl_nomfant,   
         view_aplicaciones_doc_cartera_clientes.apl_fecapl,   
         view_aplicaciones_doc_cartera_clientes.apl_accion,   
         view_aplicaciones_doc_cartera_clientes.tci_nombre,   
         view_aplicaciones_doc_cartera_clientes.tci_nombrep,   
         view_aplicaciones_doc_cartera_clientes.ccb_numero,   
         view_aplicaciones_doc_cartera_clientes.ccb_fecing,   
         view_aplicaciones_doc_cartera_clientes.ccb_fecvenc,   
         view_aplicaciones_doc_cartera_clientes.mon_nombre,   
         view_aplicaciones_doc_cartera_clientes.ddi_tipcambio,   
         view_aplicaciones_doc_cartera_clientes.api_capital,   
         view_aplicaciones_doc_cartera_clientes.api_interes,   
         view_aplicaciones_doc_cartera_clientes.api_honorario,   
         view_aplicaciones_doc_cartera_clientes.api_gastpre,   
         view_aplicaciones_doc_cartera_clientes.api_gastjud,   
         tipos_cpbtdoc_idiomas.tci_tpcid,   
         view_tipos_cpbtdoc_clasificacion.clb_cambiodoc,   
         view_tipos_cpbtdoc_clasificacion.clb_aplica,
         datepart(year,  ccb_fecing) as AnioIng,
         datepart(month,  ccb_fecing) as MesIng
         into #AplDoc2  
    FROM view_tipos_cpbtdoc_clasificacion,   
         tipos_cpbtdoc_idiomas,   
         view_aplicaciones_doc_cartera_clientes  
   WHERE ( tipos_cpbtdoc_idiomas.tci_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( tipos_cpbtdoc_idiomas.tci_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_aplicaciones_doc_cartera_clientes.apl_codemp = tipos_cpbtdoc_idiomas.tci_codemp ) and  
         ( view_aplicaciones_doc_cartera_clientes.tci_nombre = tipos_cpbtdoc_idiomas.tci_nombre ) and  
         ( ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = @ccb_codemp ) AND  
         ( view_aplicaciones_doc_cartera_clientes.ccb_pclid = @ccb_pclid ) AND  
         ( view_aplicaciones_doc_cartera_clientes.apl_fecapl >= @desde ) AND  
         ( view_aplicaciones_doc_cartera_clientes.apl_fecapl <= @hasta ) AND  
         ( view_aplicaciones_doc_cartera_clientes.ccb_tipcart = @ccb_tipcart and clb_remesa ='N' ) )    



select AnioIng, MesIng, sum(((api_capital * apl_accion) * -1)  * ddi_tipcambio) as Recuperado
into #RecDoc
from #AplDoc
where clb_cambiodoc = 'N'
group by AnioIng, MesIng


select AnioIng, MesIng, sum(((api_capital * apl_accion) * -1)  * ddi_tipcambio) as Recuperado
into #RecCam
from #AplDoc
where clb_cambiodoc = 'S'
group by AnioIng, MesIng


select AnioIng, MesIng, sum(((api_capital * apl_accion) * -1)  * ddi_tipcambio) as Recuperado
into #RecDoc2
from #AplDoc2
where clb_cambiodoc = 'N'
group by AnioIng, MesIng


select AnioIng, MesIng, sum(((api_capital * apl_accion) * -1)  * ddi_tipcambio) as Recuperado
into #RecCam2
from #AplDoc2
where clb_cambiodoc = 'S'
group by AnioIng, MesIng



select AnioIng, MesIng, Asignado, 0 as Devoluciones, 0 as CastPre, 0 as CastJud, 0 as Pagos, 0 as CamDeu, 0 as PagCDoc   
into #Resumen
from #Asignado


insert into #Resumen
select AnioIng, MesIng, 0,0, Recuperado, 0,0,0,0
from #RecCP

insert into #Resumen
select AnioIng, MesIng, 0,0, 0, Recuperado,0,0,0
from #RecCJ

insert into #Resumen
select AnioIng, MesIng, 0,Recuperado, 0, 0,0,0,0
from #RecDev

insert into #Resumen
select AnioIng, MesIng, 0,0, 0, 0,Recuperado,0,0
from #RecDoc


insert into #Resumen
select AnioIng, MesIng, 0,0, 0, 0,0,Recuperado,0
from #RecCam


insert into #Resumen
select AnioIng, MesIng, 0,0, 0, 0,0,0,Recuperado
from #RecDoc2


insert into #Resumen
select AnioIng, MesIng, 0,0, 0, 0,0,0,Recuperado
from #RecCam2



select AnioIng, MesIng, sum(asignado) as Asignado,
       sum(Devoluciones) as Devoluciones,  	
	   sum(CastPre) as CastPre,
	   sum(CastJud) as CastJud,
	   sum(Pagos) as Pagos,
	   sum(CamDeu) as CamDeu,
       sum(PagCDoc) as PagCDoc
from #Resumen
group by AnioIng, MesIng
order by AnioIng, MesIng
