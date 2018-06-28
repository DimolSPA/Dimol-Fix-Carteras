

Create Procedure Insertar_Menu(@men_menid integer, @men_nombre varchar(200), @men_imagen varchar(100), @men_orden smallint, @men_directorio varchar(200)) as

  INSERT INTO menu  
         ( men_menid,   
           men_nombre,   
           men_imagen,   
           men_orden,   
           men_directorio )  
  VALUES ( @men_menid,   
           @men_nombre,   
           @men_imagen,   
           @men_orden,   
           @men_directorio )
