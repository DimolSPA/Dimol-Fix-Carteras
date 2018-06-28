-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar documentos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Trae_CpbtDoc_Clientes_Grilla_Count]
(
@codemp int,
@pclid integer, 
@ctcid integer, 
@estcpbt char(1),
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

if @sidx = 'FechaVencimiento' and  @pclid = 424
begin
	set @sidx = 'NumeroCpbt'
end

set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ctcid ' + @sord+ ') as row from    
  ('

if @pclid = 424
begin
set @query = @query +'SELECT count (ccb_ctcid) ctcid
FROM cartera_clientes_documentos_cpbt_doc       
WHERE ( ccb_codemp = '+ CONVERT(VARCHAR,@codemp)+ ') 
AND             ( ccb_pclid = '+ CONVERT(VARCHAR,@pclid)  +' ) 
AND             ( ccb_ctcid = '+ CONVERT(VARCHAR,@ctcid)  + ' 
and ccb_estcpbt = '''+ @estcpbt +'''
and tci_idid = '+ CONVERT(VARCHAR,@idid)+ '  
and eci_idid  = '+ CONVERT(VARCHAR,@idid)+ '  
and mci_idid =  '+ CONVERT(VARCHAR,@idid)+ '  
and eci_estid > 1 )'
--Order by ccb_numero asc
end 
else if @pclid = 22
begin
set @query = @query +'SELECT  count (ccb_ctcid) ctcid
FROM cartera_clientes_documentos_cpbt_doc       
WHERE ( ccb_codemp = '+ CONVERT(VARCHAR,@codemp)+ ') 
AND             ( ccb_pclid = '+ CONVERT(VARCHAR,@pclid)  +' ) 
AND             ( ccb_ctcid = '+ CONVERT(VARCHAR,@ctcid)  + ' 
and ccb_estcpbt = '''+ @estcpbt +'''
and tci_idid = '+ CONVERT(VARCHAR,@idid)+ '  
and eci_idid  = '+ CONVERT(VARCHAR,@idid)+ '  
and mci_idid =  '+ CONVERT(VARCHAR,@idid)+ '  
and eci_estid > 1 )'  
--Order by datediff(day, ccb_fecvenc, getdate()) desc 
end
else
begin
set @query = @query +'SELECT  count (ccb_ctcid) ctcid
FROM cartera_clientes_documentos_cpbt_doc       
WHERE ( ccb_codemp = '+ CONVERT(VARCHAR,@codemp)+ ') 
AND             ( ccb_pclid = '+ CONVERT(VARCHAR,@pclid)  +' ) 
AND             ( ccb_ctcid = '+ CONVERT(VARCHAR,@ctcid)  + ' 
and ccb_estcpbt = '''+ @estcpbt +'''
and tci_idid = '+ CONVERT(VARCHAR,@idid)+ '  
and eci_idid  = '+ CONVERT(VARCHAR,@idid)+ '  
and mci_idid =  '+ CONVERT(VARCHAR,@idid)+ '  
and eci_estid > 1 )'
--  Order by ccb_fecvenc asc 
end


  
set @query = @query +') as tabla  ) as t
  where  row >= 1 and row <= 100000000'

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
