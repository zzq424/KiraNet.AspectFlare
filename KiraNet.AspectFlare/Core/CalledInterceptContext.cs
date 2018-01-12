﻿namespace KiraNet.AspectFlare
{
    public class CalledInterceptContext
    {
        public object Owner { get; set; }
        public object[] Parameters { get; set; }
        public object ReturnValue { get; set; }
        public bool HasResult { get; set; }
        public object Result { get; set; }
    }
}
