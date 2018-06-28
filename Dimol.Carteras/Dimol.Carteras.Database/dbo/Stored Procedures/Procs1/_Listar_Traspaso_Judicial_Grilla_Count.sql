-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Traspaso_Judicial_Grilla_Count]
(
@codemp int,
@idioma int,
@perfil int ,
@where varchar(1000),
@sidx varchar(255),
@sord varchar(10),
@inicio int,
@limite int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(8000) = '',
@estid int,
@tpcid int,
@totUF numeric(15,2),
@camUF numeric(15,2),
@camDol numeric(15,2),
@monUF int

select @estid=EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = @codemp and EMC_EMCID = 66
select @totUF = EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = @codemp and EMC_EMCID = 93
select @monUF=EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = @codemp and EMC_EMCID = 35
--select @camUF=   EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = ccb_codemp and EMC_EMCID = 35
--select EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = 1 and EMC_EMCID = 18

        
select top 1 @camUF =  mnv_valor from monedas_valores where mnv_codemp= @codemp
and mnv_codmon= @monUF and mnv_fecha <= CONVERT(datetime, CONVERT (char(10), getdate(), 103),103) order by MNV_FECHA desc

select top 1 @camDol =  mnv_valor from monedas_valores where mnv_codemp= @codemp
and mnv_codmon= (select EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = @codemp and EMC_EMCID = 34) 
and mnv_fecha <= CONVERT(datetime,CONVERT (char(10), getdate(), 103),103) order by MNV_FECHA desc


--select @estid,@tpcid,@totUF,@camUF,@camDol

set @query = 'select count (ccb_pclid) count from
  (select *,ROW_NUMBER() OVER (ORDER BY ccb_pclid asc) as row from    
  ('

set @query = @query + 'SELECT ccb_pclid,   
ccb_ctcid,   
pcl_nomfant,   
ctc_rut,   
ctc_nomfant,   
sum(ccb_saldo) as total 
FROM cartera_clientes_documentos_cpbt_doc,   
idiomas
WHERE  tci_idid = idiomas.idi_idid  and  
eci_idid = idiomas.idi_idid  and  
mci_idid = idiomas.idi_idid  and  
ccb_codemp = '+ convert(char,@codemp) + 
' and ccb_estcpbt = ''V''
and ESTIJ = '+ convert(char,@estid) +
' and idiomas.idi_idid = '+ convert(char,@idioma) + 
' and ccb_codmon = (select EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = ccb_codemp and EMC_EMCID = 18)
group by ccb_pclid, ccb_ctcid, pcl_nomfant, ctc_rut, ctc_nomfant '
If @perfil < 4 
begin
    set @query = @query + 'having sum(ccb_saldo) /'+ convert(char,@camUF )+ ' >= ' + convert(char,@totUF)
End 

set @query = @query + ' union ' 

set @query = @query + 'SELECT ccb_pclid,   
ccb_ctcid,   
pcl_nomfant,   
ctc_rut,   
ctc_nomfant,   
sum(ccb_saldo) as total 
FROM cartera_clientes_documentos_cpbt_doc,   
idiomas
WHERE  tci_idid = idiomas.idi_idid  and  
eci_idid = idiomas.idi_idid  and  
mci_idid = idiomas.idi_idid  and  
ccb_codemp = '+ convert(char,@codemp) + 
' and ccb_estcpbt = ''V''
and ESTIJ = '+ convert(char,@estid) +
' and idiomas.idi_idid = '+ convert(char,@idioma) +
' and ccb_codmon = '+ convert(char,@monUF) +
' group by ccb_pclid, ccb_ctcid, pcl_nomfant, ctc_rut, ctc_nomfant '
If @perfil < 4 
begin
	set @query = @query + 'having sum(ccb_saldo)  >=' + convert(char,@totUF)
End

set @query = @query + ' union '

set @query = @query + 'SELECT ccb_pclid,   
ccb_ctcid,   
pcl_nomfant,   
ctc_rut,   
ctc_nomfant,   
sum(ccb_saldo) as total 
FROM cartera_clientes_documentos_cpbt_doc,   
idiomas
WHERE  tci_idid = idiomas.idi_idid  and  
eci_idid = idiomas.idi_idid  and  
mci_idid = idiomas.idi_idid  and  
ccb_codemp = '+ convert(char,@codemp) + 
' and ccb_estcpbt = ''V''
and ESTIJ = '+ convert(char,@estid) +
' and idiomas.idi_idid = '+ convert(char,@idioma) +
' and ccb_codmon = (select EMC_VALNUM from EMPRESA_CONFIGURACION where EMC_CODEMP = ccb_codemp and EMC_EMCID = 34)
group by ccb_pclid, ccb_ctcid, pcl_nomfant, ctc_rut, ctc_nomfant '
If @perfil < 4 
begin
	set @query = @query + 'having sum(ccb_saldo * '+ convert(char,@camDol) + ' ) / '+ convert(char,@camUF) + ' >= '+ convert(char,@totUF) 
End


set @query = @query +') as tabla  ) as t
  where  row > 0'

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
