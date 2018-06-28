

Create Procedure Find_Socios(@soc_codemp integer, @soc_socid integer) as
  SELECT count(socios.soc_socid)  
    FROM socios  
   WHERE ( socios.soc_codemp = @soc_codemp ) AND  
         ( socios.soc_socid = @soc_socid )
