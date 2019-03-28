--IMEC 

if exists(select name from dbo.sysobjects where name  = 'Sp_Ins_ventaEnc')
drop  procedure  [Sp_Ins_ventaEnc]
go

create  procedure [Sp_Ins_ventaEnc](
@idVenta int,
@cedula int,
@fecha datetime,
@tipoPago varchar(20),
@montoDescuento numeric(38,2),
@subtotal numeric(38,2),
@totalDolares numeric(38,2),
@total numeric(38,2)
)
as
insert into ventaEnc(idVenta, cedula, fecha, tipoPago, montoDescuento, subtotal, totalDolares, total)
values(@idVenta, @cedula, @fecha,@tipoPago, @montoDescuento, @subtotal, @totalDolares, @total)
go

---------------------------------------------------------------------------------------
if exists(select name from dbo.sysobjects where name  = 'Sp_Cns_ventaEnc')
drop  procedure  [Sp_Cns_ventaEnc]
go

create  procedure [Sp_Cns_ventaEnc](
@idVenta int
)
as
select
	idVenta as codigo_Venta,
	cedula as cedula,
	fecha as fecha,
	tipoPago as tipo_Pago,
	montoDescuento as montoDescuento,
	subtotal as subtotal,
	totalDolares as totalDolares,
	total as total 
 from  ventaEnc
where  idVenta = @idVenta
go

--------------------------------------------------------------------------------------
if exists(select name from dbo.sysobjects where name  = 'Sp_Upd_ventaEnc')
drop  procedure  [Sp_Upd_ventaEnc]
go

create  procedure [Sp_Upd_ventaEnc](
@idVenta int,
@cedula int,
@fecha datetime,
@tipoPago varchar(20),
@montoDescuento numeric(38,2),
@subtotal numeric(38,2),
@totalDolares numeric(38,2),
@total numeric(38,2)
)
as
update  [ventaEnc] set
idVenta = @idVenta,
cedula = @cedula,
fecha = @fecha,
tipoPago = @tipoPago,
montoDescuento = @montoDescuento,
subtotal = @subtotal,
totalDolares = @totalDolares,
total = @total 
where idVenta = @idVenta
go

---------------------------------------------------------------------------------------
if exists(select name from dbo.sysobjects where name  = 'Sp_Del_ventaEnc')
drop  procedure  [Sp_Del_ventaEnc]
go
create  procedure [Sp_Del_ventaEnc](
@idVenta int)
as
delete from  ventaEnc where  idVenta = @idVenta
go

