

Create Procedure Update_Estados_Despachos(@edp_codemp integer, @edp_edpid integer, @edp_nombre varchar (100), @edp_tipo smallint) as
  UPDATE estados_despachos  
     SET edp_codemp = @edp_codemp,   
         edp_edpid = @edp_edpid,   
         edp_nombre = @edp_nombre,   
         edp_tipo = @edp_tipo  
   WHERE ( estados_despachos.edp_codemp = @edp_codemp ) AND  
         ( estados_despachos.edp_edpid = @edp_edpid )
