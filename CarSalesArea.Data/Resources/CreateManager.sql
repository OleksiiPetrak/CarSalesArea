BEGIN TRAN

INSERT INTO Manager VALUES
	(@ManagerName, @Surname, @AreaId);

	IF(@@ERROR <> 0)
		ROLLBACK;

COMMIT;