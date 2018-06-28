

Create Procedure Delete_Empresa_Bancos(@esb_codemp integer, @esb_bcoid integer, @esb_sucid integer, @esb_ctacte varchar (40)) as
  DELETE FROM empresa_bancos  
   WHERE ( empresa_bancos.esb_codemp = @esb_codemp ) AND  
         ( empresa_bancos.esb_bcoid = @esb_bcoid ) AND  
         ( empresa_bancos.esb_sucid = @esb_sucid ) AND  
         ( empresa_bancos.esb_ctacte = @esb_ctacte )
