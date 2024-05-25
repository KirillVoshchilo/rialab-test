using UnityEngine;
namespace App.SimplesScipts
{
    public static class SGeometryOperations
    {
        public static Vector3 RotateVector(Vector3 vector, Vector3 axis, float angle)
        {
            angle = angle * Mathf.PI / 180;
            float[,] rotatingVector = new float[3, 1];
            rotatingVector[0, 0] = vector.x;
            rotatingVector[1, 0] = vector.y;
            rotatingVector[2, 0] = vector.z;
            float[,] rotationMatrix = new float[3, 3];
            rotationMatrix[0, 0] = Mathf.Cos(angle) + (1 - Mathf.Cos(angle)) * Mathf.Pow(axis.x, 2);
            rotationMatrix[0, 1] = ((1 - Mathf.Cos(angle)) * axis.x * axis.y) - (Mathf.Sin(angle) * axis.z);
            rotationMatrix[0, 2] = ((1 - Mathf.Cos(angle)) * axis.x * axis.z) + (Mathf.Sin(angle) * axis.y);
            rotationMatrix[1, 0] = ((1 - Mathf.Cos(angle)) * axis.y * axis.x) + (Mathf.Sin(angle) * axis.z);
            rotationMatrix[1, 1] = Mathf.Cos(angle) + ((1 - Mathf.Cos(angle)) * Mathf.Pow(axis.y, 2));
            rotationMatrix[1, 2] = ((1 - Mathf.Cos(angle)) * axis.y * axis.z) - (Mathf.Sin(angle) * axis.x);
            rotationMatrix[2, 0] = ((1 - Mathf.Cos(angle)) * axis.z * axis.x) - (Mathf.Sin(angle) * axis.y);
            rotationMatrix[2, 1] = ((1 - Mathf.Cos(angle)) * axis.z * axis.y) + (Mathf.Sin(angle) * axis.x);
            rotationMatrix[2, 2] = Mathf.Cos(angle) + ((1 - Mathf.Cos(angle)) * Mathf.Pow(axis.z, 2));
            float[,] result = new float[3, 1];
            result = MultiplyMatrices(rotationMatrix, rotatingVector);
            Vector3 resultVector = new()
            {
                x = result[0, 0],
                y = result[1, 0],
                z = result[2, 0]
            };
            return resultVector;
        }
        public static Quaternion RotateQuaternion(Quaternion quaternion, Vector3 axis, float angle)
            => Quaternion.AngleAxis(angle, axis) * quaternion;
        public static Vector3 FromLocalToWorld(Vector3 localVector, Transform origin)
            => origin.position + (localVector.x * origin.right) + (localVector.y * origin.up) + (localVector.z * origin.forward);
        public static Vector3 FromLocalToWorld(Vector3 localVector, Vector3 originPosition)
            => originPosition + (localVector.x * Vector3.right) + (localVector.y * Vector3.up) + (localVector.z * Vector3.forward);
        public static Vector3 FromWorldTolLocal(Vector3 globalVector, Transform origin)
        {
            return new Vector3(Vector3.Dot(globalVector - origin.position, origin.right)
                , Vector3.Dot(globalVector - origin.position, origin.up)
                , Vector3.Dot(globalVector - origin.position, origin.forward));
        }

        private static float[,] MultiplyMatrices(float[,] matrix_1, float[,] matrix_2)
        {
            float[,] result = new float[matrix_1.GetLength(0), matrix_2.GetLength(1)];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = 0;
                    for (int n = 0; n < matrix_2.GetLength(0); n++)
                        result[i, j] += matrix_1[i, n] * matrix_2[n, j];
                }
            }
            return result;
        }
    }
}