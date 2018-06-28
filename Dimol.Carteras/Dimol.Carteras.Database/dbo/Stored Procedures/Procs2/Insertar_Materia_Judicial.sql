

Create Procedure Insertar_Materia_Judicial(@esj_codemp integer, @esj_esjid integer, @esj_nombre varchar (120), @esj_orden smallint) as  
INSERT INTO materia_judicial  
         ( esj_codemp,   
           esj_esjid,   
           esj_nombre,   
           esj_orden )  
  VALUES ( @esj_codemp,   
           @esj_esjid,   
           @esj_nombre,   
           @esj_orden )
