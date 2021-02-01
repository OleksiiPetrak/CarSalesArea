SELECT 
	m.Id,
	m.ManagerName,
	m.Surname,
	m.AreaId,
	sa.AreaLocation,
	sa.Capacity
FROM Manager m
LEFT JOIN SalesArea sa
ON m.AreaId = sa.Id

