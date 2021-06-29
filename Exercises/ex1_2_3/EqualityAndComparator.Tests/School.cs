
namespace EqualityAndComparator.Test {
    public class School {
        public string Name { get; }
        
        public string Location{ get; }

        public School(string Name, string Location) {
            this.Name = Name;
            this.Location = Location;
        }
        public School() { }

        public override bool Equals(object obj) {
            School s = obj as School;
            if(s!=null)  {
                if(s.Name.Equals(Name) && s.Location.Equals(Location)) return true;
            }
            return false;
        }



        public override int GetHashCode() { //I had to override this or the IDE kept giving errors
            return this.GetHashCode();
        }

    }
}