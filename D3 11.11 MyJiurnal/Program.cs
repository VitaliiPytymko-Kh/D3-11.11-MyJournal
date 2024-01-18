using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3_11._11_MyJournal
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            Console.Write("Введите количество журналов: ");
            if (int.TryParse(Console.ReadLine(), out int numberOfJournals) && numberOfJournals > 0)
            {
                Journal[] journalsArray = Journal.CreateJournalArray(numberOfJournals);

                string json = JsonConvert.SerializeObject(journalsArray);
                File.WriteAllText("journals2.json", json);
                Console.WriteLine("Массив журналов успешно сохранен в файл.");

                try
                {
                    string loadedJson = File.ReadAllText("journals.json");
                    Journal[] loadedJournalsArray = JsonConvert.DeserializeObject<Journal[]>(loadedJson);

                    foreach (var journal in loadedJournalsArray)
                    {
                        journal.OutputJournalInfo();
                        Console.WriteLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка при загрузке или десериализации файла: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("Некорректное количество журналов.");
            }
            Console.WriteLine("Нажмите Enter, чтобы закрыть консоль...");
            Console.ReadLine();
        }
    }
}
