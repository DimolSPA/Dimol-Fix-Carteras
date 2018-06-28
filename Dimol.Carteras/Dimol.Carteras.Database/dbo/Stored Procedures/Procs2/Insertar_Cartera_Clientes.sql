CREATE Procedure [dbo].[Insertar_Cartera_Clientes](@ctc_codemp integer, @ctc_pclid numeric (15), @ctc_ctcid numeric (15)) as 
declare @existe int = 0
select @existe = count(ctc_pclid) from cartera_clientes
where ctc_codemp =@ctc_codemp
and  ctc_pclid= @ctc_pclid
and  ctc_ctcid= @ctc_ctcid 

if @existe <= 0 
begin

  INSERT INTO cartera_clientes  
         ( ctc_codemp,   
           ctc_pclid,   
           ctc_ctcid )  
  VALUES ( @ctc_codemp,   
           @ctc_pclid,   
           @ctc_ctcid )
       
end
