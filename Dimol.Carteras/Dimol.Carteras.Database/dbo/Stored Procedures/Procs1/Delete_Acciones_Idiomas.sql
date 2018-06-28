

Create Procedure Delete_Acciones_Idiomas(@aci_codemp integer, @aci_accid integer, @aci_idid integer) as
  DELETE FROM acciones_idiomas  
   WHERE ( acciones_idiomas.aci_codemp = @aci_codemp ) AND  
         ( acciones_idiomas.aci_accid = @aci_accid ) AND  
         ( acciones_idiomas.aci_idid = @aci_idid )
