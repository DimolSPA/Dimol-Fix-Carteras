-- =============================================                        
-- Author:  Pablo Leyton                        
-- Create date: 05-08-2014                        
-- Description: Procedimiento para listar Gestor para jQgrid                        
-- =============================================                     
              
CREATE PROCEDURE [dbo].[_Listar_Gestor_Grilla]                        
(                        
     @codemp int,                        
     @idid int,                        
     @where varchar(1000),                        
     @sidx varchar(255),                        
     @sord varchar(10),                        
     @inicio int,                        
     @limite int,          
     @sucursal int                 
     )                        
AS                        
BEGIN                        
 SET NOCOUNT ON;                        
                        
declare @query varchar(7000);                        
                          
set @query = 'select * from (
	select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from (   
    select * from (
    Select                 
    GES_CODEMP AS CODEMP,GES_SUCID AS CODSUC,GES_GESID AS ID,GES_NOMBRE AS NOMBRE,        
    GES_TELEFONO AS TELEFONO,GES_EMAIL AS EMAIL,      
    GES_TIPCART AS TIPOCARTID,      
    TIPOCART =(SELECT  Top 1 etiquetas_idiomas.eti_descripcion              
      FROM etiquetas,  etiquetas_idiomas              
    WHERE ( etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid ) and              
       ( etiquetas.etq_codigo = ''TipCart'' + Convert(varchar,gestor.GES_TIPCART)))  ,          
    GES_COMKI AS COMKI,GES_COMHON AS COMHON,  
    GES_EMPLID AS IDEMPLEADO,  
    EMPLEADO=(SELECT   
     epl_nombre + '' '' + epl_apepat as nombre   
    FROM empleados,    
    estados_empleado  
    WHERE  estados_empleado.eem_codemp = empleados.epl_codemp  and   
    estados_empleado.eem_eemid = empleados.epl_eemid  and   
    empleados.epl_codemp = ' + CONVERT(VARCHAR,@codemp) +'   and  
    empleados.epl_emplid = GES_EMPLID  
    and estados_empleado.eem_accion = ''A'' ) ,  
        
    GES_REMOTO AS REMOTO,GES_ESTADO AS ESTADO,GES_COMJKI AS COMJKI,GES_COMJHON AS COMHJON,    
    grupo_cobranza_gestor.GCG_GRCID AS GRUPOID,    
    GRUPO = (SELECT grc_nombre     
            from grupos_cobranza WHERE grc_grcid=grupo_cobranza_gestor.GCG_GRCID)    
   from gestor  inner join grupo_cobranza_gestor    
  on GESTOR.GES_CODEMP =  grupo_cobranza_gestor.GCG_CODEMP AND     
  GESTOR.GES_SUCID =  grupo_cobranza_gestor.GCG_SUCID AND    
  GESTOR.GES_GESID =  grupo_cobranza_gestor.GCG_GESID           
   where GES_CODEMP = ' + CONVERT(VARCHAR,@codemp) +'                 
   and GES_SUCID = ' + CONVERT(VARCHAR,@sucursal) +'                 
 ) as r
  where 1 = 1 ' 
 if @where is not null          
begin          
set @query = @query + @where;          
end   
 set @query = @query + ' ) as tabla  ) as t          
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)          
                 
--select @query                        
 exec(@query)                         
                         
                        
END 