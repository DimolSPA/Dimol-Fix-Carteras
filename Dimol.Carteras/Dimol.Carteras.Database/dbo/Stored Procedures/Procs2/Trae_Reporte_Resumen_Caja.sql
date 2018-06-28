Create Procedure Trae_Reporte_Resumen_Caja(@ddi_codemp integer, @ddi_sucid integer, @idi_idid integer, @ddi_desde datetime, @ddi_hasta datetime) as  

DECLARE @ArrDat2 Varchar(MAX)
select @ArrDat2 = emc_valtxt from empresa_configuracion where emc_codemp = @ddi_codemp and emc_emcid = 110 

DECLARE @ArrDat3 Varchar(MAX)
select @ArrDat3 = emc_valtxt from empresa_configuracion where emc_codemp = @ddi_codemp and emc_emcid = 124



SELECT view_documentos_diarios.pcl_rut,   
         view_documentos_diarios.pcl_nomfant,   
         view_documentos_diarios.bco_nombre,   
         view_documentos_diarios.mon_nombre,   
         view_documentos_diarios.ctc_rut,   
         view_documentos_diarios.ctc_nomfant,   
         view_documentos_diarios.epl_rut,   
         view_documentos_diarios.epl_nombre,   
         view_documentos_diarios.epl_apepat,   
         view_documentos_diarios.ddi_monto,   
         view_documentos_diarios.ddi_saldo,   
         view_documentos_diarios.ddi_numcta,   
         view_documentos_diarios.ddi_fecdoc,   
         view_documentos_diarios.ddi_fecvenc,   
         view_documentos_diarios.ddi_tipmov,   
         view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.ddi_custodia,   
         view_documentos_diarios.ddi_docemp,   
         view_documentos_diarios.ddi_pagdir,   
         space(800) as TipPag,   
         space(800) as NumPag,   
         99999999999999.9999 as Capital,   
         99999999999999.9999 as Intereses,   
         99999999999999.9999 as Honorarios,   
         99999999999999.9999 as GastPre,   
         99999999999999.9999 as GastJud,
         ddi_tipcambio,
         ddi_fecing,
         0 as apl_accion,
         view_documentos_diarios.ddi_rutpag,
         view_documentos_diarios.ddi_nompag,
         convert(varchar, ddi_anio) + '_' + convert(varchar, ddi_numdoc) as numesp,
         ddi_fecing as apl_fecapl,
         ddi_tpcid
         into #DocDia    
    FROM view_documentos_diarios,   
         idiomas,   
         estados_documentos_diarios  
   WHERE  view_documentos_diarios.edi_idiid = idiomas.idi_idid  and  
          view_documentos_diarios.tci_idid = idiomas.idi_idid  and  
          view_documentos_diarios.ddi_codemp = estados_documentos_diarios.edc_codemp  and  
          view_documentos_diarios.ddi_edcid = estados_documentos_diarios.edc_edcid  and  
          view_documentos_diarios.ddi_tipmov = estados_documentos_diarios.edc_tipmov  and  
           view_documentos_diarios.ddi_codemp = @ddi_codemp  AND  
          view_documentos_diarios.ddi_sucid = @ddi_sucid  AND  
          idiomas.idi_idid = @idi_idid  AND  
          view_documentos_diarios.ddi_fecing >= @ddi_desde  AND  
          view_documentos_diarios.ddi_fecing <= @ddi_hasta  AND  
          estados_documentos_diarios.edc_estado <> 3 and
          ddi_tpcid in (     SELECT tipos_cpbtdoc.tpc_tpcid  
							 FROM tipos_cpbtdoc,   
									clasificacion_cpbtdoc  
							WHERE  clasificacion_cpbtdoc.clb_codemp = tipos_cpbtdoc.tpc_codemp  and  
									 clasificacion_cpbtdoc.clb_clbid = tipos_cpbtdoc.tpc_clbid  and  
									  tipos_cpbtdoc.tpc_codemp =@ddi_codemp  AND  
									 clasificacion_cpbtdoc.clb_aplica = 'S'    )

          and ddi_tpcid   not in (select * from dbo.Arreglo(@ArrDat2, ','))


delete from #DocDia 
where ddi_tpcid  in (select * from dbo.Arreglo(@ArrDat3, ',')) and
      ddi_saldo = 0  



SELECT DISTINCT convert(varchar,api_aniodoc) + '_' + convert(varchar,api_numdoc) numesp2 into #Apl  
                                                                                   FROM aplicaciones_items  
                                                                                  WHERE  aplicaciones_items.api_codemp =@ddi_codemp  AND  
                                                                                         aplicaciones_items.api_sucid = @ddi_sucid  AND  
                                                                                         aplicaciones_items.api_anio = datepart(year,  @ddi_hasta) 


delete from #DocDia
from #DocDia, #Apl
where numesp = numesp2




update #DocDia
set Capital = 0,
    Intereses = 0,
    Honorarios = 0,
    GastPre = 0,
    GastJud = 0





insert into #DocDia

SELECT view_documentos_diarios.pcl_rut,   
         view_documentos_diarios.pcl_nomfant,   
         view_documentos_diarios.bco_nombre,   
         view_documentos_diarios.mon_nombre,   
         view_documentos_diarios.ctc_rut,   
         view_documentos_diarios.ctc_nomfant,   
         view_documentos_diarios.epl_rut,   
         view_documentos_diarios.epl_nombre,   
         view_documentos_diarios.epl_apepat,   
         view_documentos_diarios.ddi_monto,   
         view_documentos_diarios.ddi_saldo,   
         view_documentos_diarios.ddi_numcta,   
         view_documentos_diarios.ddi_fecdoc,   
         view_documentos_diarios.ddi_fecvenc,   
         view_documentos_diarios.ddi_tipmov,   
         view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.ddi_custodia,   
         view_documentos_diarios.ddi_docemp,   
         view_documentos_diarios.ddi_pagdir,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,
         ddi_tipcambio,
         ddi_fecing,
         apl_accion,
         view_documentos_diarios.ddi_rutpag,
         view_documentos_diarios.ddi_nompag,
         convert(varchar, ddi_anio) + '_' + convert(varchar, ddi_numdoc) as numesp,
         apl_fecapl,
         0    
    FROM view_documentos_diarios,   
         idiomas,   
         estados_documentos_diarios,   
         aplicaciones_items,   
         cartera_clientes_documentos_cpbt_doc,
         aplicaciones  
   WHERE  view_documentos_diarios.edi_idiid = idiomas.idi_idid  and  
          view_documentos_diarios.tci_idid = idiomas.idi_idid  and  
          view_documentos_diarios.ddi_codemp = estados_documentos_diarios.edc_codemp  and  
          view_documentos_diarios.ddi_edcid = estados_documentos_diarios.edc_edcid  and  
          view_documentos_diarios.ddi_tipmov = estados_documentos_diarios.edc_tipmov  and  
          view_documentos_diarios.ddi_codemp = aplicaciones_items.api_codemp  and  
          view_documentos_diarios.ddi_sucid = aplicaciones_items.api_sucid  and  
          view_documentos_diarios.ddi_anio = aplicaciones_items.api_aniodoc  and  
          view_documentos_diarios.ddi_numdoc = aplicaciones_items.api_numdoc  and  
          aplicaciones_items.api_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp  and  
          aplicaciones_items.api_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid  and  
          aplicaciones_items.api_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid  and  
          aplicaciones_items.api_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid  and  
	    aplicaciones_items.api_codemp =apl_codemp  and  
          aplicaciones_items.api_sucid =apl_sucid  and  
          aplicaciones_items.api_anio =apl_anio  and  
          aplicaciones_items.api_numapl = apl_numapl  and  
          cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid  and  
          cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid  and  
          cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid  and  
           view_documentos_diarios.ddi_codemp = @ddi_codemp  AND  
          view_documentos_diarios.ddi_sucid = @ddi_sucid  AND  
          idiomas.idi_idid = @idi_idid  AND  
      aplicaciones.apl_fecapl >= @ddi_desde  AND  
          aplicaciones.apl_fecapl <= @ddi_hasta  AND   
           ddi_tpcid in (     SELECT tipos_cpbtdoc.tpc_tpcid  
							 FROM tipos_cpbtdoc,   
									clasificacion_cpbtdoc  
							WHERE  clasificacion_cpbtdoc.clb_codemp = tipos_cpbtdoc.tpc_codemp  and  
									 clasificacion_cpbtdoc.clb_clbid = tipos_cpbtdoc.tpc_clbid  and  
									  tipos_cpbtdoc.tpc_codemp = @ddi_codemp  AND  
									 clasificacion_cpbtdoc.clb_aplica = 'S'    )  

           and ddi_tpcid   not in ((select * from dbo.Arreglo(@ArrDat2, ',')))
           and view_documentos_diarios.pcl_rut + '_' + view_documentos_diarios.ctc_rut + '_' + ddi_numcta not in (select  pcl_rut + '_' + ctc_rut + '_' + ddi_numcta from #DocDia where ctc_rut is not null)




insert into #DocDia


  SELECT view_documentos_diarios_a.pcl_rut,   
         view_documentos_diarios_a.pcl_nomfant,   
         view_documentos_diarios_a.bco_nombre,   
         view_documentos_diarios_a.mon_nombre,   
         view_documentos_diarios_a.ctc_rut,   
         view_documentos_diarios_a.ctc_nomfant,   
         view_documentos_diarios_a.epl_rut,   
         view_documentos_diarios_a.epl_nombre,   
         view_documentos_diarios_a.epl_apepat,   
         view_documentos_diarios_a.ddi_monto,   
         view_documentos_diarios_a.ddi_saldo,   
         view_documentos_diarios_a.ddi_numcta,   
         view_documentos_diarios_a.ddi_fecdoc,   
         view_documentos_diarios_a.ddi_fecvenc,   
         view_documentos_diarios_a.ddi_tipmov,   
         view_documentos_diarios_a.tci_nombre,   
         view_documentos_diarios_a.ddi_custodia,   
         view_documentos_diarios_a.ddi_docemp,   
         view_documentos_diarios_a.ddi_pagdir,   
         view_documentos_diarios_b.tci_nombre, 
         view_documentos_diarios_b.ddi_numcta,   
         aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,
        view_documentos_diarios_a.ddi_tipcambio,
         view_documentos_diarios_a.ddi_fecing,
         apl_accion,
         view_documentos_diarios_a.ddi_rutpag,
         view_documentos_diarios_a.ddi_nompag,
         convert(varchar, view_documentos_diarios_a.ddi_anio) + '_' + convert(varchar, view_documentos_diarios_a.ddi_numdoc) as numesp,
         apl_fecapl,
         0    
    FROM view_documentos_diarios view_documentos_diarios_a,   
         idiomas,   
         estados_documentos_diarios,   
         aplicaciones_items,   
         view_documentos_diarios view_documentos_diarios_b,
         aplicaciones  
   WHERE  view_documentos_diarios_a.edi_idiid = idiomas.idi_idid  and  
          view_documentos_diarios_a.tci_idid = idiomas.idi_idid  and  
          view_documentos_diarios_a.ddi_codemp = estados_documentos_diarios.edc_codemp  and  
          view_documentos_diarios_a.ddi_edcid = estados_documentos_diarios.edc_edcid  and  
          view_documentos_diarios_a.ddi_tipmov = estados_documentos_diarios.edc_tipmov  and  
          view_documentos_diarios_a.ddi_codemp = aplicaciones_items.api_codemp  and  
          view_documentos_diarios_a.ddi_sucid = aplicaciones_items.api_sucid  and  
          view_documentos_diarios_a.ddi_anio = aplicaciones_items.api_aniodoc  and  
          view_documentos_diarios_a.ddi_numdoc = aplicaciones_items.api_numdoc  and  
          aplicaciones_items.api_codemp = view_documentos_diarios_b.ddi_codemp  and  
          aplicaciones_items.api_sucid = view_documentos_diarios_b.ddi_sucid  and  
          aplicaciones_items.api_aniodoc2 = view_documentos_diarios_b.ddi_anio  and  
          aplicaciones_items.api_numdoc2 = view_documentos_diarios_b.ddi_numdoc  and  
          aplicaciones_items.api_codemp =apl_codemp  and  
          aplicaciones_items.api_sucid =apl_sucid  and  
          aplicaciones_items.api_anio =apl_anio  and  
          aplicaciones_items.api_numapl = apl_numapl  and  
          view_documentos_diarios_b.tci_idid = idiomas.idi_idid  and  
          view_documentos_diarios_b.edi_idiid = idiomas.idi_idid  and  
           view_documentos_diarios_a.ddi_codemp = @ddi_codemp  AND  
          view_documentos_diarios_a.ddi_sucid = @ddi_sucid  AND  
          idiomas.idi_idid = @idi_idid  AND  
      aplicaciones.apl_fecapl >= @ddi_desde  AND  
          aplicaciones.apl_fecapl <= @ddi_hasta  AND  
          view_documentos_diarios_a.ddi_tpcid in (     SELECT tipos_cpbtdoc.tpc_tpcid  
							 FROM tipos_cpbtdoc,   
									clasificacion_cpbtdoc  
							WHERE  clasificacion_cpbtdoc.clb_codemp = tipos_cpbtdoc.tpc_codemp  and  
									 clasificacion_cpbtdoc.clb_clbid = tipos_cpbtdoc.tpc_clbid  and  
									  tipos_cpbtdoc.tpc_codemp = @ddi_codemp  AND  
									 clasificacion_cpbtdoc.clb_aplica = 'S'    )  
           and view_documentos_diarios_a.ddi_tpcid   not in ((select * from dbo.Arreglo(@ArrDat2, ',')))
          and view_documentos_diarios_a.pcl_rut + '_' + view_documentos_diarios_a.ctc_rut + '_' + view_documentos_diarios_a.ddi_numcta not in (select  pcl_rut + '_' + ctc_rut + '_' + ddi_numcta from #DocDia) 



             

insert into #DocDia

SELECT view_documentos_diarios.pcl_rut,   
         view_documentos_diarios.pcl_nomfant,   
         view_documentos_diarios.bco_nombre,   
         view_documentos_diarios.mon_nombre,   
         view_documentos_diarios.ctc_rut,   
         view_documentos_diarios.ctc_nomfant,   
         view_documentos_diarios.epl_rut,   
         view_documentos_diarios.epl_nombre,   
         view_documentos_diarios.epl_apepat,   
         view_documentos_diarios.ddi_monto,   
         view_documentos_diarios.ddi_saldo,   
         view_documentos_diarios.ddi_numcta,   
         view_documentos_diarios.ddi_fecdoc,   
         view_documentos_diarios.ddi_fecvenc,   
         view_documentos_diarios.ddi_tipmov,   
         view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.ddi_custodia,   
         view_documentos_diarios.ddi_docemp,   
         view_documentos_diarios.ddi_pagdir,   
         tipos_cpbtdoc_idiomas.tci_nombre,      
         case cabacera_comprobantes.cbc_numprovcli when '' then convert(varchar, cabacera_comprobantes.cbc_numero) else cbc_numprovcli end,   
         aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,
         ddi_tipcambio,
         ddi_fecing,
         apl_accion,
         view_documentos_diarios.ddi_rutpag,
         view_documentos_diarios.ddi_nompag,
         convert(varchar, ddi_anio) + '_' + convert(varchar, ddi_numdoc) as numesp,
         apl_fecapl,
         0
    FROM view_documentos_diarios,   
         idiomas,   
         estados_documentos_diarios,   
         aplicaciones_items,   
         cabacera_comprobantes,   
         tipos_cpbtdoc_idiomas,
        aplicaciones  
   WHERE  view_documentos_diarios.edi_idiid = idiomas.idi_idid  and  
          view_documentos_diarios.tci_idid = idiomas.idi_idid  and  
          view_documentos_diarios.ddi_codemp = estados_documentos_diarios.edc_codemp  and  
          view_documentos_diarios.ddi_edcid = estados_documentos_diarios.edc_edcid  and  
          view_documentos_diarios.ddi_tipmov = estados_documentos_diarios.edc_tipmov  and  
          view_documentos_diarios.ddi_codemp = aplicaciones_items.api_codemp  and  
          view_documentos_diarios.ddi_sucid = aplicaciones_items.api_sucid  and  
          view_documentos_diarios.ddi_anio = aplicaciones_items.api_aniodoc  and  
          view_documentos_diarios.ddi_numdoc = aplicaciones_items.api_numdoc  and 
   aplicaciones_items.api_codemp =apl_codemp  and  
          aplicaciones_items.api_sucid =apl_sucid  and  
          aplicaciones_items.api_anio =apl_anio  and  
          aplicaciones_items.api_numapl = apl_numapl  and  
           cabacera_comprobantes.cbc_codemp = aplicaciones_items.api_codemp  and  
          cabacera_comprobantes.cbc_sucid = aplicaciones_items.api_sucid  and  
          cabacera_comprobantes.cbc_tpcid = aplicaciones_items.api_tpcid2  and  
          cabacera_comprobantes.cbc_numero = aplicaciones_items.api_numero2  and  
          tipos_cpbtdoc_idiomas.tci_codemp = cabacera_comprobantes.cbc_codemp  and  
          cabacera_comprobantes.cbc_tpcid = tipos_cpbtdoc_idiomas.tci_tpcid  and  
          tipos_cpbtdoc_idiomas.tci_idid = idiomas.idi_idid  and  
           view_documentos_diarios.ddi_codemp = @ddi_codemp  AND  
          view_documentos_diarios.ddi_sucid = @ddi_sucid  AND  
          idiomas.idi_idid = @idi_idid  AND  
          aplicaciones.apl_fecapl >= @ddi_desde  AND  
          aplicaciones.apl_fecapl <= @ddi_hasta  AND   
           ddi_tpcid in (     SELECT tipos_cpbtdoc.tpc_tpcid  
							 FROM tipos_cpbtdoc,   
									clasificacion_cpbtdoc  
							WHERE  clasificacion_cpbtdoc.clb_codemp = tipos_cpbtdoc.tpc_codemp  and  
									 clasificacion_cpbtdoc.clb_clbid = tipos_cpbtdoc.tpc_clbid  and  
									  tipos_cpbtdoc.tpc_codemp = 1  AND  
									 clasificacion_cpbtdoc.clb_aplica = 'S'    ) 

           and ddi_tpcid   not in ((select * from dbo.Arreglo(@ArrDat2, ',')))
           and view_documentos_diarios.pcl_rut + '_' + view_documentos_diarios.ctc_rut + '_' + ddi_numcta not in (select  pcl_rut + '_' + ctc_rut + '_' + ddi_numcta from #DocDia) 



select * from #DocDia