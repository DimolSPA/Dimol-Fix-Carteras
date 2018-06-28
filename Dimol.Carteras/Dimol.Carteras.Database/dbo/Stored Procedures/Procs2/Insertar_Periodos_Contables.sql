

 Create Procedure Insertar_Periodos_Contables(@pec_codemp integer, @pec_anio integer, @pec_habilitado char (1), @pec_finalizado char (1)) as
 INSERT INTO periodos_contables  
         ( pec_codemp,   
           pec_anio,   
           pec_habilitado,   
           pec_finalizado )  
  VALUES ( @pec_codemp,   
           @pec_anio,   
           @pec_habilitado,   
           @pec_finalizado )
