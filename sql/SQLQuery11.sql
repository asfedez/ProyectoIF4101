/***Procedimiento de clientes****/
--Procedimiento de insertar
create  procedure InsCliente(
@cedula int,
@nombre varchar(25),
@telefono varchar(10),
@direccion varchar(30))
as
insert  into  Cliente(cedula, nombre, telefono, direccion)
values(@cedula, @nombre, @telefono, @direccion)
go

---procedimiento de modificar
create  procedure ModCliente(
@cedula int,
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
create  procedure ConsCliente(@cedula int)
as
select cedula, nombre, telefono, direccion from Cliente where cedula=@cedula
go



