﻿-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Telefonos_Deudor_Grilla_Count]
(
@codemp int,
@ctcid integer, 
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
  ('

set @query = @query +'select count(ddt_numero) count
			from deudores_telefonos 
			where ddt_codemp = ' + CONVERT(VARCHAR,@codemp)+ '
			and ddt_ctcid = ' + CONVERT(VARCHAR,@ctcid ) 

set @query = @query +') as tabla  ) as t
  where  row > 0 and row <= 100000000'

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END