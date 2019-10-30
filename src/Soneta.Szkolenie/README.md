# Projekt do Szkolenia Technicznego - część warsztatowa

## Wstęp

Utworzenie przy pomocy szablonu "Soneta AddOn (Soneta.SDK)" solution zawierającego typowe projekty dodatku oraz omówienie ich zastosowania: warstwa biznesowa, warstwa UI i warstwa testowa.

Ułatwienia przy tworzeniu i debuggowaniu dodatku. 

## Zad.1

Tworzenie własnegho widoku z danych  programu enova365 - szablon "Soneta - ViewInfo". Rejestracja widoku dla programu enova365.

    Soneta.Szkolenie.UI/ViewInfo/KlienciViewInfo.cs
    Soneta.Szkolenie.UI/ViewInfo/Klienci.viewform.xml
    Soneta.Szkolenie.UI/ViewInfo/ViewInfoReg.cs

## Zad.2

Tworzenie własnego workera operującego na obiektach enova365 - szablon "Soneta - Worker".

Parametry workera. Definiowanie własnego okna parametrów workera.

Rezultaty operacji oraz klasa MessageBoxInformation.

    Soneta.Szkolenie.UI/Workers/Kontrahent.UstawRabatWorker.cs
    Soneta.Szkolenie.UI/UI/UstawRabatWorkerParams.Ogolne.pageform.xml

## Zad.3

Rozszerzanie bazy danych enova365 o własne tabele zdefiniowane w dodatku (szablon "Soneta - Business Xml") oraz ich inicjowanie: pliki business.xml i dbinit.xml

    Soneta.Szkolenie/Szkolenie.business.xml (=> Soneta.Szkolenie/Szkolenie.busines.cs)
    Soneta.Szkolenie/Szkolenie.dbinit.xml
    Soneta.Szkolenie/Loty.cs + Soneta.Szkolenie/Lot.cs
    Soneta.Szkolenie/Maszyny.cs + Soneta.Szkolenie/Maszyna.cs
    Soneta.Szkolenie/Rezerwacje.cs + Soneta.Szkolenie/Rezerwacja.cs

## Zad.4

Tworzenie widoków dla własnych tabel dodatku.

Definiowanie filtrów dla widoków.

Umożliwienie edycji danych we własnych tabelach (NewRowAttribute).

    Soneta.Szkolenie.UI/ViewForm/KatalogLotow.viewform.xml
    Soneta.Szkolenie.UI/ViewInfo/KatalogLotowViewInfo.cs
    Soneta.Szkolenie.UI/ViewForm/KatalogMaszyn.viewform.xml
    Soneta.Szkolenie.UI/ViewInfo/KatalogMaszynViewInfo.cs
    Soneta.Szkolenie.UI/ViewForm/Rezerwacje.viewform.xml
    Soneta.Szkolenie.UI/ViewInfo/RezerwacjeViewInfo.cs

## Zad.5

Okna edycji danych dla obiektów wprowadzonych przez dodatek (szablon "Soneta - PageForm"). Elementy używane w plikach pageform.xml.

    Soneta.Szkolenie.UI/UI/Lot.ogolne.pageform.xml
    Soneta.Szkolenie.UI/UI/Maszyna.ogolne.pageform.xml
    Soneta.Szkolenie.UI/UI/Rezerwacja.ogolne.pageform.xml

Okna lookupów dla naszych tabel - lookupform.xml

    Soneta.Szkolenie.UI/LookupForm/Lot.lookupform.xml
    Soneta.Szkolenie.UI/LookupForm/Maszyna.lookupform.form

Możliwość nadpisania domyślnych lookupów i dropdownów (GetList).

    Soneta.Szkolenie/Rezerwacja.cs

## Zad.6

Dodanie własnej zakładki na obiekcie z enova365 - extender na podstawie szablonu "Soneta - PageForm".

    Soneta.Szkolenie/Extenders/KontrahentExtender.cs
    Soneta.Szkolenie.UI/UI/Kontrahent.Loty.pageform.xml
    