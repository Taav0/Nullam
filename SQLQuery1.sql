CREATE TABLE events (
	id INT NOT NULL PRIMARY KEY IDENTITY,
	name VARCHAR (100) NOT NULL,
	eventDate smalldatetime NULL,
	eventLocation VARCHAR (100) null,
	info VARCHAR (250)
	);

	INSERT INTO events (name, eventDate, eventLocation, info)
	VALUES
	('IT event', '2022-12-23 12:43:10', 'Talinn','Dresscode Casual')