

Create Procedure Trae_Reporte_CompraVentas(@cbc_codemp integer, @desde datetime, @hasta datetime, @clb_tipcpbtdoc char(1), @idi_idid integer) as
SELECT distinct view_cabecera_comprobantes.pcl_rut,   
         view_cabecera_comprobantes.pcl_nombre,   
         view_cabecera_comprobantes.pcl_apepat,   
         view_cabecera_comprobantes.pcl_apemat,   
         view_cabecera_comprobantes.tci_nombre,   
         view_cabecera_comprobantes.cbc_numprovcli,   
         view_cabecera_comprobantes.cbc_feccpbt,   
         view_cabecera_comprobantes.mon_nombre,   
         asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         asientos_contables.ast_numero,
         ast_tot_debe as TotalDebe,  
         ast_tot_debe as TotalHaber,
         ast_tot_debe as TotalImpuesto,
         ast_tot_debe as TotalExento,
         ast_tot_debe as TotalRetenido,
         ast_tot_debe as TotalImpImp,
         gii_nombre,
         view_cabecera_comprobantes.tpc_codigo,
         view_cabecera_comprobantes.cbc_feccont,
         view_cabecera_comprobantes.cbt_estado
         into #Cpbt
    FROM {oj asientos_contables_detalle LEFT OUTER JOIN impuestos ON asientos_contables_detalle.acd_codemp = impuestos.ipt_codemp AND asientos_contables_detalle.acd_pctid = impuestos.ipt_pctid},   
         asientos_contables,   
         asientos_contables_cpbtdoc_apl,   
         view_cabecera_comprobantes,   
         view_tipos_cpbtdoc_clasificacion,   
         clasificacion_cpbtdoc_contable,   
         provcli,   
         giros_idiomas  
   WHERE ( asientos_contables_detalle.acd_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_detalle.acd_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_detalle.acd_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_detalle.acd_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_cpbtdoc_apl.ada_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_cpbtdoc_apl.ada_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_cpbtdoc_apl.ada_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_cpbtdoc_apl.ada_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_cpbtdoc_apl.ada_codemp = view_cabecera_comprobantes.cbc_codemp ) and  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = view_cabecera_comprobantes.cbc_sucid ) and  
         ( asientos_contables_cpbtdoc_apl.ada_tpcid = view_cabecera_comprobantes.cbc_tpcid ) and  
         ( asientos_contables_cpbtdoc_apl.ada_numcpbt = view_cabecera_comprobantes.cbc_numero ) and  
         ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = clasificacion_cpbtdoc_contable.cct_codemp ) and  
         ( view_tipos_cpbtdoc_clasificacion.clb_clbid = clasificacion_cpbtdoc_contable.cct_clbid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = provcli.pcl_codemp ) and  
         ( view_cabecera_comprobantes.cbc_pclid = provcli.pcl_pclid ) and  
         ( provcli.pcl_codemp = giros_idiomas.gii_codemp ) and  
         ( provcli.pcl_girid = giros_idiomas.gii_girid ) and  
         ( ( asientos_contables.ast_codemp = @cbc_codemp ) AND  
         ( asientos_contables.ast_fecperiodo >= @desde ) AND  
         ( asientos_contables.ast_fecperiodo <= @hasta ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_aniodoc is null ) AND  
         ( clasificacion_cpbtdoc_contable.cct_libcomven = 'S' and clb_libcompra=1) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc = @clb_tipcpbtdoc ) and  view_cabecera_comprobantes.idi_idid = @idi_idid and gii_idid = @idi_idid and asientos_contables.ast_estado in('V','P','N') )    

  SELECT asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         asientos_contables.ast_numero,  
         sum(asientos_contables_detalle.acd_debe * cct_debhab ) as debe,   
         sum(asientos_contables_detalle.acd_haber * cct_debhab) as haber   
         into #Valores
    FROM {oj asientos_contables_detalle LEFT OUTER JOIN impuestos ON asientos_contables_detalle.acd_codemp = impuestos.ipt_codemp AND asientos_contables_detalle.acd_pctid = impuestos.ipt_pctid},   
         asientos_contables,   
         asientos_contables_cpbtdoc_apl,   
         view_cabecera_comprobantes,   
         view_tipos_cpbtdoc_clasificacion,   
         clasificacion_cpbtdoc_contable,   
         provcli,   
         giros_idiomas  
   WHERE ( asientos_contables_detalle.acd_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_detalle.acd_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_detalle.acd_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_detalle.acd_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_cpbtdoc_apl.ada_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_cpbtdoc_apl.ada_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_cpbtdoc_apl.ada_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_cpbtdoc_apl.ada_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_cpbtdoc_apl.ada_codemp = view_cabecera_comprobantes.cbc_codemp ) and  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = view_cabecera_comprobantes.cbc_sucid ) and  
         ( asientos_contables_cpbtdoc_apl.ada_tpcid = view_cabecera_comprobantes.cbc_tpcid ) and  
         ( asientos_contables_cpbtdoc_apl.ada_numcpbt = view_cabecera_comprobantes.cbc_numero ) and  
         ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = clasificacion_cpbtdoc_contable.cct_codemp ) and  
         ( view_tipos_cpbtdoc_clasificacion.clb_clbid = clasificacion_cpbtdoc_contable.cct_clbid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = provcli.pcl_codemp ) and  
         ( view_cabecera_comprobantes.cbc_pclid = provcli.pcl_pclid ) and  
         ( provcli.pcl_codemp = giros_idiomas.gii_codemp ) and  
         ( provcli.pcl_girid = giros_idiomas.gii_girid ) and  
         ( ( asientos_contables.ast_codemp = @cbc_codemp ) AND  
         ( asientos_contables.ast_fecperiodo >= @desde ) AND  
         ( asientos_contables.ast_fecperiodo <= @hasta ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_aniodoc is null ) AND  
         ( clasificacion_cpbtdoc_contable.cct_libcomven = 'S' and clb_libcompra=1 ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc = @clb_tipcpbtdoc ) and  view_cabecera_comprobantes.idi_idid = @idi_idid and gii_idid = @idi_idid and ast_estado in ('P','V','N') )        
group by asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         asientos_contables.ast_numero


  SELECT asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         asientos_contables.ast_numero,  
         sum(asientos_contables_detalle.acd_debe  * cct_debhab) as debe,   
         sum(asientos_contables_detalle.acd_haber  * cct_debhab) as haber   
         into #Impuestos
    FROM {oj asientos_contables_detalle LEFT OUTER JOIN impuestos ON asientos_contables_detalle.acd_codemp = impuestos.ipt_codemp AND asientos_contables_detalle.acd_pctid = impuestos.ipt_pctid},   
         asientos_contables,   
         asientos_contables_cpbtdoc_apl,   
         view_cabecera_comprobantes,   
         view_tipos_cpbtdoc_clasificacion,   
         clasificacion_cpbtdoc_contable,   
         provcli,   
         giros_idiomas  
   WHERE ( asientos_contables_detalle.acd_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_detalle.acd_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_detalle.acd_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_detalle.acd_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_cpbtdoc_apl.ada_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_cpbtdoc_apl.ada_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_cpbtdoc_apl.ada_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_cpbtdoc_apl.ada_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_cpbtdoc_apl.ada_codemp = view_cabecera_comprobantes.cbc_codemp ) and  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = view_cabecera_comprobantes.cbc_sucid ) and  
         ( asientos_contables_cpbtdoc_apl.ada_tpcid = view_cabecera_comprobantes.cbc_tpcid ) and  
         ( asientos_contables_cpbtdoc_apl.ada_numcpbt = view_cabecera_comprobantes.cbc_numero ) and  
         ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = clasificacion_cpbtdoc_contable.cct_codemp ) and  
         ( view_tipos_cpbtdoc_clasificacion.clb_clbid = clasificacion_cpbtdoc_contable.cct_clbid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = provcli.pcl_codemp ) and  
         ( view_cabecera_comprobantes.cbc_pclid = provcli.pcl_pclid ) and  
         ( provcli.pcl_codemp = giros_idiomas.gii_codemp ) and  
         ( provcli.pcl_girid = giros_idiomas.gii_girid ) and  
         ( ( asientos_contables.ast_codemp = @cbc_codemp ) AND  
         ( asientos_contables.ast_fecperiodo >= @desde ) AND  
         ( asientos_contables.ast_fecperiodo <= @hasta ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_aniodoc is null ) AND  
         ( clasificacion_cpbtdoc_contable.cct_libcomven = 'S' and clb_libcompra=1 ) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc = @clb_tipcpbtdoc and ipt_pctid is not null )  and  view_cabecera_comprobantes.idi_idid = @idi_idid and gii_idid = @idi_idid and ast_estado in ('P','V','N') )                  
group by asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         asientos_contables.ast_numero


 SELECT asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         asientos_contables.ast_numero,  
         sum(asientos_contables_detalle.acd_debe  * cct_debhab) as debe,   
         sum(asientos_contables_detalle.acd_haber  * cct_debhab) as haber   
         into #ImpRet
    FROM {oj asientos_contables_detalle LEFT OUTER JOIN impuestos ON asientos_contables_detalle.acd_codemp = impuestos.ipt_codemp AND asientos_contables_detalle.acd_pctid = impuestos.ipt_pctid},   
         asientos_contables,   
         asientos_contables_cpbtdoc_apl,   
         view_cabecera_comprobantes,   
         view_tipos_cpbtdoc_clasificacion,   
         clasificacion_cpbtdoc_contable,   
         provcli,   
         giros_idiomas  
   WHERE ( asientos_contables_detalle.acd_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_detalle.acd_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_detalle.acd_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_detalle.acd_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_cpbtdoc_apl.ada_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_cpbtdoc_apl.ada_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_cpbtdoc_apl.ada_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_cpbtdoc_apl.ada_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_cpbtdoc_apl.ada_codemp = view_cabecera_comprobantes.cbc_codemp ) and  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = view_cabecera_comprobantes.cbc_sucid ) and  
         ( asientos_contables_cpbtdoc_apl.ada_tpcid = view_cabecera_comprobantes.cbc_tpcid ) and  
         ( asientos_contables_cpbtdoc_apl.ada_numcpbt = view_cabecera_comprobantes.cbc_numero ) and  
         ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = clasificacion_cpbtdoc_contable.cct_codemp ) and  
         ( view_tipos_cpbtdoc_clasificacion.clb_clbid = clasificacion_cpbtdoc_contable.cct_clbid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = provcli.pcl_codemp ) and  
         ( view_cabecera_comprobantes.cbc_pclid = provcli.pcl_pclid ) and  
         ( provcli.pcl_codemp = giros_idiomas.gii_codemp ) and  
         ( provcli.pcl_girid = giros_idiomas.gii_girid ) and  
         ( ( asientos_contables.ast_codemp = @cbc_codemp ) AND  
         ( asientos_contables.ast_fecperiodo >= @desde ) AND  
         ( asientos_contables.ast_fecperiodo <= @hasta ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_aniodoc is null ) AND  
         ( clasificacion_cpbtdoc_contable.cct_libcomven = 'S' and clb_libcompra=1) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc = @clb_tipcpbtdoc and ipt_pctid is not null and ipt_retenido = 'S' )  and  view_cabecera_comprobantes.idi_idid = @idi_idid and gii_idid = @idi_idid  and ast_estado in ('P','V','N') )                  
group by asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         asientos_contables.ast_numero


SELECT asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         asientos_contables.ast_numero,  
         sum(asientos_contables_detalle.acd_debe  * cct_debhab) as debe,   
         sum(asientos_contables_detalle.acd_haber  * cct_debhab) as haber   
         into #Exento
    FROM {oj asientos_contables_detalle LEFT OUTER JOIN impuestos ON asientos_contables_detalle.acd_codemp = impuestos.ipt_codemp AND asientos_contables_detalle.acd_pctid = impuestos.ipt_pctid},   
         asientos_contables,   
         asientos_contables_cpbtdoc_apl,   
         view_cabecera_comprobantes,   
         view_tipos_cpbtdoc_clasificacion,   
         clasificacion_cpbtdoc_contable,   
         provcli,   
         giros_idiomas  
   WHERE ( asientos_contables_detalle.acd_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_detalle.acd_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_detalle.acd_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_detalle.acd_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_cpbtdoc_apl.ada_codemp = asientos_contables.ast_codemp ) and  
         ( asientos_contables_cpbtdoc_apl.ada_anio = asientos_contables.ast_anio ) and  
         ( asientos_contables_cpbtdoc_apl.ada_tipo = asientos_contables.ast_tipo ) and  
         ( asientos_contables_cpbtdoc_apl.ada_numero = asientos_contables.ast_numero ) and  
         ( asientos_contables_cpbtdoc_apl.ada_codemp = view_cabecera_comprobantes.cbc_codemp ) and  
         ( asientos_contables_cpbtdoc_apl.ada_sucid = view_cabecera_comprobantes.cbc_sucid ) and  
         ( asientos_contables_cpbtdoc_apl.ada_tpcid = view_cabecera_comprobantes.cbc_tpcid ) and  
         ( asientos_contables_cpbtdoc_apl.ada_numcpbt = view_cabecera_comprobantes.cbc_numero ) and  
         ( view_cabecera_comprobantes.cbc_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp ) and  
         ( view_cabecera_comprobantes.cbc_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid ) and  
         ( view_tipos_cpbtdoc_clasificacion.tpc_codemp = clasificacion_cpbtdoc_contable.cct_codemp ) and  
         ( view_tipos_cpbtdoc_clasificacion.clb_clbid = clasificacion_cpbtdoc_contable.cct_clbid ) and  
         ( view_cabecera_comprobantes.cbc_codemp = provcli.pcl_codemp ) and  
         ( view_cabecera_comprobantes.cbc_pclid = provcli.pcl_pclid ) and  
         ( provcli.pcl_codemp = giros_idiomas.gii_codemp ) and  
         ( provcli.pcl_girid = giros_idiomas.gii_girid ) and  
         ( ( asientos_contables.ast_codemp = @cbc_codemp) AND  
         ( asientos_contables.ast_fecperiodo >= @desde ) AND  
         ( asientos_contables.ast_fecperiodo <= @hasta ) AND  
         ( asientos_contables_cpbtdoc_apl.ada_aniodoc is null ) AND  
         ( clasificacion_cpbtdoc_contable.cct_libcomven = 'S' and clb_libcompra=1) AND  
         ( view_tipos_cpbtdoc_clasificacion.clb_tipcpbtdoc = @clb_tipcpbtdoc and acd_exento = 'S' ) and  view_cabecera_comprobantes.idi_idid = @idi_idid and gii_idid = @idi_idid and ast_estado in ('P','V','N') )                   
group by asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         asientos_contables.ast_numero




update #cpbt
set TotalDebe = 0,
    TotalHaber = 0,
    TotalImpuesto = 0,
    TotalExento = 0,
    TotalRetenido = 0




update #cpbt
set TotalDebe = debe,
    TotalHaber = haber
from  #cpbt a, #Valores b
where a.ast_anio = b.ast_anio and
	  a.ast_tipo = b.ast_tipo and
      a.ast_numero = b.ast_numero 


update #cpbt
set TotalImpuesto = debe + haber
from  #cpbt a, #Impuestos b
where a.ast_anio = b.ast_anio and
	  a.ast_tipo = b.ast_tipo and
      a.ast_numero = b.ast_numero  


update #cpbt
set TotalRetenido = debe + haber
from  #cpbt a, #ImpRet b
where a.ast_anio = b.ast_anio and
	  a.ast_tipo = b.ast_tipo and
      a.ast_numero = b.ast_numero  		


update #cpbt
set TotalExento = debe + haber
from  #cpbt a, #Exento b
where a.ast_anio = b.ast_anio and
	  a.ast_tipo = b.ast_tipo and
      a.ast_numero = b.ast_numero  	



update #cpbt
set TotalImpImp = 0
from  #cpbt


update #cpbt
set TotalDebe = 0,
    TotalHaber = 0,
    TotalImpuesto =0,
    TotalExento =0,
    TotalRetenido=0,
    TotalImpImp =0   
from  #cpbt
where cbt_estado = 'X'





select #cpbt.*, TotalDebe - (TotalImpuesto + TotalExento + TotalRetenido + TotalImpImp) as Neto, TotalDebe as Bruto
 from #cpbt
order by tpc_codigo, cbc_feccont, cbc_numprovcli
