

Create Procedure Find_Notarias(@not_codemp integer, @not_notid integer) as
  SELECT count(notarias.not_notid)  
    FROM notarias  
   WHERE notarias.not_codemp = @not_codemp  and
                 not_notid = @not_notid
