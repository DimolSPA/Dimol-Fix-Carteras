CREATE PROCEDURE [dbo].[_Listar_FormasDePago_Grilla_Count]
(
@codemp int,
@idid int,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10)
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
  
set @query = '  select count(Nombre) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  (' set @query = @query + 'SELECT [FPI_CODEMP] Codemp        
	  ,[FPI_FRPID] IdFP        
	  ,[FPI_IDID] Idioma        
	  ,[FPI_NOMBRE] Nombre  
	  ,f.FRP_DIASVENC DiasVenc    
	  FROM [dbo].[FORMAS_PAGO_IDIOMAS],[dbo].[FORMAS_PAGO] f    
	  where f.FRP_FRPID = [FPI_FRPID] and f.FRP_CODEMP = [FPI_CODEMP] and [FPI_CODEMP] = '+ CONVERT(VARCHAR,@codemp) +'
   '
   
   set @query = @query +')as tabla ) as t
  where  row >= 0' 

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
