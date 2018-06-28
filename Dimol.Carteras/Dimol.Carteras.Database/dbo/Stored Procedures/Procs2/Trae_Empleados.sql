

create Procedure Trae_Empleados(@epl_codemp integer) as    
SELECT empleados.epl_emplid,
             empleados.epl_rut,              
             epl_nombre + '  ' + epl_apepat as Nombre        
             FROM empleados,              estados_empleado     
 WHERE ( estados_empleado.eem_codemp = empleados.epl_codemp ) and   
         ( estados_empleado.eem_eemid = empleados.epl_eemid ) and   
         ( ( empleados.epl_codemp = @epl_codemp ) AND             
         ( estados_empleado.eem_accion= 'A' ) )    
         order by epl_nombre, epl_apepat
