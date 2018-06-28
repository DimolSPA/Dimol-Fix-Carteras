
CREATE PROCEDURE [dbo].[_Listar_Doc_Estampes_Grilla_count]      
      (          
       @codemp int,          
       @pclid int,     
	   @ctcid int,
       @rolid int,     
       @where varchar(1000),          
       @sidx varchar(255),          
       @sord varchar(10),          
       @inicio int,          
       @limite int          
      )          
      
AS      
      
declare @query varchar(7000);          
            
set @query = '  select count(*) count from          
  (select *,ROW_NUMBER() OVER (ORDER BY NOMBRE asc) as row from              
  (   
   select * from (
   SELECT DDE_DDEID ID,
    DDE_NOMBRE + CAST(DDE_DDEID AS VARCHAR) + DDE_EXT as NOMBRE 
	from DEUDORES_ESTAMPES    
	WHERE DDE_CODEMP ='  + CONVERT(VARCHAR,@codemp) +'
	and DDE_PCLID ='  + CONVERT(VARCHAR,@pclid) +'
	and DDE_CTCID ='  + CONVERT(VARCHAR,@ctcid) +'
	and DDE_ROLID = '  + CONVERT(VARCHAR,@rolid) +'
	and DDE_TPCID IS NULL 
	UNION 
	SELECT DDE_DDEID ID,
    DDE_NOMBRE + CAST(DDE_DDEID AS VARCHAR) + DDE_EXT as NOMBRE 
	FROM CABACERA_COMPROBANTES 
	INNER JOIN DEUDORES_ESTAMPES 
	ON DDE_CODEMP = CBC_CODEMP 
	AND DDE_TPCID = CBC_TPCID 
	AND DDE_NUMERO = CBC_NUMERO 
	AND CBT_ESTADO = ''F'' 
	WHERE DDE_CODEMP ='  + CONVERT(VARCHAR,@codemp) +'
	and DDE_PCLID ='  + CONVERT(VARCHAR,@pclid) +'
	and DDE_CTCID ='  + CONVERT(VARCHAR,@ctcid) +'
	and DDE_ROLID = '  + CONVERT(VARCHAR,@rolid) +' 
  ) as r
  where 1 = 1 ' 
 if @where is not null          
begin          
set @query = @query + @where;          
end   
 set @query = @query + ' ) as tabla  ) as t          
  where  row > 0'         
                  
exec(@query)
