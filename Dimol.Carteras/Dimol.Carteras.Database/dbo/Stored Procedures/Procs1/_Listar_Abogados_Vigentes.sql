CREATE PROCEDURE [dbo].[_Listar_Abogados_Vigentes]                        
(                        
     @codemp int,
     @sucid int
)                        
AS                        
BEGIN  

select epl_emplid, 
epl_nombre 
from empleados emp, entes_judicial ej 
where emp.epl_emplid = ej.ETJ_EMPLID and 
ej.ETJ_ABOGADO_ENCARGADO = 'S' and 
emp.EPL_CODEMP = @codemp and 
emp.EPL_SUCID = @sucid 
union 
select 0, 'SIN TRIBUNAL' 
                      
END
