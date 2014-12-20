namespace SerializationSpike
{
    public static class StringExtentions
    {
        public static string FormatUsing(this string template, object arg0)
        {
            return string.Format(template, arg0);
        }
    }
}