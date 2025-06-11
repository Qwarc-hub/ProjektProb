using System;
using System.Collections.Generic;
using System.IO;
namespace SchroniskoApp
{
    public class Schronisko
    {
        public List<Zwierze> Zwierzeta;
        public List<Adopcja> Adopcje;
        public List<HistoriaStatusu> Historia;

        // Inicjalizacja list danych
        public Schronisko()
        {
            Zwierzeta = new List<Zwierze>();
            Adopcje = new List<Adopcja>();
            Historia = new List<HistoriaStatusu>();
        }

        // Dodaj nowe zwierzę do schroniska
        public void DodajZwierze(Zwierze zwierze)
        {
            Zwierzeta.Add(zwierze);
        }

        // Filtrowanie zwierząt po gatunku
        public List<Zwierze> FiltrujPoGattunku(string gatunek)
        {
            List<Zwierze> wynik = new List<Zwierze>();
            foreach (var z in Zwierzeta)
            {
                if (z.Gatunek == gatunek)
                {
                    wynik.Add(z);
                }
            }
            return wynik;
        }

        // Zarejestruj proces adopcji
        public void ZarejestrujAdopcje(Adopcja adopcja)
        {
            Adopcje.Add(adopcja);
            adopcja.Zwierze.OznaczJakoAdoptowany(); // zmień status zwierzęcia
        }

        // Prosty raport wszystkich danych
        public void GenerujRaport()
        {
            Console.WriteLine("\n=== RAPORT SCHRONISKA ===");

            Console.WriteLine("\nZwierzęta:");
            foreach (var z in Zwierzeta)
            {
                Console.WriteLine($"ID: {z.Id}, Imię: {z.Imie}, Gatunek: {z.Gatunek}, Status: {z.Status}");
            }

            Console.WriteLine("\nAdopcje:");
            foreach (var a in Adopcje)
            {
                Console.WriteLine($"Zwierzę: {a.Zwierze.Imie}, Adoptowany przez: {a.Uzytkownik.Imie} {a.Uzytkownik.Nazwisko}, Data: {a.DataAdopcji.ToShortDateString()}");
            }

            Console.WriteLine("\nHistoria statusów:");
            foreach (var h in Historia)
            {
                Console.WriteLine($"Zwierzę: {h.Zwierze.Imie}, {h.StaryStatus} → {h.NowyStatus}, Data: {h.Data.ToShortDateString()}");
            }

            Console.WriteLine("==========================\n");
        }

        public void ZapiszRaportDoPliku(string sciezka)
        {
            using (StreamWriter writer = new StreamWriter(sciezka))
            {
                writer.WriteLine("=== RAPORT SCHRONISKA ===\n");

                writer.WriteLine("Zwierzęta:");
                foreach (var z in Zwierzeta)
                    writer.WriteLine($"ID: {z.Id}, Imię: {z.Imie}, Gatunek: {z.Gatunek}, Status: {z.Status}");

                writer.WriteLine("\nAdopcje:");
                foreach (var a in Adopcje)
                    writer.WriteLine($"Zwierzę: {a.Zwierze.Imie}, Adoptowany przez: {a.Uzytkownik.Imie} {a.Uzytkownik.Nazwisko}");

                writer.WriteLine("\nHistoria statusów:");
                foreach (var h in Historia)
                    writer.WriteLine($"Zwierzę: {h.Zwierze.Imie}, {h.StaryStatus} -> {h.NowyStatus}, Data: {h.Data.ToShortDateString()}");

                writer.WriteLine("\n==========================");
            }

            Console.WriteLine("Raport zapisany do pliku: " + sciezka);
        }

    }
}
