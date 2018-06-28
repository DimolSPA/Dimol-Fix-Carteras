

Create Procedure Update_Insumos_Equivalencias(@ieq_codemp integer, @ieq_insid integer, @ieq_unmident integer, @ieq_valorent decimal(8,2), @ieq_unmidsal integer, @ieq_valorsal decimal(8,2), @ieq_factor decimal(8,2)) as
   UPDATE insumo_equivalencias  
     SET ieq_unmident = @ieq_unmident,   
         ieq_valorent = @ieq_valorent,   
         ieq_unmidsal = @ieq_unmidsal,   
         ieq_valorsal = @ieq_valorsal,   
         ieq_factor = @ieq_factor  
   WHERE ( insumo_equivalencias.ieq_codemp = @ieq_codemp ) AND  
         ( insumo_equivalencias.ieq_insid = @ieq_insid )
