using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace D3_11._11_MyJournal
{
    internal class Journal
    {
        public string Title { get; set; }
        public string Publishing { get; set; }
        public DateTime PublicationDate { get; set; }
        public int PageCount { get; set; }
        public List<Article> articles { get; set; }=new List<Article>();

        public Journal() { }

        public Journal(string _ttl, string _pbls, DateTime _pd, int _pgct)
        { 
            Title = _ttl;
            Publishing = _pbls;
            PublicationDate = _pd;
            PageCount = _pgct;
        }

        public void InputJournalInfo()
        {
            Console.WriteLine("Введите название журнала:");
            Title= Console.ReadLine();

            Console.Write("Введите название издательства: ");
            Publishing = Console.ReadLine();

            bool validDate = false;
            while (!validDate)
            {
                Console.Write("Введите дату выпуска (гггг-мм-дд): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    PublicationDate = date;
                    validDate = true;
                }
                else
                {
                    Console.WriteLine("Некорректный формат даты. Повторите ввод.");
                }
            }


            bool validPageCount = false;
            while (!validPageCount)
            {
                Console.Write("Введите количество страниц: ");
                if (int.TryParse(Console.ReadLine(), out int pageCount))
                {
                    if (pageCount >= 0)
                    {
                        PageCount = pageCount;
                        validPageCount = true;
                    }
                    else
                    {
                        Console.WriteLine("Количество страниц не может быть отрицательным. Повторите ввод.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректное количество страниц. Повторите ввод.");
                }
            }

            Console.Write("Введите количество статей: ");
            if(int.TryParse(Console.ReadLine(), out int articleCount) && articleCount > 0)
            { for(int i = 0;i<articleCount;i++)
                {
                    Console.WriteLine($"Введите информацию о статье {i+1}" );
                    Article article = new Article();

                    Console.WriteLine($"Название статьи:");
                    article.ArticleTitle= Console.ReadLine();

                    Console.WriteLine($"Количество символов: ");
                    if(int.TryParse(Console.ReadLine(),out int charCount) && charCount >= 0)
                    {
                        article.CharacterCount = charCount;
                    }
                    else { Console.WriteLine("Некорректное количество символов. Повторите ввод.");
                        i--;
                        continue;
                    }
                    Console.WriteLine("Анонс статьи:");
                    article.ArticleContent = Console.ReadLine();

                    articles.Add(article);
                }

            }
            else
            {
                Console.WriteLine("Некорректное количество статей.");
            }
        }

        public void OutputJournalInfo()
        {
            Console.WriteLine($"Название: {Title}");
            Console.WriteLine($"Издательство: {Publishing}");
            Console.WriteLine($"Дата выпуска: {PublicationDate.ToShortDateString()}");
            Console.WriteLine($"Количество страниц: {PageCount}");
            Console.WriteLine("Статьи в журнале:");
            foreach( Article article in articles ) 
            {
                Console.WriteLine($" Название статьи: {article.ArticleTitle}");
                Console.WriteLine($" Количество символов: {article.CharacterCount}");
                Console.WriteLine($" Анонс статьи: {article.ArticleContent}");
                Console.WriteLine();
            }
        }

        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject( this);
        }

        public static Journal LoadFromFile(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<Journal>(json);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при загрузке файла:{e.Message}");
                return null;
            }
        }

        public void SaveToFile(string filePAth)
        {
            string json = SerializeToJson();
            try
            {
                File.WriteAllText(filePAth, json);
                Console.WriteLine("Журнал успешно сохранен в файл.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при сохранении файла: {e.Message}");
            }
        }

        public static Journal[] CreateJournalArray(int size)
        {
            Journal[] journals = new Journal[size]; 
            for(int i=0; i<size; i++)
            {
                Console.WriteLine($"Введите информацию о журнале {i + 1}");
                Journal journal = new Journal();
                journal.InputJournalInfo();
                journals[i] = journal;
            }
            return journals;
        }
    }
}
