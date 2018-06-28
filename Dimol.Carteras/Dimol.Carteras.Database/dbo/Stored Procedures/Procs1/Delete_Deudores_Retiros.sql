

create Procedure Delete_Deudores_Retiros(@ddr_codemp integer, @ddr_ctcid integer, @ddr_ddrid integer) as    
DELETE FROM deudores_retiros       
WHERE ( deudores_retiros.ddr_codemp= @ddr_codemp ) AND 
      ( deudores_retiros.ddr_ctcid = @ddr_ctcid) AND
      ( deudores_retiros.ddr_ddrid = @ddr_ddrid )
