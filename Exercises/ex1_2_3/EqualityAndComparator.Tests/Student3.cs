using System;

namespace EqualityAndComparator.Test {
    public class Student3 {
        int nr;             // int é IComparable
        string name;        // string é IComparable
        [ComparisonV2(typeof(AdressByStreet))] Address a; // Address NÃO é IComparable
        [ComparisonV2(typeof(AccountByBalance))] Account acc; // Account NÃO é IComparable
        
        public Student3(int nr, string name, Address a, Account acc) {
            this.nr = nr;
            this.name = name;
            this.a = a;
            this.acc = acc;
        }
        
        public Student3() { }

    }
    public class Address {
        public string street;
        int number;
        public Address(string street, int number) {
            this.street = street;
            this.number = number; 
        }
    }

    public class AdressByStreet : IComparer  {
        public int Compare(object x, object y) {
            if(x==null && y==null) return 0;
            if(x==null) return 1;
            if(y==null) return -1;

            Type xt = x.GetType();
            Type yt = y.GetType();
            if(xt==typeof(Address) && yt==typeof(Address)) {
                ((Address) x).street.CompareTo(((Address) y).street);
            }
            return 0;
        }
    }

    public class Account {
        public int balance;
        public Account(int balance) {
            this.balance = balance;
        }
    }

    public class AccountByBalance : IComparer {
        public int Compare(object x, object y) {
            if(x==null && y==null) return 0;
            if(x==null) return 1;
            if(y==null) return -1;

            Type xt = x.GetType();
            Type yt = y.GetType();
            if(xt==typeof(Account) && yt==typeof(Account)) {
                ((Account) x).balance.CompareTo(((Account) y).balance);
            }
            return 0;
        }

        
    }

    
}