using System;

namespace Voting.Services.Exceptions
{
    public class TemplateNotFoundException: Exception
    {
        public TemplateNotFoundException(string inputPath) :base("Invalid template Path.")
        {
            InputPath = inputPath;
        }
        public string InputPath { get; }
    }
}