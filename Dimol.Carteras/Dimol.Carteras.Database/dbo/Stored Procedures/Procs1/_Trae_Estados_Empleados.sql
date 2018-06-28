CREATE Procedure [dbo].[_Trae_Estados_Empleados]        
  @codemp int    
        
         
as            
       
        
BEGIN          
       select 
		eem_eemid as Id, 
		eem_nombre as Descripcion
       from estados_empleado 
	   where eem_codemp = @codemp 
	   order by eem_accion, eem_nombre     
  
END
