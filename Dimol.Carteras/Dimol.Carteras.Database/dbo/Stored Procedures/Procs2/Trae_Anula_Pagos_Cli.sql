Create Procedure Trae_Anula_Pagos_Cli(@apl_codemp integer, @apl_sucid integer, @ddi_pclid integer, @idi_idid integer) as
   SELECT aplicaciones.apl_codemp,   
         aplicaciones.apl_sucid,   
         aplicaciones.apl_anio,   
         aplicaciones.apl_mes,   
         aplicaciones.apl_numapl,   
         aplicaciones_items.api_item,   
         aplicaciones.apl_fecapl,   
         aplicaciones.apl_tipo,   
         aplicaciones.apl_accion,   
         aplicaciones.apl_fecing,   
         view_documentos_diarios.tci_nombre,   
         view_documentos_diarios.ddi_numcta,   
         view_documentos_diarios.pcl_nomfant,   
         view_documentos_diarios.ctc_nomfant,   
         view_documentos_diarios.epl_nombre,   
         view_documentos_diarios.epl_apepat ,
	   aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones_items.api_gesid,   
         aplicaciones_items.api_vdeid,
         idi_idid,
         ddi_pclid,
         ddi_ctcid,
         ddi_emplid,
         api_ctcid,
         ddi_numdoc 
         into #Apl  
    FROM aplicaciones,   
         aplicaciones_items,   
         view_documentos_diarios,   
         idiomas  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = view_documentos_diarios.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = view_documentos_diarios.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc2 = view_documentos_diarios.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc2 = view_documentos_diarios.ddi_numdoc ) and  
         ( view_documentos_diarios.tci_idid = idiomas.idi_idid ) and  
         ( view_documentos_diarios.edi_idiid = idiomas.idi_idid ) and apl_codemp = @apl_codemp
         and apl_sucid = @apl_sucid and apl_accion =-1 and ddi_pclid = @ddi_pclid  and
        idi_idid = @idi_idid



insert into #Apl 
 SELECT aplicaciones.apl_codemp,   
         aplicaciones.apl_sucid,   
         aplicaciones.apl_anio,   
         aplicaciones.apl_mes,   
         aplicaciones.apl_numapl,   
         aplicaciones_items.api_item,   
         aplicaciones.apl_fecapl,   
         aplicaciones.apl_tipo,   
         aplicaciones.apl_accion,   
         aplicaciones.apl_fecing,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.pcl_nomfant,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant, 
         '',
         '',
	    aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones_items.api_gesid,   
         aplicaciones_items.api_vdeid,
         idi_idid,
         ccb_pclid,
         ccb_ctcid,
         0,
         api_ctcid,
         0   
    FROM aplicaciones,   
         aplicaciones_items,   
         idiomas,   
         cartera_clientes_documentos_cpbt_doc  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( aplicaciones_items.api_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( aplicaciones_items.api_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( aplicaciones_items.api_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid and ccb_ccbid is null )  
         and apl_codemp = @apl_codemp
         and apl_sucid = @apl_sucid and apl_accion =-1 and ccb_pclid = @ddi_pclid  and
          idi_idid = @idi_idid


 

insert into #Apl  
 SELECT aplicaciones.apl_codemp,   
         aplicaciones.apl_sucid,   
         aplicaciones.apl_anio,   
         aplicaciones.apl_mes,   
         aplicaciones.apl_numapl,   
         aplicaciones_items.api_item,   
         aplicaciones.apl_fecapl,   
         aplicaciones.apl_tipo,   
         aplicaciones.apl_accion,   
         aplicaciones.apl_fecing,   
         cartera_clientes_documentos_cpbt_doc.tci_nombre,   
         cartera_clientes_documentos_cpbt_doc.ccb_numero,   
         cartera_clientes_documentos_cpbt_doc.pcl_nomfant,   
         cartera_clientes_documentos_cpbt_doc.ctc_nomfant, 
         '',
         '',
	    aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones_items.api_gesid,   
         aplicaciones_items.api_vdeid,
         idi_idid,
         ccb_pclid,
         ccb_ctcid,
         0,
         api_ctcid,
         0   
    FROM aplicaciones,   
         aplicaciones_items,   
         idiomas,   
         cartera_clientes_documentos_cpbt_doc  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( aplicaciones_items.api_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( aplicaciones_items.api_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( aplicaciones_items.api_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( cartera_clientes_documentos_cpbt_doc.tci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.eci_idid = idiomas.idi_idid ) and  
         ( cartera_clientes_documentos_cpbt_doc.mci_idid = idiomas.idi_idid and ccb_ccbid is not null )    
 and apl_codemp = @apl_codemp
         and apl_sucid = @apl_sucid and apl_accion =-1 and ccb_pclid = @ddi_pclid and 
          idi_idid = @idi_idid

             
insert into #Apl  
  SELECT 
         aplicaciones.apl_codemp,   
         aplicaciones.apl_sucid,   
         aplicaciones.apl_anio,   
         aplicaciones.apl_mes,   
         aplicaciones.apl_numapl,   
         aplicaciones_items.api_item,   
         aplicaciones.apl_fecapl,   
         aplicaciones.apl_tipo,   
         aplicaciones.apl_accion,   
         aplicaciones.apl_fecing,   
         view_cabecera_comprobantes.tci_nombre,   
         case cbc_numprovcli when '' then convert(varchar,cbc_numero) else cbc_numprovcli end, 
         view_cabecera_comprobantes.pcl_nomfant,  
         '', 
         '',
         '',
	    aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones_items.api_gesid,   
         aplicaciones_items.api_vdeid,
         idi_idid,
         cbc_pclid,
         0,
         0,
         api_ctcid,
         0   
    FROM aplicaciones,   
         aplicaciones_items,   
         view_cabecera_comprobantes  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = view_cabecera_comprobantes.cbc_codemp ) and  
         ( aplicaciones_items.api_sucid = view_cabecera_comprobantes.cbc_sucid ) and  
         ( aplicaciones_items.api_tpcid = view_cabecera_comprobantes.cbc_tpcid ) and  
         ( aplicaciones_items.api_numero = view_cabecera_comprobantes.cbc_numero )
 and apl_codemp = @apl_codemp
         and apl_sucid = @apl_sucid and apl_accion =-1 and cbc_pclid = @ddi_pclid  and
          idi_idid = @idi_idid



insert into #Apl  
  SELECT 
         aplicaciones.apl_codemp,   
         aplicaciones.apl_sucid,   
         aplicaciones.apl_anio,   
         aplicaciones.apl_mes,   
         aplicaciones.apl_numapl,   
         aplicaciones_items.api_item,   
         aplicaciones.apl_fecapl,   
         aplicaciones.apl_tipo,   
         aplicaciones.apl_accion,   
         aplicaciones.apl_fecing,   
         view_cabecera_comprobantes.tci_nombre,   
         case cbc_numprovcli when '' then convert(varchar,cbc_numero) else cbc_numprovcli end, 
         view_cabecera_comprobantes.pcl_nomfant,  
         '', 
         '',
         '',
	    aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones_items.api_gesid,   
         aplicaciones_items.api_vdeid,
         idi_idid,
         cbc_pclid,
         0,
         0,
         api_ctcid,
         0   
    FROM aplicaciones,   
         aplicaciones_items,   
         view_cabecera_comprobantes  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = view_cabecera_comprobantes.cbc_codemp ) and  
         ( aplicaciones_items.api_sucid = view_cabecera_comprobantes.cbc_sucid ) and  
         ( aplicaciones_items.api_tpcid = view_cabecera_comprobantes.cbc_tpcid ) and  
         ( aplicaciones_items.api_numero = view_cabecera_comprobantes.cbc_numero )
 and apl_codemp = @apl_codemp
         and apl_sucid = @apl_sucid and apl_accion =-1 and cbc_pclid = @ddi_pclid and api_ctcid is null



  SELECT apl_anio, apl_numapl, api_item, 
           case pcl_nomfant when null then epl_nombre + ' ' + epl_apepat else pcl_nomfant end as pcl_nomfant,
              ctc_nomfant, tci_nombre, ddi_numcta, apl_fecapl,
             api_capital, api_interes, api_honorario, api_gastpre, api_gastjud,
              api_capital + api_interes + api_honorario + api_gastpre + api_gastjud as totdeu, 
              case api_gesid when null then 0 else api_gesid end as api_gesid,
              case api_vdeid when null then 0 else api_vdeid end as api_vdeid
 from #Apl
where tci_nombre + '_' + ddi_numcta  not in (select tci_nombre + '_' + ddi_numcta FROM view_aplicaciones where apl_codemp = @apl_codemp and ddi_pclid = @ddi_pclid 
      and   apl_sucid = @apl_sucid
       and   apl_accion = 1)
order by tci_nombre, ddi_numcta, apl_fecapl