CREATE Procedure [dbo].[_Insertar_Deudores_Contactos](@codemp integer, 
													@ctcid numeric (15),  
													@ticid integer,
                                                    @nombre varchar (250), 
                                                    @comid integer, 
                                                    @direccion varchar (800)) as 
declare @ddcid smallint
declare @existe int = 0

select @existe = count(DDC_DDCID) from DEUDORES_CONTACTOS where DDC_CODEMP = @codemp and DDC_CTCID = @ctcid and DDC_TICID = @ticid and  UPPER(LTRIM(rtrim(DDC_NOMBRE))) = UPPER(LTRIM(rtrim(@nombre)))
if @existe = 0     
begin
set @ddcid = (select IsNull(Max(ddc_ddcid)+1, 1) from deudores_contactos where ddc_codemp = @codemp and ddc_ctcid = @ctcid)    
    
  INSERT INTO deudores_contactos  
         ( ddc_codemp,   
           ddc_ctcid,   
           ddc_ddcid,   
           ddc_ticid,   
           ddc_nombre,   
           ddc_comid,   
           ddc_direccion,   
           ddc_fecing,
           ddc_estdir,
           ddc_estado   )  
  VALUES ( @codemp,   
           @ctcid,   
           @ddcid,   
           @ticid,   
           @nombre,   
           @comid,   
           @direccion,   
           getdate(),
           1,
           'A' )
           
select @ddcid ddcid
end
else
begin
select distinct DDC_DDCID ddcid from DEUDORES_CONTACTOS where DDC_CODEMP = @codemp and DDC_CTCID = @ctcid and DDC_TICID = @ticid and  UPPER(LTRIM(rtrim(DDC_NOMBRE))) = UPPER(LTRIM(rtrim(@nombre)))
end

