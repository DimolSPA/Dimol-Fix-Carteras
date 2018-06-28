-- =============================================                      
-- Author:  Pablo Leyton                      
-- Create date: 01-09-2014                      
-- Description: Procedimiento para listar Tribunales para jQgrid                      
-- =============================================           
        
                   
CREATE PROCEDURE [dbo].[_Listar_Tribunales_Grilla_Count]                            
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
    (select *,ROW_NUMBER() OVER (ORDER BY IdComuna asc )  as row from                                       
    ( SELECT    
  tribunales.TRB_CODEMP as codemp,      
  tribunales.trb_trbid as Id,         
  tribunales.trb_rut as Rut,         
  tribunales.trb_nombre as Nombre,         
  tribunales.TRB_TTBID as IdTipo,      
  TipoTribunal=(select top 1 tbi_nombre from tipos_tribunal_idiomas     
    where tbi_codemp = ' + CONVERT(VARCHAR,@codemp) +'         
    and TBI_IDID = '  + CONVERT(VARCHAR,@idid) +'     
    and TBI_TTBID = tribunales.TRB_TTBID    ),      
  tribunales.TRB_COMID as IdComuna,      
  tribunales.TRB_DIRECCION as Direccion,      
  tribunales.TRB_TELEFONO1 as Telefono1,      
  tribunales.TRB_TELEFONO2 as Telefono2,      
  tribunales.TRB_FAX as Fax,      
  tribunales.TRB_EMAIL as Email,      
  tribunales.TRB_BCOID as IdBanco,      
  Banco=(select top 1 bco_nombre from bancos     
   where bco_codemp = ' + CONVERT(VARCHAR,@codemp) +'     
   and BCO_BCOID = tribunales.TRB_BCOID),    
  tribunales.TRB_CTACTE as CuentaCorriente      
 FROM tribunales,         
 tipos_tribunal_idiomas      
 WHERE  tribunales.trb_codemp = tipos_tribunal_idiomas.tbi_codemp  and        
 tribunales.trb_ttbid = tipos_tribunal_idiomas.tbi_ttbid  and        
 tribunales.trb_codemp =  '  + CONVERT(VARCHAR,@codemp) +'       
 and tipos_tribunal_idiomas.tbi_idid =  '  + CONVERT(VARCHAR,@idid) +'                       
  ) as tabla  ) as t  
 where  row > 0 '                    
                      
if @where is not null                      
begin                      
set @query = @query + @where;                      
end                      
                      
--select @query                      
 exec(@query)                       
                       
                      
END
