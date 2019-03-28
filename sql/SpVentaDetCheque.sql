--StoredProcedure insert [VentaDet]

Create procedure [Sp_Ins_ventaDET](
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

--StoredProcedure upd [VentaDet]

Create procedure Sp_upd_ventaDET(
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

--StoredProcedure Delete [VentaDet]

Create  procedure Sp_Del_ventaDET (
@idVenta int,
@idMoto  int)
as
delete from  VentaDet where  idVenta=@idVenta and idMoto = @idMoto

--StoredProcedure Consultar [VentaDet]

create  procedure Sp_Cns_ventaDET (
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

-----------------------------------------------------------------------------------------------------

--StoredProcedure insert[Cheque]
Create   procedure [Sp_Ins_Cheque](
@idCheque int,
@idVenta int,
@numeroCheque int,
@nombreBanco varchar(25) )

as 
insert into cheque(idCheque,idVenta,numeroCheque,nombreBanco)
values (@idCheque,@idVenta,@numeroCheque,@nombreBanco);



--StoredProcedure Delete [Cheque]

Create  procedure [Sp_Del_Cheque] (
@idCheque int)
as
delete from  Cheque where  idCheque=@idCheque


--StoredProcedure consultar [Cheque]

Create  procedure [Sp_Cns_Cheque] (
@idCheque int)
as

select c.idVenta as idVenta,
c.numeroCheque as numeroCheque,
c.nombreBanco as nombreBanco
 from  Cheque c  where  idCheque=@idCheque


