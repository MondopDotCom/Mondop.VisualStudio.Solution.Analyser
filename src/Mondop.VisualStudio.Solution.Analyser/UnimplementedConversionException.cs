using System;

namespace Mondop.VisualStudio.Solution.Analyser
{
    public class UnimplementedConversionException: Exception
    {
        public UnimplementedConversionException(string value,string name):
            base($"Unable to convert {value} to type {name}")

        {

        }
    }
}
