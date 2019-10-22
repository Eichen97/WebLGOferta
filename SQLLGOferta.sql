use master
go
if exists (select * from sys.sysdatabases where name='DBLGOferta')
drop database DBLGOferta
go
create database DBLGOferta
go
if exists (select * from sys.sql_logins where name='LogLGOferta')
drop login LogLGOferta
go
create login LogLGOferta with password='Oferta321$%'
go
use DBLGOferta
go
create user Usuario from login LogLGOferta
go
grant exec to Usuario 
go
--CREACION DE TABLAS
--TABLA ROLES
create table Roles(
Nombre varchar (24) primary key
)
go
--HARDCODEO DE ROLES
insert Roles values ('Administrador')
insert Roles values ('Deposito')
insert Roles values ('Cliente')
insert Roles values ('En Proceso')
insert Roles values ('No Autorizado')
insert Roles values ('Eliminado')
go
create table Usuarios(
ID int identity (1,1) primary key,
Nombre varchar (40) not null,
DNI int unique not null,
FechaNacimiento smalldatetime not null,
Direccion varchar (60) not null,
Mail varchar (50) unique not null,
Password varchar (40) not null,
FechaAceptacion smalldatetime default '01/01/1900',
Rol varchar (24) foreign key references Roles (Nombre) not null default 'No Autorizado'
)
go
--HARDCODEO ADMIN
insert Usuarios (Nombre,DNI,FechaNacimiento,Direccion,Mail,Password,Rol) values ('Administrador', 00 ,'01/01/2000','Dir','admin@mail.com','D033E22AE348AEB5660FC2140AEC35850C4DA997','Administrador')
go
create table Aleatorios(
IDUsuario int foreign key references Usuarios(ID) not null,
Aleatorio varchar (40) primary key
)
go
--CREACION DE STORE PROCEDURES
create procedure Usuarios_Insert(
@Nombre varchar(40),
@DNI int,
@FechaNacimiento smalldatetime,
@Direccion varchar (50),
@Mail varchar (50),
@Password varchar (40)
)
as begin
insert Usuarios (Nombre,DNI,FechaNacimiento,Direccion,Mail,Password) values
(@Nombre,@DNI,@FechaNacimiento,@Direccion,@Mail,@Password)
select @@IDENTITY
end
go
create procedure Usuarios_Delete(
@ID int)
as
delete Usuarios where ID=@ID
go
create procedure Usuarios_Update(
@ID int,
@Nombre varchar (40),
@DNI int,
@FechaNacimiento smalldatetime,
@Direccion varchar (50),
@Mail varchar (50),
@Password varchar (40),
@FechaAceptacion smalldatetime,
@Rol varchar (24)
)
as
if (@Password='')
update Usuarios set Nombre=@Nombre,
DNI=@DNI,
FechaNacimiento=@FechaNacimiento,
Direccion=@Direccion,
Mail=@Mail,
FechaAceptacion=@FechaAceptacion,
Rol=@Rol
where ID=@ID
else
update Usuarios set Nombre=@Nombre,
DNI=@DNI,
FechaNacimiento=@FechaNacimiento,
Direccion=@Direccion,
Mail=@Mail,
Password=@Password,
FechaAceptacion=@FechaAceptacion,
Rol=@Rol
where ID=@ID
go
create procedure Usuarios_Find(
@ID int)
as
select * from Usuarios where ID=@ID
go
--VALIDACIONES
create procedure Usuarios_MailExists(
@ID int,@Mail varchar (50))
as
select * from Usuarios where Mail=@Mail and ID!=@ID
go
create procedure Usuarios_DNIExists(
@ID int,@DNI int)
as
select * from Usuarios where DNI=@DNI and ID!=@ID
go
create procedure Usuarios_Login(
@Mail varchar (50),@Password varchar (40))
as
select * from Usuarios where Mail=@Mail and Password=@Password
go
create procedure Usuarios_List
as
select * from Usuarios order by Nombre asc 
go
create procedure Usuarios_FindByMail
(@Mail varchar (50))
as
select * from Usuarios where Mail=@Mail
go
create procedure Aleatorios_Insert
(@IDUsuario int,@Aleatorio varchar(40))
as 
insert Aleatorios values (@IDUsuario,@Aleatorio)
go
create procedure Aleatorios_Delete(@Aleatorio varchar(40))
as
delete Aleatorios where Aleatorio=@Aleatorio
go
create procedure Aleatorios_Find(@Aleatorio varchar (40))
as
select * from Aleatorios where Aleatorio=@Aleatorio
go

use DBLGOferta
create table Sectores
(
ID int identity (1,1) primary key,
Nombre varchar (30) unique not null
)
go
create procedure Sectores_Insert (@Nombre varchar (30))
as begin
insert Sectores values (@Nombre)
select @@IDENTITY
end
go
create procedure Sectores_Delete (@ID int) as
delete Sectores where ID=@ID
go
create procedure Sectores_Update (@ID int, @Nombre varchar (30)) 
as
update Sectores set
Nombre=@Nombre where ID=@ID
go
create procedure Sectores_Find (@ID int) as
select * from Sectores where ID=@ID
go
create procedure Sectores_List as
select * from Sectores
go
create procedure Sectores_Exists as
select Count (*) from Sectores
go
create procedure Sectores_NameExists (@ID int, @Nombre varchar (30)) 
as
select Count (*) from Sectores where Nombre=@Nombre and ID!=@ID
go
--Tabla Categorias
create table Categorias
(
ID int identity (1,1) primary key,
Nombre varchar (30) not null,
IDSector int foreign key references Sectores(ID) not null,
Constraint UKCategorias unique (Nombre, IDSector)
)
go
--Store Procedures de Categorias
create procedure Categorias_Insert (@Nombre varchar (30), @IDSector int)
as begin
insert Categorias values (@Nombre, @IDSector)
select @@IDENTITY
end
go
create procedure Categorias_Delete (@ID int)
as
delete Categorias where ID=@ID
go
create procedure Categorias_Update (@ID int, @Nombre varchar (30), @IDSector int)
as
update Categorias set
Nombre=@Nombre, IDSector=@IDSector where ID=@ID
go
create procedure Categorias_Find (@ID int)
as
select * from Categorias where ID=@ID
go
create procedure Categorias_List (@IDSector int)
as
select * from Categorias where IDSector=@IDSector
go
create procedure Categorias_Exists 
as
select Count (*) from Categorias
go
create procedure Categorias_NameExists (@ID int, @Nombre varchar (30))
as
select Count (*) from Categorias where Nombre=@Nombre and ID!=@ID
go
--Tabla Productos
create table Productos
(
ID int identity (1,1) primary key,
Nombre varchar (60) unique not null,
Precio float default 0,
Stock int default 0,
Reserva int default 0,
IDCategoria int foreign key references Categorias(ID) not null
)
go
create table Pedidos
(
ID int identity (1,1) primary key,
IDCliente int foreign key references Usuarios(ID) not null,
Fecha smalldatetime default getdate(),
Estado varchar (20) default 'Iniciado', --LOS ESTADOS SON INICIADO, FINALIZADO, FACTURADO Y ANULADO
)
go
create table ItemsPedidos
(
ID int identity (1,1) primary key,
IDPedido int foreign key references Pedidos(ID) not null,
IDProducto int foreign key references Productos(ID) not null,
Cantidad int default 0,
Constraint UKItemPedido unique (IDPedido, IDProducto)
)
go
create table Facturas
(
ID int identity (1,1) primary key,
NumeroFactura int not null,
FechaFactura smalldatetime default getdate(),
IDPedido int foreign key references Pedidos(ID) not null,
)
go

--Store Procedures de Productos
create procedure Productos_Insert (@Nombre varchar (60), @Precio float, @Stock int, @Reserva int, @IDCategoria int )
as begin
insert Productos values (@Nombre, @Precio, @Stock, @Reserva, @IDCategoria)
select @@IDENTITY
end
go
create procedure Productos_Delete (@ID int)
as
delete Productos where ID=@ID
go
create procedure Productos_Update (@ID int, @Nombre varchar (60), @Precio float, @Stock int, @Reserva int, @IDCategoria int )
as
update Productos set
Nombre = @Nombre, Precio = @Precio, Stock = @Stock, Reserva = @Reserva, IDCategoria = @IDCategoria where ID=@ID
go
create procedure Productos_Find (@ID int)
as
select * from Productos where ID=@ID
go
create procedure Productos_List
as
select * from Productos
go
create procedure Productos_UpdateReserva(@Cant int, @IDProducto int)
as
begin
declare @Disponible as int
select @Disponible = Productos.Stock - Productos.Reserva from Productos
where Productos.ID = @IDProducto
if (@Disponible - @Cant) >= 0
begin
update Productos set
Productos.Reserva = Productos.Reserva + @Cant
where Productos.ID = @IDProducto
end
else
raiserror ('ERROR: No tenemos suficiente stock.',2,2)
end
go
create procedure Productos_ListAvailable
as
select * from Productos where Productos.Reserva < Productos.Stock
go
create procedure Productos_NameExists (@ID int, @Nombre varchar (60))
as
select Count (*) from Productos where Nombre=@Nombre and ID!=@ID
go
create procedure Pedidos_Update(@ID int, @IDCliente int, @Fecha smalldatetime,@Estado varchar(20))
as
update Pedidos set IDCliente=@IDCliente, Fecha=@Fecha, Estado=@Estado where ID=@ID
go
create procedure Pedidos_Delete(@ID int)
as
delete Pedidos where ID=@ID
go
create procedure Pedidos_Find(@ID int)
as
select * from Pedidos where ID=@ID
go
create procedure Pedidos_List
as
select * from Pedidos
go
create procedure ItemsPedidos_Insert(@IDPedido int, @IDProducto int,@Cantidad int)
as begin
insert ItemsPedidos (IDPedido,IDProducto,Cantidad) values (@IDPedido, @IDProducto, @Cantidad)
select @@IDENTITY
end
go
create procedure ItemsPedidos_Update(@ID int, @IDPedido int, @IDProducto int, @Cantidad int)
as
update ItemsPedidos set IDPedido=@IDPedido, IDProducto=@IDProducto, Cantidad=@Cantidad where ID=@ID
go
create procedure ItemsPedidos_Find(@ID int)
as
select * from ItemsPedidos where ID=@ID
go
create procedure ItemsPedidos_List(@IDPedido int)
as
select * from ItemsPedidos where IDPedido=@IDPedido
go
create procedure ItemsPedidos_ModificarCantidad (@IDPedido int,@IDProducto int,@Cant int)
as begin
if exists (select * from ItemsPedidos where IDPedido=@IDPedido and IDProducto=@IDProducto)
update ItemsPedidos set Cantidad=Cantidad+@Cant where @IDProducto=IDProducto and IDPedido=@IDPedido
else
insert ItemsPedidos (IDPedido,IDProducto,Cantidad) values (@IDPedido,@IDProducto,@Cant)
delete ItemsPedidos where IDPedido=@IDPedido and IDProducto=IDProducto and Cantidad<=0
end
go
create procedure Facturas_Insert(@NumeroFactura int,@FechaFactura smalldatetime,@IDPedido int)
as begin
insert Facturas (NumeroFactura,FechaFactura,IDPedido) values (@NumeroFactura,@FechaFactura,@IDPedido)
select @@IDENTITY
end
go
create procedure Facturas_Update(@ID int,@NumeroFactura varchar (20),@FechaFactura smalldatetime,@IDPedido int)
as
update Facturas set NumeroFactura=@NumeroFactura, FechaFactura=@FechaFactura, IDPedido=@IDPedido where ID=@ID
go
create procedure Facturas_Delete(@ID int)
as
delete Facturas where ID=@ID
go
create procedure Facturas_Find(@ID int)
as
select * from Facturas where ID=@ID
go
create procedure Facturas_List
as
select * from Facturas
go


