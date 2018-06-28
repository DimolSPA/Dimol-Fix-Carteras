

Create Procedure Update_Ejercicios_Fotos(@ejc_codemp integer, @ejc_ejcid integer, @ejc_imagen1 image, @ejc_imagen2 image, @ejc_imagen3 image) as
  UPDATE ejercicios  
     SET ejc_imagen1 = @ejc_imagen1,
             ejc_imagen2 = @ejc_imagen2,
             ejc_imagen3 = @ejc_imagen3  
   WHERE ( ejercicios.ejc_codemp = @ejc_codemp ) AND  
         ( ejercicios.ejc_ejcid = @ejc_ejcid )
