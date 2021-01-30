using System;
using System.Collections.Generic;
using System.Text;

namespace Buisness.Information
{
    public class InformationLabel
    {
        public InformationLabel(string label, object value)
        {
            Label = label;
            Value = value.ToString();
        }

        public string Label { get; }
        public string Value { get; }

        public override string ToString()
        {
            return $"{Label}: {Value}";
        }
    }
}
