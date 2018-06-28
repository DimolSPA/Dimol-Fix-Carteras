-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Doc_Rol_Grilla]
(
@codemp int,
@rolid int,
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

set @query = '  select * from
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from    
  ('

set @query = @query +'SELECT rdc_ccbid ccbid, 
			tci.tci_nombre Tipo,  
            cccd.ccb_numero Numero,  
            cccd.ccb_fecvenc FechaVencimiento,  
            rd.rdc_monto Monto,  
            rd.rdc_saldo Saldo
            FROM rol_documentos rd,  
            cartera_clientes_cpbt_doc cccd,  
            tipos_cpbtdoc_idiomas tci
            WHERE ( cccd.ccb_codemp = rd.rdc_codemp ) and 
            ( cccd.ccb_pclid = rd.rdc_pclid ) and 
            ( cccd.ccb_ctcid = rd.rdc_ctcid ) and 
            ( cccd.ccb_ccbid = rd.rdc_ccbid ) and 
            ( cccd.ccb_codemp = tci.tci_codemp ) and 
            ( cccd.ccb_tpcid = tci.tci_tpcid )   
            and rdc_codemp =' + CONVERT(VARCHAR,@codemp) +'
            and tci_idid =' + CONVERT(VARCHAR,@idioma) +'
            and rdc_rolid =' + CONVERT(VARCHAR,@rolid) 

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
