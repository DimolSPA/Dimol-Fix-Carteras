

Create Procedure Insertar_Insumos_Equivalencias(@ieq_codemp integer, @ieq_insid integer, @ieq_unmident integer, @ieq_valorent decimal(8,2), @ieq_unmidsal integer, @ieq_valorsal decimal(8,2), @ieq_factor decimal(8,2)) as
  INSERT INTO insumo_equivalencias  
         ( ieq_codemp,   
           ieq_insid,   
           ieq_unmident,   
           ieq_valorent,   
           ieq_unmidsal,   
           ieq_valorsal,   
           ieq_factor )  
  VALUES ( @ieq_codemp,   
           @ieq_insid,   
           @ieq_unmident,   
           @ieq_valorent,   
           @ieq_unmidsal,   
           @ieq_valorsal,   
           @ieq_factor )
