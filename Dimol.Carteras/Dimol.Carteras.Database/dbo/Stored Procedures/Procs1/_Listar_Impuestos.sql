-- =============================================
-- Author:		Rodrigo GarridoA
-- Create date: 26-08-2014
-- Description:	Procedimiento para listar periodos para jQgrid
-- =============================================
CREATE PROCEDURE [dbo].[_Listar_Impuestos]
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
  (' set @query = @query + 'SELECT DISTINCT [IPT_CODEMP] Codemp        
	  ,[IPT_IPTID] id
	  ,[IPT_NOMBRE] nombre        
	  ,[IPT_NOMCORT] nombreCorto
	  ,[IPT_PCTID] idPlanDeCuentas       
	  ,p.PCI_NOMBRE nombrePlanDeCuentas
	  ,[IPT_RETENIDO] retenido
	  ,[IPT_MONTO] monto     
	  FROM [dbo].[IMPUESTOS],[dbo].[PLAN_CUENTAS_IDIOMAS] p 
	  WHERE ipt_codemp = p.pci_codemp and ipt_pctid = p.pci_pctid and
	  [IPT_CODEMP] = '+ CONVERT(VARCHAR,@codemp)+' AND pci_idid = '+ CONVERT(VARCHAR,@idid)+'
   '
   
   set @query = @query +')as tabla ) as t
  where  row >= ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)

if @where is not null
begin
set @query = @query + @where;
end

select @query
 exec(@query)	
	

END
