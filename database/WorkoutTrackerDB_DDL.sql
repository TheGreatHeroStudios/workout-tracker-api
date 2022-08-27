IF NOT EXISTS(SELECT 1 FROM sys.databases WHERE name = 'workout-tracker') BEGIN CREATE DATABASE [workout-tracker] END 
GO 



USE [workout-tracker] 
GO



--------------------------------------------------------------------------------------------------------------------------
--												c_muscle_group															--
--------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'c_muscle_group') 
BEGIN 

CREATE TABLE c_muscle_group
(
	muscle_group_id INT IDENTITY (1, 1),
	muscle_group_desc NVARCHAR(20) NOT NULL
)

ALTER TABLE c_muscle_group ADD CONSTRAINT muscle_group_id PRIMARY KEY CLUSTERED (muscle_group_id ASC)

SET IDENTITY_INSERT c_muscle_group ON

INSERT INTO c_muscle_group
	(muscle_group_id, muscle_group_desc)
VALUES
	(1,	'Chest/Shoulders'	),
	(2,	'Abs'				),
	(3,	'Arms'				),
	(4,	'Back'				),
	(5,	'Legs/Glutes'		)

SET IDENTITY_INSERT c_muscle_group OFF

END
GO



--------------------------------------------------------------------------------------------------------------------------
--													c_muscle															--
--------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'c_muscle') 
BEGIN

CREATE TABLE c_muscle
(
	muscle_id INT IDENTITY (1, 1),
	muscle_group_id INT NOT NULL CONSTRAINT FK_cmuscle_cmusclegroup_musclegroupid REFERENCES c_muscle_group (muscle_group_id),
	muscle_short_desc NVARCHAR(20) NOT NULL,
	muscle_long_desc NVARCHAR(50) NOT NULL
)

ALTER TABLE c_muscle ADD CONSTRAINT muscle_id PRIMARY KEY CLUSTERED (muscle_id ASC)

SET IDENTITY_INSERT c_muscle ON

INSERT INTO c_muscle 
	(muscle_id, muscle_group_id, muscle_long_desc, muscle_short_desc)
VALUES
	(1,		1,	'Sternomastoid',		'Neck'				),
	(2,		1,	'Pectoralis Major',		'Chest'				),
	(3,		3,	'Biceps',				'Front of Arm'		),
	(4,		2,	'Obliques',				'Waist'				),
	(5,		3,	'Brachioradials',		'Forearm'			),
	(6,		5,	'Hip Flexors',			'Upper Thigh'		),
	(7,		5,	'Abductor',				'Outer Thigh'		),
	(8,		5,	'Quadriceps',			'Front of Thigh'	),
	(9,		5,	'Sartorius',			'Front of Thigh'	),
	(10,	5,	'Tibialis Anterior',	'Front of Calf'		),
	(11,	5,	'Soleus',				'Front of Calf'		),
	(12,	1,	'Anterior Deltoid',		'Shoulder'			),
	(13,	2,	'Rectus Abdominus',		'Stomach'			),
	(14,	5,	'Adductor',				'Inner Thigh'		),
	(15,	4,	'Trapezius',			'Upper Back'		),
	(16,	4,	'Rhomboideus',			'Upper Back'		),
	(17,	1,	'Posterior Deltoid',	'Shoulder'			),
	(18,	3,	'Triceps',				'Back of Arm'		),
	(19,	4,	'Latissimus Dorsi',		'Mid Back'			),
	(20,	4,	'Spinae Erectors',		'Lower Back'		),
	(21,	5,	'Gluteus Medius',		'Hip'				),
	(22,	5,	'Gluteus Maximus',		'Buttocks'			),
	(23,	5,	'Hamstring',			'Back of Leg'		),
	(24,	5,	'Gastrocnemius',		'Back of Calf'		)

SET IDENTITY_INSERT c_muscle OFF

END
GO



--------------------------------------------------------------------------------------------------------------------------
--													weigh_in															--
--------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'weigh_in') 
BEGIN

CREATE TABLE weigh_in
(
	weigh_in_id INT IDENTITY (1, 1),
	weigh_in_date DATETIME NOT NULL,
	weight_lbs NUMERIC(6,2) NOT NULL
)

ALTER TABLE weigh_in ADD CONSTRAINT weigh_in_id PRIMARY KEY CLUSTERED (weigh_in_id ASC)

END
GO



--------------------------------------------------------------------------------------------------------------------------
--													  workout  															--
--------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'workout') 
BEGIN

CREATE TABLE workout
(
	workout_id INT IDENTITY (1, 1),
	workout_date DATETIME NOT NULL,
	primary_muscle_group_id INT NOT NULL CONSTRAINT FK_workout_cmusclegroup_musclegroupid REFERENCES c_muscle_group (muscle_group_id)
)

ALTER TABLE workout ADD CONSTRAINT workout_id PRIMARY KEY CLUSTERED (workout_id ASC)

END
GO



--------------------------------------------------------------------------------------------------------------------------
--													exercise															--
--------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'exercise') 
BEGIN

CREATE TABLE exercise
(
	exercise_id INT IDENTITY (1, 1),
	exercise_name NVARCHAR(50) NOT NULL,
	exercise_desc NVARCHAR(MAX) NULL,
	exercise_image VARBINARY(MAX) NULL
)

ALTER TABLE exercise ADD CONSTRAINT exercise_id PRIMARY KEY CLUSTERED (exercise_id ASC)

SET IDENTITY_INSERT exercise ON

INSERT INTO exercise 
	(exercise_id, exercise_name)
VALUES
	(1,	'Bench Press'	)

SET IDENTITY_INSERT exercise OFF

END
GO



--------------------------------------------------------------------------------------------------------------------------
--											    x_exercise_muscle														--
--------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'x_exercise_muscle') 
BEGIN

CREATE TABLE x_exercise_muscle
(
	exercise_muscle_id INT IDENTITY (1, 1),
	exercise_id INT NOT NULL CONSTRAINT FK_xexercisemuscle_exercise_exerciseid REFERENCES exercise (exercise_id),
	muscle_id INT NOT NULL CONSTRAINT FK_xexercisemuscle_cmuscle_muscleid REFERENCES c_muscle (muscle_id)
)	

ALTER TABLE x_exercise_muscle ADD CONSTRAINT exercise_muscle_id PRIMARY KEY CLUSTERED (exercise_muscle_id ASC)

SET IDENTITY_INSERT x_exercise_muscle ON

INSERT INTO x_exercise_muscle
	(exercise_muscle_id, exercise_id, muscle_id)
VALUES
	(1,	1,	2	),
	(2,	1,	12	),
	(3,	1,	18	)

SET IDENTITY_INSERT x_exercise_muscle OFF

END
GO



--------------------------------------------------------------------------------------------------------------------------
--											  x_workout_exercise														--
--------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'x_workout_exercise') 
BEGIN

CREATE TABLE x_workout_exercise
(
	workout_exercise_id INT IDENTITY (1, 1),
	workout_id INT NOT NULL CONSTRAINT FK_xworkoutexercise_workout_workoutid REFERENCES workout (workout_id),
	exercise_id INT NOT NULL CONSTRAINT FK_xworkoutexercise_exercise_exerciseid REFERENCES exercise (exercise_id)
)

ALTER TABLE x_workout_exercise ADD CONSTRAINT workout_exercise_id PRIMARY KEY CLUSTERED (workout_exercise_id ASC)

END
GO



--------------------------------------------------------------------------------------------------------------------------
--													set_lap																--
--------------------------------------------------------------------------------------------------------------------------
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = 'set_lap') 
BEGIN

CREATE TABLE set_lap
(
	set_lap_id INT IDENTITY (1, 1),
	workout_exercise_id INT NOT NULL CONSTRAINT FK_setlap_xworkoutexercise_workoutexerciseid REFERENCES x_workout_exercise (workout_exercise_id),
	start_time DATETIME NULL,
	end_time DATETIME NULL,
	weight_lbs NUMERIC(4, 1) NULL
)

ALTER TABLE set_lap ADD CONSTRAINT set_lap_id PRIMARY KEY CLUSTERED (set_lap_id ASC)

END
GO