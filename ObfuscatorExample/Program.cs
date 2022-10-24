using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscatorExample
{
    internal class Program
    {

        static Int32 ReturnAge(Int32 @return)
        {
            return @return;
        }

        static string RandName()
        {
            var arr = new List<string> { "Петя Петькин", "Вася Васькин", "Жерар Жерарович", "Простотит Простотитов" };
            return arr[new Random().Next(1, arr.Count)];
        }


        static void Main(string[] args)
        {
            Person person1 = new Person(firstName: "Vasiliy", lastName: "Ignatyev", age: ReturnAge(40));
            
            Console.WriteLine($"First Name : {person1.FirstName}\r\nLast Name : {person1.LastName}\r\nAge : {person1.Age}");
            Console.WriteLine($"Random Name {RandName()}");

            Console.WriteLine("Введите путь до картинки");
            string path = Console.ReadLine();

            var byteImage = person1.imageToByteArray(Image.FromFile(path.Replace("\"", "")));

            Console.WriteLine("Byte {0}", person1.ByteArrayToString(byteImage));

            Console.ReadLine();
        }
    }
}
