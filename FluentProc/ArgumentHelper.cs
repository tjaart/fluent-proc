namespace Tests
{
    public class ArgumentHelper
    {
        private readonly string _argument;

        public ArgumentHelper(string argument)
        {
            _argument = argument;
        }
        
        public string SanitizeArgument()
        {
            var finalArg = _argument;

            finalArg = QuoteArgumentWithSpaces(finalArg);
            
            return finalArg;
        }

        private string QuoteArgumentWithSpaces(string argument)
        {
            if (argument.Contains(" "))
            {
                return $"\"{argument}\"";     
            }

            return argument;
        }
    }
}