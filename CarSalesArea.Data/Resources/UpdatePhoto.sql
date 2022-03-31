BEGIN TRAN

	UPDATE Photo
	SET PhotoPath = @PhotoPath,
		CarId = @CarId
	WHERE Id = @Id

	IF(@@ERROR<>0)
		ROLLBACK;

COMMIT;