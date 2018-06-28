

Create Procedure Update_Negociacion(@neg_codemp integer, @neg_anio smallint, @neg_negid integer,
											      @neg_fecfin datetime, @neg_estado char (1), @neg_intfut decimal (5,2), @neg_dias smallint) as 
  UPDATE negociacion  
     SET neg_codemp = @neg_codemp,   
         neg_anio = @neg_anio,   
         neg_negid = @neg_negid,   
         neg_fecfin = @neg_fecfin,   
         neg_estado = @neg_estado,   
         neg_intfut = @neg_intfut,
         neg_dias = @neg_dias  
   WHERE ( negociacion.neg_codemp = @neg_codemp ) AND  
         ( negociacion.neg_anio = @neg_anio ) AND  
         ( negociacion.neg_negid = @neg_negid )
