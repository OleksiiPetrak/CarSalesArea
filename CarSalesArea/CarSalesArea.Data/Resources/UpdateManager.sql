BEGIN TRAN

	UPDATE Manager
	SET ManagerName = @ManagerName,
		Surname = @Surname,
		AreaId = @AreaId
	WHERE Id = @Id

	IF(@@ERROR <> 0)
		ROLLBACK;

COMMIT;