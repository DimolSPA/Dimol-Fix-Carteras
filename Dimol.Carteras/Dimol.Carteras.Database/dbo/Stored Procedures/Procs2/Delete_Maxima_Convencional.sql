

Create Procedure Delete_Maxima_Convencional(@mxc_codemp integer, @mxc_mxcid integer) as
  DELETE FROM maxima_convencional  
   WHERE ( maxima_convencional.mxc_codemp = @mxc_codemp ) AND  
         ( maxima_convencional.mxc_mxcid = @mxc_mxcid ) 
  DELETE FROM maxima_convencional  
   WHERE ( maxima_convencional.mxc_codemp = @mxc_codemp ) AND  
         ( maxima_convencional.mxc_mxcid = @mxc_mxcid )
