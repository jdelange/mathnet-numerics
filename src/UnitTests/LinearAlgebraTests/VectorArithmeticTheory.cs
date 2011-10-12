﻿// <copyright file="VectorArithmeticTheory.cs" company="Math.NET">
// Math.NET Numerics, part of the Math.NET Project
// http://numerics.mathdotnet.com
// http://github.com/mathnet/mathnet-numerics
// http://mathnetnumerics.codeplex.com
//
// Copyright (c) 2009-2011 Math.NET
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// </copyright>

namespace MathNet.Numerics.UnitTests.LinearAlgebraTests
{
    using System;
    using LinearAlgebra.Generic;
    using NUnit.Framework;

    [TestFixture]
    public abstract class VectorArithmeticTheory<T>
        where T : struct, IEquatable<T>, IFormattable
    {
        protected abstract T Minus(T value);
        protected abstract T Add(T first, T second);
        private T Subtract(T first, T second) { return Add(first, Minus(second)); }

        [Theory, Timeout(100)]
        public void CanCloneVectorUsingUnaryPlusOperator(Vector<T> vector)
        {
            var hash = vector.GetHashCode();

            var result = +vector;

            Assert.That(vector.GetHashCode(), Is.EqualTo(hash));
            Assert.That(result, Is.Not.SameAs(vector));
            Assert.That(result.Equals(vector));
        }

        [Theory, Timeout(100)]
        public void CanNegateVectorUsingUnaryMinusOperator(Vector<T> vector)
        {
            var hash = vector.GetHashCode();

            var result = -vector;

            Assert.That(vector.GetHashCode(), Is.EqualTo(hash));
            Assert.That(result, Is.Not.SameAs(vector));
            Assert.That((-result).Equals(vector));

            for (var i = 0; i < Math.Min(vector.Count, 20); i++)
            {
                Assert.That(result[i], Is.EqualTo(Minus(vector[i])), i.ToString());
            }
        }

        [Theory, Timeout(100)]
        public void CanAddTwoVectorsUsingOperator(Vector<T> a, Vector<T> b)
        {
            Assume.That(a.Count, Is.EqualTo(b.Count));

            var hasha = a.GetHashCode();
            var hashb = b.GetHashCode();

            var result = a + b;

            Assert.That(a.GetHashCode(), Is.EqualTo(hasha));
            Assert.That(b.GetHashCode(), Is.EqualTo(hashb));
            Assert.That(result, Is.Not.SameAs(a));
            Assert.That(result, Is.Not.SameAs(b));

            for (var i = 0; i < Math.Min(a.Count, 20); i++)
            {
                Assert.That(result[i], Is.EqualTo(Add(a[i], b[i])), i.ToString());
            }
        }

        [Theory, Timeout(100)]
        public void CanAddTwoVectorsInplace(Vector<T> a, Vector<T> b)
        {
            Assume.That(a.Count, Is.EqualTo(b.Count));

            var hasha = a.GetHashCode();
            var hashb = b.GetHashCode();

            var result = a.Clone();
            result.Add(b, result);

            Assert.That(a.GetHashCode(), Is.EqualTo(hasha));
            Assert.That(b.GetHashCode(), Is.EqualTo(hashb));
            Assert.That(result, Is.Not.SameAs(a));
            Assert.That(result, Is.Not.SameAs(b));

            for (var i = 0; i < Math.Min(a.Count, 20); i++)
            {
                Assert.That(result[i], Is.EqualTo(Add(a[i], b[i])), i.ToString());
            }
        }

        [Theory, Timeout(100)]
        public void CanSubtractTwoVectorsUsingOperator(Vector<T> a, Vector<T> b)
        {
            Assume.That(a.Count, Is.EqualTo(b.Count));

            var hasha = a.GetHashCode();
            var hashb = b.GetHashCode();

            var result = a - b;

            Assert.That(a.GetHashCode(), Is.EqualTo(hasha));
            Assert.That(b.GetHashCode(), Is.EqualTo(hashb));
            Assert.That(result, Is.Not.SameAs(a));
            Assert.That(result, Is.Not.SameAs(b));

            for (var i = 0; i < Math.Min(a.Count, 20); i++)
            {
                Assert.That(result[i], Is.EqualTo(Subtract(a[i], b[i])), i.ToString());
            }
        }

        [Theory, Timeout(100)]
        public void CanSubtractTwoVectorsInplace(Vector<T> a, Vector<T> b)
        {
            Assume.That(a.Count, Is.EqualTo(b.Count));

            var hasha = a.GetHashCode();
            var hashb = b.GetHashCode();

            var result = a.Clone();
            result.Subtract(b, result);

            Assert.That(a.GetHashCode(), Is.EqualTo(hasha));
            Assert.That(b.GetHashCode(), Is.EqualTo(hashb));
            Assert.That(result, Is.Not.SameAs(a));
            Assert.That(result, Is.Not.SameAs(b));

            for (var i = 0; i < Math.Min(a.Count, 20); i++)
            {
                Assert.That(result[i], Is.EqualTo(Subtract(a[i], b[i])), i.ToString());
            }
        }
    }
}