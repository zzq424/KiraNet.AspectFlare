﻿namespace KiraNet.AspectFlare
{
    public class CallingInterceptContext
    {
        public object Owner { get; set; }
        public object[] Parameters { get; set; }
        public bool HasResult { get; set; }
        public object Result { get; set; }
    }
}
