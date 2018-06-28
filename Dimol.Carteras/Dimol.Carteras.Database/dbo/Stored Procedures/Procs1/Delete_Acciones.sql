

Create Procedure Delete_Acciones(@acc_codemp integer, @acc_accid integer) as

  DELETE FROM acciones_idiomas  
   WHERE ( acciones_idiomas.aci_codemp = @acc_codemp ) AND  
         ( acciones_idiomas.aci_accid = @acc_accid ) 


   DELETE FROM acciones  
   WHERE ( acciones.acc_codemp = @acc_codemp ) AND  
         ( acciones.acc_accid = @acc_accid )
