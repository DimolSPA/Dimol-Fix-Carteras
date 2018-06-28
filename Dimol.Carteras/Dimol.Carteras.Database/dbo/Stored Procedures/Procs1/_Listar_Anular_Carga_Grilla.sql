-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 27-04-2014
-- Description:	Procedimiento para listar acciones para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Anular_Carga_Grilla]
(
@codemp int ,
@estid int,

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
  (	' 
  
set @query = @query + 'SELECT DISTINCT provcli.pcl_codemp Codemp,
						provcli.pcl_pclid Pclid,   
						provcli.pcl_rut RutCliente,   
						provcli.pcl_nomfant NombreCliente,   
						cceh.ceh_fecha Fecha,
						Usuarios.USR_USRID IdUsuario,  
						Usuarios.usr_nombre Usuario
						FROM provcli,   
						cartera_clientes_cpbt_doc cccd,   
						cartera_clientes_estados_historial cceh,   
						deudores,   
						cartera_clientes,   
						Usuarios
						WHERE  cceh.ceh_codemp = cccd.ccb_codemp  and  
						cceh.ceh_pclid = cccd.ccb_pclid  and  
						cceh.ceh_ctcid = cccd.ccb_ctcid  and  
						cceh.ceh_ccbid = cccd.ccb_ccbid  and  
						cartera_clientes.ctc_codemp = deudores.ctc_codemp  and  
						cartera_clientes.ctc_ctcid = deudores.ctc_ctcid  and  
						cartera_clientes.ctc_codemp = cccd.ccb_codemp  and  
						cartera_clientes.ctc_pclid = cccd.ccb_pclid  and  
						cartera_clientes.ctc_ctcid = cccd.ccb_ctcid  and  
						cartera_clientes.ctc_codemp = provcli.pcl_codemp  and  
						cartera_clientes.ctc_pclid = provcli.pcl_pclid  and  
						usuarios.usr_codemp = cceh.ceh_codemp  and  
						usuarios.usr_usrid = cceh.ceh_usrid  
						and cccd.ccb_codemp =  ' + CONVERT(VARCHAR,@codemp) +'
						and cccd.ccb_estid =  ' + CONVERT(VARCHAR,@estid) +'
						and cccd.ccb_estcpbt = ''V'''
					

  
set @query = @query + ') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

 --select @query
 exec(@query)	
	

END
