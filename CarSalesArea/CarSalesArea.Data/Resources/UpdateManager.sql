DECLARE @Id INT,
	@ManagerName NVARCHAR(100),
	@Surname NVARCHAR(100),
	@AreaId INT;

BEGIN TRAN

	UPDATE Manager
	SET ManagerName = @ManagerName,
		Surname = @Surname,
		AreaId = @AreaId
	WHERE Id = @Id

	IF(@@ERROR <> 0)
		ROLLBACK;

COMMIT;