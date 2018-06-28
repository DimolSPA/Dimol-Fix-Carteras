

Create Procedure Update_Periodo_Contable_Cierre(@ast_codemp integer, @ast_anio integer, @usrid integer)  as
insert into asientos_contables_estados
  SELECT asientos_contables.ast_codemp,   
         asientos_contables.ast_anio,   
         asientos_contables.ast_tipo,   
         asientos_contables.ast_numero,   
         'P',
         getdate(),
         'FINALIZACION DE AÑO CONTABLE',
         @usrid 
    FROM asientos_contables  
   WHERE ( asientos_contables.ast_codemp = @ast_codemp ) AND  
         ( asientos_contables.ast_anio = @ast_anio )   


  UPDATE asientos_contables  
     SET ast_estado = 'P'  
   WHERE ( asientos_contables.ast_codemp = @ast_codemp ) AND  
         ( asientos_contables.ast_anio = @ast_anio and ast_estado not in ('P','X') )   
           

  UPDATE periodos_contables  
     SET pec_habilitado = 'N',   
         pec_finalizado = 'S'  
   WHERE ( periodos_contables.pec_codemp = @ast_codemp ) AND  
         ( periodos_contables.pec_anio = @ast_anio  )
