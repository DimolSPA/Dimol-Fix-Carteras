

Create Procedure Update_Insumos_Especificaciones(@ise_codemp integer, @ise_insid numeric(15), @ise_iseid integer, @ise_tpdid integer) as    
UPDATE insumo_especificaciones
        SET ise_tpdid = @ise_tpdid       
WHERE ( insumo_especificaciones.ise_codemp= @ise_codemp ) AND           
              ( insumo_especificaciones.ise_insid =@ise_insid ) AND            
              ( insumo_especificaciones.ise_iseid = @ise_iseid)
