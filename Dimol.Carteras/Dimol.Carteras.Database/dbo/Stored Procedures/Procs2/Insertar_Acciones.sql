

Create Procedure Insertar_Acciones(@acc_codemp integer, @acc_accid integer, @acc_nombre varchar (80), @acc_agrupa smallint) as
  INSERT INTO acciones  
         ( acc_codemp,   
           acc_accid,   
           acc_nombre,   
           acc_agrupa )  
  VALUES ( @acc_codemp,   
           @acc_accid,   
           @acc_nombre,   
           @acc_agrupa )
