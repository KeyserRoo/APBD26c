CREATE TABLE Animal (
    IdAnimal int  NOT NULL IDENTITY,
    Name nvarchar(200)  NOT NULL,
    Description nvarchar(200)  NULL,
    Category nvarchar(200)  NOT NULL,
    Area nvarchar(200)  NOT NULL,
    CONSTRAINT Animal_pk PRIMARY KEY  (IdAnimal)
);

DECLARE @i INT = 0;
WHILE @i < 20
BEGIN
    INSERT INTO Animal (Name, Description, Category, Area)
    VALUES
    (
    'Animal ' + CAST(NEWID() AS NVARCHAR(200)),
    'Description ' + CAST(NEWID() AS NVARCHAR(200)),
    'Category ' + CAST(NEWID() AS NVARCHAR(200)),
    'Area ' + CAST(NEWID() AS NVARCHAR(200))
    );
    SET @i = @i + 1;
END;