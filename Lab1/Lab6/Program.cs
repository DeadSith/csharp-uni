using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleLoop();
        }

        static void ConsoleLoop()
        {
            Console.WriteLine("Натисніть Q або F10 щоб закрити консоль.");
            Console.WriteLine("Введіть клас та ім'я розділені пробілом.");
            Human first = null;
            Human second = null;
            var sb = new StringBuilder();
            var input = Console.ReadKey();
            while (input.Key != ConsoleKey.F10 && input.Key != ConsoleKey.Q)
            {
                if (input.Key == ConsoleKey.Enter)
                {
                    if (first == null)
                    {
                        if (!TryParseInput(sb.ToString(), out first))
                        {
                            Console.WriteLine("Дані введено в неправильному форматі.");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        if (!TryParseInput(sb.ToString(), out second))
                        {
                            Console.WriteLine("Дані введено в неправильному форматі.");
                        }
                        Console.WriteLine();
                        first = null;
                        var (log, result) = Couple(first, second);
                        foreach (var entry in log)
                        {
                            Console.WriteLine(entry);
                        }
                        //process result
                    }
                    sb.Clear();
                }
                else
                {
                    sb.Append(input.KeyChar);
                }
                input = Console.ReadKey();
            }
        }

        static (IEnumerable<string>, IHasName) Couple(Human h1, Human h2)
        {
            var firstType = h1.GetType();
            var secondType = h2.GetType();
            var firstAttributes = (CoupleAttribute[])Attribute.GetCustomAttributes(firstType, typeof(CoupleAttribute));
            var firstMatch = firstAttributes.FirstOrDefault(attribute => attribute.ChildType == secondType.Name);
            var messages = new List<string>();

            if (firstMatch == null)
            {
                messages.Add("Suitable child type not found");
                return (messages, null);
            }

            var firstLikes = new Random().NextDouble() <= firstMatch.Probability;
            if (firstLikes)
            {
                messages.Add("Першій особі подобається друга.");
            }
            else
            {
                messages.Add("Першій особі не подобається друга.");
            }

            var secondAttributes = (CoupleAttribute[])Attribute.GetCustomAttributes(secondType, typeof(CoupleAttribute));
            var secondMatch = secondAttributes.FirstOrDefault(attribute => attribute.ChildType == firstType.Name);

            if (secondMatch == null)
            {
                messages.Add("Suitable child type not found");
                return (messages, null);
            }

            var secondLikes = new Random().NextDouble() <= secondMatch.Probability;
            if (secondLikes)
            {
                messages.Add("Другій особі подобається перша.");
            }
            else
            {
                messages.Add("Другій особі не подобається перша.");
            }
            string name;

            try
            {
                var method = secondType.GetMethods().FirstOrDefault(m => m.ReturnType == typeof(string));
                if (method == null)
                {
                    messages.Add("Suitable Name method not found");
                    return (messages, null);
                }

                name = method.Invoke(h2, null) as string;
            }
            catch (TargetParameterCountException)
            {
                messages.Add("Wrong number of arguments.");
                return (messages, null);
            }

            var childType = Type.GetType(firstMatch.ChildType);
            var child = (IHasName)Activator.CreateInstance(childType);
            var nameProp = childType.GetProperty("Name");
            if (nameProp.CanWrite)
            {
                nameProp.SetValue(child, name);
            }
            else
            {
                messages.Add("No setter for name was found");
                return (messages, null);
            }

            var parName = childType.GetProperty("ParentalName");
            if (parName != null && parName.CanWrite)
            {
                if (firstType.Name.Contains("Girl"))
                {
                    parName.SetValue(child, GenerateParName(name));
                }
                else
                {
                    parName.SetValue(child, h1.Name);
                }
            }

            return (messages, child);
        }

        static bool TryParseInput(string input, out Human h)
        {
            h = null;
            var fields = input.Split(' ');
            if (fields.Length != 2)
            {
                return false;
            }
            var type = Type.GetType(fields[0]);
            if (type == null)
            {
                return false;
            }

            h = Activator.CreateInstance(type) as Human;
            if (h == null)
            {
                return false;
            }

            h.Name = fields[1];
            return true;
        }

        static string GenerateParName(string name)
        {
            return name + "овна";
        }
    }
}
