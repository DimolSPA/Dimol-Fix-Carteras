-- =============================================      
-- Author:     Pablo Leyton      
-- Create date: 12/06/2014      
-- Description:   Lista nombre Empleados      
-- =============================================      
CREATE PROCEDURE [dbo].[_Listar_Nombre_Empleados_Todos]       
   @codemp int
  
AS      
BEGIN      
      
		SELECT 
			empleados.epl_emplid as id,   
			epl_nombre + ' ' + epl_apepat as nombre,   
			estados_empleado.eem_accion as accion
			FROM empleados,   
			estados_empleado
		 WHERE  estados_empleado.eem_codemp = empleados.epl_codemp  and  
		 estados_empleado.eem_eemid = empleados.epl_eemid and  
		 empleados.epl_codemp =  @codemp
		 and estados_empleado.eem_accion = 'A'  
		 order by nombre  
        
END
