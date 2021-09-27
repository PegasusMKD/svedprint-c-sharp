using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Utility
{
    public enum Grades
    {
        FAIL = 1,
        BAD = 2,
        GOOD = 3,
        GREAT = 4,
        EXCELLENT = 5
    }

    public static class GradeExtension
    {
        public static String getGradeLabel(this Grades grade)
        {
            switch (grade)
            {
                case Grades.FAIL:
                    return "Не доволен";

                case Grades.BAD:
                    return "Доволен";

                case Grades.GOOD:
                    return "Добар";

                case Grades.GREAT:
                    return "Многу добар";

                case Grades.EXCELLENT:
                    return "Одличен";
            }
            return null;
        }
    }
}
