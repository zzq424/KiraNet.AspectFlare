﻿using System;
using System.Reflection;
using System.Reflection.Emit;

namespace KiraNet.AspectFlare.DynamicProxy
{
    internal class GeneratorContext : GeneratorTypeContext
    {
        public GeneratorContext(GeneratorTypeContext context)
        {
            this.TypeBuilder = context.TypeBuilder;
            this.Wrappers = context.Wrappers;
            this.ProxyType = context.ProxyType;
            this.InterfaceType = context.InterfaceType;
            this.Interface = context.Interface;
            this.InitMethod = context.InitMethod;
            this.Token = context.Token++;
        }

        public MethodBuilder MethodBuilder { get; set; }
        public ConstructorBuilder ConstructorBuilder { get; set; }
        public MethodInfo Method { get; set; }
        public ConstructorInfo Constructor { get; set; }
        public ILGenerator Generator{ get; set; }
        public ParameterInfo[] Parameters { get; set; }
        public Type ReturnType { get; set; }
        public LocalBuilder[] Locals { get; set; }
        public FieldInfo Caller { get; set; }
        public CallerType CallerType { get; set; }
        public Type CallType { get; set; }
        public MethodInfo CallerMethod { get; set; }
        public TypeBuilder DisplayTypeBuilder { get; set; }
        public ConstructorInfo DisplayConstructor { get; set; }
        public FieldInfo[] DisplayFields { get; set; }
        public MethodInfo DisplayMethod { get; set; }
    }
}