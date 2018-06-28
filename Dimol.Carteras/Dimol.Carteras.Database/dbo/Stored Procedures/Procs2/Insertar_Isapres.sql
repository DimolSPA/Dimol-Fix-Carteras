

Create Procedure Insertar_Isapres(@isp_codemp integer, @isp_ispid integer,@isp_rut varchar(20),  @isp_nombre varchar(20), @isp_pctid integer) as
  INSERT INTO isapres  
         ( isp_codemp,   
           isp_ispid,   
           isp_rut,   
           isp_nombre,   
           isp_pctid )  
  VALUES ( @isp_codemp,   
           @isp_ispid,   
           @isp_rut,   
           @isp_nombre,   
           @isp_pctid )
