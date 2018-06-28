

Create Procedure Insertar_ProvCli_Mandato(@pcm_codemp integer, @pcm_pclid numeric (15), @pcm_notid integer, 
													@pcm_numrep varchar (15), @pcm_indefinido char (1), @pcm_fecvenc datetime, @pcm_fecasig datetime) as
  INSERT INTO provcli_mandato  
         ( pcm_codemp,   
           pcm_pclid,   
           pcm_notid,   
           pcm_numrep,   
           pcm_indefinido,   
           pcm_fecvenc,
           pcm_fecasig )  
  VALUES ( @pcm_codemp,   
           @pcm_pclid,   
           @pcm_notid,   
           @pcm_numrep,   
           @pcm_indefinido,   
           @pcm_fecvenc,
		@pcm_fecasig )
