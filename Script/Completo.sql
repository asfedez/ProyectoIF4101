Create database Db_Vehiculos
go
USE Db_Vehiculos
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cheque](
	[idcheque] [int] IDENTITY(1,1) NOT NULL,
	[idventa] [int] NOT NULL,
	[numeroCheque] [int] NOT NULL,
	[nombreBanco] [varchar](25) NULL,
PRIMARY KEY CLUSTERED 
(
	[idcheque] ASC,
	[idventa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cliente](
	[cedula] [int] NOT NULL,
	[nombre] [varchar](25) NULL,
	[telefono] [varchar](10) NULL,
	[direccion] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FacilidadPago]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FacilidadPago](
	[cedula] [int] NOT NULL,
	[efectivo] [int] NULL,
	[tarjeta] [int] NULL,
	[cheque] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[motos]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[motos](
	[idmoto] [int] NOT NULL,
	[nombre] [varchar](20) NULL,
	[precio] [numeric](38, 2) NULL,
	[porcentajeFlete] [numeric](38, 2) NULL,
	[existencias] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idmoto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ventaDet]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ventaDet](
	[idventa] [int] NOT NULL,
	[idMoto] [int] NOT NULL,
	[Cantidad] [int] NULL,
	[montoFleteEnvio] [numeric](38, 2) NULL,
	[montoImpuestoAduana] [numeric](38, 2) NULL,
	[montoGanancia] [numeric](38, 2) NULL,
	[montoIVA] [numeric](38, 2) NULL,
	[subtotal] [numeric](38, 2) NULL,
	[total] [numeric](38, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idventa] ASC,
	[idMoto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ventaEnc]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ventaEnc](
	[idventa] [int] NOT NULL,
	[cedula] [int] NOT NULL,
	[fecha] [datetime] NULL,
	[tipoPago] [varchar](20) NULL,
	[montoDescuento] [numeric](38, 2) NULL,
	[subtotal] [numeric](38, 2) NULL,
	[totalDolares] [numeric](38, 2) NULL,
	[total] [numeric](38, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[idventa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[cheque]  WITH CHECK ADD FOREIGN KEY([idventa])
REFERENCES [dbo].[ventaEnc] ([idventa])
GO
ALTER TABLE [dbo].[FacilidadPago]  WITH CHECK ADD FOREIGN KEY([cedula])
REFERENCES [dbo].[Cliente] ([cedula])
GO
ALTER TABLE [dbo].[ventaDet]  WITH CHECK ADD FOREIGN KEY([idMoto])
REFERENCES [dbo].[motos] ([idmoto])
GO
ALTER TABLE [dbo].[ventaDet]  WITH CHECK ADD FOREIGN KEY([idventa])
REFERENCES [dbo].[ventaEnc] ([idventa])
GO
ALTER TABLE [dbo].[ventaEnc]  WITH CHECK ADD FOREIGN KEY([cedula])
REFERENCES [dbo].[Cliente] ([cedula])
GO
/****** Object:  StoredProcedure [dbo].[ConsCliente]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--procedimiento de consulta
create  procedure [dbo].[ConsCliente](@cedula varchar(15))
as
select cedula, nombre, telefono, direccion from Cliente where cedula=@cedula

GO
/****** Object:  StoredProcedure [dbo].[DelCliente]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--procedimiento de eliminar
create  procedure [dbo].[DelCliente](@cedula int)
as
delete from Cliente where cedula=@cedula

GO
/****** Object:  StoredProcedure [dbo].[InsCliente]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***Procedimiento de clientes****/
--Procedimiento de insertar
create  procedure [dbo].[InsCliente](
@cedula varchar(15),
@nombre varchar(25),
@telefono varchar(10),
@direccion varchar(30))
as
insert  into  Cliente(cedula, nombre, telefono, direccion)
values(@cedula, @nombre, @telefono, @direccion)

GO
/****** Object:  StoredProcedure [dbo].[ModCliente]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---procedimiento de modificar
create  procedure [dbo].[ModCliente](
@cedula varchar(15),
@nombre varchar(25),
@telefono varchar(10),
@direccion varchar(30))
as
update Cliente set nombre=@nombre, telefono = @telefono, direccion = @direccion where cedula=@cedula

GO
/****** Object:  StoredProcedure [dbo].[Sp_Cns_Cheque]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create  procedure [dbo].[Sp_Cns_Cheque] (
@idCheque int)
as

select c.idVenta as idVenta,
c.numeroCheque as numeroCheque,
c.nombreBanco as nombreBanco
 from  Cheque c  where  idCheque=@idCheque
GO
/****** Object:  StoredProcedure [dbo].[Sp_Cns_FacilidadPago]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  procedure [dbo].[Sp_Cns_FacilidadPago](
@cedula int)
as
select cedula, efectivo, tarjeta, cheque 
 from  FacilidadPago  with (nolock)
where  cedula = @cedula

GO
/****** Object:  StoredProcedure [dbo].[Sp_Cns_motos]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  procedure [dbo].[Sp_Cns_motos](
@idmoto int
)
as
select
	idmoto as idmoto,
	nombre as nombre,
	precio as precio,
	porcentajeFlete as porcentajeFlete,
	existencias as existencias
 from  motos p with (nolock)
where  idmoto = @idmoto

GO
/****** Object:  StoredProcedure [dbo].[Sp_Cns_ventaDET]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  procedure [dbo].[Sp_Cns_ventaDET] (
@idVenta int)
as

select v.idVenta as idVenta,
 v.idMoto as idMoto,
v.cantidad as cantidad,
v.montoFleteEnvio as montoFleteEnvio,
v.montoImpuestoAduana as montoImpuestoAduana,
v.montoGanancia as montoGanancia,
v.montoIVA as montoIVA,
v.subTotal as subTotal,
v.total as total  from VentaDet v with (nolock)  where idVenta=@idVenta
GO
/****** Object:  StoredProcedure [dbo].[Sp_Cns_ventaEnc]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  procedure [dbo].[Sp_Cns_ventaEnc](
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

GO
/****** Object:  StoredProcedure [dbo].[Sp_Del_Cheque]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[Sp_Del_Cheque] (
@idVenta int)
as
delete from  Cheque where  idventa=@idVenta
GO
/****** Object:  StoredProcedure [dbo].[Sp_Del_FacilidadPago]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  procedure [dbo].[Sp_Del_FacilidadPago](
@cedula int)
as
delete from  FacilidadPago where  cedula = @cedula

GO
/****** Object:  StoredProcedure [dbo].[Sp_Del_motos]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  procedure [dbo].[Sp_Del_motos](
@idmoto int)
as
delete from  motos where  idmoto = @idmoto

GO
/****** Object:  StoredProcedure [dbo].[Sp_Del_ventaDET]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  procedure [dbo].[Sp_Del_ventaDET] (
 @idVenta int)
as
delete from  VentaDet where  idVenta=@idVenta
GO
/****** Object:  StoredProcedure [dbo].[Sp_Del_ventaEnc]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  procedure [dbo].[Sp_Del_ventaEnc](
@idVenta int)
as
delete from  ventaEnc where  idVenta = @idVenta

GO
/****** Object:  StoredProcedure [dbo].[Sp_Ins_Cheque]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[Sp_Ins_Cheque](
@idVenta int,
@numeroCheque int,
@nombreBanco varchar(25) )

as 
insert into cheque(idVenta,numeroCheque,nombreBanco)
values (@idVenta,@numeroCheque,@nombreBanco);
GO
/****** Object:  StoredProcedure [dbo].[Sp_Ins_FacilidadPago]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  procedure [dbo].[Sp_Ins_FacilidadPago](
@cedula int,
@efectivo int,
@tarjeta int,
@cheque int)
as
insert into [FacilidadPago](cedula, efectivo, tarjeta, cheque)
values(@cedula, @efectivo, @tarjeta, @cheque)

GO
/****** Object:  StoredProcedure [dbo].[Sp_Ins_motos]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  procedure [dbo].[Sp_Ins_motos](
@idmoto int,
@nombre varchar(20),
@precio numeric(38,3),
@porcentajeFlete numeric(38,3),
@existencias int
)
as
insert into [motos](idmoto, nombre, precio, porcentajeFlete, existencias )
values(@idmoto, @nombre, @precio, @porcentajeFlete, @existencias)

GO
/****** Object:  StoredProcedure [dbo].[Sp_Ins_ventaDET]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[Sp_Ins_ventaDET](
@idVenta int, 
@idMoto  int,
@cantidad int,
@montoFleteEnvio numeric(38,2),
@montoImpuestoAduana  numeric(38,2),
@montoGanancia numeric(38,2),
@montoIVA numeric(38,2),
@subTotal  numeric(38,2),
@total numeric(38,2))

as
insert  into  VentaDet(idVenta,idMoto,Cantidad, montoFleteEnvio, montoImpuestoAduana,montoGanancia,montoIVA,subTotal,total)
values(@idVenta,@idMoto,@cantidad,@montoFleteEnvio,@montoImpuestoAduana,@montoGanancia,@montoIVA,@subTotal,@total)
GO
/****** Object:  StoredProcedure [dbo].[Sp_Ins_ventaEnc]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  procedure [dbo].[Sp_Ins_ventaEnc](
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

GO
/****** Object:  StoredProcedure [dbo].[Sp_Upd_FacilidadPago]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  procedure [dbo].[Sp_Upd_FacilidadPago](
@cedula int,
@efectivo int,
@tarjeta int,
@cheque int)
as
update  [FacilidadPago] set
efectivo=@efectivo, 
tarjeta = @tarjeta,
cheque = @cheque
where cedula = @cedula

GO
/****** Object:  StoredProcedure [dbo].[Sp_Upd_motos]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  procedure [dbo].[Sp_Upd_motos](
@idmoto int,
@nombre varchar(20),
@precio numeric(38,3),
@porcentajeFlete numeric(38,3),
@existencias int
)
as
update  [motos] set
nombre = @nombre,
precio = @precio,
porcentajeFlete = @porcentajeFlete,
existencias = @existencias
where idmoto = @idmoto

GO
/****** Object:  StoredProcedure [dbo].[Sp_upd_ventaDET]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[Sp_upd_ventaDET](
@idVenta int, 
@idMoto  int,
@cantidad int,
@montoFleteEnvio numeric(38,2),
@montoImpuestoAduana  numeric(38,2),
@montoGanancia numeric(38,2),
@montoIVA numeric(38,2),
@subTotal  numeric(38,2),
@total numeric(38,2))

as
update [VentaDet] set

Cantidad=@cantidad,
montoFleteEnvio = @montoFleteEnvio,
montoImpuestoAduana=@montoImpuestoAduana,
montoGanancia=@montoGanancia,
montoIVA=@montoIVA,
subTotal=@subTotal,
total=@total 
where idVenta=@idVenta and idMoto = @idMoto

GO
/****** Object:  StoredProcedure [dbo].[Sp_Upd_ventaEnc]    Script Date: 8/7/2017 16:58:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  procedure [dbo].[Sp_Upd_ventaEnc](
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

GO
