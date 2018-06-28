

Create Procedure Delete_Estados_Cartera_Idiomas(@eci_codemp integer, @eci_estid smallint, @eci_idid integer) as  
  DELETE FROM estados_cartera_idiomas  
   WHERE ( estados_cartera_idiomas.eci_codemp = @eci_codemp ) AND  
         ( estados_cartera_idiomas.eci_estid = @eci_estid ) AND  
         ( estados_cartera_idiomas.eci_idid = @eci_idid )
