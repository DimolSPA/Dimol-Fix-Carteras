create PROC [dbo].[_Listar_Rol_Entes_Grilla]      
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
            
set @query = '  select * from          
  (select *,ROW_NUMBER() OVER (ORDER BY ' + @sidx + ' ' + @sord+ ') as row from              
  (   
   select * from (
	SELECT entejud_rol.ejr_etjid Etjid,   
	provcli.pcl_nomfant Nombre,   
	entes_judicial.etj_sindico  as Sindico,   
	entes_judicial.etj_abogado  as Abogado,   
	entes_judicial.etj_procurador  as Procurador,   
	entes_judicial.etj_receptor  as Receptor,   
	ejr_rolid Rolid
	FROM entejud_rol,   
	entes_judicial,   
	provcli
	WHERE  entes_judicial.etj_codemp = entejud_rol.ejr_codemp  and  
	entes_judicial.etj_etjid = entejud_rol.ejr_etjid  and  
	provcli.pcl_codemp = entes_judicial.etj_codemp  and  
	provcli.pcl_pclid = entes_judicial.etj_pclid
	and ejr_codemp =  '  + CONVERT(VARCHAR,@codemp) +'
	and ejr_rolid =  '  + CONVERT(VARCHAR,@rolid) +'
	UNION
	SELECT entejud_rol.ejr_etjid,   
	epl_nombre + '' '' + epl_apepat as nombre,   
	entes_judicial.etj_sindico  as Sindico,   
	entes_judicial.etj_abogado  as Abogado,   
	entes_judicial.etj_procurador  as Procurador,   
	entes_judicial.etj_receptor  as Receptor,   
	ejr_rolid
	FROM entejud_rol,   
	entes_judicial,   
	empleados
	WHERE  entes_judicial.etj_codemp = entejud_rol.ejr_codemp  and  
	entes_judicial.etj_etjid = entejud_rol.ejr_etjid  and  
	empleados.epl_codemp = entes_judicial.etj_codemp  and  
	empleados.epl_emplid = entes_judicial.etj_emplid
	and ejr_codemp =  '  + CONVERT(VARCHAR,@codemp) +'
	and ejr_rolid =  '  + CONVERT(VARCHAR,@rolid) +'
  ) as r
  where 1 = 1 ' 
 if @where is not null          
begin          
set @query = @query + @where;          
end   
 set @query = @query + ' ) as tabla  ) as t          
  where  row > ' + CONVERT(VARCHAR,@inicio) + ' and row <= '+CONVERT(VARCHAR,@limite)          
          
        
          
--select @query          
exec(@query) 