

Create Procedure Find_Estados_Despachos(@edp_codemp integer, @edp_edpid integer) as
  SELECT count(estados_despachos.edp_edpid)  
    FROM estados_despachos  
   WHERE ( estados_despachos.edp_codemp = @edp_codemp ) AND  
         ( estados_despachos.edp_edpid = @edp_edpid )
