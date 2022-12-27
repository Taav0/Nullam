CREATE TABLE private_participants (
    id INT NOT NULL PRIMARY KEY IDENTITY,
    firstName VARCHAR (100) NOT NULL,
    lastName VARCHAR (100) NOT NULL,
    securityNumber VARCHAR (11) NOT NULL UNIQUE,
    payingMethod VARCHAR(20) NULL,
    additionalInfo VARCHAR(1500) NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO private_participants (firstName, lastName, securityNumber, payingMethod, additionalInfo)
VALUES
('Bill', 'Gates', '31234567890', 'Cash', 'Comes with helicopter'),
('Elon','Musk', '32806711234', 'DogeCoin', 'SpaceShuttle'),
('Will','Smith', '33211234561', 'Card',''),
('Bob','Marley', '32147581234', 'Cash', 'Texas USA'),
('Cristiano', 'Ronaldo', '33212254568', 'Card', 'Banana'),
('Boris','Johnson', '30202453214', 'Cash', '');