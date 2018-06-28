using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dimol.Slider.dto
{
    public class Combobox : IEnumerable<string>
    {
        public string Value { get; set; }
        public string Text { get; set; }

        private IEnumerable<string> Events()
        {
            yield return Value;
            yield return Text;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Events().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
       
    }
}
