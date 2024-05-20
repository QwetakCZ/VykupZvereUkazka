DECLARE @dodavatel INT
DECLARE @cenik INT
DECLARE @pocet INT

-- ########################################### --
-- ZDE DEKLARUJ ID DODAVATELE A ID CENIKU Z DB --
SET @dodavatel = 19
SET @cenik = 6
SET @pocet = 10 -- pocet kusu do db
-- ########################################### --

DECLARE @counter INT
DECLARE @vaha INT
DECLARE @druh INT
DECLARE @kategorie INT
DECLARE @plomba NVARCHAR(10)
SET @counter = 1
WHILE @counter <= @pocet 
BEGIN
    SET @plomba = '09-' + CAST((ABS(CHECKSUM(NEWID()) % 999999) + 100000) AS NVARCHAR(10));
    SET @vaha = ROUND(RAND() * 130, 1);
    SET @druh = ABS(CHECKSUM(NEWID()) % 6) + 1;
    SET @kategorie = ABS(CHECKSUM(NEWID()) % 3) + 1;
	 
	 INSERT INTO "LuRa_Vykup"."dbo"."Vykup" ("IdDodavatel", "Plomba", "Druh", "Vaha", "DatumVykupu", "Kategorie", "IdUzivatele", "CisloM", "VykupnaId") VALUES (@dodavatel, @plomba, @druh, @vaha, GETDATE(), @kategorie, N'553c5ede-ca57-42a2-8f4d-6d4e3ae1fb7c', N'M132456', @cenik);
   
    SET @counter = @counter + 1
END