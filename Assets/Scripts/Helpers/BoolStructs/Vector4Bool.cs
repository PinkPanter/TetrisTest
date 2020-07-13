using System;
using UnityEngine;

namespace Helpers.BoolStructs
{
    [Serializable]
    public struct Vector4Bool : IEquatable<Vector4Bool>
    {
        [SerializeField]
        private bool m_X;
        [SerializeField]
        private bool m_Y;
        [SerializeField]
        private bool m_Z;
        [SerializeField]
        private bool m_W;

        /// <summary>
        ///   <para>X component of the vector.</para>
        /// </summary>
        public bool x
        {
            get => this.m_X;
            set => this.m_X = value;
        }

        /// <summary>
        ///   <para>Y component of the vector.</para>
        /// </summary>
        public bool y
        {
            get => this.m_Y;
            set => this.m_Y = value;
        }

        /// <summary>
        ///   <para>Z component of the vector.</para>
        /// </summary>
        public bool z
        {
            get => this.m_Z;
            set => this.m_Z = value;
        }

        /// <summary>
        ///   <para>W component of the vector.</para>
        /// </summary>
        public bool w
        {
            get => this.m_W;
            set => this.m_W = value;
        }

        public int Max
        {
            get
            {
                int index = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (this[i])
                        index = i + 1;
                }

                return index;
            }
        }

        public Vector4Bool(bool x, bool y, bool z, bool w)
        {
            this.m_X = x;
            this.m_Y = y;
            this.m_Z = z;
            this.m_W = w;
        }

        public Vector4Bool(int x, int y, int z, int w)
        {
            this.m_X = x == 1;
            this.m_Y = y == 1;
            this.m_Z = z == 1;
            this.m_W = w == 1;
        }

        public bool this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.x;
                    case 1:
                        return this.y;
                    case 2:
                        return this.z;
                    case 3:
                        return this.w;
                    default:
                        throw new IndexOutOfRangeException($"Invalid Vector3Int index addressed: {(object) index}!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    case 2:
                        this.z = value;
                        break;
                    case 3:
                        this.w = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException($"Invalid Vector3Int index addressed: {(object) index}!");
                }
            }
        }
        public static bool operator ==(Vector4Bool lhs, Vector4Bool rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.m_W;
        }

        public static bool operator !=(Vector4Bool lhs, Vector4Bool rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(Vector4Bool other)
        {
            return m_X == other.m_X && m_Y == other.m_Y && m_Z == other.m_Z && m_W == other.m_W;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector4Bool other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = m_X.GetHashCode();
                hashCode = (hashCode * 397) ^ m_Y.GetHashCode();
                hashCode = (hashCode * 397) ^ m_Z.GetHashCode();
                hashCode = (hashCode * 397) ^ m_W.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"({m_X}, {m_Y}, {m_Z}, {m_W})";
        }
    }
}
