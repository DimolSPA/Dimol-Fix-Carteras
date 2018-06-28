CREATE PROCEDURE [dbo].[_Listar_Materia_Estado_Grilla_Count]                
(                          
    @codemp int,                
    @idid int,                
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
         
set @query = '  select count(*) count from                    
  (select *,ROW_NUMBER() OVER (ORDER BY idmateria asc )  as row from                    
  (   SELECT     
     materia_estados.MEJ_CODEMP AS codemp,    
     materia_judicial_idiomas.mji_esjid as idmateria ,       
                 materia_judicial_idiomas.mji_nombre as nombremateria,       
                 estados_cartera_idiomas.eci_estid as idestado,       
                 estados_cartera_idiomas.eci_nombre as nombreestado,       
                 materia_judicial.esj_orden    
                 FROM materia_estados,       
                 materia_judicial_idiomas,       
                 estados_cartera_idiomas,       
                 materia_judicial    
                 WHERE  materia_estados.mej_codemp = materia_judicial_idiomas.mji_codemp  and      
                 materia_estados.mej_esjid = materia_judicial_idiomas.mji_esjid  and      
                 materia_estados.mej_codemp = estados_cartera_idiomas.eci_codemp  and      
                 materia_estados.mej_estid = estados_cartera_idiomas.eci_estid  and      
                 materia_judicial.esj_codemp = materia_judicial_idiomas.mji_codemp  and      
                 materia_judicial.esj_esjid = materia_judicial_idiomas.mji_esjid  and      
                 materia_judicial.esj_codemp = materia_estados.mej_codemp  and      
                 materia_judicial.esj_esjid = materia_estados.mej_esjid  and      
                 materia_estados.mej_codemp =  '  + CONVERT(VARCHAR,@codemp) +'      
                 and materia_judicial_idiomas.mji_idid = '  + CONVERT(VARCHAR,@idid) +'      
                 and estados_cartera_idiomas.eci_idid =  '  + CONVERT(VARCHAR,@idid) +'          
              
 ) as tabla  ) as t                
  where  row >0 '             
                
if @where is not null                
begin                
set @query = @query + @where;                
end                
                
--select @query                
 exec(@query)                 
                 
                
END
