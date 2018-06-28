-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Comprobante_Aceptar_Grilla_Count]
(
@codemp int,
@idioma int,
@codsuc int ,
@tipo varchar(2) ,
@estado varchar(2),
@cartera varchar(2),
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(8000) = ''
declare @query2 varchar(8000)= ''
declare @query_final varchar(8000)=''


set @query = 'select count(Numero) count from
  (select *,ROW_NUMBER() OVER (ORDER BY Numero asc) as row from    
  ('

set @query = @query + 'SELECT vcc.cbc_tpcid IdTipoDocumento,   
vcc.cbc_numero Numero,   
 vcc.pcl_rut Rut,   
vcc.pcl_nomfant NombreFantasia,   
vcc.tci_nombre TipoDocumento,   
vcc.cbc_numprovcli NumeroCliente,   
vcc.cbc_feccpbt Fecha,   
vcc.mon_nombre Moneda,   
vcc.cbc_final Monto,   
ccc.clb_contable Contable,   
ccc.clb_cartcli Cartera,   
ccc.clb_findeuda FinDeuda, 
cbc_tipcambio TipoCambio, 
cbc_feccont FechaContable,
(select top 1 i.INS_NOMBRE 
from DETALLE_COMPROBANTES d
inner join INSUMOS i
on i.INS_CODEMP = d.DCC_CODEMP
and i.INS_INSID = d.DCC_INSID
where d.DCC_CODEMP = vcc.cbc_codemp
and d.DCC_TPCID = vcc.cbc_tpcid
and d.DCC_NUMERO=vcc.cbc_numero) Gestion,
(select USR_NOMBRE  
from CABACERA_COMPROBANTES_ESTADOS e 
inner join USUARIOS u
on u.USR_CODEMP = e.CBE_CODEMP
and u.USR_USRID = e.CBE_USRID
where e.CBE_ESTADO = ''E''
and e.CBE_CODEMP = vcc.cbc_codemp
and e.CBE_TPCID = vcc.cbc_tpcid
and e.CBE_NUMERO = vcc.cbc_numero) Usuario
FROM view_cabecera_comprobantes vcc,   
clasificacion_cpbtdoc ccc,   
tipos_cpbtdoc tcc
WHERE  tcc.tpc_codemp = ccc.clb_codemp  and  
tcc.tpc_clbid = ccc.clb_clbid  and  
vcc.cbc_codemp = tcc.tpc_codemp  and  
vcc.cbc_tpcid = tcc.tpc_tpcid  and  
vcc.cbc_codemp =  '+ convert(char,@codemp) +'
and vcc.cbc_sucid = '+ convert(char,@codsuc) +'
and vcc.cbt_estado = '''+ @estado +'''
and vcc.idi_idid =  '+ convert(char,@idioma) +'
and clb_cartcli = '''+ @cartera +'''
and clb_tipcpbtdoc = '''+ @tipo +'''

and cbc_tpcid in (35,59)
union 
SELECT vcc.cbc_tpcid,   
vcc.cbc_numero,   
 vcc.pcl_rut,   
vcc.pcl_nomfant,   
vcc.tci_nombre,   
vcc.cbc_numprovcli,   
vcc.cbc_feccpbt,   
vcc.mon_nombre,   
vcc.cbc_final,   
ccc.clb_contable,   
ccc.clb_cartcli,   
ccc.clb_findeuda, cbc_tipcambio, cbc_feccont,
(select top 1 i.INS_NOMBRE 
from DETALLE_COMPROBANTES d
inner join INSUMOS i
on i.INS_CODEMP = d.DCC_CODEMP
and i.INS_INSID = d.DCC_INSID
where d.DCC_CODEMP = vcc.cbc_codemp
and d.DCC_TPCID = vcc.cbc_tpcid
and d.DCC_NUMERO=vcc.cbc_numero) Gestion,
(select USR_NOMBRE  
from CABACERA_COMPROBANTES_ESTADOS e 
inner join USUARIOS u
on u.USR_CODEMP = e.CBE_CODEMP
and u.USR_USRID = e.CBE_USRID
where e.CBE_ESTADO = ''E''
and e.CBE_CODEMP = vcc.cbc_codemp
and e.CBE_TPCID = vcc.cbc_tpcid
and e.CBE_NUMERO = vcc.cbc_numero) Usuario
FROM view_cabecera_comprobantes vcc,   
clasificacion_cpbtdoc ccc,   
tipos_cpbtdoc tcc
WHERE  tcc.tpc_codemp = ccc.clb_codemp  and  
tcc.tpc_clbid = ccc.clb_clbid  and  
vcc.cbc_codemp = tcc.tpc_codemp  and  
vcc.cbc_tpcid = tcc.tpc_tpcid  and  
vcc.cbc_codemp = '+ convert(char,@codemp) +'
and vcc.cbc_sucid =  '+ convert(char,@codsuc) +'
and vcc.cbt_estado = '''+ @estado +'''
and vcc.idi_idid =  '+ convert(char,@idioma) +'
and clb_cartcli <> '''+ @cartera +'''
and clb_tipcpbtdoc = '''+ @tipo +'''
and cbc_tpcid in (35,59)'

set @query = @query +') as tabla  ) as t
  where  row >= 1' 
  
if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
