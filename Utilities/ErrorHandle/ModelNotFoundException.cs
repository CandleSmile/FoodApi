namespace Utilities.ErrorHandle
{
    public class ModelNotFoundException: ApplicationException
    {
        public ModelNotFoundException(string name, string key) : base($"{name} ({key}) is not found")
        {

        }
    }
}
