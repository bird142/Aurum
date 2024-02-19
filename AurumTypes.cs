using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum
{
    /// <summary>
    /// Represents an object at runtime. Every object has a class.
    /// </summary>
    public class AurumObject
    {
        public AurumClass Class { get; }
        public AurumObject(AurumClass @class)
        {
            Class = @class;
        }
    }

    /// <summary>
    /// Represents a class. All classes are objects of class Class. Every class has a superclass except for Object.
    /// </summary>
    public class AurumClass : AurumObject
    {
        public AurumClass Superclass { get; }
        public AurumClass(AurumClass superclass) : base(AurumBuiltins.Class)
        {
            Superclass = superclass;
        }
    }
    public static class AurumBuiltins
    {
        public static AurumClass Object = new AurumClass(null); // Superclass for all objects. Has no superclass.
        public static AurumClass Class = new AurumClass(Object); // Class for all classes, including itself.
        public static AurumClass Integer = new AurumClass(Object);
        public static AurumClass Float = new AurumClass(Object);
        public static AurumClass String = new AurumClass(Object);
        public static AurumClass Char = new AurumClass(Object);
        public static AurumClass Boolean = new AurumClass(Object);
        public static AurumObject NullLit = new AurumObject(Object); // Represents the null literal.
    }
}
