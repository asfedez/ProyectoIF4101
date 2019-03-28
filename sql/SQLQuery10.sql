drop table FacilidadPago;
drop table tipoPago;

create table FacilidadPago( cedula int not null primary key,
							efectivo int, 
							tarjeta int,
							cheque int);

alter table FacilidadPago add foreign key (cedula) references cliente(cedula);
