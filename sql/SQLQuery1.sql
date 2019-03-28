create database Db_Vehiculos;
go

use Db_Vehiculos;
go
create table Cliente (	cedula int primary key,
						nombre varchar(25),
						telefono varchar(10),
						direccion varchar(30));



create table FacilidadPago( cedula int not null primary key,
							efectivo int, 
							tarjeta int,
							cheque int);


create table motos( idmoto int primary key,
					nombre varchar(20),
					precio numeric(38,2),
					porcentajeFlete numeric(38,2),
					existencias int);

create table ventaEnc (	idventa int primary key,
						cedula int not null,
						fecha datetime,
						tipoPago varchar(20),
						montoDescuento numeric(38,2),
						subtotal numeric(38,2),
						totalDolares numeric(38,2),
						total numeric(38,2));

create table cheque (	idcheque int identity,
						idventa int not null,
						numeroCheque int not null,
						nombreBanco varchar(25));




create table ventaDet (idventa int not null,
						idMoto int not null,
						Cantidad int,
						montoFleteEnvio numeric(38,2),
						montoImpuestoAduana numeric(38,2),
						montoGanancia numeric(38,2),
						montoIVA numeric(38,2),
						subtotal numeric(38,2),
						total numeric(38,2));


alter table FacilidadPago add foreign key (cedula) references cliente(cedula);



alter table cheque add primary key (idcheque, idventa);
alter table cheque add foreign key (idventa) references ventaEnc(idventa);

alter table ventaEnc add foreign key(cedula) references cliente(cedula);

alter table ventaDet add primary key (idventa, idmoto);
alter table ventaDet add foreign key (idventa) references ventaEnc(idventa);
alter table ventaDet add foreign key (idmoto) references motos(idmoto);	



/***Procedimiento de clientes****/
--Procedimiento de insertar
create  procedure InsCliente(
@cedula varchar(15),
@nombre varchar(25),
@telefono varchar(10),
@direccion varchar(30))
as
insert  into  Cliente(cedula, nombre, telefono, direccion)
values(@cedula, @nombre, @telefono, @direccion)
go

---procedimiento de modificar
create  procedure ModCliente(
@cedula varchar(15),
@nombre varchar(25),
@telefono varchar(10),
@direccion varchar(30))
as
update Cliente set nombre=@nombre, telefono = @telefono, direccion = @direccion where cedula=@cedula
go

--procedimiento de eliminar
create  procedure DelCliente(@cedula int)
as
delete from Cliente where cedula=@cedula
go

--procedimiento de consulta
create  procedure ConsCliente(@cedula varchar(15))
as
select cedula, nombre, telefono, direccion from Cliente where cedula=@cedula
go









