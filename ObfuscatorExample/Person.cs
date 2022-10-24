using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMyNameSpace;

namespace ObfuscatorExample
{
    internal class Person : PersonTransform
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public int Age { get; set; }

        public Person(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;

            //Interpolate
            Move();
            Rotate(0,0);
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }

    class PersonTransform : IMovePerson
    {
        public void Move()
        {
            Console.WriteLine("My Person moving!");
        }

        public void Rotate(int x, int y)
        {
            x += new Random().Next(40);
            y += new Random().Next(20);

            Console.WriteLine("Person rotation x={0}, y={1}", x,y);
        }
    }
}
