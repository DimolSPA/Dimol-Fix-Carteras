
CREATE PROCEDURE [dbo].[_Listar_Roles_Grilla]
(
	@codemp int,
	@idioma int,
	@idCompetencia int = 3, -- Por defecto es "Civil"
	@rol_numero varchar(255),
	@rol_trbid int,
	@rol_tcaid int,
	@rol_pclid int,
	@ctc_rut varchar(255),
	@ctc_nomfant varchar(255),
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
		(' set @query = @query + 'Select * from view_rol 
		where rol_codemp = ' + CONVERT(VARCHAR,@codemp) + '
		and eci_idid = ' + CONVERT(VARCHAR,@idioma) + '
		and tci_idid = ' + CONVERT(VARCHAR,@idioma) + '
		and mji_idid = ' + CONVERT(VARCHAR,@idioma) + '
		and ID_COMPETENCIA = ' + CONVERT(VARCHAR,@idCompetencia)
	  
	if @rol_numero is not null and @rol_numero != ''
	begin
		set @query = @query + ' and rol_numero like ''%' +@rol_numero + '%''';
	end

	if @rol_trbid is not null and @rol_trbid > 0
	begin
		set @query = @query + ' and rol_trbid='+ CONVERT(VARCHAR,@rol_trbid) + ')';
	end

	if @rol_tcaid is not null and @rol_tcaid > 0
	begin
		set @query = @query + ' and rol_tcaid='+ CONVERT(VARCHAR,@rol_tcaid);
	end

	if @rol_pclid is not null and @rol_pclid > 0
	begin
		set @query = @query + ' and rol_pclid='+ CONVERT(VARCHAR,@rol_pclid);
	end

	if @ctc_rut is not null and @ctc_rut != ''
	begin
		set @query = @query + ' and ctc_rut like ''%' +@ctc_rut + '%''';
	end

	if @ctc_nomfant is not null and @ctc_nomfant != ''
	begin
		set @query = @query + ' and ctc_nomfant like ''%' +@ctc_nomfant + '%''';
	end
	
	set @query = @query +')as tabla ) as t
	where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

	if @where is not null
	begin
		set @query = @query + @where;
	end

	exec(@query)	
END
