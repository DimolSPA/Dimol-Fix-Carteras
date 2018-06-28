-- =============================================
-- Author:		Rodrigo GarridoA
-- Create date: 26-08-2014
-- Description:	Procedimiento para listar periodos para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_MonedasValores_Grilla]
(
@codemp int,
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
  (' set @query = @query + 'SELECT DISTINCT [MNV_CODEMP] Codemp        
	  ,[MON_NOMBRE] nombre
	  ,[MNV_FECHA] fecha        
	  ,[MNV_VALOR] valor
	  ,[MNV_CODMON] codMoneda        
	  ,CONVERT(varchar(5), MNV_CODEMP) + '''' + CONVERT(varchar(5), MNV_CODMON) + CONVERT(VARCHAR(30), MNV_FECHA, 112)Id
	  FROM [dbo].[MONEDAS_VALORES],[dbo].[MONEDAS] m 
	  WHERE [MNV_CODEMP] = '+ CONVERT(VARCHAR,@codemp)+' AND m.MON_CODMON = MNV_CODMON
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
