

Create Procedure Insertar_Eventos_Fotos(@evf_codemp integer, @evf_eveid integer, @evf_evfid integer, @evf_nombre varchar(200)) as
   
  INSERT INTO eventos_fotos  
         ( evf_codemp,   
           evf_eveid,   
           evf_evfid,   
           evf_nombre )  
  VALUES ( @evf_codemp,   
           @evf_eveid,   
           @evf_evfid,   
           @evf_nombre )
