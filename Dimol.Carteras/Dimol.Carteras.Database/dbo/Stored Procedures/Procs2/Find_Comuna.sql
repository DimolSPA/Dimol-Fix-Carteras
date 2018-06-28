

Create Procedure Find_Comuna(@com_comid integer) as
  SELECT count(comuna.com_comid)  
    FROM comuna  
   WHERE comuna.com_comid = @com_comid
