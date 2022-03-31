BEGIN TRAN

	UPDATE SalesArea
	SET AreaLocation = @AreaLocation,
		Capacity = @Capacity
	WHERE Id = @Id;

	IF(@@ERROR<>0)
		ROLLBACK;

COMMIT;