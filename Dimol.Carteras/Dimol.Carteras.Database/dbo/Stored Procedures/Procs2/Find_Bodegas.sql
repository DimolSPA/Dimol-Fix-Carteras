

Create Procedure Find_Bodegas(@bod_codemp integer, @bod_bodid integer) as
  SELECT count(bodegas.bod_bodid)  
    FROM bodegas  
   WHERE bodegas.bod_codemp = @bod_codemp and 
			  bod_bodid = @bod_bodid
