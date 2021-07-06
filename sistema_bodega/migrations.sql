CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `Bodegas` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Ciudad` text NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Empleados` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Rut` text NULL,
    `Nombre` text NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Productos` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nombre` text NULL,
    `Umbral` int NOT NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `ProductosBodegas` (
    `ProductoId` int NOT NULL,
    `BodegaId` int NOT NULL,
    `Cantidad` int NOT NULL,
    PRIMARY KEY (`ProductoId`, `BodegaId`),
    CONSTRAINT `FK_ProductosBodegas_Bodegas_BodegaId` FOREIGN KEY (`BodegaId`) REFERENCES `Bodegas` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductosBodegas_Productos_ProductoId` FOREIGN KEY (`ProductoId`) REFERENCES `Productos` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ProductosBodegasEmpleados` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ProductoId` int NOT NULL,
    `BodegaId` int NOT NULL,
    `EmpleadoId` int NOT NULL,
    `Fecha` datetime NOT NULL,
    `Cantidad` int NOT NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ProductosBodegasEmpleados_Bodegas_BodegaId` FOREIGN KEY (`BodegaId`) REFERENCES `Bodegas` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductosBodegasEmpleados_Empleados_EmpleadoId` FOREIGN KEY (`EmpleadoId`) REFERENCES `Empleados` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductosBodegasEmpleados_Productos_ProductoId` FOREIGN KEY (`ProductoId`) REFERENCES `Productos` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_ProductosBodegas_BodegaId` ON `ProductosBodegas` (`BodegaId`);

CREATE INDEX `IX_ProductosBodegasEmpleados_BodegaId` ON `ProductosBodegasEmpleados` (`BodegaId`);

CREATE INDEX `IX_ProductosBodegasEmpleados_EmpleadoId` ON `ProductosBodegasEmpleados` (`EmpleadoId`);

CREATE INDEX `IX_ProductosBodegasEmpleados_ProductoId` ON `ProductosBodegasEmpleados` (`ProductoId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20210702064636_reset', '5.0.7');

COMMIT;

