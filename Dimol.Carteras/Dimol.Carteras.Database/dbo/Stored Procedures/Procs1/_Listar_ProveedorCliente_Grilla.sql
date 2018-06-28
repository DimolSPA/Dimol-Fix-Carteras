CREATE PROCEDURE [dbo].[_Listar_ProveedorCliente_Grilla]
(
	@codemp int,
	@tipo varchar(255),
	@nombre varchar(255),
	@apellidoPaterno varchar(255),
	@apellidoMaterno varchar(255),
	@rut varchar(255),
	@nombreFantasia varchar(255),
	@estado varchar(255),
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
		(' 
		
	set @query = @query + 'SELECT tpc_codemp as codemp, pcl_pclid as ID, pcl_rut as RUT, pcl_nombre as NOMBRE, 
		pcl_apepat as APELLIDOPATERNO, pcl_apemat as APELLIDOMATERNO, pcl_logo as LOGO, 
		pcl_nomfant as NOMBREFANTASIA, PCL_TIPCLI as TIPOCLIENTE 

		FROM PROVCLI, TIPOS_PROVCLI

		WHERE pcl_codemp = ' + CONVERT(VARCHAR,@codemp) + '
		AND pcl_codemp = tpc_codemp and pcl_tpcid = tpc_tpcid 
		AND (tpc_agrupa =''A'' or tpc_agrupa like ''%' +@tipo + '%'')'

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

		if @nombreFantasia is not null and @nombreFantasia != ''
		begin
			set @query = @query + ' and pcl_nomfant like ''%' +@nombreFantasia + '%''';
		end

		if @estado is not null and @estado != ''
		begin
			set @query = @query + ' and pcl_estado='+ CONVERT(VARCHAR,@estado);
		end

		set @query = @query +')as tabla ) as t
			where  row > '+ CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

	if @where is not null
	begin
		set @query = @query + @where;
	end

	select @query
	exec(@query)
END
