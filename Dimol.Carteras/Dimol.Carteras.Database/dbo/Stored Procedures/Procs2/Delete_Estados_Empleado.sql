

Create Procedure Delete_Estados_Empleado(@eem_codemp integer, @eem_eemid integer) as  

  DELETE FROM estados_empleado_idiomas  
   WHERE ( estados_empleado_idiomas.eei_codemp = @eem_codemp ) AND  
         ( estados_empleado_idiomas.eei_eemid = @eem_eemid ) 

   DELETE FROM estados_empleado  
   WHERE ( estados_empleado.eem_codemp = @eem_codemp ) AND  
         ( estados_empleado.eem_eemid = @eem_eemid )
