DECLARE @ManagerName NVARCHAR(100),
	@Surname NVARCHAR(100),
	@AreaId INT;

BEGIN TRAN

INSERT INTO Manager VALUES
	(@ManagerName, @Surname, @AreaId);

	IF(@@ERROR <> 0)
		ROLLBACK;

COMMIT;