

Create Procedure Delete_Isapres(@isp_codemp integer, @isp_ispid integer) as  
  DELETE FROM isapres  
   WHERE ( isapres.isp_codemp = @isp_codemp ) AND  
         ( isapres.isp_ispid = @isp_ispid )
