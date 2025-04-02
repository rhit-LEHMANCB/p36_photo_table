using System;
using System.Runtime.Serialization;

namespace p36_photo_table
{
    [Serializable]
    internal class ArduinoNotFoundException : Exception
    {
        public ArduinoNotFoundException()
        {
        }

        public ArduinoNotFoundException(string message) : base(message)
        {
        }

        public ArduinoNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ArduinoNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}