-- =============================================                              
-- Author:  Pablo Leyton                              
-- Create date: 04-09-2014                              
-- Description: Procedimiento para listar Notarias para jQgrid                              
-- =============================================                              
CREATE PROCEDURE [dbo].[_Listar_Notarias_Grilla]                              
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
  (  SELECT     
  notarias.NOT_CODEMP AS codemp,    
  notarias.NOT_NOTID AS Id,       
  notarias.NOT_RUT AS Rut,       
  notarias.NOT_NOMBRE AS Nombre,       
  notarias.NOT_NOMNOT AS NombreNotaria,    
  notarias.NOT_COMID As IdComuna,    
  notarias.NOT_DIRECCION AS Direccion ,    
  notarias.NOT_TELEFONO1 AS Telefono1,    
  notarias.NOT_TELEFONO2 As Telefono2,    
  notarias.NOT_FAX AS Fax,    
  notarias.NOT_CELULAR AS Celular,    
  notarias.NOT_MAIL AS Mail    
 FROM notarias    
 WHERE notarias.not_codemp =' + CONVERT(VARCHAR,@codemp) +'       
                
 ) as tabla  ) as t                              
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)                              
                              
if @where is not null                              
begin                              
set @query = @query + @where;                              
end                              
                              
--select @query                              
 exec(@query)                               
                     
          
                              
END
