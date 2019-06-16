USE GameMachine;

CREATE TABLE Player
(
  playerID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  playerName VARCHAR(100) NOT NULL,
  email VARCHAR(200),
  basePass VARCHAR(255),
  baseSalt VARCHAR(255)
);


CREATE TABLE Result
(
  resultID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  gameID VARCHAR(3) NOT NULL,
  playerID INT NOT NULL FOREIGN KEY REFERENCES Player(playerID),
  humanWin BIT NULL
);

