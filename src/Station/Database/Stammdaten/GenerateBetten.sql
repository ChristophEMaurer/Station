
select 'insert into Betten (ID_Betten, ID_Zimmer, Nummer) values (' +
convert(varchar(10), ID_Betten) + ', ' +
convert(varchar(10), ID_Zimmer) + ', ' +
convert(varchar(10), Nummer) + ')'

from
Betten
order by 
ID_Betten
