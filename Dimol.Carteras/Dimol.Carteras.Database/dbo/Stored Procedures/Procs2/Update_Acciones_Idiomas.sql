

Create Procedure Update_Acciones_Idiomas(@aci_codemp integer, @aci_accid integer, @aci_idid integer, @aci_nombre varchar (90)) as
  UPDATE acciones_idiomas  
     SET aci_nombre = @aci_nombre  
   WHERE ( acciones_idiomas.aci_codemp = @aci_codemp ) AND  
         ( acciones_idiomas.aci_accid = @aci_accid ) AND  
         ( acciones_idiomas.aci_idid = @aci_idid )
