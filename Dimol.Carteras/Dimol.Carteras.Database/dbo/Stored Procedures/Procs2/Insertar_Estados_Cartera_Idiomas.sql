

Create Procedure Insertar_Estados_Cartera_Idiomas(@eci_codemp integer, @eci_estid smallint, @eci_idid integer, @eci_nombre varchar (350)) as 
  INSERT INTO estados_cartera_idiomas  
         ( eci_codemp,   
           eci_estid,   
           eci_idid,   
           eci_nombre )  
  VALUES ( @eci_codemp,   
           @eci_estid,   
           @eci_idid,   
           @eci_nombre )
