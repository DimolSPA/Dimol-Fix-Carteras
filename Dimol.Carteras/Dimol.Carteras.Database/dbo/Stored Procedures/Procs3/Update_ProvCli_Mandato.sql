

Create Procedure Update_ProvCli_Mandato(@pcm_codemp integer, @pcm_pclid numeric (15), @pcm_notid integer, 
													@pcm_numrep varchar (15), @pcm_indefinido char (1), 
                                                    @pcm_fecvenc datetime, @pcm_fecasig datetime) as
  UPDATE provcli_mandato  
     SET pcm_codemp = @pcm_codemp,   
         pcm_pclid = @pcm_pclid,   
         pcm_notid = @pcm_notid,   
         pcm_numrep = @pcm_numrep,   
         pcm_indefinido = @pcm_indefinido,   
         pcm_fecvenc = @pcm_fecvenc,  
         pcm_fecasig = @pcm_fecasig  
   WHERE ( provcli_mandato.pcm_codemp = @pcm_codemp ) AND  
         ( provcli_mandato.pcm_pclid = @pcm_pclid ) AND  
         ( provcli_mandato.pcm_notid = @pcm_notid ) AND  
         ( provcli_mandato.pcm_numrep = @pcm_numrep )
