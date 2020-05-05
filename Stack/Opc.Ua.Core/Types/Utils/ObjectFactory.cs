/* Copyright (c) 1996-2019 The OPC Foundation. All rights reserved.
   The source code in this file is covered under a dual-license scenario:
     - RCL: for OPC Foundation members in good-standing
     - GPL V2: everybody else
   RCL license terms accompanied with this source code. See http://opcfoundation.org/License/RCL/1.00/
   GNU General Public License as published by the Free Software Foundation;
   version 2 of the License are accompanied with this source code. See http://opcfoundation.org/License/GPLv2
   This source code is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
*/

using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Opc.Ua.Core
{
    public static class ObjectFactory<T>
    {
        private static readonly Type s_type = typeof(T);
        private static readonly Expression[] s_expression = new Expression[] { Expression.New(s_type) };
        private static readonly BlockExpression s_block = Expression.Block(s_type, s_expression);
        private static readonly Func<T> s_constructorDelegate = Expression.Lambda<Func<T>>(s_block).Compile();

        public static T CreateInstance()
        {
            return s_constructorDelegate();
        }
    }

    public static class ObjectFactory
    {
        private static readonly ConcurrentDictionary<Type, ObjectActivator> s_typeDelegates = new ConcurrentDictionary<Type, ObjectActivator>();

        public delegate object ObjectActivator();

        public static object CreateInstance(Type type)
        {
            var func = s_typeDelegates.GetOrAdd(type, t =>
            {
                // Create a lambda with the New expression as body and our param object[] as arg
                var newExp = Expression.New(t);
                LambdaExpression lambda = t.IsValueType
                    ? Expression.Lambda(typeof(ObjectActivator), Expression.Convert(newExp, typeof(object)))
                    : Expression.Lambda(typeof(ObjectActivator), newExp);
                return (ObjectActivator)lambda.Compile();
            });
            return func();
        }
    }
}
