-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[ST_Listar_Direcciones_Deudor_Grilla_Count]
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

set @query = '  select count (Fecha) as count from
  (select *,ROW_NUMBER() OVER ( order by Fecha asc) as row from    
  ('

set @query = @query +'SELECT [CTCID] Ctcid
							  ,[DIRECCION] Direccion
							  ,isnull([COMID],0) Comuna
							  ,[TIPO] TipoDireccion
							  ,[FECHA] Fecha
							  ,(select COM_CIUID from COMUNA where COM_COMID = comid) as Ciudad
							  ,(select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = comid)) as Region
							  ,(select REG_PAIID from REGION where REG_REGID = (select CIU_REGID from CIUDAD where CIU_CIUID = (select COM_CIUID from COMUNA where COM_COMID = comid))) as Pais   
							  ,[ORIGEN]
							  ,[ENVIADO]
						  FROM [DEUDORES_DIRECCION_SITREL] 
						  where [CODEMP] =' + CONVERT(VARCHAR,@codemp)+ '
						  and [CTCID]=' + CONVERT(VARCHAR,@ctcid )

set @query = @query +') as tabla  ) as t
  where  row > 0'

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
