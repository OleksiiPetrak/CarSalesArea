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
		@FuelTypeId);

	IF(@@ERROR<>0)
		ROLLBACK;

COMMIT;
