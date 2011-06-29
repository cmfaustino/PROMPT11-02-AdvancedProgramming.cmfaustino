using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mod02_AdvProgramming.LinqToXML
{
    using System.Collections;
    using System.Xml.Linq;

    using Mod02_AdvProgramming.Data;
    using Mod02_AdvProgramming.Utils;

    class Program
    {
        static void Main(string[] args)
        {

            XElement root = new XElement("html",
                new XElement("head",
                    new XElement("Title", "Page title")),
                new XElement("body",
                    new XElement("h1", "SLB"), 
                    new XElement("img", new XAttribute("src", "http://1.bp.blogspot.com/_LEaabIgK5mI/TB3Adj7bHHI/AAAAAAAAFfE/HLOaidwJu74/s200/200px-SL_Benfica_logo_svg.png")), 
                    new XElement("h1")     
                        ));

            Console.WriteLine(root.ToString());

            IEnumerable<Customer> customers = GetCustomers();

            //ObjectDumper.Write(customers);
        }

        private static IEnumerable<Customer> GetCustomers()
        {
            return XElement.Load("customers.xml").Descendants("customer").
                Select(cxml => new Customer { 
                    CustomerID = cxml.Element("id").Value,
                    Name = cxml.Element("name").Value
                });
            
        }
    }
}
