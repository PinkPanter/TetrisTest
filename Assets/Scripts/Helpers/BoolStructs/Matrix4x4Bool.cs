using System;
using UnityEngine;

namespace Helpers.BoolStructs
{
    [Serializable]
    public struct Matrix4x4Bool : IEquatable<Matrix4x4Bool>
    {
        private static readonly Matrix4x4Bool zeroMatrix = new Matrix4x4Bool(new Vector4Bool(false, false, false, false), new Vector4Bool(false, false, false, false), new Vector4Bool(false, false, false, false), new Vector4Bool(false, false, false, false));

        public bool m00;
               
        public bool m10;
               
        public bool m20;
               
        public bool m30;
               
        public bool m01;
               
        public bool m11;
               
        public bool m21;
               
        public bool m31;
               
        public bool m02;
               
        public bool m12;
               
        public bool m22;
               
        public bool m32;
               
        public bool m03;
               
        public bool m13;
               
        public bool m23;
               
        public bool m33;

        public Matrix4x4Bool(Vector4Bool column0, Vector4Bool column1, Vector4Bool column2, Vector4Bool column3)
        {
            m00 = column0.x;
            m01 = column1.x;
            m02 = column2.x;
            m03 = column3.x;
            m10 = column0.y;
            m11 = column1.y;
            m12 = column2.y;
            m13 = column3.y;
            m20 = column0.z;
            m21 = column1.z;
            m22 = column2.z;
            m23 = column3.z;
            m30 = column0.w;
            m31 = column1.w;
            m32 = column2.w;
            m33 = column3.w;
        }

        public bool this[int row, int column]
        {
            get => this[row + column * 4];
            set => this[row + column * 4] = value;
        }

        public bool this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return m00;
                    case 1:
                        return m10;
                    case 2:
                        return m20;
                    case 3:
                        return m30;
                    case 4:
                        return m01;
                    case 5:
                        return m11;
                    case 6:
                        return m21;
                    case 7:
                        return m31;
                    case 8:
                        return m02;
                    case 9:
                        return m12;
                    case 10:
                        return m22;
                    case 11:
                        return m32;
                    case 12:
                        return m03;
                    case 13:
                        return m13;
                    case 14:
                        return m23;
                    case 15:
                        return m33;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        m00 = value;
                        break;
                    case 1:
                        m10 = value;
                        break;
                    case 2:
                        m20 = value;
                        break;
                    case 3:
                        m30 = value;
                        break;
                    case 4:
                        m01 = value;
                        break;
                    case 5:
                        m11 = value;
                        break;
                    case 6:
                        m21 = value;
                        break;
                    case 7:
                        m31 = value;
                        break;
                    case 8:
                        m02 = value;
                        break;
                    case 9:
                        m12 = value;
                        break;
                    case 10:
                        m22 = value;
                        break;
                    case 11:
                        m32 = value;
                        break;
                    case 12:
                        m03 = value;
                        break;
                    case 13:
                        m13 = value;
                        break;
                    case 14:
                        m23 = value;
                        break;
                    case 15:
                        m33 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
        }

        public override int GetHashCode()
        {
            Vector4Bool column = GetColumn(0);
            int hashCode = column.GetHashCode();
            column = GetColumn(1);
            int num1 = column.GetHashCode() << 2;
            int num2 = hashCode ^ num1;
            column = GetColumn(2);
            int num3 = column.GetHashCode() >> 2;
            int num4 = num2 ^ num3;
            column = GetColumn(3);
            int num5 = column.GetHashCode() >> 1;
            return num4 ^ num5;
        }

        public override bool Equals(object other)
        {
            if (!(other is Matrix4x4Bool))
                return false;
            return Equals((Matrix4x4Bool)other);
        }

        public bool Equals(Matrix4x4Bool other)
        {
            int num;
            if (GetColumn(0).Equals(other.GetColumn(0)))
            {
                Vector4Bool column = GetColumn(1);
                if (column.Equals(other.GetColumn(1)))
                {
                    column = GetColumn(2);
                    if (column.Equals(other.GetColumn(2)))
                    {
                        column = GetColumn(3);
                        num = column.Equals(other.GetColumn(3)) ? 1 : 0;
                        goto label_5;
                    }
                }
            }
            num = 0;
            label_5:
            return num != 0;
        }

        public static bool operator ==(Matrix4x4Bool lhs, Matrix4x4Bool rhs)
        {
            return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
        }

        public static bool operator !=(Matrix4x4Bool lhs, Matrix4x4Bool rhs)
        {
            return !(lhs == rhs);
        }

        public int GetSum()
        {
            int sum = 0;
            for (int i = 0; i < 16; i++)
            {
                sum += this[i] ? 1 : 0;
            }

            return sum;
        }

        /// <summary>
        ///   <para>Get a column of the matrix.</para>
        /// </summary>
        /// <param name="index"></param>
        public Vector4Bool GetColumn(int index)
        {
            switch (index)
            {
                case 0:
                    return new Vector4Bool(m00, m10, m20, m30);
                case 1:
                    return new Vector4Bool(m01, m11, m21, m31);
                case 2:
                    return new Vector4Bool(m02, m12, m22, m32);
                case 3:
                    return new Vector4Bool(m03, m13, m23, m33);
                default:
                    throw new IndexOutOfRangeException("Invalid column index!");
            }
        }

        /// <summary>
        ///   <para>Returns a row of the matrix.</para>
        /// </summary>
        /// <param name="index"></param>
        public Vector4Bool GetRow(int index)
        {
            switch (index)
            {
                case 0:
                    return new Vector4Bool(m00, m01, m02, m03);
                case 1:
                    return new Vector4Bool(m10, m11, m12, m13);
                case 2:
                    return new Vector4Bool(m20, m21, m22, m23);
                case 3:
                    return new Vector4Bool(m30, m31, m32, m33);
                default:
                    throw new IndexOutOfRangeException("Invalid row index!");
            }
        }

        /// <summary>
        ///   <para>Sets a column of the matrix.</para>
        /// </summary>
        /// <param name="index"></param>
        /// <param name="column"></param>
        public void SetColumn(int index, Vector4Bool column)
        {
            this[0, index] = column.x;
            this[1, index] = column.y;
            this[2, index] = column.z;
            this[3, index] = column.w;
        }

        /// <summary>
        ///   <para>Sets a row of the matrix.</para>
        /// </summary>
        /// <param name="index"></param>
        /// <param name="row"></param>
        public void SetRow(int index, Vector4Bool row)
        {
            this[index, 0] = row.x;
            this[index, 1] = row.y;
            this[index, 2] = row.z;
            this[index, 3] = row.w;
        }

        public void Rotate()
        {
            var row0 = GetRow(0);
            var row1 = GetRow(1);
            var row2 = GetRow(2);
            var row3 = GetRow(3);

            SetColumn(0, row0);
            SetColumn(1, row1);
            SetColumn(2, row2);
            SetColumn(3, row3);
        }

        public void MirrorWithShift()
        {
            var row0 = GetColumn(0);
            var row1 = GetColumn(1);
            var row2 = GetColumn(2);
            var row3 = GetColumn(3);

            SetColumn(0, new Vector4Bool(row0.w, row0.z, row0.y, row0.x));
            SetColumn(1, new Vector4Bool(row1.w, row1.z, row1.y, row1.x));
            SetColumn(2, new Vector4Bool(row2.w, row2.z, row2.y, row2.x));
            SetColumn(3, new Vector4Bool(row3.w, row3.z, row3.y, row3.x));

            int minX = 0;
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (this[i, j])
                    {
                        if (j > minX)
                        {
                            minX = j;
                            break;
                        }
                    }
                }
            }


            if (minX != 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    var row = GetColumn(i);
                    for (int j = 0; j < 4; j++)
                    {
                        if (j + minX <= 3)
                            row[j] = row[j + minX];
                        else
                            row[j] = false;
                    }

                    SetColumn(i, row);
                }
            }
        }

        public Vector2Int GetBounds()
        {
            Vector2Int bounds = new Vector2Int();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (this[i, j])
                    {
                        if (i >= bounds.x)
                            bounds.x = i + 1;
                        if (j >= bounds.y)
                            bounds.y = j + 1;
                    }
                }
            }

            return bounds;
        }


        /// <summary>
        ///   <para>Returns a nicely formatted string for this matrix.</para>
        /// </summary>
        /// <param name="format"></param>
        public override string ToString()
        {
            return
                $"{m00}\t{m01}\t{m02}\t{m03}\n{m10}\t{m11}\t{m12}\t{m13}\n{m20}\t{m21}\t{m22}\t{m23}\n{m30}\t{m31}\t{m32}\t{m33}\n";
        }
    }
}