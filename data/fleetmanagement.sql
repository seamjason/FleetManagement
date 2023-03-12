CREATE TABLE Chassis (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, ChassisId VARCHAR (50) NOT NULL, Series VARCHAR (50) NOT NULL, ChassisNumber INTEGER NOT NULL);
CREATE TABLE VehicleType (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Description VARCHAR (50) NOT NULL);
CREATE TABLE Vehicle (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, ChassisId INTEGER NOT NULL, VehicleTypeId INTEGER NOT NULL, Passengers INTEGER NOT NULL, Color VARCHAR (50) NOT NULL);
CREATE INDEX idx_Chassis ON Vehicle (ChassisId);
INSERT INTO VehicleType (Description) select 'Bus';
INSERT INTO VehicleType (Description) select 'Truck';
INSERT INTO VehicleType (Description) select 'Car';
INSERT INTO Chassis (ChassisId, Series, ChassisNumber) select 'Chassis 101', 'Series A', 18376;
INSERT INTO Chassis (ChassisId, Series, ChassisNumber) select 'Chassis 102', 'Series B', 24806;
INSERT INTO Chassis (ChassisId, Series, ChassisNumber) select 'Chassis 103', 'Series C', 19485;
INSERT INTO Chassis (ChassisId, Series, ChassisNumber) select 'Chassis 104', 'Series D', 72173;
INSERT INTO Chassis (ChassisId, Series, ChassisNumber) select 'Chassis 105', 'Series E', 50589;
INSERT INTO Chassis (ChassisId, Series, ChassisNumber) select 'Chassis 106', 'Series F', 7141;
INSERT INTO Vehicle (ChassisId, VehicleTypeId, Passengers, Color) select 1, 1, 42, 'Green';
INSERT INTO Vehicle (ChassisId, VehicleTypeId, Passengers, Color) select 2, 1, 42, 'Red';
INSERT INTO Vehicle (ChassisId, VehicleTypeId, Passengers, Color) select 3, 2, 1, 'Yellow';
INSERT INTO Vehicle (ChassisId, VehicleTypeId, Passengers, Color) select 4, 2, 1, 'Blue';
INSERT INTO Vehicle (ChassisId, VehicleTypeId, Passengers, Color) select 5, 3, 4, 'Orange';
INSERT INTO Vehicle (ChassisId, VehicleTypeId, Passengers, Color) select 6, 3, 4, 'Cyan';
