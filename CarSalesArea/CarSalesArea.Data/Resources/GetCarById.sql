SELECT
	c.Id,
	c.Brand,
	c.Model,
	c.Year,
	c.EngineVolume,
	c.Mileage,
	c.Description,
	c.Price,
	c.VinCode,
	c.Color,
	c.Body,
	c.AreaId,
	sa.Id,
	sa.AreaLocation,
	sa.Capacity,
	c.FuelTypeId,
	ft.FuelType,
	c.PhotoPath,
	p.PhotoPath
FROM Car c
LEFT JOIN SalesArea sa ON c.AreaId = sa.Id
LEFT JOIN FuelType ft ON c.FuelTypeId = ft.FuelType
LEFT JOIN Photo p ON c.PhotoPath = p.PhotoPath
WHERE c.Id = @Id