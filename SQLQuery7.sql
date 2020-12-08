DECLARE @id INT 
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
imoseq INT,
iquantity DECIMAL(18,2)

)


INSERT INTO #tmp1(cinvcode)
SELECT DISTINCT cinvcode  FROM zdy_sml_beiliao a,dbo.MaterialAppVouchs b
WHERE a.iappids = b.AutoID AND a.id = @id

----存货编码循环
DECLARE @imax INT,@imin INT  ---最大值，最小值（也用于循环）
DECLARE @cinvcode VARCHAR(50),@cinvname VARCHAR(50),@cposcode VARCHAR(50)
SELECT @imax = MAX(id1) ,@imin =MIN(id1) FROM #tmp1

DECLARE @icnt INT,@i INT  ----存货编码明细循环用

WHILE @imin<=@imax
	BEGIN
		SELECT @cinvcode = a.cinvcode,@cinvname = b.cInvName,@cposcode = b.cinvdefine4 FROM #tmp1  a,inventory b WHERE id1 = @imin AND b.cinvcode = a.cinvcode
		TRUNCATE TABLE #tmp2
		
		----查询 存货编码下的行数
		INSERT INTO #tmp2(cinvcode,cmocode,imoseq,iquantity)
		SELECT b.cinvcode,b.cmocode,b.imoseq,a.iquantity  FROM zdy_sml_beiliao a,dbo.MaterialAppVouchs b
WHERE a.iappids = b.AutoID AND id = @id  AND cinvcode = @cinvcode
		
		SELECT @icnt = COUNT(*) FROM #tmp2
		WHILE 
		

		SELECT @imin = @imin+1
	end





