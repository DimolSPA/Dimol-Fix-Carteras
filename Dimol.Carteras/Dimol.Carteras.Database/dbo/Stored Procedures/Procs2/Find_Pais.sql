

Create Procedure Find_Pais(@pai_paiid integer) as
  SELECT count(pais.pai_paiid)  
    FROM pais  
   WHERE pais.pai_paiid = @pai_paiid
