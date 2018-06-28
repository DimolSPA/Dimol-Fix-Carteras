

Create Procedure Insertar_Estados_Cartera(@ect_codemp integer, @ect_estid smallint, @ect_nombre varchar (300), @ect_agrupa smallint,
                                                                     @ect_utiliza char (1), @ect_prejud char (1), @ect_solfecha char (1), @ect_genret char (1), @ect_compromiso char(1)) as 
  INSERT INTO estados_cartera  
         ( ect_codemp,   
           ect_estid,   
           ect_nombre,   
           ect_agrupa,   
           ect_utiliza,   
           ect_prejud,   
           ect_solfecha,   
           ect_genret,
           ect_compromiso )  
  VALUES ( @ect_codemp,   
           @ect_estid,   
           @ect_nombre,   
           @ect_agrupa,   
           @ect_utiliza,   
           @ect_prejud,   
           @ect_solfecha,   
           @ect_genret,
           @ect_compromiso )
