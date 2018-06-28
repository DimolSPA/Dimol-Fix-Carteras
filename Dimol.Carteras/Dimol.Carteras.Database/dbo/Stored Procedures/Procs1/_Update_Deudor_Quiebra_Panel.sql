CREATE Procedure [dbo].[_Update_Deudor_Quiebra_Panel](@ctc_codemp integer, 
										@ctc_ctcid integer) as 
                                                          

declare @quiebra int = 0

SELECT @quiebra = count(rol_rolid) 
  FROM [PANEL_QUIEBRA]
  join rol 
  on rol_rolid = rolid
  where rol_ctcid = @ctc_ctcid

if @quiebra > 0 
begin
	update DEUDORES set
	CTC_QUIEBRA = 'S'
	where CTC_CODEMP= @ctc_codemp
	and ctc_ctcid = @ctc_ctcid

end
