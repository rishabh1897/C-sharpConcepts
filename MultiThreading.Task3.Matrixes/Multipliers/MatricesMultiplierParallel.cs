using MultiThreading.Task3.MatrixMultiplier.Matrices;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            // todo: feel free to add your code here
            var result = new Matrix(m1.RowCount, m2.ColCount);

            long matACols = m1.ColCount;
            long matBCols = m2.ColCount;
            long matARows = m1.RowCount;

            Parallel.For(0, matARows, i =>
            {
                for (int j = 0; j < matBCols; j++)
                {
                    long temp = 0;
                    for (int k = 0; k < matACols; k++)
                    {
                        temp += m1.GetElement(i, k) * m2.GetElement(k, j);
                    }
                    result.SetElement(i, j, temp);
                }
            });

            return result;
        }
    }
}
