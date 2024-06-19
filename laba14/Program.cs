using System;
using System.Collections.Generic;
using lab14;
using ClassLibraryLabor10;

namespace lab14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Музыкальные группы
            List<Band> bands = new List<Band>();

            // Участники концерта
            SortedDictionary<string, List<Musicalinstrument>> concert1 = new SortedDictionary<string, List<Musicalinstrument>>();

            // Участники (Музыкальные инструменты)
            List<Musicalinstrument> participants1 = new List<Musicalinstrument>();
            List<Musicalinstrument> participants2 = new List<Musicalinstrument>();
            List<Musicalinstrument> participants3 = new List<Musicalinstrument>();

            // Заполнение списков инструментами
            for (int i = 0; i < 5; i++)
            {
                Musicalinstrument instrument = new Musicalinstrument();
                instrument.RandomInit();
                participants1.Add(instrument);
            }
            for (int i = 0; i < 5; i++)
            {
                Guitar guitar = new Guitar();
                guitar.RandomInit();
                Band band = new Band(guitar.Name);
                participants1.Add(guitar);
                bands.Add(band);
            }
            concert1.Add("Concert1", participants1);

            for (int i = 0; i < 5; i++)
            {
                Piano piano = new Piano();
                piano.RandomInit();
                Band band = new Band(piano.Name);
                participants2.Add(piano);
                bands.Add(band);
            }
            for (int i = 0; i < 5; i++)
            {
                ElectricGuitar electricGuitar = new ElectricGuitar();
                electricGuitar.RandomInit();
                Band band = new Band(electricGuitar.Name);
                participants2.Add(electricGuitar);
                bands.Add(band);
            }
            concert1.Add("Concert2", participants2);

            for (int i = 0; i < 5; i++)
            {
                Musicalinstrument instrument = new Musicalinstrument();
                instrument.RandomInit();
                participants3.Add(instrument);
            }
            for (int i = 0; i < 5; i++)
            {
                ElectricGuitar electricGuitar = new ElectricGuitar();
                electricGuitar.RandomInit();
                participants3.Add(electricGuitar);
            }
            concert1.Add("Concert3", participants3);

            int answer = 0;
            string menu = "Выберете запрос:\n1) Вывод всех Guitar в формате ИМЯ и Тип\n2) Найти среднюю мощность ElectricGuitar\n3) Сгруппировать по типу инструмента\n4) Найти инструменты с одинаковыми именами в разных концертах\n5) Объединить с коллекцией музыкальных групп\n6) Завершить работу";
            bool start = true;
            while (start)
            {
                Console.WriteLine("Данная программа выполняет запросы к коллекциям с помощью LINQ и методов расширения. Коллекции уже сгенерированы. ");
                Console.WriteLine(menu);

                answer = ChooseAnswer(1, 6);

                switch (answer)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Выберете способ получения информации\n1) LINQ запрос\n2) Методом расширения");
                        answer = ChooseAnswer(1, 2);
                        Console.Clear();
                        Console.WriteLine("Исходная коллекция:");
                        foreach (var concert in concert1)
                        {
                            Console.WriteLine($"Concert: {concert.Key}");
                            foreach (var participant in concert.Value)
                            {
                                Console.WriteLine(participant);
                            }
                        }
                        if (answer == 1)
                        {
                            var result = Collection.GetNamesAndTypesOfGuitar(concert1);
                            Collection.PrintNamesAndTypes(result);
                        }
                        else
                        {
                            var result = Collection.GetNamesAndTypesOfGuitarExt(concert1);
                            Collection.PrintNamesAndTypes(result);
                        }
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Выберете способ получения информации\n1) LINQ запрос\n2) Методом расширения");
                        answer = ChooseAnswer(1, 2);
                        Console.Clear();
                        Console.WriteLine("Исходная коллекция:");
                        foreach (var concert in concert1)
                        {
                            Console.WriteLine($"Concert: {concert.Key}");
                            foreach (var participant in concert.Value)
                            {
                                Console.WriteLine(participant);
                            }
                        }
                        if (answer == 1)
                        {
                            var result = Collection.GetAveragePower(concert1);
                            Console.WriteLine(result);
                        }
                        else
                        {
                            var result = Collection.GetAveragePowerExt(concert1);
                            Console.WriteLine(result);
                        }
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Выберете способ получения информации\n1) LINQ запрос\n2) Методом расширения");
                        answer = ChooseAnswer(1, 2);
                        if (answer == 1)
                        {
                            var res = Collection.GroupByType(concert1);
                            foreach (var item in res)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        else
                        {
                            var res = Collection.GroupByTypeExt(concert1);
                            foreach (var item in res)
                            {
                                Console.WriteLine(item);
                            }
                        }

                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("Выберете способ получения информации\n1) LINQ запрос\n2) Методом расширения");
                        answer = ChooseAnswer(1, 2);
                        if (answer == 1)
                        {
                            var res = Collection.MergeCollections(concert1, concert1);
                            foreach (var participant in res)
                            {
                                foreach (var item in participant)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                        }
                        else
                        {
                            var res = Collection.MergeCollectionsExt(concert1, concert1);
                            foreach (var participant in res)
                            {
                                foreach (var item in participant)
                                {
                                    Console.WriteLine(item);
                                }
                            }
                        }
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Выберете способ получения информации\n1) LINQ запрос\n2) Методом расширения");
                        answer = ChooseAnswer(1, 2);
                        if (answer == 1)
                        {
                            var res = Collection.MergeWithBands(concert1, bands);
                            foreach (var item in res)
                            {
                                Console.WriteLine($"Инструмент: {item.Key} Группа: {item.BandName}");
                            }
                        }
                        else
                        {
                            var res = Collection.MergeWithBandsExt(concert1, bands);
                            foreach (var item in res)
                            {
                                Console.WriteLine($"Инструмент: {item.Key} Группа: {item.BandName}");
                            }
                        }
                        break;
                    case 6:
                        start = false;
                        break;
                }
            }
        }

        // Обработка ввода чисел
        static int ChooseAnswer(int a, int b)
        {
            int answer = 0;
            bool checkAnswer;
            do
            {
                checkAnswer = int.TryParse(Console.ReadLine(), out answer);
                if ((answer > b || answer < a) || (!checkAnswer))
                {
                    Console.WriteLine("Вы некорректно ввели число, повторите ввод еще раз. Обратите внимание на то, что именно нужно ввести.");
                }
            } while ((answer > b || answer < a) || (!checkAnswer));

            return answer;
        }
    }
}
