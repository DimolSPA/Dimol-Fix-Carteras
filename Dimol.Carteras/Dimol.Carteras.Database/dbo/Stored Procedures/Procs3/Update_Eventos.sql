

Create Procedure Update_Eventos(@eve_codemp integer, @eve_eveid integer, @eve_titulo varchar(200), @eve_fecha datetime) as
    UPDATE eventos  
     SET eve_titulo = @eve_titulo,   
         eve_fecha = @eve_fecha  
   WHERE ( eventos.eve_codemp = @eve_codemp ) AND  
         ( eventos.eve_eveid = @eve_eveid )
