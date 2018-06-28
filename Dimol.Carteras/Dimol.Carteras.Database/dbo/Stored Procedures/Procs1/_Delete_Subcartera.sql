create Procedure [dbo].[_Delete_Subcartera]
(
			@codemp integer, 
			@id integer) 
as  
  
   delete from [SUBCARTERAS]
  where [SBC_CODEMP]= @codemp
  and [SBC_SBCID] = @id
