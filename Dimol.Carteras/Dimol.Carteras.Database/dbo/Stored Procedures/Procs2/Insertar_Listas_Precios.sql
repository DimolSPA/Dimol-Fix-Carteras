

Create Procedure Insertar_Listas_Precios(@ltp_codemp integer, @ltp_ltpid integer, @ltp_nombre varchar (200), @ltp_tipo char (1),
													@ltp_vigencia char (1), @ltp_desde datetime, @ltp_hasta datetime, @ltp_descuento decimal (5,2),
													@ltp_gastjud char (1), @ltp_codmon integer) as
  INSERT INTO listas_precios  
         ( ltp_codemp,   
           ltp_ltpid,   
           ltp_nombre,   
           ltp_tipo,   
           ltp_vigencia,   
           ltp_desde,   
           ltp_hasta,   
           ltp_descuento,   
           ltp_gastjud,
           ltp_codmon )  
  VALUES ( @ltp_codemp,   
           @ltp_ltpid,   
           @ltp_nombre,   
           @ltp_tipo,   
           @ltp_vigencia,   
           @ltp_desde,   
           @ltp_hasta,   
           @ltp_descuento,   
           @ltp_gastjud,
		@ltp_codmon )
