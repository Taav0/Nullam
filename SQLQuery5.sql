CREATE TABLE events_2(
	id INT Primary key identity,
	private_participants_id INT Not NUll,
	name VARCHAR (100) NOT NULL,
	eventDate Date NULL,
	eventLocation VARCHAR (100) null,
	info VARCHAR (250),
	FOREIGN KEY (private_participants_id) REFERENCES private_participants(id)

	);

