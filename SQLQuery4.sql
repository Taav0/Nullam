ALTER TABLE events
ADD FOREIGN KEY (id) REFERENCES private_participants(id);