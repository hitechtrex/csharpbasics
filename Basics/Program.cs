using System;

namespace Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            //Company company = new Company();  // error: cannot declare static type
            Company.Name = "ABC";

            // hiding parent method
            Employee a = new FullTimeEmployee();
            a.CheckIn(); // prints: checking in
            FullTimeEmployee fte = new FullTimeEmployee();
            fte.CheckIn(); // prints: fte - checking in

            // overriding
            Employee b = new ContractEmployee();
            b.CheckIn(); // prints: ce - checking in
            ContractEmployee ce = new ContractEmployee();
            ce.CheckIn(); // prints: ce - checking in

            ITimeSheet t = new ContractEmployee();
            t.CreateTimeSheet();

            //Dog d = new Pet(); // casting error
            Pet p = new Dog();
            p.DoTrick(); // override: dog trick; new: pet trick
            Dog d = new Dog();
            d.DoTrick(); // dog trick

            // changing value types
            int i = 10;
            int j = i; // int j is created on the stack, value of i is assigned to j
            j++;
            Console.WriteLine($"i: {i}, j: {j}"); // i: 10, j: 11
            i--;
            Console.WriteLine($"i: {i}, j: {j}"); // i: 9, j: 11

            // changing references
            Dog d1 = new Dog { Name = "ID1" };
            Dog d2 = d1;
            d2.Name = "ID2";
            Console.WriteLine($"d1-name: {d1.Name}, d2-name: {d2.Name}"); // references the same object
            d1.Name = "i'm sure it's id1"; // changes the property of that one object
            Console.WriteLine($"d1-name: {d1.Name}, d2-name: {d2.Name}");

            Console.ReadLine();
        }
    }

    #region abstract/interface/inheritance/polymorphism
    public class Tool
    {
        public string Name { get; set; }
    }

    public interface ITimeSheet
    {
        Tool Tool { get; set; }         // interface properties
        string FileName { get; set; }   // interface properties
        //int FileSize;                 // error: interface cannot have fields
        void CreateTimeSheet();
        void UpdateTimeSheet();
        void Print();
        void Print(string filename);
    }

    public interface IAccount
    {
        void UpdateBalance();
        void Print();
        void Print(int balance);
    }

    public static class Company
    {
        public static string Name { get; set; }
        public static string Address { get; set; }
    }

    public abstract class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address;
        
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public abstract int GetMonthlySalary();

        //protected void CheckIn()  // cannot access from outside
        //private void CheckIn()    // cannot access from subclass and outside
        public virtual void CheckIn()
        {
            Console.WriteLine("checking in");
        }
    }

    public class FullTimeEmployee : Employee
    {
        public int AnnualSalary { get; set; }
        public string Benefits { get; } // this marks the property readonly
        public FullTimeEmployee()
        {
            Benefits = "all sorts";
        }

        public override int GetMonthlySalary()
        {
            return AnnualSalary / 12;
        }

        public new void CheckIn()
        {
            Console.WriteLine("fte - checking in");
        }

        public void UpdateBenefits()
        {
            //Benefits = "super good";  // error: cannot assign, read-only
        }
    }

    public class ContractEmployee : Employee, ITimeSheet, IAccount
    {
        public int HourlySalary { get; set; }
        public int HoursWorked { get; set; }
        public Tool Tool { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FileName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int GetMonthlySalary()
        {
            return HourlySalary * HoursWorked;
        }

        public void CreateTimeSheet()
        {
            Console.WriteLine("Time sheet created");
        }

        public void UpdateTimeSheet()
        {
            Console.WriteLine("Time sheet updated");
        }

        public override void CheckIn()
        {
            Console.WriteLine("ce - checking in");
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public void UpdateBalance()
        {
            throw new NotImplementedException();
        }

        public void Print(string filename)
        {
            throw new NotImplementedException();
        }

        public void Print(int balance)
        {
            throw new NotImplementedException();
        }
    }

    public class Pet
    {
        public string Name { get; set; }
        public virtual void DoTrick()
        {
            Console.WriteLine("pet trick");
        }
    }

    public class Dog : Pet
    {
        //public new void DoTrick()
        public override void DoTrick()
        {
            Console.WriteLine("dog trick");
        }
    }
    #endregion


    #region class/struct/value-types/reference-types/heap/stack
    //public struct Account : Employee  // error: cannot inherit from a class
    public struct Account : ITimeSheet
    {
        public Tool Tool { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FileName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void CreateTimeSheet()
        {
            throw new NotImplementedException();
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public void Print(string filename)
        {
            throw new NotImplementedException();
        }

        public void UpdateTimeSheet()
        {
            throw new NotImplementedException();
        }
    }

    //public class Clerk : Account  // error: structs are sealed types
    public class Clerk : FullTimeEmployee, ITimeSheet
    {
        public Tool Tool { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FileName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void CreateTimeSheet()
        {
            Console.WriteLine("Time sheet created by a clerk");
        }

        public void Print()
        {
            throw new NotImplementedException();
        }

        public void Print(string filename)
        {
            throw new NotImplementedException();
        }

        public void UpdateTimeSheet()
        {
            Console.WriteLine("Time sheet updated by a clerk");
        }
    }
    #endregion
}
