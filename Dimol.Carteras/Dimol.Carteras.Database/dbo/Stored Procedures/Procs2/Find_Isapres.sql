

Create Procedure Find_Isapres(@isp_codemp integer, @isp_ispid integer) as
  SELECT count(isapres.isp_ispid)  
    FROM isapres  
   WHERE ( isapres.isp_codemp = @isp_codemp ) AND  
         ( isapres.isp_ispid = @isp_ispid )
