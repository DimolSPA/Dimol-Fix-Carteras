

Create Procedure UltNum_Comuna as
 SELECT IsNull(Max(comuna.com_comid)+1, 1)
    FROM comuna
