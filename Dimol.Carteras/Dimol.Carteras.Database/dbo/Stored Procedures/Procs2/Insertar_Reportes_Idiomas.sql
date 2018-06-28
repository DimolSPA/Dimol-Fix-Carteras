

Create Procedure Insertar_Reportes_Idiomas(@rti_trvid integer, @rti_rptid integer, @rti_idid smallint, @rti_nombre varchar(250), @rti_fisico varchar(800)) as
   INSERT INTO reportes_idiomas  
         ( rti_trvid,   
           rti_rptid,   
           rti_idid,   
           rti_nombre,   
           rti_fisico )  
  VALUES ( @rti_trvid,   
           @rti_rptid,   
           @rti_idid,   
          @rti_nombre,   
          @rti_fisico )
