DECLARE @Id INT;

BEGIN TRAN

	DELETE Manager
	WHERE Id = @Id;

	IF (@@ERROR <>0)
		ROLLBACK;

COMMIT;