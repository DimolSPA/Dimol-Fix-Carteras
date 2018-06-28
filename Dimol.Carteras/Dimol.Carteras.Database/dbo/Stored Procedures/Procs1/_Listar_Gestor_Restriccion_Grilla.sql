-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Gestor_Restriccion_Grilla]
(
@codemp int,
@codsuc int,
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

set @query = @query +'SELECT grn_usrid Usrid,   
						grn_gesid Gesid,   
						usr_nombre NombreUsuario,   
						ges_nombre NombreGestor, 
						grn_desde FechaDesde, 
						grn_hasta FechaHasta
						FROM gestor_restriccion_nula grn,   
						usuarios,   
						gestor
						WHERE  usr_codemp = grn_codemp  and  
						usr_usrid = grn_usrid  and  
						ges_codemp = grn_codemp  and  
						ges_sucid = grn_sucid  and  
						ges_gesid = grn_gesid  and  
						grn_codemp = ' + CONVERT(VARCHAR,@codemp) +'
						and grn.grn_sucid =  ' + CONVERT(VARCHAR,@codsuc ) 
			
set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
