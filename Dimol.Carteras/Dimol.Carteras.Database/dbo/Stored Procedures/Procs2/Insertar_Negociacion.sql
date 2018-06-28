

Create Procedure Insertar_Negociacion(@neg_codemp integer,  @neg_negid integer, @neg_usrid integer, @neg_ctcid numeric (15), 
												@neg_fecfin datetime, @neg_intfut decimal (5,2), @neg_dias smallint) as 
 INSERT INTO negociacion  
         ( neg_codemp,   
           neg_anio,   
           neg_negid,   
           neg_usrid,   
           neg_ctcid,   
           neg_fecini,   
           neg_fecfin,   
           neg_estado,   
           neg_intfut,
           neg_dias )  
  VALUES ( @neg_codemp,   
          datepart(year, getdate()),   
           @neg_negid,   
           @neg_usrid,   
           @neg_ctcid,   
            getdate(),   
           @neg_fecfin,   
           'E',   
           @neg_intfut,
           @neg_dias )
