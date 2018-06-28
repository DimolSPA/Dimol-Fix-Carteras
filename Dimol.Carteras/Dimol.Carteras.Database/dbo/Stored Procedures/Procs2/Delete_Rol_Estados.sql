

CREATE Procedure [dbo].[Delete_Rol_Estados](@rle_codemp integer, @rle_rolid integer, @rle_estid smallint, @rle_esjid integer, @rle_fecha datetime) as

declare @count int = 0

  select @count = count(RLE_CODEMP) FROM rol_estados  
   WHERE  rol_estados.rle_codemp = @rle_codemp  AND  
          rol_estados.rle_rolid = @rle_rolid  AND  
          rol_estados.rle_estid = @rle_estid  AND  
          rol_estados.rle_esjid = @rle_esjid  AND  
          datepart(year,rol_estados.rle_fecha) = datepart(year,@rle_fecha)  AND  
          datepart(month,rol_estados.rle_fecha) = datepart(month,@rle_fecha)  AND
          datepart(day,rol_estados.rle_fecha) = datepart(day,@rle_fecha) AND
          datepart(hour,rol_estados.rle_fecha) = datepart(hour,@rle_fecha) AND
          datepart(minute,rol_estados.rle_fecha) = datepart(minute,@rle_fecha) AND
          datepart(second,rol_estados.rle_fecha) = datepart(second,@rle_fecha)
  if @count > 1
  begin
   DELETE FROM rol_estados  
   WHERE  rol_estados.rle_codemp = @rle_codemp  AND  
          rol_estados.rle_rolid = @rle_rolid  AND  
          rol_estados.rle_estid = @rle_estid  AND  
          rol_estados.rle_esjid = @rle_esjid  AND  
          datepart(year,rol_estados.rle_fecha) = datepart(year,@rle_fecha)  AND  
          datepart(month,rol_estados.rle_fecha) = datepart(month,@rle_fecha)  AND
          datepart(day,rol_estados.rle_fecha) = datepart(day,@rle_fecha) AND
          datepart(hour,rol_estados.rle_fecha) = datepart(hour,@rle_fecha) AND
          datepart(minute,rol_estados.rle_fecha) = datepart(minute,@rle_fecha) AND
          datepart(second,rol_estados.rle_fecha) = datepart(second,@rle_fecha)
          
          
---- modificacion informe judicial
declare @estid int, @esjid int, @fecjud datetime


select top 1 @estid=RLE_ESTID, @esjid = RLE_ESJID, @fecjud = RLE_FECHA  FROM rol_estados  
   WHERE  rol_estados.rle_codemp = @rle_codemp  AND  
          rol_estados.rle_rolid = @rle_rolid  AND  
          rol_estados.rle_estid = @rle_estid  AND  
          rol_estados.rle_esjid = @rle_esjid  
          order by rol_estados.rle_fecha desc

  UPDATE rol  
     SET rol_estid = @estid,   
         rol_esjid = @esjid,   
         rol_fecjud = @fecjud,   
         rol_fecultgest = getdate()  
   WHERE  rol.rol_codemp = @rle_codemp  AND  
          rol.rol_rolid =  @rle_rolid 
          
  end