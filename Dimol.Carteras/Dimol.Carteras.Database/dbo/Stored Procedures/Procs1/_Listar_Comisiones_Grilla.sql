CREATE PROCEDURE [dbo].[_Listar_Comisiones_Grilla]
(
@codemp int,
@codsuc int,
@anio int,
@mes int,
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
  (' set @query = @query + 'SELECT comisiones.cms_anio,
            comisiones.cms_mes,
            view_aplicaciones_doc_cartera_clientes.pcl_nomfant,
            view_aplicaciones_doc_cartera_clientes.tci_nombre,
            view_aplicaciones_doc_cartera_clientes.ddi_numcta,
            convert(char(10), apl_fecapl, 103) FecCanc,
            sum(cms_capital) as Capital, 
            sum(cms_interes) as Interes,
            sum(cms_honorario) as Honorario,
            sum(cms_capital + cms_interes + cms_honorario) as Total,
            comisiones.cms_porcfcob * 100 PorFact,
            view_aplicaciones_doc_cartera_clientes.ctc_rut,
            view_aplicaciones_doc_cartera_clientes.ctc_nomfant,
            sum(cms_comki) as ComKI,
            sum(cms_comh) as ComH, sum(cms_comki + cms_comh) as ComTotal,
            gestor.ges_nombre, tci_nombre + ''_'' + ddi_numcta + ''_'' + ctc_rut + ''_'' + ges_nombre + ''_'' + convert(char(10), apl_fecapl, 103) as DocPad
            FROM comisiones, 
            view_aplicaciones_doc_cartera_clientes,
            gestor
            WHERE  comisiones.cms_codemp = view_aplicaciones_doc_cartera_clientes.apl_codemp  and
            comisiones.cms_sucid = view_aplicaciones_doc_cartera_clientes.apl_sucid  and 
            comisiones.cms_anioapl = view_aplicaciones_doc_cartera_clientes.apl_anio  and
            comisiones.cms_numapl = view_aplicaciones_doc_cartera_clientes.apl_numapl  and
            comisiones.cms_itemapl = view_aplicaciones_doc_cartera_clientes.api_item  and
            gestor.ges_codemp = comisiones.cms_codemp  and
            gestor.ges_sucid = comisiones.cms_sucid  and
            gestor.ges_gesid = comisiones.cms_gesid  and
            comisiones.cms_codemp = '+ CONVERT(VARCHAR,@codemp) +'
            and comisiones.cms_sucid = '+ CONVERT(VARCHAR,@codsuc) +'
            and comisiones.cms_anio = '+ CONVERT(VARCHAR,@anio) +'
            and comisiones.cms_mes = '+ CONVERT(VARCHAR,@mes) +'
			GROUP BY comisiones.cms_anio,
            comisiones.cms_mes, 
            view_aplicaciones_doc_cartera_clientes.pcl_nomfant, 
            view_aplicaciones_doc_cartera_clientes.ctc_rut, 
            view_aplicaciones_doc_cartera_clientes.ctc_nomfant, 
            view_aplicaciones_doc_cartera_clientes.tci_nombre,
            view_aplicaciones_doc_cartera_clientes.ddi_numcta, 
            gestor.ges_nombre,
            convert(char(10), apl_fecapl, 103),
            comisiones.cms_porcfcob,
            view_aplicaciones_doc_cartera_clientes.ctc_rut,
            view_aplicaciones_doc_cartera_clientes.ctc_nomfant, tci_nombre + ''_'' + ddi_numcta + ''_'' + ctc_rut + ''_'' + ges_nombre + ''_'' + convert(char(10), apl_fecapl, 103)
  
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
