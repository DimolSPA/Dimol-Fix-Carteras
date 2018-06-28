-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Contacto_Deudor_Grilla]
(
@codemp int,
@ctcid integer, 
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

set @query = @query +'SELECT dc.DDC_CODEMP Codemp,
						dc.DDC_CTCID Ctcid, 
						dc.ddc_ddcid Ddcid,   
						dc.ddc_nombre Nombre,   
						tci.tci_nombre Tipo, 
						ddc_estado Estado,
						CASE ddc_estado
								WHEN ''A'' THEN ''Activo''
								WHEN ''D'' THEN ''Desactivado''
								ELSE ''''
								END as EstadoContacto, 
						(select com_nombre  from COMUNA where COM_COMID = dc.ddc_comid ) as Comuna, 
						dc.ddc_direccion Direccion
						FROM deudores_contactos dc,   
						tipos_contacto_idiomas tci
						WHERE  dc.ddc_codemp = tci.tci_codemp  and  
						dc.ddc_ticid = tci.tci_ticid  and  
						dc.ddc_codemp = ' + CONVERT(VARCHAR,@codemp)+ '
						and dc.ddc_ctcid = ' + CONVERT(VARCHAR,@ctcid )+ '
						and tci.tci_idid = ' + CONVERT(VARCHAR,@idioma)

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
