using E02.ValidationAttributes.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationAttributes
{
    internal class Person
    {
        public Person(string fullName, int age)
        {
            FullName = fullName;
            Age = age;
        }

        [MyRequired]
        public string FullName { get; }

        [MyRange(12, 90)]
        public int Age { get; }
    }
}
