using System;
using Godot;

namespace MachineLearning
{
    public struct Matrix
    {
        public int rows { get; private set; }
        public int cols { get; private set; }
        public float[,] data { get; private set; }

        public float this[int i, int j]
        {
            get
            {
                return this.data[i, j];
            }
            set {
                this.data[i, j] = value;
            }
        }

        public Matrix(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;

            this.data = new float[rows, cols];
        }

        public Matrix(float[] arr)
        {
            this.rows = arr.Length;
            this.cols = 1;
            this.data = new float[rows, cols];

            for (int i = 0; i < this.rows; i++)
            {
                this[i, 0] = arr[i];
            }
        }

        public Matrix(Matrix m) {
            this.rows = m.rows;
            this.cols = m.cols;
            this.data = new float[rows, cols];

            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    this[i,j] = m[i,j];
                }
            }
        }

        public void Randomize()
        {
            Random r = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    this[i, j] = (float)r.NextDouble() * 2f - 1f;
                }
            }
        }

        public Matrix Transposed()
        {
            Matrix m = new Matrix(this.cols, this.rows);
            Matrix a = this;

            m.Map((el, i, j) =>
            {
                return a[j, i];
            });

            return m;
        }

        public override String ToString()
        {
            String s = "{\n";

            for (int i = 0; i < this.rows; i++)
            {
                s += "  {" + this[i, 0];
                for (int j = 1; j < this.cols; j++)
                {
                    s += $", {this[i, j]}";
                }
                s += "}\n";
            }
            s += "}\n";

            return s;
        }

        public void Map(Func<float, int, int, float> func)
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.cols; j++)
                {
                    this[i, j] = func.Invoke(this[i, j], i, j);
                }
            }
        }

        public static Matrix Map(Matrix a, Func<float, int, int, float> func) {
            Matrix m = new Matrix(a.rows, a.cols);
            for (int i = 0; i < m.rows; i++)
            {
                for (int j = 0; j < m.cols; j++)
                {
                    m[i, j] = func.Invoke(a[i, j], i, j);
                }
            }
            return m;
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix m = new Matrix(a.rows, a.cols);

            m.Map((el, i, j) =>
            {
                return a[i, j] + b[i, j];
            });

            return m;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            Matrix m = new Matrix(a.rows, a.cols);

            m.Map((el, i, j) =>
            {
                return a[i, j] - b[i, j];
            });

            return m;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix m = new Matrix(a.rows, b.cols);
            
            m.Map((el, i, j) =>
            {
                float sum = 0f;
                for (int k = 0; k < a.cols; k++)
                {
                    float el1 = a[i, k];
                    float el2 = b[k, j];
                    sum += el1 * el2;
                }
                return sum;
            });

            return m;
        }

        public static Matrix operator *(Matrix a, float b)
        {
            Matrix m = new Matrix(a.rows, a.cols);

            m.Map((el, i, j) =>
            {
                return a[i, j] * b;
            });

            return m;
        }

        public static Matrix Hadamard(Matrix a, Matrix b)
        {
            Matrix m = new Matrix(a.rows, a.cols);

            m.Map((el, i, j) =>
            {
                return a[i, j] * b[i, j];
            });
            return m;
        }
    }
}