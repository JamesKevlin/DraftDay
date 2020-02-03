
namespace DraftDay {
    public class Person {
        private string name;
        private int age;
        private int id;
        public Person(string name, int age, int id){
            this.name = name;
            this.age = age;
            this.id = id;
        }
        public string getName(){
            return this.name;
        }
        public int getAge(){
            return this.age;
        }
        public int getID(){
            return this.id;
        }
        public void setName(string name){
            this.name = name;
        }
        public void setAge(int age){
            this.age = age;
        }
    }
}