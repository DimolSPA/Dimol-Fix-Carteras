CREATE Procedure [dbo].[_Insertar_Monedas](
				@mon_codemp integer, 
				@mon_nombre varchar(50), 
				@mon_simbolo varchar(5), 
				@mon_default char(1), 
				@mon_porcint decimal(10,3), 
				@mon_decimales smallint)
as    

declare @id int                          
set @id = (select IsNull(Max(mon_codmon )+1, 1) from monedas
			where mon_codemp= @mon_codemp)                      

INSERT INTO monedas             
( mon_codemp,mon_codmon,mon_nombre,mon_simbolo,mon_default,mon_porcint,mon_decimales)    
 VALUES 
 (@mon_codemp,@id,@mon_nombre,@mon_simbolo,@mon_default,@mon_porcint,@mon_decimales )
