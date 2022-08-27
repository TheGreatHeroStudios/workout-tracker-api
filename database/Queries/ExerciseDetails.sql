SELECT 
	ex.exercise_name,
	m.muscle_short_desc,
	m.muscle_long_desc,
	mg.muscle_group_desc
FROM 
	exercise ex
	INNER JOIN x_exercise_muscle ex_m
		ON ex.exercise_id = ex_m.exercise_id
	INNER JOIN c_muscle m
		ON m.muscle_id = ex_m.muscle_id
	INNER JOIN c_muscle_group mg
		ON mg.muscle_group_id = m.muscle_group_id