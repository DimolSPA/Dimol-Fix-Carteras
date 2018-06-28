-- =============================================                                  
-- Author:  Pablo Leyton                                                
-- Create date: 13-11-2014                                                
-- Description: Procedimiento para cantidad ente asignado para jQgrid                                  
-- =============================================                       
                    
                               
CREATE PROCEDURE [dbo].[_Listar_EnteAsignado_Grilla_Count]                                        
(                                  
    @codemp int,                                  
    @idid int,                                  
    @where varchar(1000),                                  
    @sidx varchar(255),                                  
    @sord varchar(10),                                  
    @inicio int,                                  
    @limite int ,
    @tipo varchar(1)                                 
)                                  
AS                                  
BEGIN                                  
 SET NOCOUNT ON;                                  
               
declare @condicion varchar(1000);  
if (@tipo != '')  
begin  
 if @tipo='S'   
  set @condicion = ' and entes_judicial.ETJ_SINDICO = ''S'' '  
 if @tipo='A'   
  set @condicion = ' and entes_judicial.ETJ_ABOGADO = ''S'' '  
 if @tipo='P'   
  set @condicion = ' and entes_judicial.ETJ_PROCURADOR = ''S'' '  
 if @tipo='R'   
  set @condicion = ' and entes_judicial.ETJ_RECEPTOR = ''S'' '   
end                                             
                      
declare @query varchar(7000);                                  
                          
set @query = '  select count(*) count from                                  
    (select *,ROW_NUMBER() OVER (ORDER BY Id asc )  as row from                                         
    (SELECT DISTINCT     
     entes_judicial.etj_codemp as Codemp,    
        entes_judicial.etj_etjid as Id,       
              provcli.pcl_nomfant as Nombre,    
              entes_judicial.ETJ_SINDICO as Sindico,    
              entes_judicial.ETJ_ABOGADO as Abogado,    
              entes_judicial.ETJ_PROCURADOR as Procurador,    
              entes_judicial.ETJ_RECEPTOR as Receptor    
              FROM entes_judicial,       
              provcli,       
              entejud_rol    
              WHERE  provcli.pcl_codemp = entes_judicial.etj_codemp  and      
              provcli.pcl_pclid = entes_judicial.etj_pclid  and      
              entejud_rol.ejr_codemp = entes_judicial.etj_codemp  and      
              entejud_rol.ejr_etjid = entes_judicial.etj_etjid  and      
              entes_judicial.etj_codemp = ' + CONVERT(VARCHAR,@codemp) + '    
              ' +  @condicion + '  
                     
                  
     UNION    
         
      SELECT DISTINCT     
    empleados.epl_codemp,    
    entes_judicial.etj_etjid,       
    epl_nombre + '' '' + epl_apepat ,    
    entes_judicial.ETJ_SINDICO as Sindico,    
    entes_judicial.ETJ_ABOGADO as Abogado,    
    entes_judicial.ETJ_PROCURADOR as Procurador,    
    entes_judicial.ETJ_RECEPTOR as Receptor     
  FROM empleados,       
  entes_judicial,       
  entejud_rol    
        WHERE  entes_judicial.etj_codemp = empleados.epl_codemp  and      
        entes_judicial.etj_emplid = empleados.epl_emplid  and      
        entejud_rol.ejr_codemp = entes_judicial.etj_codemp  and      
        entejud_rol.ejr_etjid = entes_judicial.etj_etjid  and      
        empleados.epl_codemp = ' + CONVERT(VARCHAR,@codemp) + '     
        ' +  @condicion + '  
  ) as tabla  ) as t                                  
  where  row > 0'                              
                                  
if @where is not null                                  
begin                                  
set @query = @query + @where;                                  
end                                  
                                  
--select @query                                  
exec(@query)                                   
                                   
                                  
END
