﻿namespace KiraNet.AspectFlare
{
    public interface ICalledInterceptor : IInterceptor
    {
        void Called(CalledInterceptContext calledInterceptorContext);
    }
}
