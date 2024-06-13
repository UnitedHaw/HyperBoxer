using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Project_HyperBoxer.Scripts.Test
{
    public class DITest
    {
        private string _name;
        public string Name => _name;

        private float _value;
        public float Value => _value;


        public DITest(string name, float value)
        {
            _name = name;
            _value = value;
        }
    }
}
