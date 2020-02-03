using System;
using System.Collections;
using Npgsql;

namespace DraftDay {
    public class PlayerCreation{


        ArrayList playerList =  new ArrayList();
        NpgsqlConnection conn;
        public int display(){
            int input = -1;
            initPlayerList();
            
            Console.WriteLine("Welcome to the Draft");
            

            do{
                Console.WriteLine("Here you're able to draft a team!\n" + 
                              "1. Create Player\n" +
                              "2. Delete Player\n" + 
                              "3. Edit Player\n" +
                              "4. List Players\n" +
                              "5. Return to Home\n"
                );
                Console.WriteLine("Please Select a menu");
                input = int.Parse(Console.ReadLine());
                
                if(input == 1){
                    createPlayer();
                }else if(input == 2){
                    deletePlayer();
                } else if(input == 3){
                    editPlayer();
                } else if( input == 4){
                    Console.WriteLine("ID\t\t\tNAME\t\t\tAGE\t\tSTRENGTH\tSPEED");
                    foreach (Player player in playerList)
                    {

                        Console.WriteLine(player.ToString());
                    }
                }

            }while(input != 5);
            
            input = 1;
           

            return input;
        }

        private string createPlayer(){
            
            string name;
            int age;
            int id;
            int strength;
            int speed;
            Player player =  null;
            
            bool finished = true;
            string answer = "";
            
            do{
                Console.WriteLine("Please enter a name.");
                name = Console.ReadLine();
                Console.WriteLine("Please enter an age.");
                age = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter in their Strength.");
                strength = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter in their speed.");
                speed = int.Parse(Console.ReadLine());
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

            
            player =  new Player(name, age, strength, speed, id);
            
            playerList.Add(player);
            return player.createPlayer(player);
            
        }
        private string deletePlayer(){
            string answer;
            int id = -1;
            Player draftee = null;
            
            Console.WriteLine("Enter Player ID");
            answer = Console.ReadLine();

            try {
                id = int.Parse(answer);
            } catch(Exception){
                id = -1;
            }
            if(id != -1){
                foreach (Player player in playerList)
                {
                    if(player.getID() == id){
                        draftee = player;
                        
                    }
                }
            }
            if(draftee != null){
                Console.WriteLine(draftee.getName() + " was removed");
                playerList.Remove(draftee);
                return draftee.deletePlayer(draftee);
            } else {
                return "That player does not exist";
            }          
        }
        private string editPlayer(){
            string answer;
            int id = -1;
            Player draftee = null;
            
            Console.WriteLine("Enter Player ID");
            answer = Console.ReadLine();

            try {
                id = int.Parse(answer);
            } catch(Exception){
                id = -1;
            }
            if(id != -1){
                foreach (Player player in playerList)
                {
                    if(player.getID() == id){
                        draftee = player;
                    }
                }
            }

            Player temp = getNewStats(id);
            draftee.setName(temp.getName());
            draftee.setAge(temp.getAge());
            draftee.setStrength(temp.getStrength());
            draftee.setSpeed(temp.getSpeed());


            if(draftee != null){
                Console.WriteLine(draftee.getName() + " was edited");
                return draftee.updatePlayer(draftee);
            } else {
                return "That player does not exist";
            }       

        }

        private Player getNewStats(int id){
            string name;
            int age;
            int strength;
            int speed;
            Player player =  null;
            
            bool finished = true;
            string answer = "";
            
            do{
                Console.WriteLine("Please enter a name.");
                name = Console.ReadLine();
                Console.WriteLine("Please enter an age.");
                age = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter in their Strength.");
                strength = int.Parse(Console.ReadLine());
                Console.WriteLine("Please enter in their speed.");
                speed = int.Parse(Console.ReadLine());
                

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

            
            player =  new Player(name, age, strength, speed, id);
            
            
            Console.Write(player.getAge());
            return player;
        }
        // TODO
        private void initPlayerList(){
            connectDB();
            string sql =  "SELECT * FROM Player;";
            NpgsqlCommand cmd; 
            try{
                cmd =  new NpgsqlCommand(sql, conn);
                var reader = cmd.ExecuteReader();
                while(reader.Read()){
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    int age = reader.GetInt32(2);
                    int strength = reader.GetInt32(3);
                    int speed = reader.GetInt32(4);

                    Player player =  new Player(name, age, strength, speed, id);
                    playerList.Add(player);
                }
                // for (int i = 0; reader.Read(); i++)
                // {
                    
                // }
                
            } catch(Exception e){
                Console.WriteLine(e.StackTrace);
            }
            
            
        }

        private void connectDB(){

            try{
                string connString = "Host=127.0.0.1;Username=postgres;Password=foxconnit;Database=mydb3";          
                conn = new NpgsqlConnection(connString);
                conn.Open();
            } catch(Exception ex){
                Console.WriteLine(ex.StackTrace);
            }  
            
        }
        
    }
}
