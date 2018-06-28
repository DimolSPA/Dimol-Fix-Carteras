

Create Procedure Find_Servicios(@sve_codemp integer, @sve_sveid integer) as
  SELECT count(servicios.sve_sveid)  
    FROM servicios  
   WHERE ( servicios.sve_codemp = @sve_codemp ) AND  
         ( servicios.sve_sveid = @sve_sveid )
