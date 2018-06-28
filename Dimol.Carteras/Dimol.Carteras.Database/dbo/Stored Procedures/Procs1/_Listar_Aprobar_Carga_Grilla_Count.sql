-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar acciones para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Aprobar_Carga_Grilla_Count]
(
@codemp int ,
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
--declare @provcli_consulta int = (Select PCC_PCLID_VER FROM PROVCLI_CONSULTA  where pcc_usrid = @usrid)

  
set @query = '  select count(Ccbid) count from
  (select *,ROW_NUMBER() OVER (ORDER BY Ccbid asc) as row from    
  (	' 
  
set @query = @query + 'SELECT ccb_pclid Pclid, ccb_ctcid Ctcid,  ccb_ccbid Ccbid, 
						pcl_rut RutCliente,   
						pcl_nomfant NombreCliente,   
						ctc_rut RutDeudor,   
						ctc_nomfant NombreDeudor,   
						tci_nombre TipoDocumento,   
						ccb_numero Numero,   
						ccb_fecdoc FechaDocumento,   
						ccb_fecvenc FechaVencimiento,   
						ccb_asignado MontoAsignado,   
						ccb_monto Monto,   
						ccb_saldo Saldo, 
						ccb_fecing FechaIngreso
						FROM cartera_clientes_documentos_cpbt_doc
						WHERE  ccb_codemp =  ' + CONVERT(VARCHAR,@codemp) +'
						and ccb_tipcart in (1,2)    
						and eci_estid = 1    
						and ccb_estcpbt = ''V'''  

  
set @query = @query + ') as tabla  ) as t
  where  row >= 0'

if @where is not null
begin
set @query = @query + @where;
end

-- select @query
 exec(@query)	
	

END
