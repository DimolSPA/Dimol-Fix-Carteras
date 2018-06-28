

Create Procedure Update_Acciones(@acc_codemp integer, @acc_accid integer, @acc_nombre varchar (80), @acc_agrupa smallint) as
  UPDATE acciones  
     SET acc_nombre = @acc_nombre,   
         acc_agrupa = @acc_agrupa  
   WHERE ( acciones.acc_codemp = @acc_codemp ) AND  
         ( acciones.acc_accid = @acc_accid )
