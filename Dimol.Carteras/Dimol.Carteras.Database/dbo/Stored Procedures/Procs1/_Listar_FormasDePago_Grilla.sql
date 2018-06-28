CREATE PROCEDURE [dbo].[_Listar_FormasDePago_Grilla]
(
@codemp int,
@idid int,
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
  (' set @query = @query + 'SELECT [FPI_CODEMP] Codemp        
	  ,[FPI_FRPID] IdFP        
	  ,[FPI_IDID] Idioma        
	  ,[FPI_NOMBRE] Nombre  
	  ,f.FRP_DIASVENC DiasVenc
	  ,[FRP_FECESP] IngFV
	  ,[FRP_CUOTAS] IngCuotas
	  ,[FRP_TIPCPBT] Tipo
	  FROM [dbo].[FORMAS_PAGO_IDIOMAS],[dbo].[FORMAS_PAGO] f    
	  where f.FRP_FRPID = [FPI_FRPID] and f.FRP_CODEMP = [FPI_CODEMP] and [FPI_CODEMP] = '+ CONVERT(VARCHAR,@codemp) +'
   '
   
   set @query = @query +')as tabla ) as t
  where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
