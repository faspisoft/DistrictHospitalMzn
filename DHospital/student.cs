using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DHospital
{
    class student
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }

        public static List<student> AllStudents()
        {
            List<student> Liststudent = new List<student>();
            student student1 = new student
            {
                ID = 101,
                Name = "Aman",
                Gender = "Male"
            };
            Liststudent.Add(student1);

            student student2 = new student
            {
                ID = 102,
                Name = "Prachi",
                Gender = "Female"
            };
            Liststudent.Add(student2);

             student student3 = new student
            {
                ID = 103,
                Name = "Pragati",
                Gender = "Female"
            };
            Liststudent.Add(student3);


            return Liststudent;
        }


    }
}
