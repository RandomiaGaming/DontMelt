using System.Collections.Generic;
using DontMelt.Data;
namespace DontMelt.Helpers
{
    public static class GeometryHelper
    {
        //Degrees
        public static float DegreeClamp(float inputDegree)
        {
            while (inputDegree > 360)
            {
                inputDegree -= 360;
            }
            while (inputDegree < 0)
            {
                inputDegree += 360;
            }
            return inputDegree;
        }
        public static float DegreeDifference(float degreeA, float degreeB)
        {
            degreeA = DegreeClamp(degreeA);
            degreeB = DegreeClamp(degreeB);
            float Output = MathHelper.Abs(degreeA - degreeB);
            if (Output > 180)
            {
                Output = 360 - Output;
            }
            return MathHelper.Abs(Output);
        }
        //Vectors
        public static Vector VectorFromDirection(float direction, float magnitude)
        {
            return new Vector(Mathf.Cos(direction * Mathf.Deg2Rad), Mathf.Sin(direction * Mathf.Deg2Rad)) * magnitude;
        }
        public static Vector VectorFromDirection(float direction)
        {
            return VectorFromDirection(direction, 1);
        }
        public static float Distance(Vector vectorA, Vector vectorB)
        {
            return Mathf.Sqrt(((vectorA.x - vectorB.x) * (vectorA.x - vectorB.x)) + ((vectorA.y - vectorB.y) * (vectorA.y - vectorB.y)));
        }
        public static Vector ClampUnitCircle(Vector inputVector)
        {
            float Distance = VectorMagnitude(inputVector);
            return new Vector(inputVector.x / Distance, inputVector.y / Distance);
        }
        public static float VectorDirection(Vector inputVector)
        {
            inputVector = ClampUnitCircle(inputVector);
            float Output = Mathf.Atan(Mathf.Abs(inputVector.x) / Mathf.Abs(inputVector.y)) * Mathf.Rad2Deg;
            if (inputVector.x >= 0 && inputVector.y >= 0)
            {
                return 90 - Output;
            }
            else if (inputVector.x < 0 && inputVector.y >= 0)
            {
                return 90 + Output;
            }
            else if (inputVector.x < 0 && inputVector.y < 0)
            {
                return 180 + (90 - Output);
            }
            else if (inputVector.x >= 0 && inputVector.y < 0)
            {
                return 270 + Output;
            }
            else
            {
                return Output;
            }
        }
        public static float VectorMagnitude(Vector inputVector)
        {
            return Mathf.Sqrt((inputVector.x * inputVector.x) + (inputVector.y * inputVector.y));
        }
        //Tringulation and Polygons
        public static List<List<Vector>> Triangulate(List<Vector> polygonPoints)
        {
            List<List<Vector>> Output = new List<List<Vector>>();
            while (polygonPoints.Count > 3)
            {
                int First_Convex_Vertice = -1;
                for (int i = 0; i < polygonPoints.Count; i++)
                {
                    if (!VerticeConcave(polygonPoints, i))
                    {
                        First_Convex_Vertice = i;
                        break;
                    }
                }
                if (First_Convex_Vertice >= 0 && First_Convex_Vertice < polygonPoints.Count)
                {
                    Vector Point_A = polygonPoints[First_Convex_Vertice];
                    Vector Point_B = polygonPoints[First_Convex_Vertice];
                    Vector Point_C = polygonPoints[First_Convex_Vertice];
                    if (First_Convex_Vertice - 1 < 0)
                    {
                        Point_A = polygonPoints[polygonPoints.Count - 1];
                    }
                    else
                    {
                        Point_A = polygonPoints[First_Convex_Vertice - 1];
                    }
                    if (First_Convex_Vertice + 1 >= polygonPoints.Count)
                    {
                        Point_C = polygonPoints[0];
                    }
                    else
                    {
                        Point_C = polygonPoints[First_Convex_Vertice + 1];
                    }
                    Output.Add(new List<Vector>() { Point_A, Point_B, Point_C });
                    polygonPoints.RemoveAt(First_Convex_Vertice);
                }
                else
                {
                    Output.Add(polygonPoints);
                    return Output;
                }
            }
            Output.Add(polygonPoints);
            return Output;
        }
        public static bool PolygonConcave(List<Vector> polygonPoints)
        {
            for (int i = 0; i < polygonPoints.Count; i++)
            {
                if (VerticeConcave(polygonPoints, i))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool VerticeConcave(List<Vector> polygonPoints, int pointIndex)
        {
            Vector Point_A = polygonPoints[pointIndex];
            Vector Point_B = polygonPoints[pointIndex];
            Vector Point_C = polygonPoints[pointIndex];
            if (pointIndex - 1 < 0)
            {
                Point_A = polygonPoints[polygonPoints.Count - 1];
            }
            else
            {
                Point_A = polygonPoints[pointIndex - 1];
            }
            if (pointIndex + 1 >= polygonPoints.Count)
            {
                Point_C = polygonPoints[0];
            }
            else
            {
                Point_C = polygonPoints[pointIndex + 1];
            }
            float Degree_AB = VectorDirection(new Vector(Point_A.x - Point_B.x, Point_A.y - Point_B.y));
            float Degree_BC = VectorDirection(new Vector(Point_C.x - Point_B.x, Point_C.y - Point_B.y));
            if (Degree_BC > Degree_AB && Degree_BC - Degree_AB > 180)
            {
                return true;
            }
            else if (Degree_BC < Degree_AB && (360 - Degree_AB) + Degree_BC > 180)
            {
                return true;
            }
            return false;
        }
        //Lines and Stuff
        public static float distanceToLine(Vector lineStart, Vector lineEnd, Vector targetPoint)
        {
            return Distance(closestPointOnLine(lineStart, lineEnd, targetPoint), targetPoint);
        }
        public static Vector closestPointOnLine(Vector lineStart, Vector lineEnd, Vector targetPoint)
        {
            Vector line = (lineEnd - lineStart);
            float len = line.magnitude;
            line.Normalize();

            Vector v = targetPoint - lineStart;
            float d = Vector3.Dot(v, line);
            d = Mathf.Clamp(d, 0f, len);
            return lineStart + line * d;
        }
    }
}