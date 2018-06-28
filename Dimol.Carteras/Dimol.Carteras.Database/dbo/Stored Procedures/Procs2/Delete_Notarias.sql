

Create Procedure Delete_Notarias(@not_codemp integer, @not_notid integer) as
  DELETE FROM notarias  
   WHERE ( notarias.not_codemp = @not_codemp ) AND  
         ( notarias.not_notid = @not_notid )
