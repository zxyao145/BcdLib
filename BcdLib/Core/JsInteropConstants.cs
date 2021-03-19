namespace BcdLib.Core
{
    public static class JsInteropConstants
    {
        private const string Prefix = "bcd.interop";
        public static string EnableDraggable = $"{Prefix}.enableDraggable";
        public static string DisableDraggable = $"{Prefix}.disableDraggable";
        public static string ResetDraggableElePosition = $"{Prefix}.resetDraggableElePosition";

        public static string MinResetStyle = $"{Prefix}.minResetStyle";
        public static string MaxResetStyle = $"{Prefix}.maxResetStyle";
        public static string NormalResetStyle = $"{Prefix}.normalResetStyle";
        
    }
}
