

Create Procedure Delete_Insumos_Equivalencias(@ieq_codemp integer, @ieq_insid integer) as
    DELETE FROM insumo_equivalencias  
   WHERE ( insumo_equivalencias.ieq_codemp = @ieq_codemp ) AND  
         ( insumo_equivalencias.ieq_insid = @ieq_insid )
