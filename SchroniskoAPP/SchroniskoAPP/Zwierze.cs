using System;

namespace SchroniskoApp
{
    // Klasa bazowa dla każdego zwierzaka
    public class Zwierze
    {
        public int Id;
        public string Gatunek;
        public string Imie;
        public string Rasa;
        public int Wiek;
        public string Opis;
        public string StanZdrowia;
        public string Status;

        public Zwierze(int id, string gatunek, string imie, string rasa, int wiek, string opis, string stanZdrowia)
        {
            Id = id;
            Gatunek = gatunek;
            Imie = imie;
            Rasa = rasa;
            Wiek = wiek;
            Opis = opis;
            StanZdrowia = stanZdrowia;
            Status = "Nowy"; // domyślny status
        }

        public void OznaczDoAdopcji()
        {
            Status = "Do adopcji";
        }

        public void OznaczJakoWLeczeniu()
        {
            Status = "W leczeniu";
        }

        public void OznaczJakoAdoptowany()
        {
            Status = "Adoptowany";
        }
    }

    // Podklasy klasy Zwierze
    public class Kot : Zwierze
    {
        public Kot(int id, string gatunek, string imie, string rasa, int wiek, string opis, string stanZdrowia)
            : base(id, gatunek, imie, rasa, wiek, opis, stanZdrowia) { }
    }
    public class Pies : Zwierze
    {
        public Pies(int id, string gatunek, string imie, string rasa, int wiek, string opis, string stanZdrowia)
            : base(id, gatunek, imie, rasa, wiek, opis, stanZdrowia) { }
    }

    // Klasa użytkownika, który adoptuje zwierzę
    public class Uzytkownik
    {
        public int Id;
        public string Imie;
        public string Nazwisko;
        public string Email;

        public Uzytkownik(int id, string imie, string nazwisko, string email)
        {
            Id = id;
            Imie = imie;
            Nazwisko = nazwisko;
            Email = email;
        }
    }

    // Klasa reprezentująca proces adopcji (referencja do obiektu Zwierze i Uzytkownik)
    public class Adopcja
    {
        public Zwierze Zwierze;
        public Uzytkownik Uzytkownik;
        public DateTime DataAdopcji;

        public Adopcja(Zwierze zwierze, Uzytkownik uzytkownik, DateTime data)
        {
            Zwierze = zwierze;
            Uzytkownik = uzytkownik;
            DataAdopcji = data;
        }
    }

    // Klasa reprezentująca historię zmiany statusów
    public class HistoriaStatusu
    {
        public Zwierze Zwierze;
        public DateTime Data;
        public string StaryStatus;
        public string NowyStatus;

        public HistoriaStatusu(Zwierze zwierze, DateTime data, string stary, string nowy)
        {
            Zwierze = zwierze;
            Data = data;
            StaryStatus = stary;
            NowyStatus = nowy;
        }
    }
}
