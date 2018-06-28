

Create Procedure Delete_ProvCli_Mandato(@pcm_codemp integer, @pcm_pclid numeric (15), @pcm_notid integer, @pcm_numrep varchar (15)) as
  DELETE FROM provcli_mandato  
   WHERE ( provcli_mandato.pcm_codemp = @pcm_codemp ) AND  
         ( provcli_mandato.pcm_pclid = @pcm_pclid ) AND  
         ( provcli_mandato.pcm_notid = @pcm_notid ) AND  
         ( provcli_mandato.pcm_numrep = @pcm_numrep )
