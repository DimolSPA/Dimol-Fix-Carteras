

Create Procedure Insertar_Eventos(@eve_codemp integer, @eve_eveid integer, @eve_titulo varchar(200), @eve_fecha datetime) as
  INSERT INTO eventos  
         ( eve_codemp,   
           eve_eveid,   
           eve_titulo,   
           eve_fecha )  
  VALUES ( @eve_codemp,   
           @eve_eveid,   
           @eve_titulo,   
           @eve_fecha )
