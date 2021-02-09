BEGIN TRAN

	INSERT INTO Car VALUES (
		@Brand,
		@Model,
		@Year,
		@EngineVolume,
		@Mileage,
		@Description,
		@Price,
		@VinCode,
		@Color,
		@Body,
		@AreaId,
		@FuelTypeId,
		@PhotoPath);

	IF(@@ERROR<>0)
		ROLLBACK;

COMMIT;
