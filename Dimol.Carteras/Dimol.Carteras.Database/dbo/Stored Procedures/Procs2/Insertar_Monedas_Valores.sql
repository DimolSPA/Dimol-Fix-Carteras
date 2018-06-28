

CREATE Procedure [dbo].[Insertar_Monedas_Valores](@mnv_codemp integer, @mnv_codmon integer, @mnv_fecha datetime, @mnv_valor decimal(10,2)) as

if not exists (select mnv_valor from monedas_valores where MNV_CODEMP = @mnv_codemp and MNV_CODMON = @mnv_codmon and MNV_FECHA = @mnv_fecha)
begin
  INSERT INTO monedas_valores  values
         ( @mnv_codemp,   
           @mnv_codmon,   
         @mnv_fecha,
         @mnv_valor)
end
