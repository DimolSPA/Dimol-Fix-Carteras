

CREATE Procedure [dbo].[Insertar_Deudores_Mail](@ddm_codemp integer, @ddm_ctcid numeric (15), @ddm_mail varchar (80), @ddm_tipo char (1), @ddm_masivo char(1)) as 
declare @existe int=0

set @existe = (select count(@ddm_codemp)    from deudores_mail where  ddm_codemp = @ddm_codemp and ddm_ctcid=@ddm_ctcid and ddm_mail=@ddm_mail)  
                                                                  
if @existe = 0
begin  

  INSERT INTO deudores_mail  
         ( ddm_codemp,   
           ddm_ctcid,   
           ddm_mail,   
           ddm_tipo,
           ddm_masivo )  
  VALUES ( @ddm_codemp,   
           @ddm_ctcid,   
           @ddm_mail,   
           @ddm_tipo,
           @ddm_masivo )
end