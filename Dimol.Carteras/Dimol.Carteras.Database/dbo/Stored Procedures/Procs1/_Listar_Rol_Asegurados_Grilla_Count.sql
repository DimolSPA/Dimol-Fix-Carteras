
CREATE PROC [dbo].[_Listar_Rol_Asegurados_Grilla_Count]      
      (          
       @codemp int,           
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
  (select *,ROW_NUMBER() OVER (ORDER BY Rut desc) as row from              
  (   
   select * from (
	select dbo._Formato_Rut( sc.sbc_rut) Rut,
	sc.sbc_nombre Nombre
	from ROL_DOCUMENTOS rd
	inner join CARTERA_CLIENTES_CPBT_DOC cc
	on rd.RDC_CODEMP = cc.CCB_CODEMP
	and rd.RDC_CTCID = cc.CCB_CTCID
	and rd.RDC_CCBID = cc.CCB_CCBID
	inner join SUBCARTERAS sc
	on rd.RDC_CODEMP = sc.SBC_CODEMP
	and cc.CCB_sbcID = sc.SBC_SBCID
	where rd.RDC_CODEMP = '  + CONVERT(VARCHAR,@codemp) +'
	and rd.RDC_ROLID = '+ CONVERT(VARCHAR,@rolid) +'
  ) as r
  where 1 = 1 ' 
 if @where is not null          
begin          
set @query = @query + @where;          
end   
 set @query = @query + ' ) as tabla  ) as t          
  where  row > 0'    
          
        
          
--select @query          
exec(@query)
