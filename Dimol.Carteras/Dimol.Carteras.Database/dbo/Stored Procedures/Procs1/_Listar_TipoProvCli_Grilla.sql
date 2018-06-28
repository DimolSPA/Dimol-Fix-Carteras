﻿-- =============================================
-- Author:		Rodrigo Garrido
-- Create date: 25-09-2014
-- Description:	Procedimiento para listar TipoProvCli para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_TipoProvCli_Grilla]
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
  (' set @query = @query + 'SELECT tpc_codemp codemp, tpc_tpcid id, tpc_nombre nombre, tpc_agrupa agrupa
  from tipos_provcli 
  where tpc_codemp = '+ CONVERT(VARCHAR,@codemp)+'
   '
   
   set @query = @query +')as tabla ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END