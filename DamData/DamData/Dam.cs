using System;
using System.Collections.Generic;

namespace DamData
{
    class Dam
    {
        private readonly string _name;
        private Dictionary<string, Dictionary<Enum, double?>> _data;

        public Dam(string name, Dictionary<string, Dictionary<Enum, double?>> data)
        {
            _name = name;
            _data = data;
        }

        public string GetName()
        {
            return _name;
        }

        public Dictionary<string, Dictionary<Enum, double?>> GetData()
        {
            return _data;
        }
    }
}
