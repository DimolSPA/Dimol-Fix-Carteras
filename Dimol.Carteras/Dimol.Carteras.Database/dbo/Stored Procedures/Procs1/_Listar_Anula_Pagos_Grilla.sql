CREATE Procedure [dbo].[_Listar_Anula_Pagos_Grilla]
(@codemp integer, 
@sucid integer, 
@pclid integer, 
@ctcid numeric(15), 
@idioma integer,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int) as

BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
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
         DocA.tci_nombre,   
         DocA.ddi_numcta,   
         DocA.pcl_nomfant,   
         DocA.ctc_nomfant,   
         DocA.epl_nombre,   
         DocA.epl_apepat ,
	   aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones_items.api_gesid,   
         aplicaciones_items.api_vdeid,
         idi_idid,
         DocA.ddi_pclid,
         DocA.ddi_ctcid,
         DocA.ddi_emplid,
         api_ctcid,
         DocA.ddi_anio,
         DocA.ddi_numdoc,
         DocB.ddi_anio anio,
         DocB.ddi_numdoc numdoc,
         DocB.ddi_numcta numcta,
         0 as ccbid  
         into #Apl  
 FROM aplicaciones,   
         aplicaciones_items,   
         view_documentos_diarios  DocA,   
         view_documentos_diarios  DocB,   
         idiomas  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = DocA.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = DocA.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc2 = DocA.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc2 = DocA.ddi_numdoc ) and  
         ( aplicaciones_items.api_codemp = DocB.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = DocB.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc = DocB.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc = DocB.ddi_numdoc ) and  
         ( DocA.edi_idiid = idiomas.idi_idid ) and  
         ( ( aplicaciones.apl_codemp = @codemp ) AND  
         ( aplicaciones.apl_sucid = @sucid ) AND  
         ( aplicaciones.apl_accion = -1 ) AND  
         ( DocA.ddi_pclid = @pclid ) AND  
         ( DocA.ddi_ctcid = @ctcid ) AND  
         ( idiomas.idi_idid = @idioma)   
         )    



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
         DocA.tci_nombre,   
         DocA.ddi_numcta,   
         DocA.pcl_nomfant,   
         DocA.ctc_nomfant,   
         DocA.epl_nombre,   
         DocA.epl_apepat ,
	   aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones_items.api_gesid,   
         aplicaciones_items.api_vdeid,
         idi_idid,
         DocA.ddi_pclid,
         DocA.ddi_ctcid,
         DocA.ddi_emplid,
         api_ctcid,
         DocA.ddi_anio,
         DocA.ddi_numdoc,
         DocB.ddi_anio anio,
         DocB.ddi_numdoc numdoc,
         DocB.ddi_numcta numcta,
         0 as ccbid  
 FROM aplicaciones,   
         aplicaciones_items,   
         view_documentos_diarios  DocA,   
         view_documentos_diarios  DocB,   
         idiomas  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = DocA.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = DocA.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc2 = DocA.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc2 = DocA.ddi_numdoc ) and  
         ( aplicaciones_items.api_codemp = DocB.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = DocB.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc = DocB.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc = DocB.ddi_numdoc ) and  
         ( DocA.edi_idiid = idiomas.idi_idid ) and  
         ( ( aplicaciones.apl_codemp = @codemp ) AND  
         ( aplicaciones.apl_sucid = @sucid ) AND  
         ( aplicaciones.apl_accion = -1 ) AND  
         ( DocA.ddi_pclid = @pclid ) AND  
         ( DocA.ddi_ctcid is null and api_ctcid is not null ) AND  
         ( idiomas.idi_idid = @idioma )   
         )    


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
         DocA.tci_nombre,   
         DocA.ddi_numcta,   
         DocA.pcl_nomfant,   
         DocA.ctc_nomfant,   
         DocA.epl_nombre,   
         DocA.epl_apepat ,
	   aplicaciones_items.api_capital,   
         aplicaciones_items.api_interes,   
         aplicaciones_items.api_honorario,   
         aplicaciones_items.api_gastpre,   
         aplicaciones_items.api_gastjud,   
         aplicaciones_items.api_gesid,   
         aplicaciones_items.api_vdeid,
         idi_idid,
         DocA.ddi_pclid,
         DocA.ddi_ctcid,
         DocA.ddi_emplid,
         api_ctcid,
         DocA.ddi_anio,
         DocA.ddi_numdoc,
         DocB.ddi_anio anio,
         DocB.ddi_numdoc numdoc,
         DocB.ddi_numcta numcta,
         0 as ccbid  
 FROM aplicaciones,   
         aplicaciones_items,   
         view_documentos_diarios DocA,   
         view_documentos_diarios DocB,   
         idiomas  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = DocA.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = DocA.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc2 = DocA.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc2 = DocA.ddi_numdoc ) and  
         ( aplicaciones_items.api_codemp = DocB.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = DocB.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc = DocB.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc = DocB.ddi_numdoc ) and  
         ( DocA.edi_idiid = idiomas.idi_idid ) and  
         ( ( aplicaciones.apl_codemp = @codemp ) AND  
         ( aplicaciones.apl_sucid = @sucid ) AND  
         ( aplicaciones.apl_accion = -1 ) AND  
         ( DocA.ddi_pclid = @pclid ) AND  
         ( DocA.ddi_ctcid is null and api_ctcid = @ctcid ) AND  
         ( idiomas.idi_idid = @idioma )   
         )    


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
         0,
         0,
         ddi_anio anio,
         ddi_numdoc numdoc,
         ddi_numcta numcta,
         ccb_ccbid as ccbid     
   FROM aplicaciones,   
         aplicaciones_items,   
         view_documentos_diarios,   
         idiomas,   
         cartera_clientes_documentos_cpbt_doc  
   WHERE ( aplicaciones_items.api_codemp = aplicaciones.apl_codemp ) and  
         ( aplicaciones_items.api_sucid = aplicaciones.apl_sucid ) and  
         ( aplicaciones_items.api_anio = aplicaciones.apl_anio ) and  
         ( aplicaciones_items.api_numapl = aplicaciones.apl_numapl ) and  
         ( aplicaciones_items.api_codemp = view_documentos_diarios.ddi_codemp ) and  
         ( aplicaciones_items.api_sucid = view_documentos_diarios.ddi_sucid ) and  
         ( aplicaciones_items.api_aniodoc = view_documentos_diarios.ddi_anio ) and  
         ( aplicaciones_items.api_numdoc = view_documentos_diarios.ddi_numdoc ) and  
         ( aplicaciones_items.api_codemp = cartera_clientes_documentos_cpbt_doc.ccb_codemp ) and  
         ( aplicaciones_items.api_pclid = cartera_clientes_documentos_cpbt_doc.ccb_pclid ) and  
         ( aplicaciones_items.api_ctcid = cartera_clientes_documentos_cpbt_doc.ccb_ctcid ) and  
         ( aplicaciones_items.api_ccbid = cartera_clientes_documentos_cpbt_doc.ccb_ccbid ) and  
         ( ( aplicaciones.apl_codemp = @codemp ) AND  
         ( aplicaciones.apl_sucid = @sucid ) AND  
         ( aplicaciones.apl_accion = -1 ) AND  
         ( api_pclid = @pclid ) AND  
         ( api_ctcid = @ctcid ) AND  
         ( idiomas.idi_idid = @idioma )   
         )    


if @ctcid = 0 or @ctcid is null
begin
  set @query = @query + 'SELECT apl_anio Anio, 
								apl_numapl NumeroAplicacion, 
								api_item Item, 
								case pcl_nomfant when null then epl_nombre + '' '' + epl_apepat else pcl_nomfant end as NombreCliente,
								ctc_nomfant NombreDeudor, 
								tci_nombre TipoDocumento, 
								ddi_numcta NumeroCuenta, 
								apl_fecapl Fecha,
								api_capital Capital, 
								api_interes Interes, 
								api_honorario Honorario, 
								api_gastpre GastoPrejudicial, 
								api_gastjud GastoJudicial,
								api_capital + api_interes + api_honorario + api_gastpre + api_gastjud as Total,
								case api_gesid when null then 0 else api_gesid end as Gestor,
								case api_vdeid when null then 0 else api_vdeid end as Vendedor, 
								numcta NumeroDocumento
 from #Apl
where numcta + ''_'' + tci_nombre   not in  ( SELECT ddi_numcta + ''_'' + tci_nombre      FROM view_aplicaciones_doc_cartera_clientes     WHERE  view_aplicaciones_doc_cartera_clientes.apl_codemp = '+convert(varchar,@codemp) +' AND            view_aplicaciones_doc_cartera_clientes.ddi_pclid = '+convert(varchar,@pclid)+'  AND            view_aplicaciones_doc_cartera_clientes.ccb_ctcid = '+convert(varchar,@ctcid)+'  AND            view_aplicaciones_doc_cartera_clientes.apl_accion = 1) '   
--order by tci_nombre, ddi_numcta, apl_fecapl
end

if @ctcid > 0 
begin
 set @query = @query + 'SELECT apl_anio Anio, 
								apl_numapl NumeroAplicacion, 
								api_item Item, 
								case pcl_nomfant when null then epl_nombre + '' '' + epl_apepat else pcl_nomfant end as NombreCliente,
								ctc_nomfant NombreDeudor, 
								tci_nombre TipoDocumento, 
								ddi_numcta NumeroCuenta, 
								apl_fecapl Fecha,
								api_capital Capital, 
								api_interes Interes, 
								api_honorario Honorario, 
								api_gastpre GastoPrejudicial, 
								api_gastjud GastoJudicial,
								api_capital + api_interes + api_honorario + api_gastpre + api_gastjud as Total,
								case api_gesid when null then 0 else api_gesid end as Gestor,
								case api_vdeid when null then 0 else api_vdeid end as Vendedor, 
								numcta NumeroDocumento
 from #Apl
where numcta + ''_'' + tci_nombre   not in  ( SELECT ddi_numcta + ''_'' + tci_nombre      FROM view_aplicaciones_doc_cartera_clientes     WHERE  view_aplicaciones_doc_cartera_clientes.apl_codemp =  '+convert(varchar,@codemp)+'  AND            view_aplicaciones_doc_cartera_clientes.ddi_pclid = '+convert(varchar,@pclid)+'  AND            view_aplicaciones_doc_cartera_clientes.ccb_ctcid = '+convert(varchar,@ctcid)+'  AND            view_aplicaciones_doc_cartera_clientes.apl_accion = 1    )'
--order by tci_nombre, ddi_numcta, apl_fecapl'
end

set @query = @query + ') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

 --select @query
 exec(@query)	
	


END
