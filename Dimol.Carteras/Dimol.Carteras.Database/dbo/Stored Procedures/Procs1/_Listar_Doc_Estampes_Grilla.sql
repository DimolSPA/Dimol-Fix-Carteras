CREATE PROCEDURE [dbo].[_Listar_Doc_Estampes_Grilla]      
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
            
set @query = '  select * from          
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from              
  (   
   select * from (
   SELECT DDE_DDEID ID,
    ISNULL(INS_NOMBRE,'''') INSUMO,
    DDE_NOMBRE + CAST(DDE_DDEID AS VARCHAR) + DDE_EXT as NOMBRE, 
	ISNULL(convert(varchar,(DDE_FECJUD),105), '''') FECHAJUD, 
	ISNULL((select top 1 u.USR_NOMBRE from USUARIOS u WHERE u.USR_USRID = DDE_USRID), '''') USUARIO
	from DEUDORES_ESTAMPES LEFT JOIN INSUMOS   
	ON DDE_CODEMP = INS_CODEMP AND DDE_INSID = INS_INSID   
	WHERE DDE_CODEMP ='  + CONVERT(VARCHAR,@codemp) +'
	and DDE_PCLID ='  + CONVERT(VARCHAR,@pclid) +'
	and DDE_CTCID ='  + CONVERT(VARCHAR,@ctcid) +'
	and DDE_ROLID = '  + CONVERT(VARCHAR,@rolid) +' 
	and DDE_TPCID IS NULL 
	UNION 
	SELECT DDE_DDEID ID, 
    ISNULL(INS_NOMBRE,'''') INSUMO, 
    DDE_NOMBRE + CAST(DDE_DDEID AS VARCHAR) + DDE_EXT as NOMBRE, 
	ISNULL(convert(varchar,(DDE_FECJUD),105), '''') FECHAJUD, 
	ISNULL((select top 1 u.USR_NOMBRE from USUARIOS u WHERE u.USR_USRID = DDE_USRID), '''') USUARIO  
	FROM CABACERA_COMPROBANTES 
	INNER JOIN DEUDORES_ESTAMPES 
	ON DDE_CODEMP = CBC_CODEMP 
	AND DDE_TPCID = CBC_TPCID 
	AND DDE_NUMERO = CBC_NUMERO 
	AND CBT_ESTADO = ''F'' 
	LEFT JOIN INSUMOS   
	ON DDE_CODEMP = INS_CODEMP AND DDE_INSID = INS_INSID   
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
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)          
                  
exec(@query)  