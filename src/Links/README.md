## Zlinkuj się z enova365.​

### Praktyczny przykład użycia linków do dokumentów.​

Następujące elementy składają się na przykład (kroki prezentacji):
* Utworzenie dodatku
* Dodanie workera generującego link (wysyłka e-mail)
* Dodanie handlera obsługującego link (wyświetlenie MessageBox'a)

### Jak uruchomić?

* ***Wymagana enova365 w wersji >= 12.1***
* W _Opcje->Systemowe->Serwer aplikacji->Adres url serwera web_ ustawić adres serwera aplikacji enova365 (np. [http://localhost:1080/](http://localhost:1080/))
* Dodać konto pocztowe skonfigurowane tylko do wysyłki i nazwę wkleić w _MailSender.cs_:
```
var konto = CRMModule.GetInstance(session).KontaPocztowe.WgNazwa[nazwa_konta];
```
* Dodać definicję dokumentu dodatkowego z następującymi polami (*nazwy używane w kodzie*):
    * _Content_ - pole wielolinijkowe zawierające treść maila
    * _Info_ - pole tekstowe wyświetlające link w czytelnej postaci
    * _Pars_ - parametry przekazywane do obsługi link'u (może je dowolnie wykorzystać)
    
    Nadać uprawnienia do tej definicji i utworzyć przynajmniej jeden dokument dodatkowy

### Autor

Klaudiusz Bryja​

Programista modułu **Workflow/DM**

GeekOut 2017, 11-12.05.2017, Smardzewice

[http://www.enova365.pl](http://www.enova365.pl)