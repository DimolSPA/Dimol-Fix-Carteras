CREATE PROCEDURE [dbo].[_Listar_CalculoComisiones]
(
@codemp int,
@codsuc int,
@idioma int,
@desde varchar(255),
@hasta varchar(255),
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'SELECT view_aplicaciones_doc_cartera_clientes.api_gesid,   
            view_aplicaciones_doc_cartera_clientes.apl_anio,   
            view_aplicaciones_doc_cartera_clientes.apl_numapl,   
            view_aplicaciones_doc_cartera_clientes.api_item,   
            view_aplicaciones_doc_cartera_clientes.api_capital,   
            view_aplicaciones_doc_cartera_clientes.api_interes,   
            view_aplicaciones_doc_cartera_clientes.api_honorario,   
            view_aplicaciones_doc_cartera_clientes.api_gastpre,   
            view_aplicaciones_doc_cartera_clientes.api_gastjud,
            view_aplicaciones_doc_cartera_clientes.apl_accion,
            cartera_clientes_cpbt_doc.ccb_pclid,   
            cartera_clientes_cpbt_doc.ccb_ctcid,   
            cartera_clientes_cpbt_doc.ccb_ccbid, view_aplicaciones_doc_cartera_clientes.ddi_tipcambio,  view_aplicaciones_doc_cartera_clientes.ddi_pagdir,  
            tci_nombre, ddi_numcta
            FROM view_aplicaciones_doc_cartera_clientes,   
            view_tipos_cpbtdoc_clasificacion,   
            cartera_clientes_cpbt_doc
            WHERE  view_aplicaciones_doc_cartera_clientes.apl_codemp = view_tipos_cpbtdoc_clasificacion.tpc_codemp  and  
            view_aplicaciones_doc_cartera_clientes.ddi_tpcid = view_tipos_cpbtdoc_clasificacion.tpc_tpcid  and  
            view_aplicaciones_doc_cartera_clientes.apl_codemp = cartera_clientes_cpbt_doc.ccb_codemp  and  
            view_aplicaciones_doc_cartera_clientes.ddi_pclid = cartera_clientes_cpbt_doc.ccb_pclid  and  
            view_aplicaciones_doc_cartera_clientes.ddi_ctcid = cartera_clientes_cpbt_doc.ccb_ctcid  and  
            view_aplicaciones_doc_cartera_clientes.ccb_ccbid = cartera_clientes_cpbt_doc.ccb_ccbid  and  
            view_aplicaciones_doc_cartera_clientes.apl_codemp = '+ CONVERT(VARCHAR,@codemp) +'
            and view_aplicaciones_doc_cartera_clientes.apl_sucid = '+ CONVERT(VARCHAR,@codsuc) +'
            and view_aplicaciones_doc_cartera_clientes.apl_fecapl >= '+ @desde +'
            and view_aplicaciones_doc_cartera_clientes.apl_fecapl <= ' + Left(@hasta, 10) + '23:59:59''
            and view_tipos_cpbtdoc_clasificacion.clb_remesa = ''S''
            and view_aplicaciones_doc_cartera_clientes.idi_idid = '+ CONVERT(VARCHAR,@idioma) +'
     '
   
   set @query = @query +')as tabla ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row < '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
