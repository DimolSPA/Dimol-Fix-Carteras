

Create Procedure Insertar_Cuentas_Padres(@ctp_codemp integer, @ctp_ctpid integer,@ctp_codigo varchar(20),  @ctp_nombre varchar(100), @ctp_agrupa numeric(2)) as
  INSERT INTO cuentas_padres  
         ( ctp_codemp,   
           ctp_ctpid,   
           ctp_codigo,   
           ctp_nombre,   
           ctp_agrupa )  
  VALUES ( @ctp_codemp,   
           @ctp_ctpid,   
           @ctp_codigo,   
           @ctp_nombre,   
           @ctp_agrupa )
