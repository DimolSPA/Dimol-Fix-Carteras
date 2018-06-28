-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
create PROCEDURE [dbo].[_Listar_Contacto_Deudor_Grilla_Count]
(
@codemp int,
@ctcid integer, 
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

set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY count asc) as row from    
  ('

set @query = @query +'SELECT count(dc.ddc_ddcid) count
						FROM deudores_contactos dc,   
						tipos_contacto_idiomas tci
						WHERE  dc.ddc_codemp = tci.tci_codemp  and  
						dc.ddc_ticid = tci.tci_ticid  and  
						dc.ddc_codemp = ' + CONVERT(VARCHAR,@codemp)+ '
						and dc.ddc_ctcid = ' + CONVERT(VARCHAR,@ctcid )+ '
						and tci.tci_idid = ' + CONVERT(VARCHAR,@idioma)

set @query = @query +') as tabla  ) as t
  where  row >= 0 and row <= 10000000'

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
