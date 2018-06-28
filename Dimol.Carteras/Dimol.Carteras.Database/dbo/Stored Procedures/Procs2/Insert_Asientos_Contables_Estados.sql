

Create Procedure Insert_Asientos_Contables_Estados(@ase_codemp integer, @ase_anio integer, @ase_tipo char(1), @ase_numero integer, 
																	@ase_estado char(1), @ase_comentario text, @ase_usrid integer) as
  INSERT INTO asientos_contables_estados  
         ( ase_codemp,   
           ase_anio,   
           ase_tipo,   
           ase_numero,   
           ase_estado,   
           ase_fecha,   
           ase_comentario,   
           ase_usrid )  
  VALUES ( @ase_codemp,   
           @ase_anio,   
           @ase_tipo,   
           @ase_numero,   
           @ase_estado,   
           getdate(),   
           @ase_comentario,   
           @ase_usrid )
