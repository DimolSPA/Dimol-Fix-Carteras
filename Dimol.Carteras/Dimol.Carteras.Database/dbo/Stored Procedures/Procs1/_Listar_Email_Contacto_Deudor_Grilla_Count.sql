-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Email_Contacto_Deudor_Grilla_Count]
(
@codemp int,
@ctcid integer, 
@email varchar(200),
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

set @query = '  select sum(count) from
  (select *,ROW_NUMBER() OVER (ORDER BY count asc) as row from    
  ('

set @query = @query +'SELECT count(vdnc.ddc_ctcid) count
             FROM view_deudores_mail_contactos vdnc
             WHERE  vdnc.ddc_codemp = ' + CONVERT(VARCHAR,@codemp)+ '
             and vdnc.ddc_ctcid = ' + CONVERT(VARCHAR,@ctcid )+ '
			and vdnc.ddc_estado =''A'''

if @email is not null
begin
set @query = @query + ' and   vdnc.dcm_mail = '+ @email
end

set @query = @query +' union select count(d.ctcid) count
						from deudores_mail_sitrel d
						where d.codemp = ' + CONVERT(VARCHAR,@codemp)+ '
						and d.ctcid = ' + CONVERT(VARCHAR,@ctcid )

set @query = @query +') as tabla  ) as t
  where  row >= 0 and row <= 10000000'

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
