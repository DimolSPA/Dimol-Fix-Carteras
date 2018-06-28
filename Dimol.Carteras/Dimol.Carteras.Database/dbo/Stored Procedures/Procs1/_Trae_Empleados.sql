CREATE Procedure [dbo].[_Trae_Empleados]        
(        
  @codemp int                     
)        
       
as          
           
      
BEGIN    
	
  SELECT '-1' as Id,UPPER(ETQ_DESCRIPCION) as Nombre
  FROM ETIQUETAS
  WHERE  ETQ_CODIGO='Selec'
  
  UNION 
    
   SELECT   
  empleados.epl_emplid,    
  epl_nombre + ' ' + epl_apepat as nombre   
 FROM empleados,    
 estados_empleado  
 WHERE  estados_empleado.eem_codemp = empleados.epl_codemp  and   
 estados_empleado.eem_eemid = empleados.epl_eemid  and   
 empleados.epl_codemp = @codemp  
 and estados_empleado.eem_accion = 'A'  
 ORDER BY 1
END
