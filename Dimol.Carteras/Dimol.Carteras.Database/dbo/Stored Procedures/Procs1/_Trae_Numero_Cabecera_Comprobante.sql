
CREATE Procedure [dbo].[_Trae_Numero_Cabecera_Comprobante](@codemp integer, @tpcid integer, @sucid integer) as

declare @existe int = 0

select @existe = count(tct_tpcid)
from tipos_cpbtdoc_talonario
where tct_codemp = @codemp and
           tct_tpcid = @tpcid and
           tct_sucid = @sucid
           
if @existe > 0 
begin
	SELECT IsNull(Max(tac_numero)+1, 1), 1
    FROM talonario_cpbtdoc,   
         tipos_cpbtdoc_talonario  
   WHERE ( tipos_cpbtdoc_talonario.tct_codemp = talonario_cpbtdoc.tac_codemp ) and  
         ( tipos_cpbtdoc_talonario.tct_tacid = talonario_cpbtdoc.tac_tacid )   AND
        ( tipos_cpbtdoc_talonario.tct_codemp = @codemp ) AND  
         ( tipos_cpbtdoc_talonario.tct_tpcid = @tpcid ) AND  
         ( tipos_cpbtdoc_talonario.tct_sucid = @sucid )
 end
 else 
 begin
	SELECT IsNull(Max(cbc_numero)+1, 1),0
    FROM cabacera_comprobantes  
   WHERE ( cabacera_comprobantes.cbc_codemp = @codemp ) AND  
         ( cabacera_comprobantes.cbc_sucid = @sucid ) AND  
         ( cabacera_comprobantes.cbc_tpcid = @tpcid )
         
 end
