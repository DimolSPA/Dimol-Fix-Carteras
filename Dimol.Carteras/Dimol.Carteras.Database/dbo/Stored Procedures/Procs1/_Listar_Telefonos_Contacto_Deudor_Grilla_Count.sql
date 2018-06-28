-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Telefonos_Contacto_Deudor_Grilla_Count]
(
@codemp int,
@ctcid integer, 
@telefono varchar(20),
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

set @query = '  select sum(count)  count from
  (select *,ROW_NUMBER() OVER (ORDER BY count asc) as row from    
  ('

set @query = @query +'SELECT count(vdtc.tci_nombre) count
             FROM view_deudores_telefonos_contactos vdtc
             WHERE  vdtc.dct_codemp =' + CONVERT(VARCHAR,@codemp)+ '
             and vdtc.dct_ctcid =' + CONVERT(VARCHAR,@ctcid )+ '  
             and vdtc.ddc_estado =''A'''
if @telefono is not null
begin
set @query = @query + ' and   vdtc.dct_numero = '+@telefono
end
set @query = @query +' union select count(d.numero) count
						from deudores_telefonos_sitrel d
						where d.codemp =' + CONVERT(VARCHAR,@codemp)+ '
						and d.ctcid = ' + CONVERT(VARCHAR,@ctcid )
						
set @query = @query +') as tabla  ) as t
  where  row > 0 and row <= 100000000'

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
