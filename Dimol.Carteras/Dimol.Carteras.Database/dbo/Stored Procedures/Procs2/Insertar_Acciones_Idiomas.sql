

Create Procedure Insertar_Acciones_Idiomas(@aci_codemp integer, @aci_accid integer, @aci_idid integer, @aci_nombre varchar (90)) as
  INSERT INTO acciones_idiomas  
         ( aci_codemp,   
           aci_accid,   
           aci_idid,   
           aci_nombre )  
  VALUES ( @aci_codemp,   
           @aci_accid,   
           @aci_idid,   
           @aci_nombre )
