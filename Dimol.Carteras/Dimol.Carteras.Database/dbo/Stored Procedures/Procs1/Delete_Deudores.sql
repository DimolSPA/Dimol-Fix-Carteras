

Create Procedure Delete_Deudores(@ctc_codemp integer, @ctc_ctcid integer) as
delete from deudores_telefonos
where ddt_codemp = @ctc_codemp and
	      ddt_ctcid = @ctc_ctcid

delete from deudores_mail
where ddm_codemp = @ctc_codemp and
           ddm_ctcid = @ctc_ctcid

delete from deudores
where ctc_codemp = @ctc_codemp and
           ctc_ctcid = @ctc_ctcid
