-- =============================================
-- Author:		Felipe Muñoz
-- Create date: 04-06-2014
-- Description:	Procedimiento para listar telefonos por deudor y cliente para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Historial_Deudor_Grilla_Count]
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
  (select count (*) count from    
  ('

set @query = @query +'SELECT distinct ai.aci_nombre,   
         cartera_clientes_estados_acciones.cea_fecha,   
         substring(cea_comentario, 1, 1000) as comentario,   
         u.usr_nombre,   
         tipos_contacto.tic_nombre,   
         deudores_contactos.ddc_nombre,
         aci_accid as accion,
         0 as estado,
         0 as agrupa,  
         '''' as utiliza,
         tic_ticid,
         cea_ddcid as contacto,
         cea_pclid     as provcli,
         cea_ctcid as ctcid,
         pcl_rut,
         pcl_nomfant,
         ctc_numero,
         ctc_digito,
         ctc_nomfant, 
         cea_telefono  
    FROM {oj cartera_clientes_estados_acciones LEFT OUTER JOIN deudores_contactos ON cartera_clientes_estados_acciones.cea_codemp = deudores_contactos.ddc_codemp AND cartera_clientes_estados_acciones.cea_ctcid = deudores_contactos.ddc_ctcid AND cartera_clientes_estados_acciones.cea_ddcid = deudores_contactos.ddc_ddcid LEFT OUTER JOIN tipos_contacto ON deudores_contactos.ddc_codemp = tipos_contacto.tic_codemp AND deudores_contactos.ddc_ticid = tipos_contacto.tic_ticid} ,   
         acciones_idiomas ai,   
         usuarios u,
         provcli,
         deudores   
   WHERE ( cartera_clientes_estados_acciones.cea_codemp = ai.aci_codemp ) and  
         ( cartera_clientes_estados_acciones.cea_accid = ai.aci_accid ) and  
         ( u.usr_codemp = cartera_clientes_estados_acciones.cea_codemp ) and  
         ( u.usr_usrid = cartera_clientes_estados_acciones.cea_usrid ) and  
         ( cea_codemp = pcl_codemp ) and  
         ( cea_pclid = pcl_pclid ) and  
         ( cea_codemp = ctc_codemp ) and  
         ( cea_ctcid = ctc_ctcid ) and  
         ( ( cea_codemp = ' + CONVERT(VARCHAR,@codemp)+ ') AND  
         ( cea_pclid = ' + CONVERT(VARCHAR,@pclid)+ ' ) AND  
         ( cea_ctcid = ' + CONVERT(VARCHAR,@ctcid )+ ')   AND
         ( ai.aci_idid = ' + CONVERT(VARCHAR,@idioma) +')   
         )'
if @tipo = 'P'
begin
set @query = @query + ' and u.USR_PRFID not in (6,7) '
end  

if @tipo = 'J'
begin
set @query = @query + ' and u.USR_PRFID in (6,7) '
end     

set @query = @query + 'Union

  SELECT DISTINCT eci.eci_nombre,   
         cceh.ceh_fecha,   
         substring(ceh_comentario, 1, 1000) as comentario,   
         u.usr_nombre ,
         '''',
         '''',
         0 as accion,
         eci_estid as estado,
         ect_agrupa as agrupa,
         ect_utiliza as utiliza,
         0,
         0 as contacto,
         ceh_pclid     as provcli,
         ceh_ctcid as ctcid,
         pcl_rut,
         pcl_nomfant,
         ctc_numero,
         ctc_digito,
         ctc_nomfant,
         0  
    FROM cartera_clientes_estados_historial cceh,   
         estados_cartera_idiomas eci,   
         usuarios u,
         estados_cartera ec,
         provcli,
         deudores     
   WHERE ( cceh.ceh_codemp = eci.eci_codemp ) and  
         ( cceh.ceh_estid = eci.eci_estid ) and  
         ( u.usr_codemp = cceh.ceh_codemp ) and  
         ( u.usr_usrid = cceh.ceh_usrid ) and  
         ( ceh_codemp = pcl_codemp ) and  
         ( ceh_pclid = pcl_pclid ) and  
         ( ceh_codemp = ctc_codemp ) and  
         ( ceh_ctcid = ctc_ctcid ) and  
         ( ( cceh.ceh_codemp = ' + CONVERT(VARCHAR,@codemp)+ ') AND  
         ( ec.ect_codemp = eci_codemp ) AND  
         ( ec.ect_estid = eci_estid )  AND
         ( cceh.ceh_pclid = ' + CONVERT(VARCHAR,@pclid)+ ' ) AND  
         ( cceh.ceh_ctcid =' + CONVERT(VARCHAR,@ctcid )+ ')  AND
         ( eci.eci_idid =  ' + CONVERT(VARCHAR,@idioma) +' )     
         )'
if @tipo = 'P'
begin
set @query = @query + ' and u.USR_PRFID not in (6,7) '
end  

if @tipo = 'J'
begin
set @query = @query + ' and u.USR_PRFID in (6,7) '
end 

set @query = @query +') as tabla  ) as t
  where  1=1'

if @where is not null
begin
set @query = @query + @where;
end

--select @query
exec(@query)	
	

END
