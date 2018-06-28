

Create Procedure Trae_Acciones(@aci_codemp integer, @aci_idid integer) as
  SELECT acciones_idiomas.aci_accid,   
         acciones_idiomas.aci_nombre  
    FROM acciones_idiomas  
   WHERE ( acciones_idiomas.aci_codemp = @aci_codemp ) AND  
         ( acciones_idiomas.aci_idid = @aci_idid )   
           
ORDER BY acciones_idiomas.aci_nombre ASC
