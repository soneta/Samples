-- versionname:Punktacja version:4

update Punkty
set Punkty.LiczbaNalezna2 = Punkty.Liczba * DefPunkty.Mnoznik
from Punkty inner join DefPunkty on Punkty.Definicja=DefPunkty.ID