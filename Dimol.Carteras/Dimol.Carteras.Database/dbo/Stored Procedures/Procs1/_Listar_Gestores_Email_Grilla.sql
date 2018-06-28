-- =============================================                        
-- Author:  Pablo Leyton                        
-- Create date: 05-08-2014                        
-- Description: Procedimiento para listar Gestor para jQgrid                        
-- =============================================                     
              
CREATE PROCEDURE [dbo].[_Listar_Gestores_Email_Grilla]                        
(                        
     @codemp int,
	 @tipo_cartera int,
	 @sucid int,
	 @gestor int,
	 @grupo int,                       
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
  ( SELECT gestor.ges_gesid Id,   
gestor.ges_nombre Nombre, ''false'' as sel 
FROM gestor
WHERE  gestor.ges_codemp = ' + CONVERT(VARCHAR,@codemp) +'
and gestor.ges_sucid =  ' + CONVERT(VARCHAR,@sucid) +'
and gestor.ges_estado = ''A''  
and gestor.ges_tipcart in (3, ' + CONVERT(VARCHAR,@tipo_cartera) +'  )'    

If @gestor > 0 begin
	set @query = @query + ' and ges_gesid = '  + CONVERT(VARCHAR,@gestor) 
End
            
If @grupo <> 0 begin
    set @query = @query + ' and ges_gesid in ('
    set @query = @query + ' select gcg_gesid from grupo_cobranza_gestor '
    set @query = @query + ' where gcg_codemp =  ' + CONVERT(VARCHAR,@codemp)
    set @query = @query + ' and gcg_sucid =  ' + CONVERT(VARCHAR,@sucid) 
    set @query = @query + ' and gcg_grcid = ' + CONVERT(VARCHAR,@grupo) 
	If @gestor > 0 begin
        set @query = @query + ' and gcg_gesid =  '  + CONVERT(VARCHAR,@gestor) 
    End 
    set @query = @query + ')'
End 
              
 set @query = @query + ') as tabla  ) as t                        
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)       


if @where is not null                        
begin                        
set @query = @query + @where;                        
end                        
                        
           
--select @query           
exec(@query)                         
                         
                        
END
