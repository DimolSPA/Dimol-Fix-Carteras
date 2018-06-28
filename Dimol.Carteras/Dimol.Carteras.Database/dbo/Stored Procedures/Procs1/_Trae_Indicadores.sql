
CREATE Procedure [dbo].[_Trae_Indicadores]      
(      
  @codemp int           
  )      
     
as        
declare 
@fecha datetime,
@dolar decimal(15,2),
@uf decimal(15,2)

select @fecha =  CONVERT(date, getdate())    
    
BEGIN      

  SELECT @dolar=isnull([MNV_VALOR],0)
  FROM [dbo].[MONEDAS_VALORES]
  where [MNV_CODEMP] = @codemp
  and  [MNV_CODMON]= 3
  and [MNV_FECHA] = @fecha
  
  SELECT @uf=isnull([MNV_VALOR],0)
  FROM [dbo].[MONEDAS_VALORES]
  where [MNV_CODEMP] = @codemp
  and  [MNV_CODMON]= 2
  and [MNV_FECHA] = @fecha

 select @dolar as dolar, @uf as uf
END      

