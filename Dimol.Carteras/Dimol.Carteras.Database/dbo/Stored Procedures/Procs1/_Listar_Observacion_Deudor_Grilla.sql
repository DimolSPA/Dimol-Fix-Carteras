-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Observacion_Deudor_Grilla]
(
@codemp int,
@pclid integer, 
@ctcid int,
@idioma int,
@tipo varchar(1),
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

set @query = @query +'SELECT distinct ai.aci_nombre Tipo,   
         cartera_clientes_estados_acciones.cea_fecha Fecha,   
         substring(cea_comentario, 1, 1000) as Comentario,   
         u.usr_nombre NombreUsuario,   
         tipos_contacto.tic_nombre TipoContacto,   
         deudores_contactos.ddc_nombre NombreContacto,
         aci_accid as Accion ,
         0 as Estado,
         0 as Agrupa ,  
         '''' as Utiliza,
         tic_ticid Ticid,
         cea_ddcid as Contacto,
         cea_telefono  Telefono
    FROM {oj cartera_clientes_estados_acciones LEFT OUTER JOIN deudores_contactos ON cartera_clientes_estados_acciones.cea_codemp = deudores_contactos.ddc_codemp AND cartera_clientes_estados_acciones.cea_ctcid = deudores_contactos.ddc_ctcid AND cartera_clientes_estados_acciones.cea_ddcid = deudores_contactos.ddc_ddcid LEFT OUTER JOIN tipos_contacto ON deudores_contactos.ddc_codemp = tipos_contacto.tic_codemp AND deudores_contactos.ddc_ticid = tipos_contacto.tic_ticid} ,   
         acciones_idiomas ai,   
         usuarios u,
         provcli,
         deudores   
   WHERE ( cartera_clientes_estados_acciones.cea_codemp = ai.aci_codemp ) and  
         ( cartera_clientes_estados_acciones.cea_accid = ai.aci_accid ) and  
         ( u.usr_codemp = cartera_clientes_estados_acciones.cea_codemp ) and  
         ( u.usr_usrid = cartera_clientes_estados_acciones.cea_usrid ) and  
         ( cea_codemp = pcl_codemp ) and    cea_accid = 11 AND
         ( cea_pclid = pcl_pclid ) and  
         ( cea_codemp = ctc_codemp ) and  
         ( cea_ctcid = ctc_ctcid ) and  
         ( ( cea_codemp = ' + CONVERT(VARCHAR,@codemp)+ ') AND  
         ( cea_pclid = ' + CONVERT(VARCHAR,@pclid)+ ' ) AND  
         ( cea_ctcid = ' + CONVERT(VARCHAR,@ctcid )+ ')   AND
         ( ai.aci_idid = ' + CONVERT(VARCHAR,@idioma) +')  
         )'

set @query = @query +') as tabla  ) as t
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
