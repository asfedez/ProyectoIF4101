--IMEC 

if exists(select name from dbo.sysobjects where name  = 'Sp_Ins_FacilidadPago')
drop  procedure  [Sp_Ins_FacilidadPago]
go

create  procedure [Sp_Ins_FacilidadPago](
@cedula int,
@efectivo int,
@tarjeta int,
@cheque int)
as
insert into [FacilidadPago](cedula, efectivo, tarjeta, cheque)
values(@cedula, @efectivo, @tarjeta, @cheque)
go

---------------------------------------------------------------------------------------
if exists(select name from dbo.sysobjects where name  = 'Sp_Cns_FacilidadPago')
drop  procedure  [Sp_Cns_FacilidadPago]
go

create  procedure [Sp_Cns_FacilidadPago](
@cedula int)
as
select cedula, efectivo, tarjeta, cheque 
 from  FacilidadPago  with (nolock)
where  cedula = @cedula
go

--------------------------------------------------------------------------------------
if exists(select name from dbo.sysobjects where name  = 'Sp_Upd_FacilidadPago')
drop  procedure  [Sp_Upd_FacilidadPago]
go

create  procedure [Sp_Upd_FacilidadPago](
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
go

---------------------------------------------------------------------------------------
if exists(select name from dbo.sysobjects where name  = 'Sp_Del_FacilidadPago')
drop  procedure  [Sp_Del_FacilidadPago]
go
create  procedure [Sp_Del_FacilidadPago](
@cedula int)
as
delete from  FacilidadPago where  cedula = @cedula
go

