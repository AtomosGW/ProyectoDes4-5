CREATE DATABASE webPedidos;

USE webPedidos;

-- Tabla Roles
CREATE TABLE Roles (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL UNIQUE
);

-- Tabla Usuarios
CREATE TABLE Usuarios (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL,
    correo VARCHAR(50) UNIQUE,
    password VARCHAR(255) NOT NULL,
    telefono VARCHAR(15),
    role_id INT NOT NULL,
    FOREIGN KEY (role_id) REFERENCES Roles(id)
);

-- Tabla Productos
CREATE TABLE Productos (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(50) NOT NULL,
    descripcion TEXT,
    precio DECIMAL(10,2) NOT NULL,
    activo BIT DEFAULT 1
);

-- Tabla Estado
CREATE TABLE Estado (
    id INT PRIMARY KEY IDENTITY(1,1),
    estado VARCHAR(50) NOT NULL UNIQUE
);

-- Tabla Pedido
CREATE TABLE Pedido (
    id INT PRIMARY KEY IDENTITY(1,1),
    cliente_id INT NOT NULL,
    estado_id INT NOT NULL,
    total DECIMAL(10,2) DEFAULT 0.0, -- Actualizable por trigger
    fecha DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (cliente_id) REFERENCES Usuarios(id),
    FOREIGN KEY (estado_id) REFERENCES Estado(id)
);

-- Tabla PedidoProducto (relación productos-pedidos)
CREATE TABLE PedidoProducto (
    id INT PRIMARY KEY IDENTITY(1,1),
    pedido_id INT NOT NULL,
    producto_id INT NOT NULL,
    cantidad INT NOT NULL CHECK (cantidad > 0),
    subtotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (pedido_id) REFERENCES Pedido(id),
    FOREIGN KEY (producto_id) REFERENCES Productos(id)
);

-- Tabla Asignaciones
CREATE TABLE Asignaciones (
    id INT PRIMARY KEY IDENTITY(1,1),
    pedido_id INT NOT NULL,
    empleado_id INT NOT NULL,
    fecha_asignacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (pedido_id) REFERENCES Pedido(id),
    FOREIGN KEY (empleado_id) REFERENCES Usuarios(id)
);

-- Tabla MetodoPago
CREATE TABLE MetodoPago (
    id INT PRIMARY KEY IDENTITY(1,1),
    metodo VARCHAR(50) NOT NULL UNIQUE
);

-- Tabla Pago
CREATE TABLE Pago (
    id INT PRIMARY KEY IDENTITY(1,1),
    pedido_id INT NOT NULL,
    metodo_id INT NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    fecha DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (pedido_id) REFERENCES Pedido(id),
    FOREIGN KEY (metodo_id) REFERENCES MetodoPago(id)
);

USE webPedidos;

--PROCEDIMIENTOS
--INSERTAR DATOS A Roles
CREATE PROCEDURE InsertarRol
@nombre VARCHAR(50)
AS BEGIN
INSERT INTO Roles (nombre) VALUES (@nombre);
END;

--INSERTAR DATOS A Usuarios
CREATE PROCEDURE InsertarUsuario
@nombre VARCHAR(50),
@correo VARCHAR(50),
@password VARCHAR(50),
@telefono VARCHAR(15),
@role_id INT
AS BEGIN
INSERT INTO Usuarios
VALUES (@nombre, @correo, @password, @telefono, @role_id);
END;

--INSERTAR DATOS A Productos
CREATE PROCEDURE InsertarProducto
@nombre VARCHAR(50),
@descripcion TEXT,
@precio DECIMAL(10,2),
@activo BIT
AS BEGIN
INSERT INTO Productos
VALUES (@nombre, @descripcion, @precio, @activo);
END;

--INSERTAR DATOS A Estados
CREATE PROCEDURE InsertarEstado
@estado VARCHAR(50)
AS BEGIN
INSERT INTO Estado
VALUES (@estado);
END;

--INSERTAR DATO A Pedidos
CREATE PROCEDURE InsertarPedido
@cliente_id INT,
@estado_id INT
AS BEGIN
INSERT INTO Pedido
VALUES (@cliente_id, @estado_id, DEFAULT, DEFAULT);
END;

--INSERTAR DATO A PedidoProductos
CREATE PROCEDURE InsertarPedidoProductos
@pedido_id INT,
@producto_id INT,
@cantidad INT,
@subtotal DECIMAL (10,2)
AS BEGIN
INSERT INTO PedidoProducto
VALUES (@pedido_id, @producto_id, @cantidad, @subtotal);
END;

--INSERTAR DATOS A Asignaciones
CREATE PROCEDURE InsertarAsignacion
@pedido_id INT,
@empleado_id INT
AS BEGIN 
INSERT INTO Asignaciones
VALUES (@pedido_id, @empleado_id, DEFAULT);
END;

--INSERTAR DATOS A Metodos de pago
CREATE PROCEDURE InsertarMetodoPago
@metodo VARCHAR(50)
AS BEGIN
INSERT INTO MetodoPago
VALUES (@metodo);
END;

--INSERTAR DATOS A Pagos
CREATE PROCEDURE InsertarPago
@pedido_id INT,
@metodo_id INT,
@monto DECIMAL (10,2)
AS BEGIN
INSERT INTO Pago
VALUES (@pedido_id, @metodo_id, @monto, DEFAULT);
END;



--METODO PARA ACTUALIZAR PEDIDO
CREATE PROCEDURE ActualizarEstadoPedido
@pedido_id INT,
@nuevo_estado_id INT
AS BEGIN
UPDATE Pedido
SET estado_id = @nuevo_estado_id
WHERE id = @pedido_id
END;

--METODO PARA ELIMINAR ASIGNACIONES
--ELIMINA 1 O MAS ASIGNACIONES ASOCIADO A UN PEDIDO
CREATE PROCEDURE EliminarAsignacion
@asignacion_id INT
AS BEGIN
DELETE FROM Asignaciones
WHERE id = @asignacion_id;
END;
--ELIMINA TODAS LAS ASIGNACIONES DE UN PEDIDO ESPECIFICO
CREATE PROCEDURE EliminarAsignacionPorPedido
@pedido_id INT
AS BEGIN 
DELETE FROM Asignaciones
WHERE pedido_id = @pedido_id;
END;

--PROCEDIMIENTO PARA ELIMINAR PEDIDOS
CREATE PROCEDURE EliminarPedido
@pedido_id INT
AS BEGIN
DELETE FROM PedidoProducto
WHERE pedido_id = @pedido_id;
DELETE FROM Pedido
WHERE id = @pedido_id;
END;


--INSERTAR DATOS
EXEC InsertarRol 'Administrador';
EXEC InsertarRol 'Mesero';
EXEC InsertarRol 'Cocinero';
EXEC InsertarRol 'Cliente';

EXEC InsertarUsuario 'Fulano de Tal', 'tal@correo.com', 'taL2468', '1234567890', 1;
EXEC InsertarUsuario 'Fulana de Te', 'te@correo.com', 'teDEtexas01', '0987654321', 2;
EXEC InsertarUsuario 'Fulanita de Tim', 'tim@correo.com', 'TimmyHere123', '1231231231', 3;
EXEC InsertarUsuario 'Fulanito de Tom', 'tom@correo.com', 'tOmAndjErrY', '1234561230', 4;

EXEC InsertarProducto 'Pizza clasico', 'Pizza jamon y pepperoni', 12.50, 1;
EXEC InsertarProducto 'Hamburguesa', 'Hamburguesa con queso y papas fritas', 8.99, 1;
EXEC InsertarProducto 'Pasta', 'Pasta con salsa carbonara', 10.00, 0; -- Producto inactivo
EXEC InsertarProducto 'Pizza diabolico', 'Pizza con piña', 19.99, 1;

EXEC InsertarEstado 'En Preparación';
EXEC InsertarEstado 'Listo para Entregar';
EXEC InsertarEstado 'Entregado';

EXEC InsertarPedido 4, 1; -- fulanito de tom, Estado 'En Preparación'
EXEC InsertarPedido 4, 2; -- fulano de tal, Estado 'Listo para Entregar'

EXEC InsertarPedidoProductos 1, 1, 2, 25.00; -- Pedido 1, 2 Pizzas clasico
EXEC InsertarPedidoProductos 1, 2, 1, 8.99;  -- Pedido 1, 1 Hamburguesa
EXEC InsertarPedidoProductos 2, 1, 1, 19.99; -- Pedido 2, 1 Pizza diabolico

EXEC InsertarMetodoPago 'Efectivo';
EXEC InsertarMetodoPago 'Tarjeta de Crédito';
EXEC InsertarMetodoPago 'PayPal';

EXEC InsertarPago 1, 1, 33.99; -- Pago en Efectivo para el Pedido 1
EXEC InsertarPago 2, 2, 19.99; -- Pago con Tarjeta de Crédito para el Pedido 2

-------------------
-------------------
--ejemplo para actualizar pedido
EXEC ActualizarEstadoPedido @pedido_id = 1, @nuevo_estado_id = 3; -- Cambiar a 'Entregado'

--ejemplo para eliminar una asignacion
EXEC EliminarAsignacion @asignacion_id = 5;

--ejemplo para eliminar todas las asignaciones de un pedido
EXEC EliminarAsignacionesPorPedido @pedido_id = 1;

--ejemplo para eliminar un pedido
EXEC EliminarPedido @pedido_id = 1;


