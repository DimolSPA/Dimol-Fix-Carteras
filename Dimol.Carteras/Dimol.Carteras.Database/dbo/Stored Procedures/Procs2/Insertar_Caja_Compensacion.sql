

Create Procedure Insertar_Caja_Compensacion(@cjc_codemp integer, @cjc_cjcid integer,@cjc_rut varchar(20),  @cjc_nombre varchar(150), @cjc_pctid integer) as
  INSERT INTO caja_compensacion  
         ( cjc_codemp,   
           cjc_cjcid,   
           cjc_rut,   
           cjc_nombre,   
           cjc_pctid )  
  VALUES ( @cjc_codemp,   
           @cjc_cjcid,   
           @cjc_rut,   
           @cjc_nombre,   
           @cjc_pctid )
