CREATE Procedure [dbo].[_Update_Cabecera_Comprobantes_Glosa] (@cbc_codemp integer, @cbc_sucid integer, @cbc_tpcid integer, @cbc_numero numeric(15), @cbc_glosa varchar(250)) as

UPDATE cabacera_comprobantes 
SET cbc_glosa = @cbc_glosa 
WHERE ( cabacera_comprobantes.cbc_codemp = @cbc_codemp ) AND  
         ( cabacera_comprobantes.cbc_sucid = @cbc_sucid ) AND  
         ( cabacera_comprobantes.cbc_tpcid = @cbc_tpcid ) AND  
         ( cabacera_comprobantes.cbc_numero = @cbc_numero )
