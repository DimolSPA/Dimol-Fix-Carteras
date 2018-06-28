

Create Procedure UltNum_Ciudad as
  SELECT IsNull(Max(ciudad.ciu_ciuid)+1, 1)
    FROM ciudad
