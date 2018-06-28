

Create Procedure Delete_SubCarteras(@sbc_codemp integer, @sbc_sbcid integer) as
  DELETE FROM subcarteras  
   WHERE ( subcarteras.sbc_codemp = @sbc_codemp ) AND  
         ( subcarteras.sbc_sbcid = @sbc_sbcid )
