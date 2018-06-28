CREATE Procedure [dbo].[_Update_Cartera_Clientes_Cpbt_Doc_Estados_Todos](@ccb_codemp integer, @ccb_pclid numeric (15), @ccb_ctcid numeric (15), @ccb_estid integer, @ccb_estcpbt char(1) ) as  
  UPDATE cartera_clientes_cpbt_doc
 Set ccb_estid = @ccb_estid,
                 ccb_fecultgest = getdate()
   WHERE ( cartera_clientes_cpbt_doc.ccb_codemp = @ccb_codemp ) AND  
         ( cartera_clientes_cpbt_doc.ccb_pclid = @ccb_pclid ) AND  
         ( cartera_clientes_cpbt_doc.ccb_ctcid = @ccb_ctcid ) AND
         CCB_ESTCPBT = @ccb_estcpbt
         and CCB_ESTID not in (13,36,38,51,239,254,40,131,253,49)  -- agregado para no sobreescribir estados de forma masiva

