-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar acciones para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Acciones_Grilla]
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
  ('
  
-- Acá va la query de la aplicación
set @query = @query +	'SELECT distinct [ACI_CODEMP] Codemp        
	  ,[ACI_ACCID] IdAccion        
	  ,[ACI_IDID] Idioma        
	  ,[ACI_NOMBRE] Nombre  
	  ,[dbo].[_Trae_Agrupa_Accion] ([ACC_AGRUPA],[ACI_IDID])  Agrupa    
	  FROM [dbo].[ACCIONES_IDIOMAS],[dbo].[ACCIONES] a    
	  where a.ACC_ACCID = [ACI_ACCID]    and [ACC_CODEMP] = [ACI_CODEMP]'
-- Acá va la query de la aplicación	  
	  
set @query = @query + ') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
 exec(@query)	
	

END
