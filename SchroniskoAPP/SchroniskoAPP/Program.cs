using System;
using System.Collections.Generic;

namespace SchroniskoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Schronisko schronisko = new Schronisko(); // Tworzenie obiektu głownej klasy Schronisko
            bool dziala = true;

            // Pętla menu
            while (dziala)
            {
                Console.WriteLine("=== MENU SCHRONISKA ===");
                Console.WriteLine("1. Dodaj nowe zwierzę");
                Console.WriteLine("2. Wyświetl wszystkie zwierzęta");
                Console.WriteLine("3. Filtruj zwierzęta po gatunku");
                Console.WriteLine("4. Rejestruj adopcję");
                Console.WriteLine("5. Wyświetl historię statusów zwierzęcia");
                Console.WriteLine("6. Generuj raport");
                Console.WriteLine("7. Zmień status zwierzęcia");
                Console.WriteLine("8. Zapisz raport do pliku"); 
                Console.WriteLine("0. Wyjdź");
                Console.Write("Wybierz opcję: ");

                string opcja = Console.ReadLine();
                Console.WriteLine();
                Console.Clear();

                // Obsługa opcji wybranej przez użytkownika
                switch (opcja)
                {
                    case "1":
                        DodajZwierze(schronisko);
                        break;
                    case "2":
                        WyswietlZwierzeta(schronisko);
                        break;
                    case "3":
                        FiltrujPoGattunku(schronisko);
                        break;
                    case "4":
                        RejestrujAdopcje(schronisko);
                        break;
                    case "5":
                        PokazHistorieStatusu(schronisko);
                        break;
                    case "6":
                        schronisko.GenerujRaport();
                        break;
                    case "7":
                        ZmienStatusZwierzecia(schronisko);
                        break;
                    case "8":
                        Console.Write("Podaj nazwę pliku: ");
                        string nazwa = Console.ReadLine();
                        schronisko.ZapiszRaportDoPliku(nazwa);
                        break;
                    case "0":
                        dziala = false;
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        break;
                }
            }
        }

        // Funkcja pozwalająca na dodanie nowego Zwierzaka do Schroniska
        static void DodajZwierze(Schronisko schronisko)
        {
            // Pobieranie danych na temat Zwierzaka
            Console.Write("Gatunek (Kot/Pies): ");
            string gatunek = Console.ReadLine();
            Console.Write("Imię: ");
            string imie = Console.ReadLine();
            Console.Write("Rasa: ");
            string rasa = Console.ReadLine();
            Console.Write("Wiek: ");
            int wiek = int.Parse(Console.ReadLine());
            Console.Write("Opis: ");
            string opis = Console.ReadLine();
            Console.Write("Stan zdrowia: ");
            string zdrowie = Console.ReadLine();

            int noweId = schronisko.Zwierzeta.Count + 1;

            Zwierze zwierze;
            if (gatunek.ToLower() == "kot")
                zwierze = new Kot(noweId, gatunek, imie, rasa, wiek, opis, zdrowie);
            else
                zwierze = new Pies(noweId, gatunek, imie, rasa, wiek, opis, zdrowie);

            schronisko.DodajZwierze(zwierze);
            Console.WriteLine("Zwierzę zostało dodane.");
        }
        // Wyświetla listę wszystkich zwierząt w Schronisku
        static void WyswietlZwierzeta(Schronisko schronisko)
        {
            foreach (var z in schronisko.Zwierzeta)
            {
                Console.WriteLine($"ID: {z.Id}, Imię: {z.Imie}, Gatunek: {z.Gatunek}, Rasa: {z.Rasa}, Wiek: {z.Wiek}, Status: {z.Status}");
            }
        }
        // Pokazuje tylko zwierzęta z jednego gatunku
        static void FiltrujPoGattunku(Schronisko schronisko)
        {
            Console.Write("Podaj gatunek do filtrowania (np. Kot, Pies): ");
            string gatunek = Console.ReadLine();

            var wynik = schronisko.FiltrujPoGattunku(gatunek);
            if (wynik.Count == 0)
            {
                Console.WriteLine("Brak zwierząt danego gatunku.");
            }
            else
            {
                foreach (var z in wynik)
                {
                    Console.WriteLine($"ID: {z.Id}, Imię: {z.Imie}, Rasa: {z.Rasa}, Wiek: {z.Wiek}, Status: {z.Status}");
                }
            }
        }

        // Pozwala na zmiane statusu zwierzęcia
        static void ZmienStatusZwierzecia(Schronisko schronisko)
        {
            Console.Write("Podaj ID zwierzęcia: ");
            int id = int.Parse(Console.ReadLine());

            Zwierze zwierze = schronisko.Zwierzeta.Find(z => z.Id == id);
            if (zwierze == null)
            {
                Console.WriteLine("Nie znaleziono zwierzęcia.");
                return;
            }

            Console.WriteLine($"Aktualny status: {zwierze.Status}");
            Console.WriteLine("Dostępne statusy:");
            Console.WriteLine("1. Do adopcji");
            Console.WriteLine("2. W leczeniu");
            Console.WriteLine("3. Adoptowany");
            Console.Write("Wybierz nowy status: ");
            string wybor = Console.ReadLine();

            string staryStatus = zwierze.Status;
            string nowyStatus = staryStatus;

            switch (wybor)
            {
                case "1":
                    zwierze.OznaczDoAdopcji();
                    nowyStatus = "Do adopcji";
                    break;
                case "2":
                    zwierze.OznaczJakoWLeczeniu();
                    nowyStatus = "W leczeniu";
                    break;
                case "3":
                    zwierze.OznaczJakoAdoptowany();
                    nowyStatus = "Adoptowany";
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    return;
            }

            HistoriaStatusu wpis = new HistoriaStatusu(zwierze, DateTime.Now, staryStatus, nowyStatus);
            schronisko.Historia.Add(wpis);

            Console.WriteLine($"Zmieniono status na: {nowyStatus}");
        }
        // Umożliwia rejestrację adopcji przez użytkownika na podstawie ID zwierzęcia
        // Tworzy obiekt użytkownika, przypisuje zwierze i zapisuje w historii
        static void RejestrujAdopcje(Schronisko schronisko)
        {
            Console.Write("ID zwierzęcia do adopcji: ");
            int id = int.Parse(Console.ReadLine());

            Zwierze zwierze = schronisko.Zwierzeta.Find(z => z.Id == id);
            if (zwierze == null)
            {
                Console.WriteLine("Nie znaleziono zwierzęcia.");
                return;
            }

            Console.Write("Imię adoptującego: ");
            string imie = Console.ReadLine();
            Console.Write("Nazwisko adoptującego: ");
            string nazwisko = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            Uzytkownik osoba = new Uzytkownik(schronisko.Adopcje.Count + 1, imie, nazwisko, email);
            Adopcja adopcja = new Adopcja(zwierze, osoba, DateTime.Now);
            schronisko.ZarejestrujAdopcje(adopcja);

            HistoriaStatusu historia = new HistoriaStatusu(zwierze, DateTime.Now, "Do adopcji", "Adoptowany");
            schronisko.Historia.Add(historia);

            Console.WriteLine("Adopcja zarejestrowana.");
        }
        // Pokazuje historię zmian statusu u wybranego zwierzęcia
        static void PokazHistorieStatusu(Schronisko schronisko)
        {
            Console.Write("Podaj ID zwierzęcia: ");
            int id = int.Parse(Console.ReadLine());

            var historia = schronisko.Historia.FindAll(h => h.Zwierze.Id == id);

            if (historia.Count == 0)
            {
                Console.WriteLine("Brak historii dla tego zwierzęcia.");
            }
            else
            {
                foreach (var wpis in historia)
                {
                    Console.WriteLine($"Data: {wpis.Data.ToShortDateString()}, {wpis.StaryStatus} -> {wpis.NowyStatus}");
                }
            }
        }
    }
}
