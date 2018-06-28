-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Email_Deudor_Grilla]
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
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('

set @query = @query +'select ddm_codemp Codemp, ddm_ctcid Ctcid,
		ddm_mail Mail, 
		ddm_tipo IdTipoMail, 
		ddm_masivo Masivo, 
		CASE ddm_tipo
		WHEN ''P'' THEN ''Particular''
		WHEN ''E'' THEN ''Empresa''
		ELSE ''''
		END as TipoMail
		from deudores_mail 
		where ddm_codemp = ' + CONVERT(VARCHAR,@codemp)+ ' 
		and ddm_ctcid = ' + CONVERT(VARCHAR,@ctcid )
		--order by ddm_tipo, ddm_mail

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
