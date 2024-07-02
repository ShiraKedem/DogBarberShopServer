CREATE TABLE [USER] (
    Id INT PRIMARY KEY IDENTITY,
    UserName VARCHAR(20) NOT NULL,
    Password VARCHAR(10) NOT NULL,
    CHECK (LEN(UserName) > 2),
    CHECK (LEN(Password) >= 6)
);


CREATE TABLE [QUEUES] (
    Id INT PRIMARY KEY IDENTITY,
    UserId INT ,
	ProductionDate datetime NOT NULL,
	
    Date DATE NOT NULL,
    Hour VARCHAR(10) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES [USER](Id)
);


drop table[QUEUES]
