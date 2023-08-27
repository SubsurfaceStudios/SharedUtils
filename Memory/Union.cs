namespace SubsurfaceStudios.Utilities.Memory {
	using System;
	using SubsurfaceStudios.Utilities.Memory.Unsafe;

    /// <summary>
    /// A tagged (and checked) union that can hold an instance of A or B.
    /// </summary>
	public readonly struct TaggedUnion<A, B> {
        private readonly byte _tag;
        /// <summary>
        /// The underlying union that holds the value of this TaggedUnion.
        /// </summary>
        private readonly Union<A, B> _repr;

        /// <summary>
        /// Construct an instance of TaggedUnion<A, B> with an instance of A.
        /// </summary>
        /// <param name="a">The instance of A to wrap.</param>
        public TaggedUnion(A a) {
            _tag = 0;
            _repr = new Union<A, B> { a = a };
        }

        /// <summary>
        /// Construct an instance of TaggedUnion<A, B> with an instance of B.
        /// </summary>
        /// <param name="b">The instance of B to wrap.</param>
        public TaggedUnion(B b) {
            _tag = 1;
            _repr = new Union<A, B> { b = b };
        }

        /// <summary>
        /// Returns true if the union contains an instance of A.
        /// </summary>
        public readonly bool IsA => _tag == 0;

        /// <summary>
        /// Returns true if the union contains an instance of B.
        /// </summary>
        public readonly bool IsB => _tag == 1;

        /// <summary>
        /// Returns the value of the union as an instance of A.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the union does not contain an instance of A.</exception>
        public readonly A UnwrapA() {
            if (!IsA)
                throw new InvalidOperationException($"Called UnwrapA() on an instance of TaggedUnion<{typeof(A).Name}, {typeof(B).Name}> that does not contain an instance of {typeof(A).Name}.");
            return _repr.a;
        }

        /// <summary>
        /// Returns the value of the union as an instance of B.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the union does not contain an instance of B.</exception>
        public readonly B UnwrapB() {
            if (!IsB)
                throw new InvalidOperationException($"Called UnwrapB() on an instance of TaggedUnion<{typeof(A).Name}, {typeof(B).Name}> that does not contain an instance of {typeof(B).Name}.");
            return _repr.b;
        }
    }

    /// <summary>
    /// A tagged (and checked) union that can hold an instance of A, B, or C.
    /// </summary>
	public readonly struct TaggedUnion<A, B, C> {
        private readonly byte _tag;
        /// <summary>
        /// The underlying union that holds the value of this TaggedUnion.
        /// </summary>
        private readonly Union<A, B, C> _repr;

        /// <summary>
        /// Construct an instance of TaggedUnion<A, B, C> with an instance of A.
        /// </summary>
        /// <param name="a">The instance of A to wrap.</param>
        public TaggedUnion(A a) {
            _tag = 0;
            _repr = new Union<A, B, C> { a = a };
        }

        /// <summary>
        /// Construct an instance of TaggedUnion<A, B, C> with an instance of B.
        /// </summary>
        /// <param name="b">The instance of B to wrap.</param>
        public TaggedUnion(B b) {
            _tag = 1;
            _repr = new Union<A, B, C> { b = b };
        }

        /// <summary>
        /// Construct an instance of TaggedUnion<A, B, C> with an instance of C.
        /// </summary>
        /// <param name="c">The instance of C to wrap.</param>
        public TaggedUnion(C c) {
            _tag = 2;
            _repr = new Union<A, B, C> { c = c };
        }

        /// <summary>
        /// Returns true if the union contains an instance of A.
        /// </summary>
        public readonly bool IsA => _tag == 0;

        /// <summary>
        /// Returns true if the union contains an instance of B.
        /// </summary>
        public readonly bool IsB => _tag == 1;

        /// <summary>
        /// Returns true if the union contains an instance of C.
        /// </summary>
        public readonly bool IsC => _tag == 2;

        /// <summary>
        /// Returns the value of the union as an instance of A.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the union does not contain an instance of A.</exception>
        public readonly A UnwrapA() {
            if (!IsA)
                throw new InvalidOperationException($"Called UnwrapA() on an instance of TaggedUnion<{typeof(A).Name}, {typeof(B).Name}, {typeof(C).Name}> that does not contain an instance of {typeof(A).Name}.");
            return _repr.a;
        }

        /// <summary>
        /// Returns the value of the union as an instance of B.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the union does not contain an instance of B.</exception>
        public readonly B UnwrapB() {
            if (!IsB)
                throw new InvalidOperationException($"Called UnwrapB() on an instance of TaggedUnion<{typeof(A).Name}, {typeof(B).Name}, {typeof(C).Name}> that does not contain an instance of {typeof(B).Name}.");
            return _repr.b;
        }

        /// <summary>
        /// Returns the value of the union as an instance of C.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the union does not contain an instance of C.</exception>
        public readonly C UnwrapC() {
            if (!IsC)
                throw new InvalidOperationException($"Called UnwrapB() on an instance of TaggedUnion<{typeof(A).Name}, {typeof(B).Name}, {typeof(C).Name}> that does not contain an instance of {typeof(C).Name}.");
            return _repr.c;
        }
    }

    /// <summary>
    /// A tagged (and checked) union that can hold an instance of A, B, C, or D.
    /// </summary>
	public readonly struct TaggedUnion<A, B, C, D> {
        private readonly byte _tag;
        /// <summary>
        /// The underlying union that holds the value of this TaggedUnion.
        /// </summary>
        private readonly Union<A, B, C, D> _repr;

        /// <summary>
        /// Construct an instance of TaggedUnion<A, B, C> with an instance of A.
        /// </summary>
        /// <param name="a">The instance of A to wrap.</param>
        public TaggedUnion(A a) {
            _tag = 0;
            _repr = new Union<A, B, C, D> { a = a };
        }

        /// <summary>
        /// Construct an instance of TaggedUnion<A, B, C> with an instance of B.
        /// </summary>
        /// <param name="b">The instance of B to wrap.</param>
        public TaggedUnion(B b) {
            _tag = 1;
            _repr = new Union<A, B, C, D> { b = b };
        }

        /// <summary>
        /// Construct an instance of TaggedUnion<A, B, C> with an instance of C.
        /// </summary>
        /// <param name="c">The instance of C to wrap.</param>
        public TaggedUnion(C c) {
            _tag = 2;
            _repr = new Union<A, B, C, D> { c = c };
        }

        /// <summary>
        /// Construct an instance of TaggedUnion<A, B, C, D> with an instance of D.
        /// </summary>
        /// <param name="d">The instance of D to wrap.</param>
        public TaggedUnion(D d) {
            _tag = 3;
            _repr = new Union<A, B, C, D> { d = d };
        }

        /// <summary>
        /// Returns true if the union contains an instance of A.
        /// </summary>
        public readonly bool IsA => _tag == 0;

        /// <summary>
        /// Returns true if the union contains an instance of B.
        /// </summary>
        public readonly bool IsB => _tag == 1;

        /// <summary>
        /// Returns true if the union contains an instance of C.
        /// </summary>
        public readonly bool IsC => _tag == 2;

        /// <summary>
        /// Returns true if the union contains an instance of C.
        /// </summary>
        public readonly bool IsD => _tag == 3;

        /// <summary>
        /// Returns the value of the union as an instance of A.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the union does not contain an instance of A.</exception>
        public readonly A UnwrapA() {
            if (!IsA)
                throw new InvalidOperationException($"Called UnwrapA() on an instance of TaggedUnion<{typeof(A).Name}, {typeof(B).Name}, {typeof(C).Name}, {typeof(D).Name}> that does not contain an instance of {typeof(A).Name}.");
            return _repr.a;
        }

        /// <summary>
        /// Returns the value of the union as an instance of B.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the union does not contain an instance of B.</exception>
        public readonly B UnwrapB() {
            if (!IsB)
                throw new InvalidOperationException($"Called UnwrapB() on an instance of TaggedUnion<{typeof(A).Name}, {typeof(B).Name}, {typeof(C).Name}, {typeof(D).Name}> that does not contain an instance of {typeof(B).Name}.");
            return _repr.b;
        }

        /// <summary>
        /// Returns the value of the union as an instance of C.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the union does not contain an instance of C.</exception>
        public readonly C UnwrapC() {
            if (!IsC)
                throw new InvalidOperationException($"Called UnwrapB() on an instance of TaggedUnion<{typeof(A).Name}, {typeof(B).Name}, {typeof(C).Name}, {typeof(D).Name}> that does not contain an instance of {typeof(C).Name}.");
            return _repr.c;
        }

        /// <summary>
        /// Returns the value of the union as an instance of D.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the union does not contain an instance of D.</exception>
        public readonly D UnwrapD() {
            if (!IsD)
                throw new InvalidOperationException($"Called UnwrapB() on an instance of TaggedUnion<{typeof(A).Name}, {typeof(B).Name}, {typeof(C).Name}, {typeof(D).Name}> that does not contain an instance of {typeof(D).Name}.");
            return _repr.d;
        }
    }
}

namespace SubsurfaceStudios.Utilities.Memory.Unsafe {
    using System.Runtime.InteropServices;

    /// <summary>
    /// An unchecked & minimal union that can hold an instance of either A or B.
    /// </summary>
    /// <remarks>
    /// This union is inherently unchecked. Accesses to fields that have not been
    /// explicitly set are technically undefined. In most cases, you will want to
    /// use a TaggedUnion with the same arguments.
    /// </remarks>
	[StructLayout(LayoutKind.Explicit)]
    public struct Union<A, B> {
        [FieldOffset(0)]
        public A a;
        [FieldOffset(0)]
        public B b;
    }

    /// <summary>
    /// An unchecked & minimal union that can hold an instance of either A, B, or C.
    /// </summary>
    /// <remarks>
    /// This union is inherently unchecked. Accesses to fields that have not been
    /// explicitly set are technically undefined. In most cases, you will want to
    /// use a TaggedUnion with the same arguments.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    public struct Union<A, B, C> {
        [FieldOffset(0)]
        public A a;
        [FieldOffset(0)]
        public B b;
        [FieldOffset(0)]
        public C c;
    }

    /// <summary>
    /// An unchecked & minimal union that can hold an instance of either A, B, C, or D.
    /// </summary>
    /// <remarks>
    /// This union is inherently unchecked. Accesses to fields that have not been
    /// explicitly set are technically undefined. In most cases, you will want to
    /// use a TaggedUnion with the same arguments.
    /// </remarks>
    [StructLayout(LayoutKind.Explicit)]
    public struct Union<A, B, C, D> {
        [FieldOffset(0)]
        public A a;
        [FieldOffset(0)]
        public B b;
        [FieldOffset(0)]
        public C c;
        [FieldOffset(0)]
        public D d;
    }

    // If necessary, more generic arguments can be added here.
    // For now, this seems like the only sensible number you
    // should need (for most cases).
}
