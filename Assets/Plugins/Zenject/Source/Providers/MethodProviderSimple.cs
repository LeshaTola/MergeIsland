using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject
{
    [NoReflectionBaking]
    public class MethodProviderSimple<TReturn> : IProvider
    {
        private readonly Func<TReturn> _method;

        public MethodProviderSimple(Func<TReturn> method)
        {
            _method = method;
        }

        public bool IsCached => false;

        public bool TypeVariesBasedOnMemberType => false;

        public Type GetInstanceType(InjectContext context)
        {
            return typeof(TReturn);
        }

        public void GetAllInstancesWithInjectSplit(
            InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
        {
            Assert.IsEmpty(args);
            Assert.IsNotNull(context);

            Assert.That(typeof(TReturn).DerivesFromOrEqual(context.MemberType));

            injectAction = null;
            buffer.Add(_method());
        }
    }
}