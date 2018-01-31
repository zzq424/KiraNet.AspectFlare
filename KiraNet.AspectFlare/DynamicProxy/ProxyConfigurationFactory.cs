﻿namespace KiraNet.AspectFlare.DynamicProxy
{
    public class ProxyConfigurationFactory : IProxyConfigurationFactory
    {
        public IProxyConfiguration BuildConfiguration()
        {
            return new ProxyConfiguration();
        }
    }
}
