
CREATE PROCEDURE [dbo].[_Buscar_Deudor_Cpbt_Mail_Grilla_Count]
(
@codemp int ,
@idioma int ,
@pclid int ,
@ctcid int ,

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
  (select *,ROW_NUMBER() OVER (ORDER BY count asc) as row from    
  (	' 
  
set @query = @query + 'SELECT count (ccb_ccbid) count
             FROM cartera_clientes cc,  
             deudores,  
             cartera_clientes_cpbt_doc cccd,  
             tipos_cpbtdoc_idiomas tci,  
             estados_cartera_idiomas eci, provcli
             WHERE  deudores.ctc_codemp = cc.ctc_codemp  and 
             deudores.ctc_ctcid = cc.ctc_ctcid  and 
              provcli.pcl_codemp = cc.ctc_codemp  and 
             provcli.pcl_pclid = cc.ctc_pclid  and 
             cccd.ccb_codemp = cc.ctc_codemp  and 
             cccd.ccb_pclid = cc.ctc_pclid  and 
             cccd.ccb_ctcid = cc.ctc_ctcid  and 
             cccd.ccb_codemp = tci.tci_codemp  and 
             cccd.ccb_tpcid = tci.tci_tpcid  and 
             cccd.ccb_codemp = eci.eci_codemp  and 
			 cccd.ccb_estcpbt = ''V'' and 
             cccd.ccb_estid = eci.eci_estid
             and ccb_codemp = ' + CONVERT(VARCHAR,@codemp) +'
             and eci_idid = ' + CONVERT(VARCHAR,@idioma) +'
             and tci_idid = ' + CONVERT(VARCHAR,@idioma) 
	-- Cliente
if @pclid is not null
	begin
		set @query = @query + ' and ccb_pclid= ' +  CONVERT(VARCHAR,@pclid)
	end
	
	-- Deudor
if @ctcid is not null
begin
	set @query = @query + ' and ccb_ctcid= ' +  CONVERT(VARCHAR,@ctcid)
end
  
set @query = @query + ') as tabla  ) as t
  where  row > 0 '

if @where is not null
begin
set @query = @query + @where;
end

 --select @query
 exec(@query)	
	

END
