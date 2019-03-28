--IMEC 

if exists(select name from dbo.sysobjects where name  = 'Sp_Ins_motos')
drop  procedure  [Sp_Ins_motos]
go

create  procedure [Sp_Ins_motos](
@idmoto int,
@nombre varchar(20),
@precio numeric(38,3),
@porcentajeFlete numeric(38,3),
@existencias int
)
as
insert into [motos](idmoto, nombre, precio, porcentajeFlete, existencias )
values(@idmoto, @nombre, @precio, @porcentajeFlete, @existencias)
go

---------------------------------------------------------------------------------------
if exists(select name from dbo.sysobjects where name  = 'Sp_Cns_motos')
drop  procedure  [Sp_Cns_motos]
go

create  procedure [Sp_Cns_motos](
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
go

--------------------------------------------------------------------------------------
if exists(select name from dbo.sysobjects where name  = 'Sp_Upd_motos')
drop  procedure  [Sp_Upd_motos]
go

create  procedure [Sp_Upd_motos](
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
go

---------------------------------------------------------------------------------------
if exists(select name from dbo.sysobjects where name  = 'Sp_Del_motos')
drop  procedure  [Sp_Del_motos]
go
create  procedure [Sp_Del_motos](
@idmoto int)
as
delete from  motos where  idmoto = @idmoto
