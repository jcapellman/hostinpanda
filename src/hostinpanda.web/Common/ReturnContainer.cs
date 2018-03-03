namespace hostinpanda.clientlibrary
{
    public class ReturnContainer<T>
    {
        public T ObjectValue { get; set; }

        public string ErrorString { get; set; }

        public bool HasError => !string.IsNullOrEmpty(ErrorString);

        public ReturnContainer(T objectValue, string errorString)
        {
            ObjectValue = objectValue;
            ErrorString = errorString;
        }

        public ReturnContainer(T objectValue) : this(objectValue, null) { }
    }
}