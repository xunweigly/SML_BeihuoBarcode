USE [UFDATA_888_2014]
GO
/****** Object:  StoredProcedure [dbo].[zdy_sml_sp_beiliao]    Script Date: 12/20/2019 15:08:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[zdy_sml_sp_beiliao]
(@id INT)
AS
begin
---获得所有的存货编码

CREATE TABLE #tmp1
(
id1 INT IDENTITY(1,1),
cinvcode VARCHAR(50)
)
CREATE TABLE #tmp2
(
id2 INT IDENTITY(1,1),
cinvcode VARCHAR(50),
cmocode VARCHAR(50),
imoseq VARCHAR(10),
iquantity DECIMAL(18,2)

)

DELETE  FROM zdy_sml_beiliaos WHERE id = @id

INSERT INTO #tmp1(cinvcode)
SELECT DISTINCT cinvcode  FROM zdy_sml_beiliao a,dbo.MaterialAppVouchs b
WHERE a.iappids = b.AutoID AND a.id = @id

----存货编码循环
DECLARE @imax INT,@imin INT  ---最大值，最小值（也用于循环）
DECLARE @imax2 INT,@imin2 INT  ---最大值，最小值（也用于循环）
DECLARE @cinvcode VARCHAR(50),@cinvname VARCHAR(50),@cposcode VARCHAR(50)
SELECT @imax = MAX(id1) ,@imin =MIN(id1) FROM #tmp1

DECLARE @cmocode1 VARCHAR(50),@cmocode2 VARCHAR(50),@cmocode3 VARCHAR(50),@cmocode4 VARCHAR(50),@cmocode5 VARCHAR(50),@cmocode6 VARCHAR(50)

DECLARE @iqty1  DECIMAL(18,2),@iqty2  DECIMAL(18,2),@iqty3  DECIMAL(18,2),@iqty4  DECIMAL(18,2),@iqty5  DECIMAL(18,2),@iqty6  DECIMAL(18,2)

DECLARE @icnt INT,@i INT  ----存货编码明细循环用

WHILE @imin<=@imax
	BEGIN
		SELECT @cinvcode = a.cinvcode,@cinvname = b.cInvName,@cposcode = b.cinvdefine4 FROM #tmp1  a,inventory b WHERE id1 = @imin AND b.cinvcode = a.cinvcode
		TRUNCATE TABLE #tmp2		
		----查询 存货编码下的行数
		INSERT INTO #tmp2(cinvcode,cmocode,imoseq,iquantity)
		SELECT b.cinvcode, c.ccode AS  cmocode,b.irowno AS   imoseq,a.iquantity  FROM zdy_sml_beiliao a,dbo.MaterialAppVouchs b,dbo.MaterialAppVouch c
WHERE a.iappids = b.AutoID AND a.id = @id  AND cinvcode = @cinvcode AND b.ID=c.ID
		
		SELECT @imax2 = MAX(id2) ,@imin2 =MIN(id2) FROM #tmp2




		WHILE @imin2<=@imax2
			BEGIN
			
			SELECT @cmocode1  =''
				SELECT @cmocode2   =''
				SELECT @cmocode3  =''
				SELECT @cmocode4  =''
				SELECT @cmocode5  =''
				SELECT @cmocode6 =''
				
				SELECT @iqty1  =0
				SELECT @iqty2  =0
				SELECT @iqty3 =0
				SELECT @iqty4 =0
				SELECT @iqty5 =0
				SELECT @iqty6 =0
			
				SELECT @cmocode1  = cmocode+'-'+imoseq,@iqty1  =iquantity  FROM #tmp2  WHERE id2 = @imin2
				SELECT @cmocode2  = cmocode+'-'+imoseq,@iqty2  =iquantity FROM #tmp2  WHERE id2 = @imin2+1
				SELECT @cmocode3  = cmocode+'-'+imoseq,@iqty3  =iquantity FROM #tmp2  WHERE id2 = @imin2+2
				SELECT @cmocode4  = cmocode+'-'+imoseq,@iqty4  =iquantity FROM #tmp2  WHERE id2 = @imin2+3
				SELECT @cmocode5 = cmocode+'-'+imoseq,@iqty5  =iquantity FROM #tmp2  WHERE id2 = @imin2+4
				SELECT @cmocode6 = cmocode+'-'+imoseq,@iqty6  =iquantity FROM #tmp2  WHERE id2 = @imin2+5
			
				INSERT INTO dbo.zdy_sml_beiliaos
				        ( id ,
				          cinvcode ,
				          cinvname ,
				          cposcode ,
				          iquantity ,
				          cmocode1 ,
				          cmocode2 ,
				          cmocode3 ,
				          cmocode4 ,
				          cmocode5 ,
				          cmocode6
				        )
				        values(@id,@cinvcode,@cinvname,@cposcode,ISNULL(@iqty1,0)+ISNULL(@iqty2,0)+ISNULL(@iqty3,0)+ISNULL(@iqty4,0)+ISNULL(@iqty5,0)+ISNULL(@iqty6,0),@cmocode1,@cmocode2,@cmocode3,@cmocode4,@cmocode5,@cmocode6)
			
			
			SELECT @imin2 = @imin2+6
			end
		

		SELECT @imin = @imin+1
	END
	
	UPDATE   b SET cdefine35 ='1'  FROM zdy_sml_beiliao a,dbo.MaterialAppVouchs b
WHERE a.iappids = b.AutoID AND a.id = @id
end




