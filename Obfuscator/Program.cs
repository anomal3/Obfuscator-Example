using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace Obfuscator
{
    internal class Program
    {
        #region Variables
        private static Random random = new Random();
        #endregion

        #region Helper Naming
        /// <summary>
        /// Random name other
        /// </summary>
        /// <param name="length">How match length new name method</param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Method random generation properties name.
        /// <para>We take one letter from the property name and convert it to unicode</para>
        /// </summary>
        /// <param name="s">inline string</param>
        /// <returns>return Unicode symbol</returns>
        static string GetUnicodeString(string s)
        {
            char[] chars = s.ToCharArray();
            var nameChar = chars[new Random().Next(chars.Length)];
            StringBuilder sb = new StringBuilder();
            sb.Append("u");
            sb.Append(String.Format("{0:x4}", (int)nameChar));
            return sb.ToString();
        }

        #endregion


        static void Main(string[] args)
        {
            Console.WriteLine("Paste to the path NET ASSEMBLY");

            var pathToFile = Console.ReadLine().Replace("\"", "");
            //ReadAssembly(PATH_TO_NET_FILE)
            AssemblyDefinition asm = AssemblyDefinition.ReadAssembly(pathToFile);

            #region Generic Info

            //var toInspect = asm.MainModule
            //    .GetTypes()
            //    .SelectMany(g => g.Methods
            //        .Where(m => m.HasBody)
            //        .Select(m => new { g, m }));

            //foreach (var method in toInspect)
            //{
            //    Console.WriteLine($"\tType = {method.g.Name}\n\t\tMethod = {method.m.Name}");
            //    foreach (var instruction in method.m.Body.Instructions)
            //        Console.WriteLine($"{instruction.OpCode} \"{instruction.Operand}\"");
            //}

            #endregion

            foreach (TypeDefinition t in asm.MainModule.Types)
            {
                #region Known method rename
                //if (t.Name == "Program" || t.Name == "Person")
                //{
                //    foreach (MethodDefinition m in t.Methods)
                //    {
                //
                //        if (m.Name == "RandName" || m.Name == "ReturnAge")
                //        {
                //            m.Name = RandomString(5);
                //        }
                //    }
                //}
                #endregion

                #region Variable Example


                //rename namespace
                t.Namespace = RandomString(t.Namespace.Length);

                //rename Properties
                foreach (var prop in t.Properties)
                    prop.Name = GetUnicodeString(prop.Name);

                //rename fields
                foreach (var fieldDefinition in t.Fields)
                    fieldDefinition.Name = RandomString(fieldDefinition.Name.Length);

                //rename interfaces
                foreach (var interfaceImplementation in t.Interfaces)
                    interfaceImplementation.InterfaceType.Name = RandomString(10);

                //rename method.
                //WARNING!!! Exclude method Main and .ctor! otherwise there will be an error
                foreach (var methodDefinition in t.Methods.Where(x => !x.Name.Contains("Main") && !x.Name.Contains("ctor")))
                {
                    foreach (var m in methodDefinition.Module.Types)
                        m.Name = RandomString(5);

                    methodDefinition.Name = GetUnicodeString(methodDefinition.Name);
                }

                //rename nested types
                foreach (var nestedType in t.NestedTypes)
                    nestedType.Name = RandomString(nestedType.Name.Length);


                #endregion

                asm.Write("ObfuscatorExample_obfuscate.exe");
            }

            Console.WriteLine("Obfuscate well done! Any key to exit...");
            Console.ReadLine();
        }
    }
}
