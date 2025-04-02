using System;
using System.Runtime.Serialization;

namespace p36_photo_table
{
    [Serializable]
    internal class CameraNotFoundException : Exception
    {
        public CameraNotFoundException()
        {
        }

        public CameraNotFoundException(string message) : base(message)
        {
        }

        public CameraNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CameraNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}