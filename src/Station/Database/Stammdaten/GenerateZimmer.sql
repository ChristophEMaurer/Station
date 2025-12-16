select 'insert into dbo.Zimmer (ID_Zimmer, Station, Nummer, Intensivbetreuung, Isolation, AnzahlBetten) values (' +
convert(varchar(10), ID_Zimmer) + ', ' +
convert(varchar(10), Station) + ', ' +
convert(varchar(10), Nummer) + ', 0, 0, 3)'

from
Zimmer
order by 
ID_Zimmer
