namespace CameraControl
{
    internal class CapturedItem
    {
        public byte[] Image { get; internal set; }
        public string Name { get; internal set; }
        public long Size { get; internal set; }
        public bool IsFolder { get; internal set; }
    }
}