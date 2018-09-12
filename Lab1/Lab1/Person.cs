﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public class Person
    {
        public Person(string name, string surname, DateTime birthday)
        {
            _name = name;
            _surname = surname;
            _birthday = birthday;
        }

        public Person()
        {
            _name = "";
            _surname = "";
            _birthday = DateTime.Now;
        }

        private string _name;

        private string _surname;

        private DateTime _birthday;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Surname
        {
            get => _surname;
            set => _surname = value;
        }

        public DateTime Birthday
        {
            get => _birthday;
            set => _birthday = value;
        }

        public int BirthYear
        {
            get => _birthday.Year;
            set => _birthday = new DateTime(value, _birthday.Month, _birthday.Day);
        }

        public override string ToString() => $"birthday:{_birthday};name:{_name};surname{_surname}";

        public virtual string ToShortString() => $"{_name} {_surname}";
    }
}
