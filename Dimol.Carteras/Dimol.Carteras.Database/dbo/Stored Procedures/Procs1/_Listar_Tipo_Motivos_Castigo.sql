CREATE PROCEDURE [dbo].[_Listar_Tipo_Motivos_Castigo]
(
@codemp int,
@idioma int,
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
set @query = 'select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (	' 
  
set @query = @query + 'SELECT distinct tmc.TMC_TMCID TIPOID,  
	tmc.TMC_NOMBRE NOMBRE
	FROM TIPOS_MOTIVOS_CASTIGOS tmc
	join tipos_motivos_castigos_idiomas tmi
	on tmc.TMC_CODEMP = tmi.TMI_CODEMP
	WHERE  tmi.tmi_codemp = '+CONVERT(VARCHAR,@codemp) + '
  and tmi.TMI_IDID = '+CONVERT(VARCHAR,@idioma) + ''
 
set @query = @query + ') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

 exec(@query)	
END
