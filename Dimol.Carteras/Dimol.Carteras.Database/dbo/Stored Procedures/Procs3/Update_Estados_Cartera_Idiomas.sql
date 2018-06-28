

Create Procedure Update_Estados_Cartera_Idiomas(@eci_codemp integer, @eci_estid smallint, @eci_idid integer, @eci_nombre varchar (350)) as  
  UPDATE estados_cartera_idiomas  
     SET eci_nombre = @eci_nombre  
   WHERE ( estados_cartera_idiomas.eci_codemp = @eci_codemp ) AND  
         ( estados_cartera_idiomas.eci_estid = @eci_estid ) AND  
         ( estados_cartera_idiomas.eci_idid = @eci_idid )
