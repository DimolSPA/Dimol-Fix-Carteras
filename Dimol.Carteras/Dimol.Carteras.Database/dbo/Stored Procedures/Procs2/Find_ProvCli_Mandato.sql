

Create Procedure Find_ProvCli_Mandato(@pcm_codemp integer, @pcm_pclid integer, @pcm_notid integer, @pcm_numrep varchar(20)) as
  SELECT count(provcli_mandato.pcm_pclid)  
    FROM provcli_mandato  
   WHERE ( provcli_mandato.pcm_codemp = @pcm_codemp ) AND  
         ( provcli_mandato.pcm_pclid = @pcm_pclid ) AND  
         ( provcli_mandato.pcm_notid = @pcm_notid ) AND  
         ( provcli_mandato.pcm_numrep = @pcm_numrep )
