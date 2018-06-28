

Create Procedure Delete_Tribunales(@trb_codemp integer, @trb_trbid integer) as  
   DELETE FROM tribunales  
   WHERE ( tribunales.trb_codemp = @trb_codemp ) AND  
         ( tribunales.trb_trbid = @trb_trbid )
