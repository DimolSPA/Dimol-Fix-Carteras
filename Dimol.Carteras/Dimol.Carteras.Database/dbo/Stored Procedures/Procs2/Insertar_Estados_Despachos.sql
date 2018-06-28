

Create Procedure Insertar_Estados_Despachos(@edp_codemp integer, @edp_edpid integer, @edp_nombre varchar (100), @edp_tipo smallint) as
  INSERT INTO estados_despachos  
         ( edp_codemp,   
           edp_edpid,   
           edp_nombre,   
           edp_tipo )  
  VALUES ( @edp_codemp,   
           @edp_edpid,   
           @edp_nombre,   
           @edp_tipo )
