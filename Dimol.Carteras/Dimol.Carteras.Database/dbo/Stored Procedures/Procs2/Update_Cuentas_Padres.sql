

Create Procedure Update_Cuentas_Padres(@ctp_codemp integer, @ctp_ctpid integer, @ctp_codigo varchar (20),
                                                                      @ctp_nombre varchar (100), @ctp_agrupa numeric (2)) as  
UPDATE cuentas_padres  
     SET ctp_codigo = @ctp_codigo,   
         ctp_nombre = @ctp_nombre,   
         ctp_agrupa = @ctp_agrupa  
   WHERE ( cuentas_padres.ctp_codemp = @ctp_codemp ) AND  
         ( cuentas_padres.ctp_ctpid = @ctp_ctpid )
