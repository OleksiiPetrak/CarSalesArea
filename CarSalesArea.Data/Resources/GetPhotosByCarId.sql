SELECT
	p.Id,
	p.PhotoPath,
	p.CarId,
	c.Id
FROM Photo P
LEFT JOIN Car c
ON p.CarId=c.Id
WHERE p.CarId = @Id