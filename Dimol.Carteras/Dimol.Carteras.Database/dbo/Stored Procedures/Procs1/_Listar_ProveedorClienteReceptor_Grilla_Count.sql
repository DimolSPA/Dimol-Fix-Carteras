CREATE PROCEDURE [dbo].[_Listar_ProveedorClienteReceptor_Grilla_Count]
(
@codemp int,
@tipo varchar(255),
@nombre varchar(255),
@apellidoPaterno varchar(255),
@apellidoMaterno varchar(255),
@rut varchar(255),
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select count(ID) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'select pcl_pclid as ID, pcl_rut as RUT, pcl_nombre as NOMBRE, pcl_apepat as APELLIDOPATERNO,
  pcl_apemat as APELLIDOMATERNO, pcl_logo as LOGO, pcl_nomfant as NOMBREFANTASIA
	  	  
	  FROM provcli, tipos_provcli, entes_judicial
	   where pcl_codemp = ' + CONVERT(VARCHAR,@codemp) + '
	  and pcl_codemp = tpc_codemp and pcl_tpcid = tpc_tpcid 
	  and (tpc_agrupa =''A'' or tpc_agrupa like ''%' +@tipo + '%'')
	  and etj_pclid = pcl_pclid and etj_receptor = ''S'''
	   if @nombre is not null and @nombre != ''
		begin
			set @query = @query + ' and pcl_nombre like ''%' +@nombre + '%''';
		end
	
	  if @apellidoPaterno is not null and @apellidoPaterno != ''
		begin
			set @query = @query + ' and pcl_apepat like ''%' +@apellidoPaterno + '%''';
		end
		
	  if @apellidoMaterno is not null and @apellidoMaterno != ''
		begin
			set @query = @query + ' and pcl_apemat like ''%' +@apellidoMaterno + '%''';
		end
		
	  if @rut is not null and @rut != ''
		begin
			set @query = @query + ' and pcl_rut like ''%' +@rut + '%''';
		end
			  
		    
   set @query = @query +')as tabla ) as t
  where  row >= 0'

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
