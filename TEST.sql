DROP TABLE Usuarios;

CREATE TABLE Usuarios (
	id_usuario INTEGER PRIMARY KEY,
    nombre varchar(20) not null,
	genero varchar(20) not null
);

INSERT INTO Usuarios VALUES (1, 'FUNCIONA', 'HOLA');
SELECT * FROM Usuarios;