USE remotedatabaseserver_
GO

-- TEST TABLES
CREATE TABLE TestTable(
    Id INT IDENTITY(1, 1) PRIMARY KEY,
    Username VARCHAR(MAX) NOT NULL,
    Email VARCHAR(MAX) NOT NULL,
    Password VARCHAR(MAX) NOT NULL
);

CREATE TABLE RefrenceTable(
    ReferenceId INT IDENTITY(1, 1) PRIMARY KEY,
    ReferenceName VARCHAR(MAX) NOT NULL,
    Id INT FOREIGN KEY REFERENCES TestTable
);

DROP TABLE TestTable
DROP TABLE RefrenceTable

DROP TABLE Projects
DROP TABLE Tasks
DROP TABLE Users

SELECT * FROM Users

UPDATE Users
SET Role='user'
WHERE UserId=1

UPDATE Users
SET Role='admin'
WHERE UserName='admin'

