-- =============================================                          
-- Author:  Pablo Leyton                          
-- Create date: 07-10-2014                          
-- Description: Procedimiento para listar estados_documentos para jQgrid                          
-- =============================================                       

CREATE PROCEDURE [dbo].[_Listar_estados_documentos_Grilla]                          
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
 set @query = '  select * from                          
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from                              
  ( 
  
  SELECT 
	EDC_CODEMP CodEmp, 
	EDC_EDCID as Id, 
	EDC_TIPMOV as IdTipo,
	CASE 
      WHEN EDC_TIPMOV = ''A'' THEN ''Apertura''
      WHEN EDC_TIPMOV = ''I'' THEN ''Ingreso'' 
      WHEN EDC_TIPMOV = ''E'' THEN ''Egreso'' 
      WHEN EDC_TIPMOV = ''T'' THEN ''Traspaso'' 
      ELSE ''''
	 END AS Tipo,
	EDC_NOMBRE as Nombre,  
	EDC_ESTADO as IdEstado,
	Estado =(SELECT  Top 1 etiquetas_idiomas.eti_descripcion                
		  FROM etiquetas,  etiquetas_idiomas                
		WHERE ( etiquetas_idiomas.eti_etiid = etiquetas.etq_etqid ) and                
		   ( etiquetas.etq_codigo = ''EstDD'' + Convert(varchar,estados_documentos_diarios.EDC_ESTADO)))  
	from estados_documentos_diarios      
   where edc_codemp = ' + CONVERT(VARCHAR,@codemp) + '                   
                                   
                
 ) as tabla  ) as t                          
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                          
                          
if @where is not null                          
begin                          
set @query = @query + @where;                          
end                          
                          
             
--select @query                          
 exec(@query)                           
                           
                          
END
