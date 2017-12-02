﻿using KiraNet.AspectFlare.DynamicProxy;
using KiraNet.AspectFlare.Validator;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace KiraNet.AspectFlare.DependencyInjection
{
    public class ProxyServiceCollection : IServiceCollection
    {
        private readonly IServiceCollection _services;
        private readonly IProxyValidator _validator;
        private readonly IProxyTypeGenerator _proxyTypeGenerator;
        public ProxyServiceCollection(IServiceCollection services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _validator = new ProxyValidator();
            _proxyTypeGenerator = new ProxyTypeGenerator();
        }

        public ServiceDescriptor this[int index] { get => _services[index]; set => _services[index] = value; }

        public int Count => _services.Count;

        public bool IsReadOnly => _services.IsReadOnly;

        public void Add(ServiceDescriptor item)
        {
            // 目前我还不能实现对已实例化的对象进行动态代理，因此直接注入
            if (item.ImplementationInstance != null && item.ImplementationFactory == null)
            {
                _services.Add(item);
                return;
            }

            Type proxyType = item.ImplementationType ??
                            item.ImplementationInstance?.GetType() ??
                            //item.ImplementationFactory?.GetType().GetGenericArguments()[1] ??
                            null;
            if (_validator.Validate(item.ServiceType, proxyType))
            {
                IExpressionConverter<IServiceProvider, object> expressionConvertr = new ServiceProviderExpressionConverter();
                Type implementType;
                if (item.ServiceType.IsInterface)
                {
                    implementType = _proxyTypeGenerator.GenerateProxyByInterface(item.ServiceType, proxyType);
                }
                else
                {
                    implementType = _proxyTypeGenerator.GenerateProxyByClass(proxyType);
                }

                if(implementType == null)
                {
                    _services.Add(item);
                    return;
                }

                if (item.ImplementationFactory == null)
                {
                    _services.Add(ServiceDescriptor.Describe(
                                    item.ServiceType,
                                    implementType,
                                    item.Lifetime));
                }
                else if (expressionConvertr.TryConvert(service =>
                                    item.ImplementationFactory(service),
                                    item.ImplementationType,
                                    implementType,
                                    out var convertLambdaExpression))
                {
                    _services.Add(ServiceDescriptor.Describe(
                                    item.ServiceType,
                                    convertLambdaExpression.Compile(),
                                    item.Lifetime));
                }
            }
            else
            {
                _services.Add(item);
            }
        }

        public void Clear()
        {
            _services.Clear();
        }

        public bool Contains(ServiceDescriptor item)
        {
            return _services.Contains(item);
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            _services.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _services.GetEnumerator();
        }

        public int IndexOf(ServiceDescriptor item)
        {
            return _services.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            _services.Insert(index, item);
        }

        public bool Remove(ServiceDescriptor item)
        {
            return _services.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _services.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _services.GetEnumerator();
        }
    }
}