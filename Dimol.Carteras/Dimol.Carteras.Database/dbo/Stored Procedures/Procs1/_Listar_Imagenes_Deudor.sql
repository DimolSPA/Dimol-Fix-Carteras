-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Imagenes_Deudor]
(
@codemp int,
@pclid integer, 
@ctcid int
)
AS
BEGIN
	SET NOCOUNT ON;

declare @query varchar(7000);
declare @max_fotos int = 20
declare @cantidad_fotos int = 0

set @cantidad_fotos = (Select count(tpi_nombre)
             FROM cartera_clientes_cpbt_doc_imagenes cccdi,  
             tipos_imagenes_cpbtdoc_idiomas tici
             WHERE  cccdi.cdi_codemp = tici.tpi_codemp  and 
             cccdi.cdi_tpcid = tici.tpi_tpcid  and 
             cccdi.cdi_codemp = @codemp
             and cccdi.cdi_pclid = @pclid
              and cccdi.cdi_ctcid = @ctcid)
              
set @max_fotos = (SELECT configuracion_sistema.cfs_valnum
    FROM configuracion_sistema  
   WHERE configuracion_sistema.cfs_cfsid = 20 )-- maximo fotos

if @cantidad_fotos > @max_fotos
begin
	set @cantidad_fotos = @max_fotos

end

set @query = 'Select top ' + CONVERT(VARCHAR,@cantidad_fotos)+ ' tici.tpi_nombre, cdi_imagen, cdi_cdid, cdi_codemp, cdi_pclid, cdi_ctcid, cdi_ccbid
                 FROM cartera_clientes_cpbt_doc_imagenes cccdi,  
                 tipos_imagenes_cpbtdoc_idiomas tici
                 WHERE  cccdi.cdi_codemp = tici.tpi_codemp  and 
                 cccdi.cdi_tpcid = tici.tpi_tpcid  and 
                 cccdi.cdi_codemp = ' + CONVERT(VARCHAR,@codemp)+ '
                 and cccdi.cdi_pclid = ' + CONVERT(VARCHAR,@pclid)+ '
                  and cccdi.cdi_ctcid =' + CONVERT(VARCHAR,@ctcid )


--select @query
exec(@query)	
	

END
