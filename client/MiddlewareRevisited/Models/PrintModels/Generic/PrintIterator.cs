using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiddlewareRevisited.Models.PrintModels
{
    public class PrintIterator<T>
    {
        List<ICustomImage> images = new List<ICustomImage>();

        public PrintIterator(ref List<Student> students, User homeroomTeacher, List<string> imageNames)
        {
            Type type = typeof(T);
            Type[] parameterTypes = new Type[0];
            ConstructorInfo constructor;
            if(type.GetInterfaces().Contains(typeof(ICustomImageCollection)))
            {
                parameterTypes = new Type[5];
                parameterTypes[0] = typeof(Student);
                parameterTypes[1] = typeof(User).MakeByRefType();
                parameterTypes[2] = typeof(List<string>);
                parameterTypes[3] = typeof(string);
                parameterTypes[4] = typeof(string);
            }
            constructor = type.GetConstructor(parameterTypes);
            foreach (Student student in students)
            {
                images.Add((ICustomImage)constructor.Invoke(new object[] { student, homeroomTeacher, imageNames, "E:\\frontal.jpg", "E:\\back.jpg" }));
            }
            
        }

        public IEnumerable<dynamic> printIterator(int horizontalMargin, int verticalMargin)
        {
            foreach(ICustomImage image in images)
            {
                image.resetData();
                yield return image.generateMainPage(horizontalMargin, verticalMargin);
                image.clearMainPage();
            }
        }

        public IEnumerable<dynamic> previewIterator(int horizontalMargin, int verticalMargin)
        {
            foreach (ICustomImage image in images)
            {
                image.resetData();
                yield return image.generatePreview(horizontalMargin, verticalMargin);
                image.clearPreview();
            }
        }

        public void clearPreview(int idx)
        {
            images[idx].clearPreview();
        }

        public dynamic getSinglePreview(int idx, int horizontalMargin, int verticalMargin) { 
                if (idx < 0 || idx >= images.Count)
                    throw new IndexOutOfRangeException("Index out of range");
                return images[idx].generatePreview(horizontalMargin, verticalMargin);
        }
    }
}
