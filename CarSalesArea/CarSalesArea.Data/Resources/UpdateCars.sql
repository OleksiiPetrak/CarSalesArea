BEGIN TRAN

	UPDATE Car
	SET 
		Brand = @Brand,
		Model = @Model,
		Year = @Year,
		EngineVolume = @EngineVolume,
		Mileage = @Mileage,
		Description = @Description,
		Price = @Price,
		VinCode = @VinCode,
		Color = @Color,
		Body = @Body,
		AreaId = @AreaId,
		FuelTypeId = @FuelTypeId,
		PhotoPath = @PhotoPath
	WHERE Id = @Id;

	IF(@@ERROR<>0)
		ROLLBACK;

COMMIT;