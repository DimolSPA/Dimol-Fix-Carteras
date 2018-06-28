

Create Procedure Update_Empleados_Desactiva(@epl_codemp integer, @epl_emplid integer, @epl_eemid integer) as 

  UPDATE usuarios  
     SET usr_fecblock = getdate(),   
         usr_estado = 'B'  
    FROM usuarios,   
         empleados  
   WHERE ( empleados.epl_codemp = usuarios.usr_codemp ) and  
         ( empleados.epl_usrid = usuarios.usr_usrid ) and  
         ( ( empleados.epl_codemp = @epl_codemp ) AND  
         ( epl_emplid = @epl_emplid )   
         )   


  UPDATE empleados  
     SET epl_eemid = @epl_eemid,   
         epl_usrid = null,   
         epl_fecfin = getdate()  
   WHERE ( empleados.epl_codemp = @epl_codemp ) AND  
         ( empleados.epl_emplid = @epl_emplid )
