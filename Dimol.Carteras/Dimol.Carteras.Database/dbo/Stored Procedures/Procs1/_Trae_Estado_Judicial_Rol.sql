CREATE Procedure [dbo].[_Trae_Estado_Judicial_Rol]      
(      
  @codemp int,                    
  @idioma int,
  @esjid int,
  @texto varchar(200)             
  )      
     
as        
   declare @nombre varchar(250) = '%' + @texto + '%'      
    
BEGIN    
if   @esjid = 0 
begin
    SELECT estados_cartera_idiomas.eci_nombre,
    estados_cartera.ect_estid, 
    ect_color
	FROM estados_cartera,   
	estados_cartera_idiomas, materia_estados
	WHERE  estados_cartera_idiomas.eci_codemp = estados_cartera.ect_codemp  and  
	estados_cartera_idiomas.eci_estid = estados_cartera.ect_estid  and  
	estados_cartera.ect_codemp = @codemp  AND  
	estados_cartera.ect_agrupa in (1,2,3)  AND  
	estados_cartera_idiomas.eci_idid =  @idioma   AND  
	estados_cartera.ect_prejud in ('J','A')  AND  
	mej_codemp = ect_codemp  AND  
	mej_estid = ect_estid and 
	eci_nombre like @nombre and
	ect_habilitado = 'S'
	order by eci_nombre

End   
else
	
	SELECT estados_cartera_idiomas.eci_nombre,
	estados_cartera.ect_estid, 
    ect_color
	FROM estados_cartera,   
	estados_cartera_idiomas, materia_estados
	WHERE  estados_cartera_idiomas.eci_codemp = estados_cartera.ect_codemp  and  
	estados_cartera_idiomas.eci_estid = estados_cartera.ect_estid  and  
	estados_cartera.ect_codemp = @codemp  AND  
	estados_cartera.ect_agrupa in (1,2,3)  AND  
	estados_cartera_idiomas.eci_idid = @idioma  AND  
	estados_cartera.ect_prejud in ('J','A')  AND  
	mej_codemp = ect_codemp  AND  
	mej_estid  = ect_estid  AND   
	mej_esjid =  @esjid and
	eci_nombre like @nombre and
	ect_habilitado = 'S'
	order by eci_nombre
end  