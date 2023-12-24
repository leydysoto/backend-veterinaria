create database veterinaria
use veterinaria

CREATE TABLE Clientes (
    ClienteID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50),
    Telefono VARCHAR(15),
    CorreoElectronico VARCHAR(100)
);
CREATE TABLE Mascotas (
    MascotaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50),
    Especie VARCHAR(50),
    Raza VARCHAR(50),
    FechaNacimiento DATE,
    ClienteID INT,
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID)
);

CREATE TABLE Citas (
    CitaID INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE,
    ClienteID INT,
    MascotaID INT,
    FOREIGN KEY (ClienteID) REFERENCES Clientes(ClienteID),
    FOREIGN KEY (MascotaID) REFERENCES Mascotas(MascotaID)
);

CREATE TABLE Servicios (
    ServicioID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50),
    Descripcion TEXT,
    Precio DECIMAL(10, 2)
);
CREATE TABLE HistorialMedico (
    HistorialID INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE,
    Descripcion TEXT,
    Diagnostico TEXT,
    Tratamiento TEXT,
    CitaID INT,
    FOREIGN KEY (CitaID) REFERENCES Citas(CitaID)
);

-- Insertar datos en la tabla Clientes
INSERT INTO Clientes (Nombre, Telefono, CorreoElectronico)
VALUES
    ('Juan Perez', '123-456-7890', 'juan@example.com'),
    ('Maria Rodriguez', '987-654-3210', 'maria@example.com');

-- Insertar datos en la tabla Mascotas
INSERT INTO Mascotas (Nombre, Especie, Raza, FechaNacimiento, ClienteID)
VALUES
    ('Max', 'Perro', 'Labrador', '2020-01-15', 1),
    ('Luna', 'Gato', 'Siames', '2019-05-20', 2);

-- Insertar datos en la tabla Citas
INSERT INTO Citas (Fecha, ClienteID, MascotaID)
VALUES
    ('2023-01-10', 1, 1),
    ('2023-02-15', 2, 2);

-- Insertar datos en la tabla Servicios
INSERT INTO Servicios (Nombre, Descripcion, Precio)
VALUES
    ('Vacunaci�n', 'Vacuna anual para mascotas', 50.00),
    ('Esterilizaci�n', 'Procedimiento quir�rgico', 120.00);

-- Insertar datos en la tabla HistorialMedico
INSERT INTO HistorialMedico (Fecha, Descripcion, Diagnostico, Tratamiento, CitaID)
VALUES
    ('2023-01-10', 'Consulta de rutina', 'Saludable', 'Ninguno', 1),
    ('2023-02-15', 'Problemas de alimentaci�n', 'Sobrepeso', 'Cambio en la dieta', 2);


