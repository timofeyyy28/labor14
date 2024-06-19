using ClassLibraryLabor10;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab14
{
    public class Band
    {
        private string[] names = { "The Beatles", "Queen", "Pink Floyd", "Led Zeppelin", "The Rolling Stones" };
        public string Instrument { get; set; }
        public string Name { get; set; }

        public Band(string instrument)
        {
            Random rnd = new Random();
            Instrument = instrument;
            Name = names[rnd.Next(0, names.Length)];
        }
    }

    public static class Collection
    {
        // Вывод всех Guitar в формате ИМЯ и Тип
        public static IEnumerable<(string Name, string Type)> GetNamesAndTypesOfGuitar(SortedDictionary<string, List<Musicalinstrument>> concert)
        {
            var result = from pair in concert
                         from instrument in pair.Value
                         where instrument.GetType() == typeof(Guitar) // Ensure only Guitar instances are selected
                         let guitar = instrument as Guitar
                         let upperName = guitar.Name.ToUpper()
                         select (Name: upperName, Type: "Guitar");

            return result;
        }

        public static IEnumerable<(string Name, string Type)> GetNamesAndTypesOfGuitarExt(SortedDictionary<string, List<Musicalinstrument>> concert)
        {
            var result = concert.SelectMany(pair => pair.Value)
                                .Where(instrument => instrument.GetType() == typeof(Guitar)) // Ensure only Guitar instances are selected
                                .Select(guitar => (Name: guitar.Name.ToUpper(), Type: "Guitar"));
            return result;
        }

        public static void PrintNamesAndTypes(IEnumerable<(string Name, string Type)> result)
        {
            foreach (var item in result)
            {
                Console.WriteLine($"Name: {item.Name}, Type: {item.Type}");
            }
        }

        // Найти среднюю мощность ElectricGuitar
        public static double GetAveragePower(SortedDictionary<string, List<Musicalinstrument>> concert)
        {
            var result = (from pair in concert
                          from instrument in pair.Value
                          let electricGuitar = instrument as ElectricGuitar
                          where electricGuitar != null
                          select electricGuitar.GuitarPowerSupply).Average(power => Convert.ToDouble(power));

            return result;
        }

        public static double GetAveragePowerExt(SortedDictionary<string, List<Musicalinstrument>> concert)
        {
            var result = concert.SelectMany(pair => pair.Value)
                                .OfType<ElectricGuitar>()
                                .Select(electricGuitar => Convert.ToDouble(electricGuitar.GuitarPowerSupply))
                                .Average();

            return result;
        }

        // Сгруппировать по типу инструмента
        public static IEnumerable<IGrouping<string, Musicalinstrument>> GroupByType(SortedDictionary<string, List<Musicalinstrument>> concert)
        {
            var result = from pair in concert
                         from instrument in pair.Value
                         group instrument by instrument.GetType().Name;

            return result;
        }

        public static IEnumerable<IGrouping<string, Musicalinstrument>> GroupByTypeExt(SortedDictionary<string, List<Musicalinstrument>> concert)
        {
            var result = concert.SelectMany(pair => pair.Value)
                                .GroupBy(instrument => instrument.GetType().Name);

            return result;
        }

        // Найти инструменты с одинаковыми именами в разных концертах
        public static IEnumerable<IGrouping<string, Musicalinstrument>> MergeCollections(SortedDictionary<string, List<Musicalinstrument>> concert1, SortedDictionary<string, List<Musicalinstrument>> concert2)
        {
            var mergedInstruments = concert1.SelectMany(pair => pair.Value)
                                            .Concat(concert2.SelectMany(pair => pair.Value))
                                            .GroupBy(instrument => instrument.Name)
                                            .Where(group => group.Count() > 1);

            return mergedInstruments;
        }

        public static IEnumerable<IGrouping<string, Musicalinstrument>> MergeCollectionsExt(SortedDictionary<string, List<Musicalinstrument>> concert1, SortedDictionary<string, List<Musicalinstrument>> concert2)
        {
            var mergedInstruments = concert1.SelectMany(pair => pair.Value)
                                            .Concat(concert2.SelectMany(pair => pair.Value))
                                            .GroupBy(instrument => instrument.Name)
                                            .Where(group => group.Count() > 1);

            return mergedInstruments;
        }

        // Объединить с коллекцией музыкальных групп
        public static IEnumerable<(Musicalinstrument Key, string BandName)> MergeWithBands(SortedDictionary<string, List<Musicalinstrument>> concert, List<Band> bands)
        {
            var result = from pair in concert
                         from instrument in pair.Value
                         join band in bands on instrument.Name equals band.Instrument
                         select (Key: instrument, band.Name);

            return result;
        }

        public static IEnumerable<(Musicalinstrument Key, string BandName)> MergeWithBandsExt(SortedDictionary<string, List<Musicalinstrument>> concert, List<Band> bands)
        {
            var result = concert.SelectMany(pair => pair.Value)
                                .Join(bands,
                                      instrument => instrument.Name,
                                      band => band.Instrument,
                                      (instrument, band) => (Key: instrument, band.Name));
            return result;
        }
    }
}
