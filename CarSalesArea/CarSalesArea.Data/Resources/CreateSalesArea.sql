BEGIN TRAN

	INSERT INTO SalesArea VALUES(
	@AreaLocation,
	@Capacity);

	IF(@@ERROR<>0)
		ROLLBACK;

COMMIT;