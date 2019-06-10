CREATE procedure [dbo].[sp_GetProductionMutation]
	@DateFrom as varchar(200) ,
	@DateTo as varchar(200)
AS BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @Date_From datetime, @Date_To datetime;
	set @Date_From = Convert(Date,@DateFrom);
	set @Date_To = Convert(Date,@DateTo);

select 
	A.TANGGAL, 
	C.BATAS_KIRIM,
	A.NO_SPB, 
	A.NO_DO, 
	A.NAMA_KONSUMEN,
    PARTY = A.JUMLAH,
	AKUMULASI = B.AKUMULASI,
	SISA = A.JUMLAH - B.AKUMULASI
from TRN_DO A 
WHERE A.TANGGAL >= @Date_From AND A.TANGGAL <= @Date_To
JOIN TRN_SPB C ON C.NO_SPB = A.NO_SPB 
JOIN (SELECT NO_SPB, NO_DO ,PARTY, AKUMULASI = SUM(JUMLAH) FROM TRN_PENGIRIMAN GROUP BY NO_SPB, NO_DO, PARTY ) as B
 ON B.NO_SPB = A.NO_SPB
 WHERE (B.PARTY - B.AKUMULASI ) > 0
 ;
 
END