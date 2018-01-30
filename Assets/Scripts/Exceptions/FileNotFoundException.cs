using System;

namespace Gameflakes.TurtleType.CustomException
{
    public class FileNotFoundException : Exception
    {
        public FileNotFoundException ( ) : base ( ) { }
        public FileNotFoundException ( string message ) : base ( message ) { }
        public FileNotFoundException ( string message, Exception innerException ) : base ( message, innerException ) { }
    }
}
