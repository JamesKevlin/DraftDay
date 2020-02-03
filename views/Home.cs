using System;
using System.Collections;
namespace DraftDay {


    public class Home{
        
        private Manager user;
        
        public Home(Manager user){
            this.user = user;
        }
        public int display(){
            int input = -1;
            Console.WriteLine("Welcome to Draft Day");
            if( user == null){
                createManager();
            }
            
            Console.WriteLine("Here you're able to create a player or draft a team!\n" + 
                              "1. Player Creation\n" +
                              "2. Draft a Team\n" + 
                              "3. Quit"
            );
            

            do{
                Console.WriteLine("Please Select a menu");
                input = int.Parse(Console.ReadLine());
                

            }while(input < 1 && input > 3);
                
            if(input == 1){
                input = 2;
            }else if(input == 2){
                input = 3;
            } else if(input == 3){
                input = -1;
            }

            return input;
        }

        private void createManager(){
            Console.WriteLine("It looks like you just started. We're going to have " +
                              "create a character");
            string name;
            int age;
            int id;
            bool finished = true;
            string answer = "y";
            
            do{
                Console.WriteLine("Please enter a name.");
                name = Console.ReadLine();
                Console.WriteLine("Please enter an age.");
                age = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter an ID.");
                id = int.Parse(Console.ReadLine());

                do{
                    Console.WriteLine("Are you satisfied with your character?(yes or no)");
                    answer = Console.ReadLine();
                }while(answer.ToLower() != "yes" && answer.ToLower() != "no" );
                if(answer == "yes"){
                    finished = false;
                } else{
                    finished = true;
                }
            }while(finished);

            
            user =  new Manager(name, age, id, new ArrayList());
            
        }
        public Manager getManager(){
            return this.user;
        }
    }
}