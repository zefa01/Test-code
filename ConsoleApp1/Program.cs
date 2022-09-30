using System;
using System.Collections.Generic;

namespace ConsoleApp1
{

    sealed public class Singleton
    {
        private static Singleton instance = null;
        private static int instanceCount = 0;

        private Singleton()
        {
            instanceCount += 1;
        }

        static public Singleton GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }

        public int GetInstanceCount()
        {
            return instanceCount;
        }

    }

    public class Student
    {
        public String Name { get; set; }
        public Student(String name) { Name = name; }
    }

    public interface IDependencyInjection
    {
        List<Student> GetAllStudents();
    }

    public class DataAccessLayer : IDependencyInjection
    {

        List<Student> IDependencyInjection.GetAllStudents()
        {
            return new List<Student>()
            {
                new Student("huzaifah")
                , new Student("wassay")
                , new Student("zaeem")
            };
        }
    }

    public class AnotherDataAccessLayer : IDependencyInjection
    {
        List<Student> IDependencyInjection.GetAllStudents()
        {
            return new List<Student>()
            {
                new Student("Azfar")
                , new Student("Hamza")
                , new Student("Ali")
            };
    }
}

    public class BusinessLogicLayer
    {
        private IDependencyInjection dependencyRef = null;
        public BusinessLogicLayer() { }
        public BusinessLogicLayer(IDependencyInjection dependency)
        {
            dependencyRef = dependency;
        }

        public List<Student> GetAllStudents()
        {
            if (dependencyRef != null)
            {
                return dependencyRef.GetAllStudents();
            }
            return new List<Student>();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var bll = new BusinessLogicLayer(new AnotherDataAccessLayer());
            foreach (var student in bll.GetAllStudents())
            {
                Console.WriteLine(student.Name);
            }

            //var instance = Singleton.GetInstance;
            //var instance2 = Singleton.GetInstance;

            //Console.WriteLine(instance.GetInstanceCount());
            //Console.WriteLine(instance.GetType());

        }
    }
}
