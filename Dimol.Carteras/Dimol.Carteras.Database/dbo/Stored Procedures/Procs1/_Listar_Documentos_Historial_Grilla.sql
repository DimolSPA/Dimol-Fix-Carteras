-- =============================================              
-- Author:  Pablo Leyton              
-- Create date: 14-07-2014              
-- Description: Procedimiento para listar estados cartera para jQgrid              
-- =============================================           
    
CREATE PROCEDURE [dbo].[_Listar_Documentos_Historial_Grilla]              
(              
     @codemp int ,              
     @pclid int,   
     @ctcid int , 
     @estado varchar(1),              
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
  (SELECT cartera_clientes_cpbt_doc.ccb_ccbid Ccbid,   
	tipos_cpbtdoc_idiomas.tci_nombre TipoCpbtNombre,   
	RIGHT(CCB_NUMERO, LEN(CCB_NUMERO+''a'') -PATINDEX(''%[^0 ]%'', CCB_NUMERO + ''a'' )) NumeroCpbt,   
	cartera_clientes_cpbt_doc.ccb_fecdoc FechaDocumento,   
	cartera_clientes_cpbt_doc.ccb_fecvenc FechaVencimiento,   
	cartera_clientes_cpbt_doc.ccb_monto Monto,   
	cartera_clientes_cpbt_doc.ccb_saldo Saldo,
	cartera_clientes_cpbt_doc.ccb_compromiso Compromiso
	FROM cartera_clientes_cpbt_doc,   
	tipos_cpbtdoc_idiomas
	WHERE  cartera_clientes_cpbt_doc.ccb_codemp = tipos_cpbtdoc_idiomas.tci_codemp  and  
	 cartera_clientes_cpbt_doc.ccb_tpcid = tipos_cpbtdoc_idiomas.tci_tpcid
	 and cartera_clientes_cpbt_doc.ccb_codemp =  ' + CONVERT(VARCHAR,@codemp) +'
	 and cartera_clientes_cpbt_doc.ccb_pclid =  ' + CONVERT(VARCHAR,@pclid) +'
	 and cartera_clientes_cpbt_doc.ccb_ctcid =  ' + CONVERT(VARCHAR,@ctcid) +'
	 and cartera_clientes_cpbt_doc.ccb_estcpbt = ''' + @estado + '''
	 and ccb_estid > 1   
 ) as tabla  ) as t              
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)         
              
if @where is not null              
begin              
set @query = @query + @where;              
end              
              
              
          
--select @query              
 exec(@query)               
               
              
END 