

Create Procedure Insertar_Ejercicios(@ejc_codemp integer, @ejc_ejcid integer, @ejc_tejid integer, @ejc_nombre varchar(200), @ejc_tiempo integer, @ejc_repeticion integer) as
  INSERT INTO ejercicios  
         ( ejc_codemp,   
           ejc_ejcid,   
           ejc_tejid,   
           ejc_nombre,   
           ejc_tiempo,   
           ejc_repeticion )  
  VALUES ( @ejc_codemp,   
           @ejc_ejcid,   
           @ejc_tejid,   
           @ejc_nombre,   
           @ejc_tiempo,   
           @ejc_repeticion    )
